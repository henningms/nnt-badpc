using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NRKNettTvBadPc.Models
{
    public class PlaylistItem
    {
        public long Bandwidth { get; set; }
        public string Resolution { get; set; }
        public string Codecs { get; set; }
        public string URL { get; set; }
    }
}
