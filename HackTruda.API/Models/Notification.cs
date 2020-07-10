using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackTruda.API.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int RecipientId { get; set; }
        public User Recipient { get; set; }
    }
}
