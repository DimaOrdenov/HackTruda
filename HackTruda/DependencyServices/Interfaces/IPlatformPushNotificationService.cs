using HackTruda.Definitions.Models;

namespace HackTruda.DependencyServices.Interfaces
{
    /// <summary>
    /// Платформозависимый сервис для пушей.
    /// </summary>
    public interface IPlatformPushNotificationService
    {
        void ShowNotification(PushNotificationData data);
    }
}
