using System;
namespace HackTruda.Definitions.Enums
{
    public enum PageStateType
    {
        None,

        /// <summary>
        /// Default, normal page state.
        /// </summary>
        Default,

        /// <summary>
        /// Loading page state. Should be used to hide page content and show Loader.
        /// </summary>
        Loading,

        /// <summary>
        /// Loading page state. Should be used to perform loading above page content.
        /// </summary>
        MinorLoading,

        /// <summary>
        /// Any app error, by default snackbar will be showed.
        /// </summary>
        AppError,

        /// <summary>
        /// No internet page state.
        /// </summary>
        NoInternet,

        /// <summary>
        /// Request timeout page state.
        /// </summary>
        Timeout,

        /// <summary>
        /// No data page state.
        /// </summary>
        NoData,

        /// <summary>
        /// Too many requests page state.
        /// </summary>
        TooManyRequests,
    }
}
