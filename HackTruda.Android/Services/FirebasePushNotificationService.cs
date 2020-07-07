using System;
using System.Collections.Generic;
using Android.App;
using Autofac;
using Firebase.Messaging;
using HackTruda.Containers;
using HackTruda.Definitions.Models;
using HackTruda.DependencyServices.Interfaces;
using HackTruda.Services;
using HackTruda.Services.Interfaces;
using Newtonsoft.Json;

namespace HackTruda.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebasePushNotificationService : FirebaseMessagingService
    {
        private const string TAG = "FirebasePushNotificationService";

        private IPlatformPushNotificationService _platformPushNotificationService;
        private IDebuggerService _debuggerService;

        public override void OnCreate()
        {
            base.OnCreate();

            if (IocInitializer.IocInitialized)
            {
                _platformPushNotificationService = IocInitializer.Container.Resolve<IPlatformPushNotificationService>();
                _debuggerService = IocInitializer.Container.Resolve<IDebuggerService>();
            }
            else
            {
                _platformPushNotificationService = new DependencyServices.PlatformPushNotificationService();
                _debuggerService = new DebuggerService();
            }
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            RemoteMessage.Notification notification = message?.GetNotification();
            IDictionary<string, string> notificationData = message?.Data;
            PushNotificationData pushNotificationData;

            try
            {
                // If push notification Notification-formatted
                if (!string.IsNullOrWhiteSpace(notification?.Title) && !string.IsNullOrWhiteSpace(notification?.Body))
                {
                    pushNotificationData = new PushNotificationData
                    {
                        Title = notification.Title,
                        Body = notification.Body,
                    };
                }

                // If push notification Data-formatted
                else
                {
                    pushNotificationData = JsonConvert.DeserializeObject<PushNotificationData>(
                        JsonConvert.SerializeObject(new Dictionary<string, string>(notificationData), Formatting.Indented));
                }

                if (pushNotificationData.IsShow)
                {
                    _platformPushNotificationService.ShowNotification(pushNotificationData);
                }
            }
            catch (Exception e)
            {
                _debuggerService?.Log(e);
            }
        }
    }
}
