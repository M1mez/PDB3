using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

        public IPictureViewModel CurrentPicture { get; set; }

        private IEnumerable<IPictureViewModel> _list;
        public IEnumerable<IPictureViewModel> List {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<IPictureModel> ModelList {
            set
            {
                List = value.Select(mdl => new PictureViewModel(mdl)).Cast<IPictureViewModel>().ToList();
            }
        }

        public IEnumerable<IPictureViewModel> PrevPictures { get; }

        public IEnumerable<IPictureViewModel> NextPictures { get; }

        public int Count { get; }

        public int CurrentIndex { get; }

        public string CurrentPictureAsString { get; }
    }
}
