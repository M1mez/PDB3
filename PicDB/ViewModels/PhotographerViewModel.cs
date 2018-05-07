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
    class PhotographerViewModel : IPhotographerViewModel, INotifyPropertyChanged
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
            if (mdl != null) photographerModel = mdl;
        }

        // model
        public IPhotographerModel photographerModel = new PhotographerModel();

        public int ID => photographerModel.ID;
        public string FirstName
        {
            get => photographerModel.FirstName;
            set
            {
                if (photographerModel.FirstName == value) return;
                photographerModel.FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => photographerModel.LastName;
            set
            {
                if (photographerModel.LastName == value) return;
                photographerModel.LastName = value;
                OnPropertyChanged();
            }
        }
        public DateTime? BirthDay
        {
            get => photographerModel.BirthDay;
            set
            {
                if (photographerModel.BirthDay == value) return;
                photographerModel.BirthDay = value;
                OnPropertyChanged();
            }
        }
        public string Notes
        {
            get => photographerModel.Notes;
            set
            {
                if (photographerModel.Notes == value) return;
                photographerModel.Notes = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfPictures { get; }
        
        public string ValidationSummary
        {
            get
            {
                if (!IsValidLastName)
                {
                    if (LastName.Length > 50) return "Nachname zu lang";
                    if (Regex.IsMatch(LastName, @"\d")) return "Keine Zahlen im Nachnamen erlaubt";
                    return "Bitte einen Nachnamen eingeben";
                }

                return !IsValidBirthDay ? "Geburtsdatum kann nicht heute oder in der Zukunft sein >.>" : "";
            }
        }

        public bool IsValid => IsValidLastName && IsValidBirthDay;
        public bool IsValidLastName => !(string.IsNullOrEmpty(LastName) || LastName.Length >= 50 || Regex.IsMatch(LastName, @"\d"));
        public bool IsValidBirthDay => (BirthDay < DateTime.Today || !BirthDay.HasValue);

        
    }
}
