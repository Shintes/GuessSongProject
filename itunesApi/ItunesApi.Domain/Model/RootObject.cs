using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItunesApi.Domain.Model
{
    public record Rootobject
    {
        public Songs[] results { get; set; }
        public class Songs
        {
            public string kind { get; set; }
            public int trackId { get; set; }
            public string artistName { get; set; }
            public string trackName { get; set; }
            public string previewUrl { get; set; }
            public string artworkUrl100 { get; set; }
        }
    }
}
