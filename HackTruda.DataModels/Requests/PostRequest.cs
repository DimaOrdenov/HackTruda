namespace HackTruda.DataModels.Requests
{
    public class PostRequest
    {
        public int PostId { get; set; }
        public byte[] Image { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}