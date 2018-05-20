using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media.Imaging;
using PicDB.Annotations;

namespace PicDB.ViewModels
{
    public class PictureListViewModel : IPictureListViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //ctors
        public PictureListViewModel() { }
        public PictureListViewModel(IEnumerable<IPictureModel> mdlList)
        {
            var newList = mdlList.Select(mdl => new PictureViewModel(mdl)).Cast<IPictureViewModel>().ToList();
            List = newList;
        }

        public IPictureViewModel CurrentPicture => List.ElementAt(CurrentIndex);

        private IEnumerable<IPictureViewModel> _list;

        public IEnumerable<IPictureViewModel> List {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }

        public List<BitmapImage> BitmapList =>
            List.ToList().Select(pic => FileInformation.LoadBitmapImage(pic.FilePath)).ToList();
            
        public IEnumerable<IPictureViewModel> PrevPictures { get; }

        public IEnumerable<IPictureViewModel> NextPictures { get; }

        public int Count => List.Count();

        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                OnPropertyChanged();
            }
        }

        public string CurrentPictureAsString { get; }
    }
}
