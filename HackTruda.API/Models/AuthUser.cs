using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackTruda.API.Models
{
    public class AuthUser : IdentityUser
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
        public List<Posts> Posts { get; set; }
    }
}
