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
    public class PhotographerListViewModel : IPhotographerListViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private IEnumerable<IPhotographerViewModel> _list;
        public IEnumerable<IPhotographerViewModel> List {
            get => _list;
            set
            {
                //if (_list == value) return;
                _list = value;
                OnPropertyChanged();
            }
        }

        public PhotographerListViewModel() { }
        public PhotographerListViewModel(IEnumerable<IPhotographerModel> pList)
        {
            List = pList.Select(p => new PhotographerViewModel(p));
        }

        public IPhotographerViewModel CurrentPhotographer { get; }
    }
}
