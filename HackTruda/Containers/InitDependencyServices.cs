using Autofac;
using HackTruda.DependencyServices.Interfaces;

namespace HackTruda.Containers
{
    partial class IocInitializer
    {
        private static void InitDependencyServices(
            IPlatformAlertMessageService platformAlertMessageServiceImplementation,
            IPlatformPushNotificationService platformPushNotificationServiceImplementation)
        {
            _builder.RegisterInstance(platformAlertMessageServiceImplementation);
            _builder.RegisterInstance(platformPushNotificationServiceImplementation);
        }
    }
}
