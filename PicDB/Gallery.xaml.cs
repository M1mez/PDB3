using PicDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicDB
{
    /// <summary>
    /// Interaktionslogik für Gallery.xaml
    /// </summary>
    public partial class Gallery : UserControl, INotifyPropertyChanged
    {
        public ImageSource SearchIconPath = new BitmapImage(new Uri(System.IO.Path.Combine(Constants.IcoPath, "ic_search_black_24dp_1x.png")));

        private ObservableCollection<BitmapImage> _pics = new ObservableCollection<BitmapImage>();
        public ObservableCollection<BitmapImage> Pics
        {
            get { return _pics; }
            set
            {
                if (_pics != value)
                {
                    _pics = value;
                    OnPropertyChanged("Pics");
                }
            }
        }

        private object _selectedPic { get; set; }
        public object SelectedPic
        {
            get { return (object)GetValue(MySelectedItemProperty); }
            set
            {
                SetValue(MySelectedItemProperty, value);
                OnPropertyChanged("SelectedPic");
            }
        }

        public static readonly DependencyProperty MySelectedItemProperty =
            DependencyProperty.Register("SelectedPic", typeof(object), typeof(BitmapImage), new UIPropertyMetadata(null));

        public Gallery()
        {
            InitializeComponent();
            var mwvmdl = new MainWindowViewModel();
            foreach (var pic in mwvmdl.List.List)
            {
                BitmapImage temppic = new BitmapImage(new Uri(Constants.PicPath + @"\" + pic.FileName + ".jpg"));
                _pics.Add(temppic);
                Pics = _pics;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                Console.WriteLine("DA WAR WAS!!!!" + propertyName);
            }
        }
    }
}
