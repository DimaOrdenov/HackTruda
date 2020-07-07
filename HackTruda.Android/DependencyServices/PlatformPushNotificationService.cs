using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using HackTruda.Definitions.Models;
using HackTruda.DependecyServices.Interfaces;
using Newtonsoft.Json;
using Plugin.CurrentActivity;

namespace HackTruda.Droid.DependencyServices
{
    public class PlatformPushNotificationService : IPlatformPushNotificationService
    {
        private int _notificationId;

        public void ShowNotification(PushNotificationData data)
        {
            Intent intent = new Intent(CrossCurrentActivity.Current.AppContext, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.SingleTop);

            intent.PutExtra(nameof(PushNotificationData), JsonConvert.SerializeObject(data));

            NotificationManager notificationManager = CrossCurrentActivity.Current.AppContext.GetSystemService(Context.NotificationService) as NotificationManager;

            var notificationBuilder =
                new NotificationCompat.Builder(CrossCurrentActivity.Current.AppContext, MainActivity.NotificationChannelId)
                    .SetAutoCancel(true)
                    .SetDefaults((int)NotificationDefaults.All)
                    //.SetSmallIcon(Resource.Drawable.ic_notification_icon)
                    .SetContentTitle(data.Title)
                    .SetContentText(data.Body)
                    .SetContentIntent(PendingIntent.GetActivity(CrossCurrentActivity.Current.AppContext, new Random().Next(), intent, PendingIntentFlags.OneShot))
                    .SetShowWhen(true)
                    ;

            notificationManager?.Notify(_notificationId++, notificationBuilder.Build());
        }
    }
}
