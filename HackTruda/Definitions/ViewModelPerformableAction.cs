using System;
using System.Threading.Tasks;

namespace HackTruda.Definitions
{
    public class ViewModelPerformableAction : PerformableAction
    {
        public ViewModelPerformableAction(Func<Task> actionToPerform)
            : base(actionToPerform)
        {
        }

        public bool ChangePageState { get; private set; } = true;

        public bool ShowSnackbar { get; private set; }

        public ViewModelPerformableAction IfChangePageState(bool ifChange)
        {
            ChangePageState = ifChange;

            return this;
        }

        public ViewModelPerformableAction IfShowSnackbar(bool ifShowSnackbar)
        {
            ShowSnackbar = ifShowSnackbar;

            return this;
        }
    }
}
