using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackTruda.Definitions;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;

namespace HackTruda.Services
{
    /// <summary>
    /// Хэндлер исключений.
    /// </summary>
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly IDebuggerService _debuggerService;

        private readonly Dictionary<Type, Func<Exception, PerformableAction, Task>> _exceptionDelegatesMap =
            new Dictionary<Type, Func<Exception, PerformableAction, Task>>();
        private Func<Exception, PerformableAction, Task> _exceptionHandlerDelegate;

        public ExceptionHandler(IDebuggerService debuggerService) =>
            _debuggerService = debuggerService;

        public void SetExceptionHandlerDelegate(Func<Exception, PerformableAction, Task> handler) =>
            _exceptionHandlerDelegate = handler ?? throw new ArgumentNullException(nameof(handler));

        public void AddExceptionTypeHandler<T>(Func<T, PerformableAction, Task> handler)
            where T : Exception, new()
        {
            handler = handler ?? throw new ArgumentNullException(nameof(handler));

            if (!_exceptionDelegatesMap.TryAdd(typeof(T), (ex, action) => handler(ex as T, action)) &&
                _exceptionDelegatesMap.TryRemove(typeof(T)))
            {
                _exceptionDelegatesMap.TryAdd(typeof(T), (ex, action) => handler(ex as T, action));
            }
        }

        public async Task<bool> PerformCatchableTask(PerformableAction performableAction, Func<Exception, Task> customActionOnFail = null)
        {
            if (performableAction?.ActionToPerform == null)
            {
                return false;
            }

            try
            {
                await performableAction.ActionToPerform.Invoke();

                if (performableAction.ActionOnSuccess != null)
                {
                    await performableAction.ActionOnSuccess.Invoke();
                }

                return true;
            }
            catch (Exception ex)
            {
                _debuggerService?.Log(ex);

                if (customActionOnFail != null)
                {
                    await customActionOnFail.Invoke(ex);

                    return false;
                }

                Type exceptionType = ex.GetType();

                if (_exceptionDelegatesMap.ContainsKey(exceptionType))
                {
                    await _exceptionDelegatesMap[exceptionType].Invoke(ex, performableAction);
                }
                else if (_exceptionHandlerDelegate != null)
                {
                    await _exceptionHandlerDelegate.Invoke(ex, performableAction);
                }

                try
                {
                    if (performableAction.ActionOnFail != null)
                    {
                        await performableAction.ActionOnFail.Invoke(ex);
                    }
                }
                catch (Exception e)
                {
                    _debuggerService?.Log(e);
                }
            }

            return false;
        }

        public async Task<T> PerformCatchableTask<T>(PerformableAction<T> performableAction, Func<Exception, Task<T>> customActionOnFail = null)
        {
            if (performableAction?.ActionToPerform == null)
            {
                return default;
            }

            try
            {
                T result = await performableAction.ActionToPerform.Invoke();

                if (performableAction.ActionOnSuccess != null)
                {
                    result = await performableAction.ActionOnSuccess.Invoke();
                }

                return result;
            }
            catch (Exception ex)
            {
                _debuggerService?.Log(ex);

                if (customActionOnFail != null)
                {
                    T result = await customActionOnFail.Invoke(ex);

                    return result;
                }

                Type exceptionType = ex.GetType();

                if (_exceptionDelegatesMap.ContainsKey(exceptionType))
                {
                    await _exceptionDelegatesMap[exceptionType].Invoke(ex, performableAction.ToNonGeneric());
                }
                else if (_exceptionHandlerDelegate != null)
                {
                    await _exceptionHandlerDelegate.Invoke(ex, performableAction.ToNonGeneric());
                }

                try
                {
                    if (performableAction.ActionOnFail != null)
                    {
                        T result = await performableAction.ActionOnFail.Invoke(ex);

                        return result;
                    }
                }
                catch (Exception e)
                {
                    _debuggerService?.Log(e);
                }
            }

            return default;
        }
    }
}
