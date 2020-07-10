using System;
using HackTruda.Definitions.Models;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Notifications
{
    public class NotificationItemViewModel : BaseViewModel
    {
        public NotificationItemViewModel(ImageSource image, string notificationReporter, string notificationAction, DateTime notificatedAt)
        {
            Image = image;
            NotificationReporter = notificationReporter;
            NotificationAction = notificationAction;
            NotificatedAt = notificatedAt;
        }

        public NotificationItemViewModel(UserModel user, string notificationAction, DateTime notificatedAt)
            : this(user.ImageSrc?.Value, $"{user.FirstName} + {user.LastName}", notificationAction, notificatedAt)
        {
        }

        public ImageSource Image { get; }

        public string NotificationReporter { get; }

        public string NotificationAction { get; }

        public DateTime NotificatedAt { get; }
    }
}
