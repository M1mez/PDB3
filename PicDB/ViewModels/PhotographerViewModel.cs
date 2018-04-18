using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Classes;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PicDB.ViewModels
{
    class PhotographerViewModel : IPhotographerViewModel
    {
        public PhotographerViewModel() { }

        public PhotographerViewModel(IPhotographerModel mdl)
        {
            if (mdl == null) return;
            ID = mdl.ID;
            FirstName = mdl.FirstName;
            LastName = mdl.LastName;
            BirthDay = mdl.BirthDay;
            Notes = mdl.Notes;
        }

        public int ID { get; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Notes { get; set; }

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
