﻿using BIF.SWE2.Interfaces.Models;
using PicDB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicDB.Layers_DA;

namespace PicDB.Models
{
    public class PictureModel : IPictureModel
    {
       public PictureModel()
       { }

       public PictureModel(string fileName)
        {
            _fileName = fileName;
        }

        private string _fileName;
        public int ID { get; set; }
        public string FileName { get; set; }
        public IIPTCModel IPTC { get; set; }
        public IEXIFModel EXIF { get; set; }
        public ICameraModel Camera { get; set; }

        public int PG_ID { get; set; }
    }
}
