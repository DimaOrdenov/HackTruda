using System;
using System.Threading.Tasks;

namespace HackTruda.Definitions
{
    public class PerformableAction<T>
    {
        public PerformableAction(Func<Task<T>> actionToPerform) =>
            ActionToPerform = actionToPerform ?? throw new ArgumentNullException(nameof(actionToPerform));

        public Func<Task<T>> ActionToPerform { get; private set; }

        public Func<Task<T>> ActionOnSuccess { get; private set; }

        public Func<Exception, Task<T>> ActionOnFail { get; private set; }

        public PerformableAction<T> OnSuccess(Func<Task<T>> successAction)
        {
            ActionOnSuccess = successAction ?? throw new ArgumentNullException(nameof(successAction));

            return this;
        }

        public PerformableAction<T> OnFail(Func<Exception, Task<T>> failAction)
        {
            ActionOnFail = failAction ?? throw new ArgumentNullException(nameof(failAction));

            return this;
        }

        public virtual PerformableAction ToNonGeneric() =>
            new PerformableAction(
                async () =>
                {
                    if (ActionToPerform != null)
                    {
                        await ActionToPerform();
                    }
                })
            .OnSuccess(
                async () =>
                {
                    if (ActionOnSuccess != null)
                    {
                        await ActionOnSuccess();
                    }
                })
            .OnFail(
                async (ex) =>
                {
                    if (ActionOnFail != null)
                    {
                        await ActionOnFail(ex);
                    }
                });
    }
}
