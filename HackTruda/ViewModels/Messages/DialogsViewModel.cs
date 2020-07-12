using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HackTruda.BusinessLogic.Interfaces;
using HackTruda.Definitions;
using HackTruda.Definitions.Enums;
using HackTruda.Definitions.VmLink;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Messages
{
    /// <summary>
    /// VM для диалогов
    /// </summary>
    public class DialogsViewModel : PageViewModel
    {
        private readonly IUsersLogic _usersLogic;

        private ObservableCollection<DialogItemViewModel> _dialogs;
        private ICommand _dialogTapCommand;

        public ICommand CreateDialogCommand { get; }

        public DialogsViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService, IUsersLogic usersLogic)
            : base(navigationService, dialogService, debuggerService)
        {
            _usersLogic = usersLogic;

            _dialogTapCommand = BuildPageVmCommand<DialogItemViewModel>((item) =>
               NavigationService.NavigateAsync(PageType.DialogPage, new DialogVmLink(item)));

            CreateDialogCommand = BuildPageVmCommand(() => DialogService.DisplayAlert(null, "Создаю новый диалог", "Ок"));
        }

        public ObservableCollection<DialogItemViewModel> Dialogs
        {
            get => _dialogs;
            set => SetProperty(ref _dialogs, value);
        }

        public override async Task OnAppearing()
        {
            if (PageDidAppear)
            {
                return;
            }

            State = PageStateType.Loading;

            await ExceptionHandler.PerformCatchableTask(
                new ViewModelPerformableAction(
                    async () =>
                    {
                        List<DialogItemViewModel> dialogs =
                            (await _usersLogic.Get(CancellationToken))
                                .Select((x, i) =>
                                {
                                    DialogItemViewModel dialogItem = new DialogItemViewModel(x)
                                    {
                                        TapCommand = _dialogTapCommand,
                                    };

                                    if (i % 3 == 0)
                                    {
                                        dialogItem.Messages = new ObservableCollection<ChatMessageItemViewModel>
                                        {
                                            new ChatMessageItemViewModel(x.UserId, 0, "Привет")
                                            {
                                                IsNew = true,
                                            },
                                            new ChatMessageItemViewModel(x.UserId, 0, "Как дела")
                                            {
                                                IsNew = true,
                                            },
                                            new ChatMessageItemViewModel(x.UserId, 0, "Что делаешь")
                                            {
                                                IsNew = true,
                                            },
                                            new ChatMessageItemViewModel(x.UserId, 0, "Длинное сообщение Длинное сообщение Длинное сообщение Длинное сообщение Длинное сообщение Длинное сообщение Длинное сообщение ")
                                            {
                                                IsNew = true,
                                            },
                                        };
                                    }
                                    else if (i % 4 == 0)
                                    {
                                        dialogItem.Messages = new ObservableCollection<ChatMessageItemViewModel>
                                        {
                                            new ChatMessageItemViewModel(1, 0, "Привет"),
                                            new ChatMessageItemViewModel(1, 0, "Ой всё, пока!"),
                                        };
                                    }

                                    return dialogItem;
                                })
                                .ToList();

                        Dialogs = new ObservableCollection<DialogItemViewModel>(dialogs);
                    }));

            State = PageStateType.Default;

            await base.OnAppearing();
        }
    }
}
