using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB.ViewModels
{
    class IPTCViewModel : IIPTCViewModel
    {
        public IPTCViewModel()
        {
        }

        public IPTCViewModel(IIPTCModel mdl)
        {
            if (mdl == null) return;
            Keywords = mdl.Keywords;
            ByLine = mdl.ByLine;
            CopyrightNotice = mdl.CopyrightNotice;
            Headline = mdl.Headline;
            Caption = mdl.Caption;
            //Pic_ID = mdl.Pic_ID;
            //IPTC_ID = mdl.IPTC_ID;
        }

        public string Keywords { get; set; }
        public string ByLine { get; set; }
        public string CopyrightNotice { get; set; }

        public IEnumerable<string> CopyrightNotices { get; } = new List<string>() {
            "All rights reserved",
            "CC-BY",
            "CC-BY-SA",
            "CC-BY-ND",
            "CC-BY-NC",
            "CC-BY-NC-SA",
            "CC-BY-NC-ND"
        };

        public string Headline { get; set; }
        public string Caption { get; set; }
        public int Pic_ID { get; set; }
        public int IPTC_ID { get; set; }
    }
}
