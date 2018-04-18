using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Classes;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

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

    public class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            string uri = value as string;

            if (uri != null)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(uri);
                image.EndInit();
                result = image;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
