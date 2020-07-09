using System.Collections.Generic;

namespace HackTruda.API.Models
{
    public class User
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

        /// <summary>
        /// посты пользователя
        /// </summary>
        public List<Post> Posts { get; set; }
    }
}