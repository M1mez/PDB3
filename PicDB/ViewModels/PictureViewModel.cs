using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PicDB.ViewModels
{
    class PictureViewModel : IPictureViewModel
    {
        public PictureViewModel()
        {
        }

        public PictureViewModel(IPictureModel mdl) {
            if (mdl != null)
            {
                ID = mdl.ID;
                FileName = mdl.FileName;
                FilePath = Path.Combine(Constants.PicPath, mdl.FileName);
                IPTC = new IPTCViewModel(mdl.IPTC);
                EXIF = new EXIFViewModel(mdl.EXIF);
                Camera = new CameraViewModel(mdl.Camera);
                DisplayName = String.Format("{0} (by {1})", FileName, IPTC.ByLine);
            }
        }

        public int ID { get; }

        public string FileName { get; }

        public string FilePath { get; }

        public string DisplayName { get; }

        public IIPTCViewModel IPTC { get; }

        public IEXIFViewModel EXIF { get; }

        public IPhotographerViewModel Photographer { get; }

        public ICameraViewModel Camera { get; }
    }
}
