using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackTruda.API.Models
{
    public class Games
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string Genre { get; set; }

        public string Link { get; set; }
    }
}
