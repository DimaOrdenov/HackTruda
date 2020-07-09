using System.Collections.ObjectModel;
using System.Windows.Input;
using FFImageLoading.Forms;
using HackTruda.Definitions.Enums;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Profile
{
    public class ProfileViewModel : PageViewModel
    {
        public ICommand ChooseAvatarCommand { get; }

        public ICommand CountryTapCommand { get; }

        public ProfileViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
            CountryTapCommand = BuildPageVmCommand(() => DialogService.DisplayAlert(null, "Ищу по стране", "Ок"));

            CachedImage icSettings = new CachedImage
            {
                Source = AppImages.IcSettings,
            };

            icSettings.SetTintColor(Color.Black);

            ToolbarItems = new ObservableCollection<ToolbarItem>
            {
                new ToolbarItem
                {
                    IconImageSource = icSettings.Source,
                    Command = BuildPageVmCommand(() => NavigationService.NavigateAsync(PageType.ProfileSettingsPage)),
                }
            };
        }
    }
}
