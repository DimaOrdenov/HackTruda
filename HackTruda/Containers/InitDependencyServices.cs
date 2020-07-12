using Autofac;
using HackTruda.DependencyServices.Interfaces;

namespace HackTruda.Containers
{
    partial class IocInitializer
    {
        /// <summary>
        /// Регистрация платформозависимых сервисов.
        /// </summary>
        private static void InitDependencyServices(
            IPlatformAlertMessageService platformAlertMessageServiceImplementation,
            IPlatformPushNotificationService platformPushNotificationServiceImplementation)
        {
            _builder.RegisterInstance(platformAlertMessageServiceImplementation);
            _builder.RegisterInstance(platformPushNotificationServiceImplementation);
        }
    }
}
