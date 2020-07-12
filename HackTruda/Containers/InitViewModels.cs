using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using HackTruda.Definitions;
using HackTruda.Definitions.Enums;
using HackTruda.Definitions.Exceptions;
using HackTruda.Services;
using HackTruda.Services.Interfaces;
using HackTruda.ViewModels;

namespace HackTruda.Containers
{
    public partial class IocInitializer
    {
        /// <summary>
        /// Регистрация VM.
        /// </summary>
        private static void InitViewModels()
        {
            _builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.FullName.Contains(nameof(ViewModels)))
                .OnActivated(e =>
                {
                    if (!(e.Instance is PageViewModel pageVm))
                    {
                        return;
                    }

                    pageVm.ExceptionHandler = e.Context.Resolve<IExceptionHandler>();

                    pageVm.ExceptionHandler.SetExceptionHandlerDelegate(
                        (ex, action) =>
                        {
                            ViewModelPerformableAction viewModelPerformableAction = action as ViewModelPerformableAction;

                            if (viewModelPerformableAction?.ShowSnackbar == true)
                            {
                                //_dialogService.ShowSnackbar(Messages.CommonErrorMessage);
                            }

                            if (viewModelPerformableAction?.ChangePageState == true)
                            {
                                pageVm.State = PageStateType.AppError;
                            }

                            return Task.FromResult(1);
                        });

                    pageVm.ExceptionHandler.AddExceptionTypeHandler<BusinessLogicException>(
                        (ex, action) =>
                        {
                            ViewModelPerformableAction viewModelPerformableAction = action as ViewModelPerformableAction;

                            if (viewModelPerformableAction != null)
                            {
                                Container.Resolve<ExceptionHandlerHelper>().HandleLogicException(
                                    pageVm,
                                    ex as BusinessLogicException,
                                    viewModelPerformableAction.ChangePageState,
                                    viewModelPerformableAction.ShowSnackbar);
                            }

                            return Task.FromResult(1);
                        });
                })
                .AsSelf();
        }
    }
}
