using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB.ViewModels
{
    class SearchViewModel : ISearchViewModel
    {
        public string SearchText {
            get {
                return _searchtext;
            }
            set {
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
