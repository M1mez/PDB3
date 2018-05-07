using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PicDB.Annotations;
using PicDB.Models;

namespace PicDB.ViewModels
{
    class CameraViewModel : ICameraViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //ctor
        public CameraViewModel() { }
        public CameraViewModel(ICameraModel mdl)
        {
            if (mdl != null) CameraModel = mdl;
        }

        //model
        public readonly ICameraModel CameraModel = new CameraModel();
        public int ID => CameraModel.ID;
        public string Producer
        {
            get => CameraModel.Producer;
            set
            {
                if (CameraModel.Producer == value) return;
                CameraModel.Producer = value;
                OnPropertyChanged();
            }
        }
        public string Make
        {
            get => CameraModel.Make;
            set
            {
                if (CameraModel.Make == value) return;
                CameraModel.Make = value;
                OnPropertyChanged();
            }
        }
        public DateTime? BoughtOn
        {
            get => CameraModel.BoughtOn;
            set
            {
                if (CameraModel.BoughtOn == value) return;
                CameraModel.BoughtOn = value;
                OnPropertyChanged();
            }
        }
        public string Notes
        {
            get => CameraModel.Notes;
            set
            {
                if (CameraModel.Notes == value) return;
                CameraModel.Notes = value;
                OnPropertyChanged();
            }
        }
        public decimal ISOLimitGood
        {
            get => CameraModel.ISOLimitGood;
            set
            {
                if (CameraModel.ISOLimitGood == value) return;
                CameraModel.ISOLimitGood = value;
                OnPropertyChanged();
            }
        }
        public decimal ISOLimitAcceptable
        {
            get => CameraModel.ISOLimitAcceptable;
            set
            {
                if (CameraModel.ISOLimitAcceptable == value) return;
                CameraModel.ISOLimitAcceptable = value;
                OnPropertyChanged();
            }
        }

        public ISORatings TranslateISORating(decimal iso)
        {
            if (iso <= 0) return ISORatings.NotDefined;
            if (iso <= 400) return ISORatings.Good;
            if (iso <= 800) return ISORatings.Acceptable;
            return ISORatings.Noisey;
        }

        public int NumberOfPictures { get; }

        //validity check
        public bool IsValidProducer => (!string.IsNullOrEmpty(Producer));
        public bool IsValidMake => (!string.IsNullOrEmpty(Make));
        public bool IsValidBoughtOn => (BoughtOn <= DateTime.Today || BoughtOn == null);
        public bool IsValid => (IsValidProducer && IsValidMake && IsValidBoughtOn);
        public string ValidationSummary
        {
            get
            {
                string errorMessage = "";
                if(!IsValidProducer)  errorMessage += "Producer, ";
                if (!IsValidMake) errorMessage += "Make, ";
                if(!IsValidBoughtOn) errorMessage += "BoughtOn, ";
                if (string.IsNullOrEmpty(errorMessage)) return "";
                return errorMessage.TrimEnd(',', ' ') + "passt nicht!";
            }
        }

        
    }
}
