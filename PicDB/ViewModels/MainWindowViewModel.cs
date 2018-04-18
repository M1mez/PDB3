using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Classes;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PicDB.ViewModels
{
    class MainWindowViewModel : ViewModel, IMainWindowViewModel
    {
        public BusinessLayer Bl = new BusinessLayer();
        public MainWindowViewModel()
        {
            Bl.Sync();
        }

        private IPictureViewModel _currentPicture;
        public IPictureViewModel CurrentPicture
        {
            get { return _currentPicture; }
            set { if(_currentPicture != value)
                {
                    _currentPicture = value;
                    OnPropertyChanged("CurrentPicture");
                }
            }
        }

        public IPictureListViewModel List { get
            {
                return new PictureListViewModel(Bl.GetDirPicModels());
            }
        }

        public ISearchViewModel Search { get; } = new SearchViewModel();
    }
}
