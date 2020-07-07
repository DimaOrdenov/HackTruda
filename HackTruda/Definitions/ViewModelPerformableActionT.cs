using System;
using System.Threading.Tasks;

namespace HackTruda.Definitions
{
    public class ViewModelPerformableAction<T> : PerformableAction<T>
    {
        public ViewModelPerformableAction(Func<Task<T>> actionToPerform)
            : base(actionToPerform)
        {
        }

        public bool ChangePageState { get; private set; } = true;

        public bool ShowSnackbar { get; private set; }

        public ViewModelPerformableAction<T> IfChangePageState(bool ifChange)
        {
            ChangePageState = ifChange;

            return this;
        }

        public ViewModelPerformableAction<T> IfShowSnackbar(bool ifShowSnackbar)
        {
            ShowSnackbar = ifShowSnackbar;

            return this;
        }

        public override PerformableAction ToNonGeneric() =>
            (base.ToNonGeneric() as ViewModelPerformableAction)
                .IfChangePageState(ChangePageState)
                .IfShowSnackbar(ShowSnackbar);
    }
}
