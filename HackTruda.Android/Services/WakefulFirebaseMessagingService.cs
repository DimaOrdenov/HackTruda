using System;
using Android.App;
using Android.Content;
using Android.Support.V4.Content;
using Autofac;
using HackTruda.Containers;
using HackTruda.Services.Interfaces;

namespace HackTruda.Droid.Services
{
    [BroadcastReceiver(Enabled = true, Permission = "com.google.android.c2dm.permission.SEND")]
    [IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE", "com.google.android.c2dm.intent.REGISTRATION" })]
    public class WakefulFirebaseMessagingService : WakefulBroadcastReceiver
    {
        private IDebuggerService _debuggerService;

        public WakefulFirebaseMessagingService()
        {
            if (IocInitializer.IocInitialized)
            {
                _debuggerService = IocInitializer.Container.Resolve<IDebuggerService>();
            }
        }

        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                ComponentName comp = new ComponentName(context.PackageName, nameof(FirebasePushNotificationService));
                StartWakefulService(context, intent.SetComponent(comp));
                SetResult(Result.Ok, ResultData, intent.Extras);
            }
            catch (Exception e)
            {
                _debuggerService?.Log(e);
            }
        }
    }
}
