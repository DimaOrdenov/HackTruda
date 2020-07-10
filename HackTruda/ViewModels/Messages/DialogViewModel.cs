using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using HackTruda.BusinessLogic.Interfaces;
using HackTruda.Definitions;
using HackTruda.Definitions.VmLink;
using HackTruda.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace HackTruda.ViewModels.Messages
{
    public class DialogViewModel : PageViewModel
    {
        private readonly HubConnection _hubConnection;

        private string _chatMessage;

        public ICommand SendMessageCommand { get; }
        private IPostsLogic _postsLogic { get; set; }

        public DialogViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IDebuggerService debuggerService,
            HubConnection hubConnection,
            IPostsLogic postsLogic)
            : base(navigationService, dialogService, debuggerService)
        {
            _hubConnection = hubConnection;
            _postsLogic = postsLogic;
            SendMessageCommand = BuildPageVmCommand<string>(
                message =>
                    ExceptionHandler.PerformCatchableTask(
                        new ViewModelPerformableAction(
                            async () =>
                            {
                                await _hubConnection.InvokeAsync("SendMessage", 999, message, CancellationToken);

                                AddMessage(new ChatMessageItemViewModel(0, 0, message));

                                ChatMessage = string.Empty;
                            })));

            NavigateBackCommand = BuildPageVmCommand(
                async () =>
                {
                    await CloseHubConnection();

                    await NavigationService.NavigateBackAsync();
                });

            _hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                AddMessage(new ChatMessageItemViewModel(999, 0, message));
            });

            _hubConnection.Closed += HubConnectionStateChanged;
            _hubConnection.Reconnected += HubConnectionReconnected; ;
            _hubConnection.Reconnecting += HubConnectionStateChanged;
        }

        public DialogItemViewModel DialogItem { get; private set; }

        public string ChatMessage
        {
            get => _chatMessage;
            set => SetProperty(ref _chatMessage, value);
        }

        public bool IsChatEntryVisible =>
            _hubConnection.State == HubConnectionState.Connected;

        public override async Task OnAppearing()
        {
            if (PageDidAppear)
            {
                return;
            }

           var p = await ExceptionHandler.PerformCatchableTask(
                new ViewModelPerformableAction(
                    async () => await _postsLogic.GetUser(new System.Threading.CancellationToken()))
                .IfChangePageState(false)
                .IfShowSnackbar(true)); ;

            //await ExceptionHandler.PerformCatchableTask(
            //    new ViewModelPerformableAction(
            //        async () => await _hubConnection.StartAsync(CancellationToken))
            //    .IfChangePageState(false)
            //    .IfShowSnackbar(true));

            OnPropertyChanged(nameof(IsChatEntryVisible));

            await base.OnAppearing();
        }

        public override bool OnBackButtonPressed()
        {
            Task.Run(() => CloseHubConnection());

            return base.OnBackButtonPressed();
        }

        public override void Prepare(object parameter)
        {
            base.Prepare(parameter);

            if (parameter is DialogVmLink vmLink)
            {
                DialogItem = vmLink.DialogItem;
            }
        }

        private void AddMessage(ChatMessageItemViewModel messageItemVm)
        {
            DialogItem.Messages.Add(messageItemVm);
        }

        private Task HubConnectionReconnected(string arg)
        {
            OnPropertyChanged(nameof(IsChatEntryVisible));

            return Task.CompletedTask;
        }

        private Task HubConnectionStateChanged(Exception arg)
        {
            OnPropertyChanged(nameof(IsChatEntryVisible));

            return Task.CompletedTask;
        }

        private Task CloseHubConnection() =>
            ExceptionHandler.PerformCatchableTask(
                new PerformableAction(
                    async () => await _hubConnection.StopAsync()));
    }
}
