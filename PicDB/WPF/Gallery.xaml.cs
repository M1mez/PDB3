using PicDB.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Data;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PicDB
{
    /// <summary>
    /// Interaktionslogik für Gallery.xaml
    /// </summary>
    public partial class Gallery : Page, INotifyPropertyChanged
    {
        
        private ObservableCollection<Image> _pics = new ObservableCollection<Image>();
        public ObservableCollection<Image> Pics
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

        private ImageSource _selectedPic { get; set; }
        public ImageSource SelectedPic
        {
            get { return _selectedPic; }
            set
            {
                if(_selectedPic != value)
                {
                    _selectedPic = value;
                    Console.WriteLine("!!!!SELECTEDPIC: " +_selectedPic);
                    OnPropertyChanged("SelectedPic");
                }
            }
        }

        public Gallery()
        {
            InitializeComponent();

            var mwvmdl = new MainWindowViewModel();
            this.DataContext = this;


            foreach (var pic in mwvmdl.List.List)
            {
                Image finalImg = new Image();
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(Constants.PicPath + @"\" + pic.FileName + ".jpg");
                img.EndInit();
                finalImg.Source = img;
                _pics.Add(finalImg);
                Pics = _pics;

                Console.WriteLine("BITMAPIMAGE:" + img);
                Console.WriteLine("IMAGE" + finalImg);
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

     public class ConvertBitmapToBitmapImage
    {
        /// <summary>
        /// Takes a bitmap and converts it to an image that can be handled by WPF ImageBrush
        /// </summary>
        /// <param name="src">A bitmap image</param>
        /// <returns>The image as a BitmapImage for WPF</returns>
        //public BitmapImage Convert(Bitmap src)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        //    BitmapImage image = new BitmapImage();
        //    image.BeginInit();
        //    ms.Seek(0, SeekOrigin.Begin);
        //    image.StreamSource = ms;
        //    image.EndInit();
        //    return image;
        //}
    }
}
