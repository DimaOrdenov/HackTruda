using System;
using System.Collections.Generic;
using System.Windows.Input;
using HackTruda.Definitions.Enums;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Onboarding
{
    public class OnboardingViewModel : PageViewModel
    {
        public ICommand SkipCommand { get; }

        public OnboardingViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
            SkipCommand = BuildPageVmCommand(
                async () =>
                {
                    App.Current.Properties["IsFirstLaunch"] = false;
                    await App.Current.SavePropertiesAsync();

                    await NavigationService.NavigateAsync(PageType.AuthPage);
                });

            Items = new List<OnboardingItemViewModel>
            {
                new OnboardingItemViewModel("Мы разные, но мы вместе", AppImages.Onboarding1),
                new OnboardingItemViewModel("Мы живем в одной стране, которая стала нам домом", AppImages.Onboarding2),
                new OnboardingItemViewModel("Здесь мы можем быть счастливы, учиться, работать, растить детей и быть собой", AppImages.Onboarding3),
            };
        }

        public IEnumerable<OnboardingItemViewModel> Items { get; }
    }
}
