using System;
using System.Collections.Generic;
using System.Text;

namespace HackTruda.DataModels.Responses
{
    public class FeedResponse : PostResponse
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public byte[] UserImage { get; set; }

        
    }
}
