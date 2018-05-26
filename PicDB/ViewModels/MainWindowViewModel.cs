using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Classes;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using BIF.SWE2.Interfaces.Models;
using PicDB.Annotations;

namespace PicDB.ViewModels
{
    class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public BusinessLayer Bl = new BusinessLayer(Constants.IsUnitTest);
        public MainWindowViewModel()
        {
            
        }

        private IPictureViewModel _currentPicture;
        public IPictureViewModel CurrentPicture
        {
            get => _currentPicture;
            set {
                //if (_currentPicture == value) return;
                _currentPicture = value;
                OnPropertyChanged();
            }
        }

        private IPictureListViewModel _list = null;
        public IPictureListViewModel List
        {
            get => _list ?? (_list = new PictureListViewModel(Bl.GetDirPicModels()));
            set
            {
                _list = value;
                if (_list.Count > 0) CurrentPicture = _list.List.First();
                OnPropertyChanged();
                OnPropertyChanged("_list");
            }
        }

        public ISearchViewModel Search { get; } = new SearchViewModel();
    }
}
