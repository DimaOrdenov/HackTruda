using System;
using System.Buffers.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HackTruda.Definitions.Enums;
using HackTruda.Definitions.Models;
using HackTruda.Definitions.VmLink;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Messages
{
    public class DialogsViewModel : PageViewModel
    {
        private ICommand _dialogTapCommand;

        public ICommand CreateDialogCommand { get; }

        public DialogsViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
            _dialogTapCommand = BuildPageVmCommand<DialogItemViewModel>((item) =>
               NavigationService.NavigateAsync(PageType.DialogPage, new DialogVmLink(item)));

            CreateDialogCommand = BuildPageVmCommand(() => DialogService.DisplayAlert(null, "Создаю новый диалог", "Ок"));

            Dialogs = new ObservableCollection<DialogItemViewModel>
            {
                new DialogItemViewModel(new UserModel
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    UserId = 1,
                })
                {
                    TapCommand = _dialogTapCommand,
                    Messages = new ObservableCollection<ChatMessageItemViewModel>
                    {
                        new ChatMessageItemViewModel(1, 0, "Привет")
                        {
                            IsNew = true,
                        },
                        new ChatMessageItemViewModel(1, 0, "Как дела")
                        {
                            IsNew = true,
                        },
                        new ChatMessageItemViewModel(1, 0, "Что делаешь")
                        {
                            IsNew = true,
                        },
                        new ChatMessageItemViewModel(1, 0, "Длинное сообщение Длинное сообщение Длинное сообщение Длинное сообщение Длинное сообщение Длинное сообщение Длинное сообщение ")
                        {
                            IsNew = true,
                        },
                    },
                },
                new DialogItemViewModel(new UserModel
                {
                    FirstName = "Иван",
                    LastName = "Иванов 2",
                    UserId = 2,
                })
                {
                    TapCommand = _dialogTapCommand,
                },
                new DialogItemViewModel(new UserModel
                {
                    FirstName = "Иван",
                    LastName = "Иванов 3",
                    UserId = 3,
                })
                {
                    TapCommand = _dialogTapCommand,
                    Messages = new ObservableCollection<ChatMessageItemViewModel>
                    {
                        new ChatMessageItemViewModel(1, 0, "Привет"),
                        new ChatMessageItemViewModel(1, 0, "Ой всё, пока!"),
                    },
                },
                new DialogItemViewModel(new UserModel
                {
                    FirstName = "Иван",
                    LastName = "Иванов 4",
                    UserId = 4,
                })
                {
                    TapCommand = _dialogTapCommand,
                },
            };
        }

        private ObservableCollection<DialogItemViewModel> _dialogs;

        public ObservableCollection<DialogItemViewModel> Dialogs
        {
            get => _dialogs;
            set => SetProperty(ref _dialogs, value);
        }
    }
}
