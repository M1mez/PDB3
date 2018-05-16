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
    public class EXIFViewModel : IEXIFViewModel, INotifyPropertyChanged
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
            if (mdl != null) EXIFModel = mdl;
        }
        
        //model
        public IEXIFModel EXIFModel { get; } = new EXIFModel();
        public string Make
        {
            get => EXIFModel.Make;
            set => EXIFModel.Make = value;
        }

        public decimal FNumber
        {
            get => EXIFModel.FNumber;
            set => EXIFModel.FNumber = value;
        }

        public decimal ExposureTime
        {
            get => EXIFModel.ExposureTime;
            set => EXIFModel.ExposureTime = value;
        }

        public decimal ISOValue
        {
            get => EXIFModel.ISOValue;
            set => EXIFModel.ISOValue = value;
        }

        public bool Flash
        {
            get => EXIFModel.Flash;
            set => EXIFModel.Flash = value;
        }

        public string ExposureProgram
        {
            get => EXIFModel.ExposureProgram.ToString();
            set
            {
                Console.WriteLine($"got {(int) (ExposurePrograms)Enum.Parse(typeof(ExposurePrograms), value)} from Exp Program");
                EXIFModel.ExposureProgram = (ExposurePrograms) Enum.Parse(typeof(ExposurePrograms), value);
            }
        }

        public string[] GetExposureProgramsStringList => Enum.GetNames(typeof(ExposurePrograms));


        public string ExposureProgramResource => 
            $"pack://application:,,,/Resources/{(int)EXIFModel.ExposureProgram}{ExposureProgram}.png";
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
