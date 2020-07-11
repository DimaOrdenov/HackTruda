using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackTruda.API.Models
{
    public class Posts
    {
        public int PostsId { get; set; }
        public DateTime Date { get; set; }
        public byte[] Image { get; set; }
        public string Content { get; set; }
        public int AuthUserId { get; set; }
        public AuthUser User { get; set; }
    }
}
