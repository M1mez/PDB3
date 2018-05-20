using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Classes;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using PicDB.Annotations;

namespace PicDB.ViewModels
{
    public class PhotographerViewModel : IPhotographerViewModel, INotifyPropertyChanged
    {
        //notify
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // ctor
        public PhotographerViewModel() { }
        public PhotographerViewModel(IPhotographerModel mdl)
        {
            if (mdl != null) PhotographerModel = mdl;
        }

        // model
        public IPhotographerModel PhotographerModel { get; } = new PhotographerModel();

        public int ID
        {
            get => PhotographerModel.ID;
            set
            {
                if (PhotographerModel.ID == value) return;
                PhotographerModel.ID = value;
                OnPropertyChanged();
            }
        }
        public string FirstName
        {
            get => PhotographerModel.FirstName;
            set
            {
                if (PhotographerModel.FirstName == value) return;
                PhotographerModel.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => PhotographerModel.LastName;
            set
            {
                if (PhotographerModel.LastName == value) return;
                PhotographerModel.LastName = value;
                OnPropertyChanged();
            }
        }

        public string FirstLastName => $"{FirstName} {LastName}";

        public DateTime? BirthDay
        {
            get => PhotographerModel.BirthDay;
            set
            {
                if (PhotographerModel.BirthDay == value) return;
                PhotographerModel.BirthDay = value;
                OnPropertyChanged();
            }
        }
        
        public string DateInString
        {
            get => BirthDay?.ToShortDateString();
            set
            {
                if (value == null) BirthDay = null;
                else BirthDay = DateTime.Parse(value);
            }
        }

        public string Notes
        {
            get => PhotographerModel.Notes;
            set
            {
                if (PhotographerModel.Notes == value) return;
                PhotographerModel.Notes = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfPictures { get; }
        
        public string ValidationSummary
        {
            get
            {
                string message = "";
                if (!IsValidLastName)
                {
                    if (LastName.Length == 0) message += "Bitte einen Nachnamen eingeben!\n";
                    if (LastName.Length > 50) message += "Nachname zu lang!\n";
                    if (Regex.IsMatch(LastName, @"\d")) message += "Keine Zahlen im Nachnamen erlaubt!\n";
                    
                }
                if (!IsValidBirthDay) message += "Geburtsdatum kann nicht heute oder in der Zukunft sein >.>\n";
                return message.TrimEnd('\n');
            }
        }

        public bool IsValid => IsValidLastName && IsValidBirthDay;
        public bool IsValidLastName => !(string.IsNullOrEmpty(LastName) || LastName.Length >= 50 || Regex.IsMatch(LastName, @"\d"));
        public bool IsValidBirthDay => (BirthDay < DateTime.Today || !BirthDay.HasValue);

        
    }
}
