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
                        DateTime.Now.AddMinutes(-1)),
                    new NotificationItemViewModel(
                        new UserModel
                        {
                            FirstName = "Мария",
                            LastName = "Иванова",
                        },
                        "ответила на ваш комментарий: \"Шикарно!!!\".",
                        DateTime.Now.AddHours(-2)),
                    new NotificationItemViewModel(
                        null,
                        "Объявления",
                        "опубликовали 3 новых поста.",
                        DateTime.Now.AddHours(-2)),
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
                        DateTime.Now.AddDays(-2)),
                    new NotificationItemViewModel(
                        new UserModel
                        {
                            FirstName = "Мария",
                            LastName = "Иванова",
                        },
                        "поделилась вашим постом.",
                        DateTime.Now.AddDays(-4)),
                    new NotificationItemViewModel(
                        null,
                        "Подкасты",
                        "добавили новый подкаст \"Праздники\".",
                        DateTime.Now.AddDays(-5)),
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
