using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Classes;
using PicDB.Models;
using PicDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BIF.SWE2.Interfaces;

namespace PicDB
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class AdvancedSearchWindow : Window
    {
        private static log4net.ILog log => FileInformation.Logger;

        public AdvancedSearchWindow(MainWindow sender, BusinessLayer BL)
        {
            DataContext = this;
            InitializeComponent();
            this.Sender = sender;
            this.BL = BL;
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private MainWindow Sender { get; }
        private BusinessLayer BL { get; }

        public string NamePart { get; set; } = null;
        public PhotographerViewModel PhotographerPart { get; set; } = new PhotographerViewModel(){ID = 0};
        public IPTCViewModel IPTCPart { get; set; } = new IPTCViewModel();
        public EXIFViewModel EXIFPart { get; set; } = new EXIFViewModel();

        //public PhotographerModel PhotographerPart { get; set; } = new PhotographerModel();
        //public IPTCModel IPTCPart = new IPTCModel();
        //public EXIFModel EXIFPart = new EXIFModel();
        

        private void Search(object sender, RoutedEventArgs e)
        {
            log.Debug($"np {NamePart}");
            log.Debug("photographer: " +
                $"id {PhotographerPart.ID} " +
                $"fn {PhotographerPart.FirstName ?? "null"} " +
                $"ln {PhotographerPart.LastName ?? "null"} " +
                $"bd {(PhotographerPart.BirthDay != null ? PhotographerPart.BirthDay : DateTime.MinValue )} " +
                $"no {PhotographerPart.Notes ?? "null"}");
            log.Debug("iptc: " +
                $"kw {(IPTCPart.Keywords != string.Empty ? IPTCPart.Keywords : "null")} " +
                $"bl {(IPTCPart.ByLine != string.Empty ? IPTCPart.ByLine : "null")} " +
                $"ca {(IPTCPart.Caption != string.Empty ? IPTCPart.Caption : "null")} " +
                $"cn {(IPTCPart.CopyrightNotice != string.Empty ? IPTCPart.CopyrightNotice : "null")} " +
                $"hl {(IPTCPart.Headline != string.Empty ? IPTCPart.Headline : "null")} ");
            log.Debug("exif: " +
                $"fn {EXIFPart.FNumber} " +
                $"ep {EXIFPart.ExposureProgram ?? "null"} " +
                $"et {EXIFPart.ExposureTime} " +
                $"fl {EXIFPart.Flash} " +
                $"iv {EXIFPart.ISOValue} " +
                $"ma {EXIFPart.Make ?? "null"} ");
            
            var searchedPictures = BL.GetPictures(
                NamePart, 
                PhotographerPart.PhotographerModel, 
                IPTCPart.IPTCModel, 
                EXIFPart.EXIFModel);

            Sender.UpdatePictureList(searchedPictures);
        }
    }
}
