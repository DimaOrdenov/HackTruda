using System.Collections.Generic;
using HackTruda.Definitions.Enums;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels
{
    public abstract class TabbedPageViewModel : PageViewModel
    {
        public int TabPagesCount;
        public readonly List<PageType> TabPagesTypes = new List<PageType>();

        public TabbedPageViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }

        public TabbedPageType TabbedPageTypeValue;

        public virtual void OnCurrentPageChanged(int pageNumber)
        {
        }
    }
}
