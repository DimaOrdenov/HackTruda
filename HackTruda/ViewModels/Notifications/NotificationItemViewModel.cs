using System.IO;
using HackTruda.DataModels.Responses;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Notifications
{
    public class NotificationItemViewModel : BaseViewModel
    {
        public NotificationItemViewModel(ImageSource image, string notificationReporter, string notificationAction, string notificatedAt)
        {
            Image = image;
            NotificationReporter = notificationReporter;
            NotificationAction = notificationAction;

            NotificatedAt = notificatedAt;
        }

        public NotificationItemViewModel(UserResponse user, string notificationAction, string notificatedAt)
            : this(
                  user.Image != null ? ImageSource.FromStream(() => new MemoryStream(user.Image)) : null,
                  $"{user.FirstName} {user.LastName}",
                  notificationAction,
                  notificatedAt)
        {
        }

        public ImageSource Image { get; }

        public string NotificationReporter { get; }

        public string NotificationAction { get; }

        public string NotificatedAt { get; }
    }
}
