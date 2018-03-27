using BIF.SWE2.Interfaces.Models;
using PicDB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB.Models
{
    class PictureModel : IPictureModel
    {
        public PictureModel()
        {
            ID = DAL_Conn.GetNextId("Pictures");
            Console.WriteLine("PictureModel ID: " + ID);
        }

        public int ID { get; set; }
        public string FileName { get; set; }
        public IIPTCModel IPTC { get; set; }
        public IEXIFModel EXIF { get; set; }
        public ICameraModel Camera { get; set; }
    }
}
