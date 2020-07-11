using System;
using System.Collections.Generic;
using System.Text;

namespace HackTruda.DataModels.Responses
{
    public class NotificationResponse
    {
         public UserResponse User { get;set;}

         public string Notfication { get; set; }

         public DateTime Date { get; set; }
    }
}
