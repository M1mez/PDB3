using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB.Models
{
    class EXIFModel : IEXIFModel
    {
        public string Make { get; set; }
        public decimal FNumber { get; set; }
        public decimal ExposureTime { get; set; }
        public decimal ISOValue { get; set; }
        public bool Flash { get; set; }
        public ExposurePrograms ExposureProgram { get; set; }
        //additional variables
        public int Pic_ID { get; set; }
        public int EXIF_ID { get; set; }
    }
}
