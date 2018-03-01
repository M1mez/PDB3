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
        public BusinessLayer bl = new BusinessLayer();

        public PhotographerViewModel() { }

        public PhotographerViewModel(IPhotographerModel mdl)
        {
            if (mdl != null)
            {
                ID = mdl.ID;
                FirstName = mdl.FirstName;
                LastName = mdl.LastName;
                BirthDay = mdl.BirthDay;
                Notes = mdl.Notes;

                //NumberOfPictures = bl.GetPictures(null, mdl, null, null).Count();
                Console.WriteLine("LastName: " + LastName);
                Console.WriteLine("BirthDay: " + BirthDay);
                Console.WriteLine("NUMBER OF PICTURES: " + NumberOfPictures);
            }
        }

        public int ID { get; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Notes { get; set; }

        public int NumberOfPictures { get; }

        public bool IsValid
        {
            get { return (IsValidLastName && IsValidBirthDay) ? true : false; }
        }

        public string ValidationSummary
        {
            get
            {
                if (!IsValidLastName)
                {
                    if (LastName.Length > 50)
                    {
                        return "Nachname zu lang";
                    }
                    else if (Regex.IsMatch(LastName, @"\d"))
                    {
                        return "Keine Zahlen im Nachnamen erlaubt";
                    }
                    else
                        return "Bitte einen Nachnamen eingeben";
                }
                else if (!IsValidBirthDay)
                {
                    return "Geburtsdatum kann nicht heute oder in der Zukunft sein >.>";
                }
                else
                    return "";
            }
        }

        public bool IsValidLastName
        {
            get { return (String.IsNullOrEmpty(LastName) || LastName.Length >= 50 || Regex.IsMatch(LastName, @"\d")) ? false : true; }
        }

        public bool IsValidBirthDay
        {
            get { return (BirthDay < DateTime.Today || !BirthDay.HasValue) ? true : false; }
        }
    }
}
