using HackTruda.Definitions.Enums;
using HackTruda.Definitions.Exceptions;
using HackTruda.Services.Interfaces;
using HackTruda.ViewModels;

namespace HackTruda.Services
{
    /// <summary>
    /// Хэлпер для обработчика исключений для работы с VM.
    /// </summary>
    public class ExceptionHandlerHelper
    {
        private readonly IDialogService _dialogService;
        private readonly IDebuggerService _debuggerService;
        private readonly NavigationExtensions _navigationExtensions;

        public ExceptionHandlerHelper(
            IDialogService dialogService,
            IDebuggerService debuggerService,
            NavigationExtensions navigationExtensions)
        {
            _dialogService = dialogService;
            _debuggerService = debuggerService;
            _navigationExtensions = navigationExtensions;
        }

        internal void HandleLogicException(PageViewModel pageVm, BusinessLogicException logicException, bool changePageState, bool showSnackbar)
        {
            TryToChangePageState(pageVm, logicException, changePageState);
            //TryToShowSnackbar(logicException, showSnackbar);
        }

        private void TryToChangePageState(PageViewModel pageVm, BusinessLogicException logicException, bool changePageState)
        {
            PageStateType pageState;

            switch (logicException?.Type)
            {
                case LogicExceptionType.TooManyRequests:
                    pageState = PageStateType.TooManyRequests;
                    break;
                case LogicExceptionType.NoInternet:
                    pageState = PageStateType.NoInternet;
                    break;
                case LogicExceptionType.Timeout:
                    pageState = PageStateType.Timeout;
                    break;
                case LogicExceptionType.NoContent:
                case LogicExceptionType.NotFound:
                    pageState = PageStateType.NoData;
                    break;
                case LogicExceptionType.Unauthorized:
                    pageState = PageStateType.AppError;
                    break;
                default:
                    pageState = PageStateType.AppError;
                    break;
            }

            if (changePageState)
            {
                pageVm.State = pageState;
            }
        }

        //private void TryToShowSnackbar(LogicException logicException, bool showSnackbar)
        //{
        //    string message;

        //    switch (logicException?.Type)
        //    {
        //        case LogicExceptionType.BadRequest:
        //            message = logicException.Message;
        //            break;
        //        case LogicExceptionType.TooManyRequests:
        //            message = Messages.TooManyRequestsMessage;
        //            break;
        //        case LogicExceptionType.NoInternet:
        //            message = Messages.NoInternetRequestMessage;
        //            break;
        //        case LogicExceptionType.Timeout:
        //            message = Messages.TimeoutRequestMessage;
        //            break;
        //        case LogicExceptionType.NoContent:
        //        case LogicExceptionType.NotFound:
        //            message = Messages.NotFoundMessage;
        //            break;
        //        case LogicExceptionType.Unauthorized:
        //            message = Messages.UnauthorizedMessage;
        //            break;
        //        default:
        //            message = Messages.CommonErrorMessage;
        //            break;
        //    }

        //    if (showSnackbar)
        //    {
        //        _dialogService.ShowSnackbar(message);
        //    }
        //}
    }
}
