using System;
using System.Linq;
using System.Threading.Tasks;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;
using Plugin.Permissions;
using Xamarin.Essentials;

namespace HackTruda.ViewModels.Search
{
    public class SearchViewModel : PageViewModel
    {
        private bool _isShowingUser;

        public SearchViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
        }

        public bool IsShowingUser
        {
            get => _isShowingUser;
            set => SetProperty(ref _isShowingUser, value);
        }

        public override async Task OnAppearing()
        {
            if (PageDidAppear)
            {
                return;
            }

            if ((await CrossPermissionsExtension.CheckAndRequestPermissionIfNeeded(
                    new Permissions.LocationWhenInUse(),
                    new Permissions.LocationAlways()))
                    .All(x => x.Value == PermissionStatus.Granted))
            {
                IsShowingUser = true;
            }

            await base.OnAppearing();
        }
    }
}
