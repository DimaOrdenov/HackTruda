using System;
using System.Threading.Tasks;

namespace HackTruda.Definitions
{
    public class PerformableAction
    {
        public PerformableAction(Func<Task> actionToPerform) =>
            ActionToPerform = actionToPerform ?? throw new ArgumentNullException(nameof(actionToPerform));

        public Func<Task> ActionToPerform { get; private set; }

        public Func<Task> ActionOnSuccess { get; private set; }

        public Func<Exception, Task> ActionOnFail { get; private set; }

        public PerformableAction OnSuccess(Func<Task> successAction)
        {
            ActionOnSuccess = successAction ?? throw new ArgumentNullException(nameof(successAction));

            return this;
        }

        public PerformableAction OnFail(Func<Exception, Task> failAction)
        {
            ActionOnFail = failAction ?? throw new ArgumentNullException(nameof(failAction));

            return this;
        }
    }
}
