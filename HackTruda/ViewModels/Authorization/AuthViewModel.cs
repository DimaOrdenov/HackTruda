using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using FFImageLoading.Svg.Forms;
using HackTruda.Definitions;
using HackTruda.Definitions.Enums;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;
using Xamarin.Essentials;

namespace HackTruda.ViewModels.Authorization
{
    public class AuthViewModel : PageViewModel
    {
        private string _newUserName;
        private string _newPhoneNumber;
        private string _newPassword;
        private string _phoneNumber;
        private string _password;
        private bool _isPasswordVisibile;
        private bool _isNewPasswordVisibile;
        private bool _isAuthProcess = true;

        public ICommand SocialAuthCommand { get; }

        public ICommand PasswordVisibilityCommand { get; }

        public ICommand NewPasswordVisibilityCommand { get; }

        public ICommand ForgotPasswordCommand { get; }

        public ICommand LoginCommand { get; }

        public ICommand RegisterCommand { get; }

        public ICommand ChangeFormCommand { get; }

        public AuthViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService) : base(navigationService, dialogService, debuggerService)
        {
            SocialAuthCommand = BuildPageVmCommand<string>(
                async (scheme) =>
                {
                    bool success = await ExceptionHandler.PerformCatchableTask(
                        new ViewModelPerformableAction(async () =>
                        {
                            WebAuthenticatorResult result =
                                await WebAuthenticator.AuthenticateAsync(
                                    new Uri(Config.BaseApiUrl + $"api/auth/{scheme}"),
                                    new Uri("hacktruda://"));

                            var accessToken = result?.AccessToken;
                        }));

                    await DialogService.DisplayAlert(null, (success ? "Успешная " : "Неуспешная  ") + $"авторизация через {scheme}", "Ок");

                    NavigationService.SetRootPage(TabbedPageType.MainPage);
                });

            ForgotPasswordCommand = BuildPageVmCommand(() => DialogService.DisplayAlert(null, "Восстанавливаю пароль", "Ок"));

            LoginCommand = BuildPageVmCommand(() => Task.Delay(1000).ContinueWith(t => NavigationService.SetRootPage(TabbedPageType.MainPage)));

            RegisterCommand = BuildPageVmCommand(() => Task.Delay(1000).ContinueWith(t => NavigationService.SetRootPage(TabbedPageType.MainPage)));

            PasswordVisibilityCommand = BuildPageVmCommand(() =>
            {
                IsPasswordVisibile = !_isPasswordVisibile;

                return Task.CompletedTask;
            });

            NewPasswordVisibilityCommand = BuildPageVmCommand(() =>
            {
                IsNewPasswordVisibile = !_isNewPasswordVisibile;

                return Task.CompletedTask;
            });

            ChangeFormCommand = BuildPageVmCommand<string>(
                (formId) =>
                {
                    IsAuthProcess = formId == "0";

                    return Task.CompletedTask;
                });

            Socials = new List<SocialItemViewModel>
            {
                new SocialItemViewModel(SocialAuthType.Vk),
                new SocialItemViewModel(SocialAuthType.Ok),
                new SocialItemViewModel(SocialAuthType.Fb),
                new SocialItemViewModel(SocialAuthType.Google),
            };
        }

        public IEnumerable<SocialItemViewModel> Socials { get; }

        public string NewUsername
        {
            get => _newUserName;
            set => SetProperty(ref _newUserName, value);
        }

        public string NewPhoneNumber
        {
            get => _newPhoneNumber;
            set => SetProperty(ref _newPhoneNumber, value);
        }

        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsPasswordVisibile
        {
            get => _isPasswordVisibile;
            set
            {
                SetProperty(ref _isPasswordVisibile, value);

                OnPropertyChanged(nameof(PasswordVisibilityImage));
            }
        }

        public bool IsNewPasswordVisibile
        {
            get => _isNewPasswordVisibile;
            set
            {
                SetProperty(ref _isNewPasswordVisibile, value);

                OnPropertyChanged(nameof(NewPasswordVisibilityImage));
            }
        }

        public bool IsAuthProcess
        {
            get => _isAuthProcess;
            set => SetProperty(ref _isAuthProcess, value);
        }

        public SvgImageSource PasswordVisibilityImage
        {
            get
            {
                SvgImageSource image = _isPasswordVisibile ? AppSvgImages.IcEyeOff : AppSvgImages.IcEye;
                image.SetStrokeTintColor(AppColors.Grey);

                return image;
            }
        }

        public SvgImageSource NewPasswordVisibilityImage
        {
            get
            {
                SvgImageSource image = _isNewPasswordVisibile ? AppSvgImages.IcEyeOff : AppSvgImages.IcEye;
                image.SetStrokeTintColor(AppColors.Grey);

                return image;
            }
        }
    }
}
