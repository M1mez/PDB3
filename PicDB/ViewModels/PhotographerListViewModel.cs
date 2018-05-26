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
    public class PhotographerListViewModel : IPhotographerListViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private IEnumerable<IPhotographerViewModel> _list;
        private IPhotographerViewModel _currentPhotographer;
        private ObservableCollection<IPhotographerViewModel> _obsList;

        public IEnumerable<IPhotographerViewModel> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
                OnPropertyChanged("ObsList");
            }
        }

        public ObservableCollection<IPhotographerViewModel> ObsList => new ObservableCollection<IPhotographerViewModel>(_list);
        public void Update(IEnumerable<IPhotographerModel> pList) => List = pList.Select(p => new PhotographerViewModel(p));
        

        public IPhotographerViewModel CurrentPhotographer
        {
            get => _currentPhotographer;
            set
            {
                _currentPhotographer = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentPhotographerBirthdayString");
            }
        }

        public string CurrentPhotographerBirthdayString
        {
            get => (CurrentPhotographer?.BirthDay != null) ? ((DateTime) CurrentPhotographer.BirthDay).ToShortDateString() : String.Empty;
            set => throw new NotImplementedException();
        }
    }
}