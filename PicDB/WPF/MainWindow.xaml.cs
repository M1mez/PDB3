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
using System.Text;
using System.Windows.Data;
using Helper;
using IronPdf;
using PicDB.Properties;

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
        private static log4net.ILog log => FileInformation.Logger;

        public MainWindow()
        {
            BL = mwvmdl.Bl;
            BL.Sync();
            Watch();
            UpdatePhotographerList();
            UpdatePictureList(BL.GetPictures());
            
            Resources.Add("NamePart", NamePart);
            this.DataContext = mwvmdl;

            InitializeComponent();
        }

        

        #region Search
        private void OpenNewSearchWindow(object sender, RoutedEventArgs e)
        {
            var searchWindow = new AdvancedSearchWindow(this, BL) {Owner = Application.Current.MainWindow};
            searchWindow.ShowDialog();
        }
        private void SimpleSearch(object sender, RoutedEventArgs e)
        {
            UpdatePictureList(BL.GetPictures(NamePart, null, null, null));
        }
        #endregion

        #region Property NamePart
        public string NamePart
        {
            get => (string)GetValue(NamePartProperty);
            set => SetValue(NamePartProperty, value);
        }
        public static readonly DependencyProperty NamePartProperty = DependencyProperty
            .Register("NamePart", typeof(string), typeof(MainWindow), new PropertyMetadata(""));
        #endregion

        #region Photographer
        private void OpenNewPhotographerWindow(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
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
        #endregion

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
            var newPictureListViewModel = new PictureListViewModel(newList);
            foreach (var pic in newPictureListViewModel.List)
            {
                ((PictureViewModel)pic).Photographer = PhotographerList.List.ToList()
                    .Find(ph => ph.ID == ((PictureModel)((PictureViewModel)pic).PictureModel).PG_ID);
            }

            mwvmdl.List = newPictureListViewModel;

        }
        public void UpdatePhotographerList() => PhotographerList.Update(BL.GetPhotographers());

        #region FileWatcher
        private void Watch()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Constants.PicPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = "*.jpg*";
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;
            watcher.EnableRaisingEvents = true;
        }
        private void OnRenamed(object sender, RenamedEventArgs e) => log.Debug($"File: {e.OldFullPath} renamed to {e.FullPath}");
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            BL.Sync();
            UpdatePictureList();
            log.Debug($"File: {e.FullPath} {e.ChangeType}");
        }
        #endregion

        private void PrintPdf(object sender, RoutedEventArgs e) => FileInformation.PrintPdf(mwvmdl.CurrentPicture);


        private void Quit(object sender, RoutedEventArgs e) { Close(); }
    }
}