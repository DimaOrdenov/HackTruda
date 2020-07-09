﻿using System.Collections.ObjectModel;
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

        public ICommand OpenSectionCommand { get; }

        public ProfileViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
            ChooseAvatarCommand = BuildPageVmCommand(() => DialogService.DisplayAlert(null, "Выбираю фото", "Ок"));

            CountryTapCommand = BuildPageVmCommand(() => DialogService.DisplayAlert(null, "Ищу по стране", "Ок"));

            OpenSectionCommand = BuildPageVmCommand<string>((i) =>
            {
                string msg = "лайки";

                switch (i)
                {
                    case "1":
                        msg = "друзей";
                        break;
                    case "2":
                        msg = "подписки";
                        break;
                }

                return DialogService.DisplayAlert(null, $"Открываю {msg}", "Ок");
            });

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