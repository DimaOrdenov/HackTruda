using System;
using System.Reflection;

namespace HackTruda.Containers
{
    public partial class IocInitializer
    {
        private void InitViewModels()
        {
            _builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.FullName.Contains(nameof(ViewModels)))
                .OnActivated(e =>
                {
                    if (!(e.Instance is PageVM pageVm))
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
                                _dialogService.ShowSnackbar(Messages.CommonErrorMessage);
                            }

                            if (viewModelPerformableAction?.ChangePageState == true)
                            {
                                pageVm.State = PageStateType.AppError;
                            }

                            return Task.FromResult(1);
                        });

                    pageVm.ExceptionHandler.AddExceptionTypeHandler<LogicException>(
                        (ex, action) =>
                        {
                            ViewModelPerformableAction viewModelPerformableAction = action as ViewModelPerformableAction;

                            if (viewModelPerformableAction != null)
                            {
                                Container.Resolve<ExceptionHandlerHelper>().HandleLogicException(
                                    pageVm,
                                    ex as LogicException,
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
