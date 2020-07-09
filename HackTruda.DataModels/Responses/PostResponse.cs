using System;

namespace HackTruda.DataModels.Responses
{
    public class PostResponse
    {
        public int PostId { get; set; }
        public DateTime Date { get; set; }
        public byte[] Image { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}