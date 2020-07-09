namespace HackTruda.DataModels.Responses
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public byte[] Image { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        /// <summary>
        /// место жительства
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// родная страна
        /// </summary>
        public string Country { get; set; }
    }
}