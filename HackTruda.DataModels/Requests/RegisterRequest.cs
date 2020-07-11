using System;
using System.Collections.Generic;
using System.Text;

namespace HackTruda.DataModels.Requests
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Phone {get;set;}
        public string Password { get; set; }
    }
}
