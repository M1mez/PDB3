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
        public MainWindowViewModel()
        {
        }

        private IPictureViewModel _currentPicture;
        public IPictureViewModel CurrentPicture
        {
            get { return _currentPicture; }
            set { if(_currentPicture != value)
                {
                    _currentPicture = value;
                    OnPropertyChanged("CurrentPicture");
                    Console.WriteLine("CURRENTPIC: " + value);
                }
            }
        }

        public IPictureListViewModel List { get
            {
                var mdlList = Directory.GetFiles(Constants.PicPath, "*.jpg")
                .Select(FilePath => new PictureModel()
                {
                    FileName = Path.GetFileNameWithoutExtension(FilePath)
                });

                return new PictureListViewModel(mdlList);
            }
        }

        public ISearchViewModel Search { get; } = new SearchViewModel();
    }
}
