using System.Threading;
using System.Threading.Tasks;
using HackTruda.DependencyServices.Interfaces;
using HackTruda.Services.Interfaces;

namespace HackTruda.Services
{
    public class DialogService
    {
        private readonly IDebuggerService _debuggerService;
        private readonly IPlatformAlertMessageService _platformAlertMessageService;
        private readonly INavigationService _navigationService;

        private readonly double _snackbarTranslationY = 100;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public DialogService(
            IDebuggerService debuggerService,
            IPlatformAlertMessageService platformAlertMessageService,
            INavigationService navigationService)
        {
            _debuggerService = debuggerService;
            _platformAlertMessageService = platformAlertMessageService;
            _navigationService = navigationService;
        }

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons) =>
            _navigationService.GetCurrentPage().DisplayActionSheet(title, cancel, destruction, buttons);

        public Task DisplayAlert(string title, string message, string cancel) =>
            _navigationService.GetCurrentPage().DisplayAlert(title, message, cancel);

        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel) =>
            _navigationService.GetCurrentPage().DisplayAlert(title, message, accept, cancel);

        //public void ShowSnackbar(string message, bool showFromTop = false, int millisecondsSnackbarDelay = 5000) =>
        //    ShowSnackbarWithAction(message, string.Empty, null, showFromTop, millisecondsSnackbarDelay);

        //public Task ShowSnackbarAsync(string message, bool showFromTop = false, int millisecondsSnackbarDelay = 5000) =>
        //    ShowSnackbarWithActionAsync(message, string.Empty, null, showFromTop, millisecondsSnackbarDelay);

        //public void ShowSnackbarWithAction(string message, string actionText, ICommand actionCommand, bool showFromTop = false, int millisecondsSnackbarDelay = 5000) =>
        //    ShowSnackbarWithAction(message, actionText, actionCommand, showFromTop, millisecondsSnackbarDelay);

        //        public async Task ShowSnackbarWithActionAsync(string message, string actionText, ICommand actionCommand, bool showFromTop = false, int millisecondsSnackbarDelay = 5000)
        //        {
        //            ViewControls.Base.BasePageContent basePageContent = (_navigationService.GetCurrentPage() as ViewControls.Base.BasePage)?.Content as ViewControls.Base.BasePageContent;
        //            BaseSnackbar snackbar = basePageContent?.Snackbar;

        //            if (snackbar == null)
        //            {
        //                return;
        //            }

        //            if (snackbar.IsVisible)
        //            {
        //                snackbar.CancellationTokenSource?.Cancel();
        //            }

        //            await _semaphoreSlim.WaitAsync();

        //            try
        //            {
        //                snackbar.Text = message;
        //                snackbar.HasActionButton = !string.IsNullOrEmpty(actionText);
        //                snackbar.ActionButtonText = !string.IsNullOrEmpty(actionText) ? actionText : "Ок";
        //                snackbar.ActionButtonCommand = actionCommand;
        //                snackbar.CancellationTokenSource = new CancellationTokenSource();

        //                AbsoluteLayout.SetLayoutFlags(snackbar, showFromTop ? AbsoluteLayoutFlags.WidthProportional : AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
        //                AbsoluteLayout.SetLayoutBounds(snackbar, new Rectangle(0, showFromTop ? basePageContent.NavigationBar.Height : 1, 1, -1));

        //                snackbar.TranslationY = showFromTop ? -_snackbarTranslationY : _snackbarTranslationY;
        //                snackbar.IsVisible = true;

        //                snackbar.GestureRecognizers.Clear();
        //                snackbar.GestureRecognizers.Add(new SwipeGestureRecognizer
        //                {
        //                    Threshold = 10,
        //                    Direction = showFromTop ? SwipeDirection.Up : SwipeDirection.Down,
        //                    Command = new Command(() =>
        //                    {
        //                        snackbar.CancellationTokenSource?.Cancel();
        //                    }),
        //                });

        //#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        //                AnimationExtension.RunAnimation(AnimationModel.Create(
        //                    async () =>
        //                    {
        //                        await Task.WhenAll(
        //                            snackbar.FadeTo(1, easing: Easing.CubicInOut),
        //                            snackbar.TranslateTo(snackbar.TranslationX, snackbar.TranslationY + (showFromTop ? _snackbarTranslationY : -_snackbarTranslationY), easing: Easing.CubicInOut));
        //                    },
        //                    () =>
        //                    {
        //                        snackbar.Opacity = 1;
        //                        snackbar.TranslationY += showFromTop ? _snackbarTranslationY : -_snackbarTranslationY;
        //                    })
        //                    .SetAndroidAnimatorDuration(App.AndroidAnimatorDuration));
        //#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        //                await Task.Delay(millisecondsSnackbarDelay, snackbar.CancellationTokenSource?.Token ?? CancellationToken.None);
        //            }
        //            catch (Exception e)
        //            {
        //                _debuggerService.Log(e);
        //            }
        //            finally
        //            {
        //#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        //                AnimationExtension.RunAnimation(AnimationModel.Create(
        //                    async () =>
        //                    {
        //                        await Task.WhenAll(
        //                            snackbar.FadeTo(0, easing: Easing.CubicInOut),
        //                            snackbar.TranslateTo(snackbar.TranslationX, snackbar.TranslationY + (showFromTop ? -_snackbarTranslationY : _snackbarTranslationY), easing: Easing.CubicInOut));

        //                        snackbar.IsVisible = false;
        //                    },
        //                    () =>
        //                    {
        //                        snackbar.Opacity = 0;
        //                        snackbar.TranslationY += showFromTop ? -_snackbarTranslationY : _snackbarTranslationY;
        //                        snackbar.IsVisible = false;
        //                    })
        //                    .SetAndroidAnimatorDuration(App.AndroidAnimatorDuration));
        //#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        //                _semaphoreSlim.Release();
        //            }
        //        }

        //public Task CustomDisplayAlert(string textLabel, string buttonText = null) =>
        //    Application.Current.MainPage.Navigation.PushPopupAsync(new CustomDisplayAlert(textLabel, buttonText));

        public void ShowPlatformLongAlert(string message) =>
            _platformAlertMessageService.LongAlert(message);

        public void ShowPlatformShortAlert(string message)
            => _platformAlertMessageService.ShortAlert(message);
    }
}
