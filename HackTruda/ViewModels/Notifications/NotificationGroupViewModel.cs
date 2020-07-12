using System.Collections.Generic;

namespace HackTruda.ViewModels.Notifications
{
    /// <summary>
    /// VM для группы уведомлений.
    /// </summary>
    public class NotificationGroupViewModel : List<NotificationItemViewModel>
    {
        public NotificationGroupViewModel(string header)
        {
            Header = header;
        }

        public string Header { get; }
    }
}
