using System;
using System.Collections.Generic;
using System.Text;

namespace HackTruda.DataModels.Requests
{
    public class NotificationRequest
    {
        public int NotificationId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int SenderId { get; set; }
        public int Recipient { get; set; }
    }
}
