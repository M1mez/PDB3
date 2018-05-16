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
        
        //ctor
        public PictureViewModel() { }
        public PictureViewModel(IPictureModel mdl)
        {
            PictureModel = mdl ?? new PictureModel();
        }

        //model
        public readonly IPictureModel PictureModel;
        public int ID => PictureModel.ID;
        public string FileName => PictureModel.FileName;
        public string FilePath => Path.Combine(Constants.PicPath, FileName);
        public string DisplayName => $"{FileName} (by {IPTC?.ByLine})";
        public IIPTCViewModel IPTC => new IPTCViewModel(PictureModel.IPTC);
        public IEXIFViewModel EXIF => new EXIFViewModel(PictureModel.EXIF);
        private IPhotographerViewModel _photographer;
        public IPhotographerViewModel Photographer
        {
            get => _photographer;
            set
            {
                if (_photographer == value) return;
                _photographer = value;
                OnPropertyChanged();
            }
        }

        public ICameraViewModel Camera => new CameraViewModel(PictureModel.Camera);
    }
}
