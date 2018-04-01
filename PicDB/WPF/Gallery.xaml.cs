using PicDB.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PicDB
{
    /// <summary>
    /// Interaktionslogik für Gallery.xaml
    /// </summary>
    public partial class Gallery : Page
    {
        private ObservableCollection<BitmapImage> _pics = new ObservableCollection<BitmapImage>();
        public ObservableCollection<BitmapImage> Pics
        {
            get { return _pics; }
            set { _pics = value; }
        }

        public Gallery()
        {
            InitializeComponent();

            var mwvmdl = new MainWindowViewModel();

            foreach (var pic in mwvmdl.List.List)
            {
                Uri uri = new Uri(Constants.PicPath + @"\" + pic.FileName + ".jpg");
                BitmapImage img = new BitmapImage(uri);
                Pics.Add(img);
                //GalleryRow.Items.Add(new BitmapImage(new Uri(PersInfo.PicPath + @"\" + pic.FileName + ".jpg")));
            }
        }
    }
}
