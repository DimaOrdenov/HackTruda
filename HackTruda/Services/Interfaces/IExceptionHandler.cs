using System;
using System.Threading.Tasks;
using HackTruda.Definitions;

namespace HackTruda.Services.Interfaces
{
    public interface IExceptionHandler
    {
        void SetExceptionHandlerDelegate(Func<Exception, PerformableAction, Task> handler);

        void AddExceptionTypeHandler<T>(Func<T, PerformableAction, Task> handler)
            where T : Exception, new();

        Task<bool> PerformCatchableTask(PerformableAction performableAction, Func<Exception, Task> customActionOnFail = null);

        Task<T> PerformCatchableTask<T>(PerformableAction<T> performableAction, Func<Exception, Task<T>> customActionOnFail = null);
    }
}
