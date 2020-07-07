using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HackTruda.Services.Interfaces
{
    public interface IDialogService
    {
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);

        Task DisplayAlert(string title, string message, string cancel);

        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);

        //Task CustomDisplayAlert(string textLabel, string buttonText = null);

        //void ShowSnackbar(string message, bool showFromTop = false, int millisecondsSnackbarDelay = 5000);

        //Task ShowSnackbarAsync(string message, bool showFromTop = false, int millisecondsSnackbarDelay = 5000);

        //void ShowSnackbarWithAction(string message, string actionText, ICommand actionCommand, bool showFromTop = false, int millisecondsSnackbarDelay = 5000);

        //Task ShowSnackbarWithActionAsync(string message, string actionText, ICommand actionCommand, bool showFromTop = false, int millisecondsSnackbarDelay = 5000);

        void ShowPlatformLongAlert(string message);

        void ShowPlatformShortAlert(string message);
    }
}
