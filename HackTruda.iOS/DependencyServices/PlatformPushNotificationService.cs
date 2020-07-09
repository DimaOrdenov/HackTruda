using Autofac;
using HackTruda.Containers;
using HackTruda.Definitions.Models;
using HackTruda.DependencyServices.Interfaces;
using HackTruda.Services.Interfaces;
using UserNotifications;

namespace HackTruda.iOS.DependencyServices
{
    public class PlatformPushNotificationService : IPlatformPushNotificationService
    {
        private readonly IDebuggerService _debuggerService;

        private int _notificationId;

        public PlatformPushNotificationService()
        {
            if (IocInitializer.IocInitialized)
            {
                _debuggerService = IocInitializer.Container.Resolve<IDebuggerService>();
            }
        }

        public void ShowNotification(PushNotificationData notification)
        {
            UNTimeIntervalNotificationTrigger trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0.1, false);

            UNMutableNotificationContent content = new UNMutableNotificationContent
            {
                Title = notification.Title,
                Body = notification.Body,
                Sound = UNNotificationSound.Default,
            };

            UNNotificationRequest request = UNNotificationRequest.FromIdentifier(_notificationId++.ToString(), content, trigger);

            UNUserNotificationCenter.Current.AddNotificationRequest(request, err =>
            {
                if (err != null)
                {
                    _debuggerService?.Log($"Failed to schedule notification: {err?.LocalizedDescription}");
                }
            });
        }
    }
}
