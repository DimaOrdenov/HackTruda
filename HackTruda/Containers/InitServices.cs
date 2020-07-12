using System;
using Autofac;
using HackTruda.Services;
using HackTruda.Services.Interfaces;

namespace HackTruda.Containers
{
    public partial class IocInitializer
    {
        /// <summary>
        /// Регистрация сервисов.
        /// </summary>
        private static void InitServices()
        {
            _builder.RegisterInstance(new Mapper().Build());

            _builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            _builder.RegisterType<PageBuilder>().As<IPageBuilder>().SingleInstance();
            _builder.RegisterType<NavigationExtensions>().AsSelf().SingleInstance();
            _builder.RegisterInstance(_debuggerService).As<IDebuggerService>().SingleInstance();
            _builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            _builder.RegisterType<ExceptionHandler>().As<IExceptionHandler>();
            _builder.RegisterType<ExceptionHandlerHelper>().AsSelf().SingleInstance();
        }
    }
}
