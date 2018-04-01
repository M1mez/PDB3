using PicDB.Classes;
using PicDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //TODO: darf das da stehen?
            //var mwvmdl = new MainWindowViewModel();

            //foreach(var pic in mwvmdl.List.List)
            //{
            //    Gallery.Items.Add(new BitmapImage(new Uri(PersInfo.PicPath + @"\" + pic.FileName + ".jpg")));
            //}
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

        //private string _searchIconPath = "";
        public string SearchIconPath {
            get {
                return System.IO.Path.Combine(Constants.IcoPath, "ic_search_black_24dp_1x.png");
            }
        }
    }
}
