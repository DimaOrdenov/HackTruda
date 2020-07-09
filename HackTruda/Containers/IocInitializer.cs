using System;
using Autofac;
using AutoMapper;
using HackTruda.DependencyServices.Interfaces;
using HackTruda.Services;
using HackTruda.Services.Interfaces;

namespace HackTruda.Containers
{
    public partial class IocInitializer
    {
        private static ContainerBuilder _builder;

        private static IPageBuilder _pageBuilder;
        private static IDebuggerService _debuggerService;
        private static IDialogService _dialogService;
        private static IMapper _mapper;

        public static IContainer Container { get; private set; }

        public static bool IocInitialized { get; private set; }

        public static void Initialize(
            IPlatformAlertMessageService platformAlertMessageServiceImplementation,
            IPlatformPushNotificationService platformPushNotificationServiceImplementation)
        {
            try
            {
                _builder = new ContainerBuilder();
                _debuggerService = new DebuggerService();
                _mapper = new Mapper().Build();

                InitDependencyServices(
                    platformAlertMessageServiceImplementation,
                    platformPushNotificationServiceImplementation);

                InitServices();
                InitBusinessLogicServices();
                InitViewModels();
                InitHubs();

                Container = _builder.Build();

                _pageBuilder = Container.Resolve<IPageBuilder>();
                _dialogService = Container.Resolve<IDialogService>();

                InitPages();

                IocInitialized = true;
                _debuggerService.Log($"{typeof(IocInitializer).FullName} finished successfully");
            }
            catch (Exception e)
            {
                _debuggerService.Log(e);
                _debuggerService.Log($"{typeof(IocInitializer).FullName} finished unsuccessfully");
            }
        }
    }
}
