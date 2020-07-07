using Android.App;
using Autofac;
using Firebase.Iid;
using HackTruda.Containers;
using HackTruda.Services.Interfaces;

namespace HackTruda.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseInstanceIdRegistrationService : FirebaseInstanceIdService
    {
        private const string TAG = "FirebaseRegistrationService";

        private IDebuggerService _debuggerService;

        public override void OnCreate()
        {
            base.OnCreate();

            if (IocInitializer.IocInitialized)
            {
                _debuggerService = IocInitializer.Container.Resolve<IDebuggerService>();
            }
        }

        public override void OnTokenRefresh()
        {
            string refreshedToken = FirebaseInstanceId.Instance.Token;

            _debuggerService?.Log($"FirebaseInstanceId refreshed token: {refreshedToken}");
        }
    }
}
