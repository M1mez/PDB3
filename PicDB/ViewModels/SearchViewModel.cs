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
    class SearchViewModel : ISearchViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string SearchText
        {
            get => _searchtext;

            set
            {
                _searchtext = value;
                IsActive = !String.IsNullOrWhiteSpace(_searchtext);
            }
        }
        private string _searchtext = "";

        public bool IsActive { get; private set; } = false;

        public int ResultCount { get; }
    }
}
