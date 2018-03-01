using BIF.SWE2.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB.Models
{
    class IPTCModel : IIPTCModel
    {
        public string Keywords { get; set; } = "N/A";
        public string ByLine { get; set; } = "N/A";
        public string CopyrightNotice { get; set; } = "N/A";
        public string Headline { get; set; } = "N/A";
        public string Caption { get; set; } = "N/A";
    }
}
