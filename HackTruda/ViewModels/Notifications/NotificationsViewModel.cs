using System;
using System.Collections.Generic;
using HackTruda.Definitions.Models;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Notifications
{
    public class NotificationsViewModel : PageViewModel
    {
        private IEnumerable<NotificationGroupViewModel> _notificationsGroups;

        public NotificationsViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
            _notificationsGroups = new List<NotificationGroupViewModel>
            {
                new NotificationGroupViewModel("Сегодня")
                {
                    new NotificationItemViewModel(
                        new UserModel
                        {
                            FirstName = "Иван",
                            LastName = "Иванов",
                        },
                        "ответил на ваш комментарий: \"Класс! Мне бы так!\".",
                        "1 мин"),
                    new NotificationItemViewModel(
                        new UserModel
                        {
                            FirstName = "Мария",
                            LastName = "Иванова",
                        },
                        "ответила на ваш комментарий: \"Шикарно!!!\".",
                        "3 часа"),
                    new NotificationItemViewModel(
                        null,
                        "Объявления",
                        "опубликовали 3 новых поста.",
                        "5 часов"),
                },
                new NotificationGroupViewModel("На этой неделе")
                {
                    new NotificationItemViewModel(
                        new UserModel
                        {
                            FirstName = "Иван",
                            LastName = "Иванов",
                        },
                        "ответил на ваш комментарий: \"Может быт да, а может быть и нет...\".",
                        "Вс"),
                    new NotificationItemViewModel(
                        new UserModel
                        {
                            FirstName = "Мария",
                            LastName = "Иванова",
                        },
                        "поделилась вашим постом.",
                        "Сб"),
                    new NotificationItemViewModel(
                        null,
                        "Подкасты",
                        "добавили новый подкаст \"Праздники\".",
                        "Пт"),
                },
            };
        }

        public IEnumerable<NotificationGroupViewModel> NotificationsGroups
        {
            get => _notificationsGroups;
            set => SetProperty(ref _notificationsGroups, value);
        }
    }
}
