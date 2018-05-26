using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BIF.SWE2.Interfaces.Models;
using PicDB.Annotations;

namespace PicDB.ViewModels
{
    public class CameraListViewModel : ICameraListViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private IEnumerable<ICameraViewModel> _list;
        private ICameraViewModel _currentCamera;
        private ObservableCollection<ICameraViewModel> _obsList;

        public IEnumerable<ICameraViewModel> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
                OnPropertyChanged("ObsList");
            }
        }

        public ObservableCollection<ICameraViewModel> ObsList => new ObservableCollection<ICameraViewModel>(_list);
        public void Update(IEnumerable<ICameraModel> pList) => List = pList.Select(p => new CameraViewModel(p));
        
        //ctor
        public CameraListViewModel() { }
        public CameraListViewModel(IEnumerable<ICameraModel> cList)
        {
            List = cList.Select(c => new CameraViewModel(c));
        }
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
