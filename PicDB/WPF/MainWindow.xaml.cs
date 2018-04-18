using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using PicDB.ViewModels;
using PicDB.Classes;
using PicDB.Models;

namespace PicDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        MainWindowViewModel mwvmdl = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            
            this.DataContext = mwvmdl;
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


        private void WriteIPTC(object sender, RoutedEventArgs e)
        {
            var im = new IPTCModel((IPTCViewModel)mwvmdl.CurrentPicture.IPTC);
            im.Pic_ID = mwvmdl.CurrentPicture.ID;
            mwvmdl.Bl.WriteIPTC(mwvmdl.CurrentPicture.FileName, im);

        }
    }
}
