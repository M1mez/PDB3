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
            get
            {
                return _searchtext;
            }

            set
            {
                _searchtext = value;
                _isActive = !String.IsNullOrWhiteSpace(_searchtext);
            }
        }
        private string _searchtext = "";
        private bool _isActive = false;

        public bool IsActive => _isActive;

        public int ResultCount { get; }
    }
}
