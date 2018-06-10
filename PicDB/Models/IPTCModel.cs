using BIF.SWE2.Interfaces.Models;
using PicDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// ReSharper disable InconsistentNaming

namespace PicDB.Models
{
    public class IPTCModel : IIPTCModel
    {
        public IPTCModel()
        { }

        /*public IPTCModel(IPTCViewModel vmdl)
        {
            if (vmdl == null) return;
            Keywords = vmdl.Keywords;
            ByLine = vmdl.ByLine;
            CopyrightNotice = vmdl.CopyrightNotice;
            Headline = vmdl.Headline;
            Caption = vmdl.Caption;
            Pic_ID = vmdl.Pic_ID;
            IPTC_ID = vmdl.IPTC_ID;
        }*/

        public string Keywords { get; set; } = "N/A";
        public string ByLine { get; set; } = "N/A";
        public string CopyrightNotice { get; set; } = "N/A";
        public string Headline { get; set; } = "N/A";
        public string Caption { get; set; } = "N/A";
        //additional variables
        public int Pic_ID { get; set; }
        public int IPTC_ID { get; set; }
    }
}
