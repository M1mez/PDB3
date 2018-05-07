using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Layers_DA;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PicDB.Annotations;

namespace PicDB.ViewModels
{
    class PictureViewModel : IPictureViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public DataAccessLayer _dal = new DataAccessLayer();
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
                DisplayName = String.Format("{0} (by {1})", FileName, IPTC?.ByLine);
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
