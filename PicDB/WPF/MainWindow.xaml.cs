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
using IronPdf;

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
            BL.Sync();
            Watch();
            UpdatePhotographerList();
            UpdatePictureList(BL.GetPictures());
            
            Resources.Add("NamePart", NamePart);
            this.DataContext = mwvmdl;

            InitializeComponent();
        }

        private void ClickOnGalleryThumbnail(object sender, RoutedEventArgs e)
        {
            /*var photographer = mwvmdl.CurrentPicture.Photographer;
            PhotographerList.CurrentPhotographer = photographer;*/
        }

        private void OpenNewPhotographerWindow(object sender, RoutedEventArgs e)
        {
            PhotographerWindow pw = new PhotographerWindow(this, BL);
            pw.ShowDialog();
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
            ClickOnGalleryThumbnail(null, null);
            Console.WriteLine(newList.Aggregate("", (x, y) => x + y.FileName + " ").Trim());
            foreach (var pic in mwvmdl.List.List)
            {
                ((PictureViewModel)pic).Photographer = PhotographerList.List.ToList()
                    .Find(ph => ph.ID == ((PictureModel)((PictureViewModel)pic).PictureModel).PG_ID);
            }

        }
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

        private void OnRenamed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("bildernamen geändert");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            BL.Sync();
            UpdatePictureList();
            
            Console.WriteLine("gallery update");
        }

        private void PrintPdf(object sender, RoutedEventArgs e)
        {
            var renderer = new HtmlToPdf();
            renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;

            var PDF = renderer.RenderHtmlAsPdf(
                $"<h1>{mwvmdl.CurrentPicture.FileName}</h1>" +
                $"</br>" +
                $"<img src='{mwvmdl.CurrentPicture.FilePath}' height='500' width='500'/>" +
                $"</br>" +
                $"</br>" +
                $"<h2> Photographer </h2>" +
                $"<table>" +
                $"<tr>" +
                $"<th>FirstName</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.Photographer.FirstName}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>LastName</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.Photographer.LastName}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>BirthDay</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.Photographer.BirthDay}</td>" +
                $"</tr>" +
                $"</table>" +
                $"</br>" +
                $"</br>" +
                $"<h2>IPTC</h2>" +
                $"<table>" +
                $"<tr>" +
                $"<th>Keywords</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.IPTC.Keywords}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>ByLine</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.IPTC.ByLine}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>CopyrightNotice</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.IPTC.CopyrightNotice}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>Headline</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.IPTC.Headline}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>Caption</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.IPTC.Caption}</td>" +
                $"</tr>" +
                $"</table>" +
                $"</br>" +
                $"</br>" +
                $"<h2>EXIF</h2>" +
                $"<table>" +
                $"<tr>" +
                $"<th>Make</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.EXIF.Make}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>FNumber</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.EXIF.FNumber}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>ExposureTime</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.EXIF.ExposureTime}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>ISOValue</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.EXIF.ISOValue}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>Flash</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.EXIF.Flash}</td>" +
                $"</tr>" +
                $"<tr>" +
                $"<th>ExposureProgram</th>" +
                $"<td></td>" +
                $"<td>{mwvmdl.CurrentPicture.EXIF.ExposureProgram}</td>" +
                $"</tr>" +
                $"</table>"



                );
            var output = $"{mwvmdl.CurrentPicture.FileName.Substring(0, mwvmdl.CurrentPicture.FileName.LastIndexOf('.'))}.pdf";
            PDF.SaveAs(output);

            Process.Start(output);
        }


        private void Quit(object sender, RoutedEventArgs e) { Close(); }
    }
}