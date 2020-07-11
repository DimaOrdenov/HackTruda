using System;
using System.Collections.Generic;
using System.Text;

namespace HackTruda.DataModels.Responses
{
    public class GamesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string Genre { get; set; }

        public string Link { get; set; }
    }
}
