using BIF.SWE2.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB.Models
{
    class CameraModel : ICameraModel
    {
        public int ID { get; set; } = -1;
        public string Producer { get; set; }
        public string Make { get; set; }
        public DateTime? BoughtOn { get; set; }
        public string Notes { get; set; }
        public decimal ISOLimitGood { get; set; }
        public decimal ISOLimitAcceptable { get; set; }
    }
}
