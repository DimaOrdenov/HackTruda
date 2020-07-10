using System;
using System.Windows.Input;
using HackTruda.Definitions;
using HackTruda.Definitions.Enums;
using HackTruda.Services.Interfaces;
using Xamarin.Essentials;

namespace HackTruda.ViewModels.Authorization
{
    public class AuthViewModel : PageViewModel
    {
        public ICommand VkAuthCommand { get; }

        public AuthViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService) : base(navigationService, dialogService, debuggerService)
        {
            VkAuthCommand = BuildPageVmCommand(
                async () =>
                {
                    bool success = await ExceptionHandler.PerformCatchableTask(
                        new ViewModelPerformableAction(async () =>
                        {
                            WebAuthenticatorResult result =
                                await WebAuthenticator.AuthenticateAsync(
                                    new Uri(Config.BaseApiUrl + "api/auth/vk"),
                                    new Uri("hacktruda://"));

                            var accessToken = result?.AccessToken;
                        }));

                    await DialogService.DisplayAlert(null, (success ? "Успешная " : "Неуспешная  ") + "авторизация через ВК", "Ок");

                    NavigationService.SetRootPage(TabbedPageType.MainPage);
                });
        }
    }
}
