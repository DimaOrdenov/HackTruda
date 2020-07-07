using System;
using Autofac;
using AutoMapper;
using HackTruda.DependecyServices.Interfaces;
using HackTruda.DependencyServices.Interfaces;
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
                _debuggerService = Initializer.Properties.DebuggerService;
                _mapper = new Mapper().Build();

                InitDependecyServices(
                    platformAlertMessageServiceImplementation,
                    platformExitServiceImplementation,
                    platformPushNotificationServiceImplementation,
                    platformFirebaseServiceImplementation,
                    platformAnalyticsServiceImplementation,
                    platformShareServiceImplementation,
                    _mapper);

                InitServices();
                InitLogicServices();
                InitPolicies();
                InitRepositories();
                InitViewModels();
                InitTemplateSelectors();
                InitHubs();

                Container = _builder.Build();

                _pageBuilder = Container.Resolve<IPageBuilder>();
                _dialogService = Container.Resolve<IDialogService>();
                _userContext = Container.Resolve<UserContext>();

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
