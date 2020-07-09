using System;
using System.Collections.Generic;

namespace HackTruda.API.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public DateTime Date { get; set; }
        public byte[] Image { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}