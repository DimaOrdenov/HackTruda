using System;
using System.Collections.Generic;
using System.Text;

namespace HackTruda.DataModels.Requests
{
    public class LoginRequest
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
