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
using System.Linq;
using BIF.SWE2.Interfaces.Models;
using System.IO;
using System.Windows.Data;
using Helper;

namespace PicDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mwvmdl = new MainWindowViewModel();
        public PhotographerListViewModel PhotographerList { get; set; } = new PhotographerListViewModel();
        private BusinessLayer BL;

        public MainWindow()
        {
            BL = mwvmdl.Bl;
            UpdatePhotographerList();
            UpdatePictureList(BL.GetPictures());
            
            Resources.Add("NamePart", NamePart);
            this.DataContext = mwvmdl;

            InitializeComponent();
        }

        private void OpenNewPhotographerWindow(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem) sender;
            Window photographerWindow = null;
            if (menuItem.Name == "tabItem_AddPhotographer") photographerWindow = new PhotographerWindow_Add(this, BL);
            else if (menuItem.Name == "tabItem_EditPhotographer")
            {
                if (PhotographerList.List.Count() == 0)
                    MessageBox.Show("No Photographer available to edit!", "Edit Photographer", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                else photographerWindow = new PhotographerWindow_Edit(this, BL);
            }
            photographerWindow?.ShowDialog();
        }

        private void OpenNewSearchWindow(object sender, RoutedEventArgs e)
        {
            AdvancedSearchWindow sw = new AdvancedSearchWindow(this, BL) { Owner = Application.Current.MainWindow };
            sw.ShowDialog();
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


        public void UpdatePhotographerList()
        {
            PhotographerList.Update(BL.GetPhotographers());
        }

        private void AssignPictureToPhotographer(object sender, RoutedEventArgs e)
        {
            try
            {
                int currentPicID = mwvmdl.CurrentPicture.ID;
                BL.AssignPictureToPhotographer(currentPicID, PhotographerList.CurrentPhotographer.ID);

                ((PictureViewModel) mwvmdl.CurrentPicture).Photographer = PhotographerList.CurrentPhotographer;
                int index = mwvmdl.List.List.ToList().FindIndex(pic => pic.ID == currentPicID);
                ((PictureViewModel) mwvmdl.List.List.ElementAt(index)).Photographer = PhotographerList.CurrentPhotographer;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "Save IPTC", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WriteIPTC(object sender, RoutedEventArgs e)
        {
            try
            {
                var iptcVm = (IPTCViewModel)mwvmdl.CurrentPicture.IPTC;
                mwvmdl.Bl.WriteIPTC(mwvmdl.CurrentPicture.FileName, iptcVm.IPTCModel);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "Save IPTC", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdatePictureList(IEnumerable<IPictureModel> newList = null)
        {
            if (newList == null) newList = BL.GetPictures();
            mwvmdl.List = new PictureListViewModel(newList);
            Console.WriteLine(newList.Aggregate("", (x, y) => x + y.FileName + " ").Trim());
            foreach (var pic in mwvmdl.List.List)
            {
                ((PictureViewModel)pic).Photographer = PhotographerList.List.ToList()
                    .Find(ph => ph.ID == ((PictureModel)((PictureViewModel)pic).PictureModel).PG_ID);
            }

        }



        private void Quit(object sender, RoutedEventArgs e) { Close(); }
    }
}