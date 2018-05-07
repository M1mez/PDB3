using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PicDB.Annotations;
using PicDB.Models;

namespace PicDB.ViewModels
{
    class EXIFViewModel : IEXIFViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //ctor
        public EXIFViewModel() { }
        public EXIFViewModel(IEXIFModel mdl)
        {
            if (mdl != null) Exifmodel = mdl;
        }
        
        //model
        public readonly IEXIFModel Exifmodel = new EXIFModel();
        public string Make => Exifmodel.Make;
        public decimal FNumber => Exifmodel.FNumber;
        public decimal ExposureTime => Exifmodel.ExposureTime;
        public decimal ISOValue => Exifmodel.ISOValue;
        public bool Flash => Exifmodel.Flash;
        public string ExposureProgram => Exifmodel.ExposureProgram.ToString();

        public string ExposureProgramResource => 
            $"pack://application:,,,/Resources/{(int)Exifmodel.ExposureProgram}{ExposureProgram}.png";
        private static string GetExposureProgramResource(string program)
        {
            if (Enum.IsDefined(typeof(ExposurePrograms), program))
                return "_" + (int)Enum.Parse(typeof(ExposurePrograms), program) + program;
            return GetExposureProgramResource("NotDefined");
        } //old version of ExposureProgramResource

        public ICameraViewModel Camera { get; set; }

        public ISORatings ISORating => GetRating(ISOValue);
        public ISORatings GetRating(decimal iso)
        {
            if (iso <= 0) return ISORatings.NotDefined;
            if (iso <= 400) return ISORatings.Good;
            if (iso <= 800) return ISORatings.Acceptable;
            return ISORatings.Noisey;
        }

        public string ISORatingResource { get; }
    }
}
