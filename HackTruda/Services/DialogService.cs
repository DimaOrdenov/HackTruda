using System.Threading.Tasks;
using HackTruda.DependencyServices.Interfaces;
using HackTruda.Services.Interfaces;

namespace HackTruda.Services
{
    /// <summary>
    /// Сервис для вызова алертов.
    /// </summary>
    public class DialogService : IDialogService
    {
        private readonly IPlatformAlertMessageService _platformAlertMessageService;
        private readonly INavigationService _navigationService;

        public DialogService(
            IPlatformAlertMessageService platformAlertMessageService,
            INavigationService navigationService)
        {
            _platformAlertMessageService = platformAlertMessageService;
            _navigationService = navigationService;
        }

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons) =>
            _navigationService.GetCurrentPage().DisplayActionSheet(title, cancel, destruction, buttons);

        public Task DisplayAlert(string title, string message, string cancel) =>
            _navigationService.GetCurrentPage().DisplayAlert(title, message, cancel);

        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel) =>
            _navigationService.GetCurrentPage().DisplayAlert(title, message, accept, cancel);

        public void ShowPlatformLongAlert(string message) =>
            _platformAlertMessageService.LongAlert(message);

        public void ShowPlatformShortAlert(string message)
            => _platformAlertMessageService.ShortAlert(message);
    }
}
