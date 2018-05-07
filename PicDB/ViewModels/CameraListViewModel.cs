using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BIF.SWE2.Interfaces.Models;
using PicDB.Annotations;

namespace PicDB.ViewModels
{
    class CameraListViewModel : ICameraListViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //ctor
        public CameraListViewModel() { }
        public CameraListViewModel(IEnumerable<ICameraModel> cList)
        {
            List = cList.Select(c => new CameraViewModel(c));
        }

        public IEnumerable<ICameraViewModel> List { get; }

        private ICameraViewModel _currentCamera;
        public ICameraViewModel CurrentCamera
        {
            get => _currentCamera;
            set
            {
                if (_currentCamera == value) return;
                _currentCamera = value;
                OnPropertyChanged();
            }
        }
    }
}
