using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Classes;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using PicDB.Annotations;

namespace PicDB.ViewModels
{
    class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public BusinessLayer Bl = new BusinessLayer(Constants.IsUnitTest);
        public MainWindowViewModel()
        {
            Bl.Sync();
        }

        private IPictureViewModel _currentPicture;
        public IPictureViewModel CurrentPicture
        {
            get => _currentPicture;
            set {
                if (_currentPicture == value) return;
                _currentPicture = value;
                OnPropertyChanged();
            }
        }

        public IPictureListViewModel List { get
            {
                return new PictureListViewModel(Bl.GetDirPicModels());
            }
        }

        public ISearchViewModel Search { get; } = new SearchViewModel();

    }

    //public class StringToImageConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (!(value is string uri)) return null;

    //        BitmapImage image = new BitmapImage();
    //        image.BeginInit();
    //        image.CacheOption = BitmapCacheOption.OnLoad;
    //        image.UriSource = new Uri(uri);
    //        image.EndInit();
    //        return image;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotSupportedException();
    //    }
    //}

}
