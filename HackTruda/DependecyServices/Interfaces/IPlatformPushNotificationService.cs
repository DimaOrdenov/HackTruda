using System;
namespace HackTruda.DependecyServices.Interfaces
{
    public interface IPlatformPushNotificationService
    {
        void ShowNotification(PushNotificationData data);
    }
}
