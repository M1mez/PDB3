using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB.ViewModels
{
    class CameraViewModel : ICameraViewModel
    {
        public BusinessLayer bl = new BusinessLayer();

        public CameraViewModel()
        {
        }

        public CameraViewModel(ICameraModel mdl)
        {
            if (mdl != null)
            {
                ID = mdl.ID;
                Producer = mdl.Producer;
                Make = mdl.Make;
                BoughtOn = mdl.BoughtOn;
                Notes = mdl.Notes;
                ISOLimitAcceptable = mdl.ISOLimitAcceptable;
                ISOLimitGood = mdl.ISOLimitGood;
            }
        }

        public int ID { get; }

        public string Producer { get; set; }
        public string Make { get; set; }
        public DateTime? BoughtOn { get; set; }
        public string Notes { get; set; }

        public int NumberOfPictures { get; }

        public bool IsValid
        {
            get { return (IsValidProducer && IsValidMake && IsValidBoughtOn) ? true : false; }
        }

        public string ValidationSummary
        {
            get
            {
                if(!IsValidProducer)
                {
                    return "Producer hat was";
                }
                else if (!IsValidMake)
                {
                    return "Make hat was";
                }
                else if(!IsValidBoughtOn)
                {
                    return "BoughtOn hat was";
                }
                else
                return "";
            }

        }

        public bool IsValidProducer
        {
            get { return (String.IsNullOrEmpty(Producer)) ? false : true; }

        }

        public bool IsValidMake
        {
            get { return (String.IsNullOrEmpty(Make)) ? false : true; }
        }

        public bool IsValidBoughtOn
        {
            get { return (BoughtOn <= DateTime.Today || BoughtOn == null) ? true : false; }
        }

        public decimal ISOLimitGood { get; set; }
        public decimal ISOLimitAcceptable { get; set; }

        public ISORatings TranslateISORating(decimal iso)
        {
            return new ISORatings();
        }
    }
}
