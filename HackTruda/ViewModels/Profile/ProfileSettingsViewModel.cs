using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FFImageLoading.Forms;
using HackTruda.Definitions.Enums;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Profile
{
    /// <summary>
    /// VM для настроек профиля.
    /// </summary>
    public class ProfileSettingsViewModel : PageViewModel
    {
        private const string SETTINGS_ITEM_PERSONAL_INFO = "Личная жизнь";
        private const string SETTINGS_ITEM_LANGUAGE = "Язык";
        private const string SETTINGS_ITEM_NOTIFICATIONS = "Уведомления";
        private const string SETTINGS_ITEM_ACTIONS = "Действия";
        private const string SETTINGS_ITEM_PRIVATE_ACCOUNT = "Закрытый аккаунт";

        private ICommand _settingsItemTapCommand;

        public ProfileSettingsViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
            _settingsItemTapCommand = BuildPageVmCommand<ProfileSettingsItemViewModel>(
                async (item) =>
                {
                    if (item.Type == ProfileSettingsItemType.Switch)
                    {
                        item.IsToggled = !item.IsToggled;
                    }

                    //switch (item.Title)
                    //{
                    //    case SETTINGS_ITEM_PERSONAL_INFO:
                    //        break;
                    //    case SETTINGS_ITEM_LANGUAGE:
                    //        break;
                    //    case SETTINGS_ITEM_NOTIFICATIONS:
                    //        break;
                    //    case SETTINGS_ITEM_ACTIONS:
                    //        break;
                    //    case SETTINGS_ITEM_PRIVATE_ACCOUNT:
                    //        break;
                    //}

                    await DialogService.DisplayAlert(null, $"{item.Title}", "Ок");
                });

            Settings = new List<ProfileSettingsGroupViewModel>
            {
                new ProfileSettingsGroupViewModel(
                    "Аккаунт",
                    new List<ProfileSettingsItemViewModel>
                    {
                        new ProfileSettingsItemViewModel(ProfileSettingsItemType.Navigation, SETTINGS_ITEM_PERSONAL_INFO)
                        {
                            TapCommand = _settingsItemTapCommand,
                        },
                        new ProfileSettingsItemViewModel(ProfileSettingsItemType.Navigation, SETTINGS_ITEM_LANGUAGE)
                        {
                            TapCommand = _settingsItemTapCommand,
                        },
                    }),
                new ProfileSettingsGroupViewModel(
                    "Настройки приложения",
                    new List<ProfileSettingsItemViewModel>
                    {
                        new ProfileSettingsItemViewModel(ProfileSettingsItemType.Switch, SETTINGS_ITEM_NOTIFICATIONS)
                        {
                            TapCommand = _settingsItemTapCommand,
                        },
                        new ProfileSettingsItemViewModel(ProfileSettingsItemType.Navigation, SETTINGS_ITEM_ACTIONS)
                        {
                            TapCommand = _settingsItemTapCommand,
                        },
                    }),
                new ProfileSettingsGroupViewModel(
                    "Приватность",
                    new List<ProfileSettingsItemViewModel>
                    {
                        new ProfileSettingsItemViewModel(ProfileSettingsItemType.Switch, SETTINGS_ITEM_PRIVATE_ACCOUNT)
                        {
                            TapCommand = _settingsItemTapCommand,
                        },
                    }),
            };

            //CachedImage icMore = new CachedImage
            //{
            //    Source = AppImages.IcMoreVertical,
            //};

            //icMore.SetTintColor(Color.Black);

            //ToolbarItems = new ObservableCollection<ToolbarItem>
            //{
            //    new ToolbarItem
            //    {
            //        IconImageSource = icMore.Source,
            //        Command = BuildPageVmCommand(() => DialogService.DisplayAlert()),
            //    }
            //};
        }

        public IEnumerable<ProfileSettingsGroupViewModel> Settings { get; }
    }
}
