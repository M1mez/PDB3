using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PicDB.Annotations;

namespace PicDB.ViewModels
{
    class IPTCViewModel : IIPTCViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //ctor
        public IPTCViewModel() { }
        public IPTCViewModel(IIPTCModel mdl)
        {
            if (mdl != null) IPTCModel = mdl;
        }

        //model
        // ReSharper disable once InconsistentNaming
        public readonly IIPTCModel IPTCModel = new IPTCModel();
        public string Keywords
        {
            get => IPTCModel.Keywords;
            set
            {
                if (IPTCModel.Keywords == value) return;
                IPTCModel.Keywords = value;
                OnPropertyChanged();
            }
        }
        public string ByLine
        {
            get => IPTCModel.ByLine;
            set
            {
                if (IPTCModel.ByLine == value) return;
                IPTCModel.ByLine = value;
                OnPropertyChanged();
            }
        }
        public string CopyrightNotice
        {
            get => IPTCModel.CopyrightNotice;
            set
            {
                if (IPTCModel.CopyrightNotice == value) return;
                IPTCModel.CopyrightNotice = value;
                OnPropertyChanged();
            }
        }
        public string Headline
        {
            get => IPTCModel.Headline;
            set
            {
                if (IPTCModel.Headline == value) return;
                IPTCModel.Headline = value;
                OnPropertyChanged();
            }
        }
        public string Caption
        {
            get => IPTCModel.Caption;
            set
            {
                if (IPTCModel.Caption == value) return;
                IPTCModel.Caption = value;
                OnPropertyChanged();
            }
        }
        public int Pic_ID { get; set; }
        public int IPTC_ID { get; set; }

        public IEnumerable<string> CopyrightNotices { get; } = new List<string>() {
            "All rights reserved",
            "CC-BY",
            "CC-BY-SA",
            "CC-BY-ND",
            "CC-BY-NC",
            "CC-BY-NC-SA",
            "CC-BY-NC-ND"
        };

    }
}
