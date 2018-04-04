using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;

namespace PicDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private Gallery gal = new Gallery();

        //public BitmapImage SearchIconPath = new BitmapImage(new Uri(System.IO.Path.Combine(PersInfo.IcoPath, "ic_search_black_24dp_1x.png")));
        private Image _bigimg { get; set; }
        public Image BigImg
        {
            get { return _bigimg; }
            set
            {
                if (_bigimg != value)
                {
                    _bigimg = value;
                    Console.WriteLine("!!!!!BIGIMG: " + _bigimg);
                    //ChangedPic("BigImg");
                    OnPropertyChanged("BigImg");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Gallery();
            Gallery.NavigationService.Navigate(DataContext);

            //BigImg = new BitmapImage(new Uri(gal.SelectedPic.UriSource.ToString(), UriKind.Absolute));
            //_test[0] = gal.SelectedPic;

            //BigImage = gal.SelectedPic;

            Console.WriteLine("SECPIC: " + gal.SelectedPic);
            Console.WriteLine("_BIGIMG: " + _bigimg);
            Console.WriteLine("BIGIMG: " + BigImg);
        }

        private void OpenNewPhotographerWindow(object sender, RoutedEventArgs e)
        {
            Photographer pw = new Photographer
            {
                ShowInTaskbar = false,
                Owner = Application.Current.MainWindow
            };
            pw.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                Console.WriteLine("UPDATE IM MAINWINDOW!!!!" + propertyName);
            }
        }

        private void ChangedPic(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(new Gallery().SelectedPic, new PropertyChangedEventArgs(propertyName));
                Console.WriteLine("CHANGEDPIC!!!!!!");
            }
        }
    }
}
