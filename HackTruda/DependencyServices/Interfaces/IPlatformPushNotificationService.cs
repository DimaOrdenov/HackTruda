using HackTruda.Definitions.Models;

namespace HackTruda.DependencyServices.Interfaces
{
    public interface IPlatformPushNotificationService
    {
        void ShowNotification(PushNotificationData data);
    }
}
