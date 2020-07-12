using System;
using System.Collections.Generic;

namespace HackTruda.DataModels.Responses
{
    public class PostResponse
    {
        public int PostId { get; set; }
        public DateTime Date { get; set; }
        public byte[] Image { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public List<int> LikedUsersId { get; set; }
        public int LikedCount { get; set; } = new Random().Next(1, 1000);
        public int CommentsCount => new Random().Next(1, 200);
        public bool IsStory { get; set; }
    }
}