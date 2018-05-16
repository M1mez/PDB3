using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.ViewModels;
using PicDB.Classes;
using PicDB.Models;
using System.Collections.Generic;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mwvmdl = new MainWindowViewModel();
        public PhotographerListViewModel PhotographerList;
        private BusinessLayer BL;

        public MainWindow()
        {
            BL = mwvmdl.Bl;
            UpdatePictureList(BL.GetPictures());
            this.DataContext = mwvmdl;
            ActualizePhotographerList();
            Resources.Add("NamePart", NamePart);

            InitializeComponent();
        }

        private void OpenNewPhotographerWindow(object sender, RoutedEventArgs e)
        {
            PhotographerWindow pw = new PhotographerWindow(this, BL);
            /*{
                ShowInTaskbar = false,
                Owner = Application.Current.MainWindow
            };*/
            pw.Show();
        }

        private void OpenNewSearchWindow(object sender, RoutedEventArgs e)
        {
            AdvancedSearchWindow sw = new AdvancedSearchWindow(this, BL);
            sw.Show();
        }
        
        public string NamePart
        {
            get => (string)GetValue(NamePartProperty);
            set => SetValue(NamePartProperty, value);
        }
        public static readonly DependencyProperty NamePartProperty = DependencyProperty
            .Register("NamePart", typeof(string), typeof(MainWindow), new PropertyMetadata(""));


        private void SimpleSearch(object sender, RoutedEventArgs e)
        {
            var simpleSearchedList = BL.GetPictures(NamePart, null, null, null);
            UpdatePictureList(simpleSearchedList);
        }

        public void ActualizePhotographerList()
        {
            PhotographerList = new PhotographerListViewModel(BL.GetPhotographers());
        }

        private void WriteIPTC(object sender, RoutedEventArgs e)
        {
            var iptcVm = (IPTCViewModel) mwvmdl.CurrentPicture.IPTC;
            mwvmdl.Bl.WriteIPTC(mwvmdl.CurrentPicture.FileName, iptcVm.IPTCModel);
        }

        public void UpdatePictureList(IEnumerable<IPictureModel> newList)
        {
            mwvmdl.List = new PictureListViewModel(newList);
            foreach (var pictureModel in newList)
            {
                Console.WriteLine(pictureModel.FileName);
            }
        }

        private void Quit(object sender, RoutedEventArgs e) { Close(); }
    }
}
