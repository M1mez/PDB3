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
    class MainWindowViewModel : IMainWindowViewModel
    {
        public MainWindowViewModel()
        {
        }

        public IPictureViewModel CurrentPicture { get; }

        public IPictureListViewModel List { get
            {
                var mdlList = Directory.GetFiles(PersInfo.PicPath, "*.jpg")
                .Select(filePath => new PictureModel()
                {
                    FileName = Path.GetFileNameWithoutExtension(filePath)
                });

                return new PictureListViewModel(mdlList);
            }
        }

        public ISearchViewModel Search { get; } = new SearchViewModel();
    }
}
