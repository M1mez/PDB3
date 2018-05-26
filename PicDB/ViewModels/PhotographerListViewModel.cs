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
                /*if (_list != null)
                {
                    //IEnumerable<IPhotographerViewModel> toRemove = _list.Except(value);
                    IEnumerable<IPhotographerViewModel> toAdd = value.ToList().Where(phnew =>
                        _list.All(phold => phnew.ID != phold.ID));
                    IEnumerable<IPhotographerViewModel> toRemove = _list.ToList().Where(phleft =>
                        value.All(phold => phleft.ID != phold.ID));
                    Console.WriteLine($"toaddcount: {toAdd.Count()}");
                    Console.WriteLine($"toremovecount: {toRemove.Count()}");

                    foreach (var ph in toRemove) ObsList.Remove(ph);
                    foreach (var ph in toAdd) ObsList.Add(ph);
                    //var newObs = _list.Union()

                }*/

                _list = value;
                OnPropertyChanged();
                OnPropertyChanged("ObsList");
            }
        }

        public ObservableCollection<IPhotographerViewModel> ObsList => new ObservableCollection<IPhotographerViewModel>(_list);

        public PhotographerListViewModel() { }

        public void Update(IEnumerable<IPhotographerModel> pList)
        {
            List = pList.Select(p => new PhotographerViewModel(p));
        }

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