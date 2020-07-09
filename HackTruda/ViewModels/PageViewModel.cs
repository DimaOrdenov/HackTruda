using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FFImageLoading.Svg.Forms;
using HackTruda.Definitions.Enums;
using HackTruda.Services.Interfaces;
using Xamarin.Forms;

namespace HackTruda.ViewModels
{
    public abstract class PageViewModel : BaseViewModel
    {
        private bool _isLoading;
        private bool _isBusy;
        private bool _pageDidAppear;
        private PageStateType _state = PageStateType.None;
        private ICommand _pageStateCommand;
        private string _pageTitle;
        //private bool _hasNavigationBar = true;
        private bool _hasNavigationBarBackButton;
        private ObservableCollection<ToolbarItem> _toolbarItems;

        private readonly CancellationTokenSource _networkTokenSource = new CancellationTokenSource();

        public readonly INavigationService NavigationService;
        protected readonly IDialogService DialogService;
        protected readonly IDebuggerService DebuggerService;

        public ICommand NavigateBackCommand { get; protected set; }

        protected PageViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
            DebuggerService = debuggerService;

            NavigateBackCommand = BuildPageVmCommand(() => NavigationService.NavigateBackAsync());

            PageStateCommand = new Command(
                async () =>
                {
                    if (!PageStateCommand.CanExecute(null))
                    {
                        return;
                    }

                    PageDidAppear = false;

                    await OnAppearing();
                },
                CommandCanExecute);
        }

        ~PageViewModel()
        {
            Dispose(false);
        }

        public IExceptionHandler ExceptionHandler { get; set; }

        public PageType PageTypeValue { get; set; }

        public bool PageDidAppear
        {
            get => _pageDidAppear;
            set => SetProperty(ref _pageDidAppear, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                IsBusy = value;
                SetProperty(ref _isLoading, value);
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public PageStateType State
        {
            get => _state;
            set
            {
                SetProperty(ref _state, value);

                if (value == PageStateType.Loading || value == PageStateType.MinorLoading)
                {
                    IsLoading = true;
                    IsBusy = true;
                }
                else
                {
                    IsLoading = false;
                    IsBusy = false;
                }
            }
        }

        public ICommand PageStateCommand
        {
            get => _pageStateCommand;
            set => SetProperty(ref _pageStateCommand, value);
        }

        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        //public bool HasNavigationBar
        //{
        //    get => _hasNavigationBar;
        //    set
        //    {
        //        _hasNavigationBar = value;

        //        OnPropertyChanged();
        //    }
        //}

        public bool HasNavigationBarBackButton
        {
            get => _hasNavigationBarBackButton;
            set => SetProperty(ref _hasNavigationBarBackButton, value);
        }

        public ObservableCollection<ToolbarItem> ToolbarItems
        {
            get => _toolbarItems;
            set => SetProperty(ref _toolbarItems, value);
        }

        public CancellationToken CancellationToken => _networkTokenSource?.Token ?? CancellationToken.None;

        public void CancelNetworkRequests() =>
            _networkTokenSource?.Cancel();

        public virtual Task OnAppearing() =>
            Task.Run(() => PageDidAppear = true);

        public virtual Task OnDisappearing() =>
            Task.Run(() => { });

        public virtual bool OnBackButtonPressed()
        {
            NavigationService.NavigateBackAsync();

            return true;
        }

        public virtual async Task OnPopped()
        {
            await Dispose(false);

            await Task.Run(() => { });
        }

        public virtual Task OnPushed() =>
            Task.Run(() => { });

        protected bool CommandCanExecute() =>
            State != PageStateType.Loading && State != PageStateType.MinorLoading;

        protected virtual Task Dispose(bool disposing)
        {
            CancelNetworkRequests();

            GC.SuppressFinalize(this);

            return Task.Run(() => { });
        }

        protected ICommand BuildPageVmCommand(Func<Task> action)
        {
            ICommand command = null;

            if (action == null)
            {
                return command;
            }

            command = new Command(
                async () =>
                {
                    if (!command.CanExecute(null))
                    {
                        return;
                    }

                    State = PageStateType.MinorLoading;

                    await action();

                    State = PageStateType.Default;
                },
                CommandCanExecute);

            return command;
        }

        protected ICommand BuildPageVmCommand<T>(Func<T, Task> action)
        {
            ICommand command = null;

            if (action == null)
            {
                return command;
            }

            command = new Command<T>(
                async (item) =>
                {
                    if (!command.CanExecute(item))
                    {
                        return;
                    }

                    State = PageStateType.MinorLoading;

                    await action(item);

                    State = PageStateType.Default;
                },
                item => CommandCanExecute());

            return command;
        }
    }
}
