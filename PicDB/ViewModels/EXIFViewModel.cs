﻿using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PicDB.ViewModels
{
    class EXIFViewModel : IEXIFViewModel
    {
        public EXIFViewModel()
        {
        }

        public EXIFViewModel(IEXIFModel mdl)
        {
            if (mdl == null) return;
            Make = mdl.Make;
            FNumber = mdl.FNumber;
            ExposureTime = mdl.ExposureTime;
            ISOValue = mdl.ISOValue;
            Flash = mdl.Flash;
            ExposureProgram = Enum.GetName(typeof(ExposurePrograms), mdl.ExposureProgram);
            ISORating = GetRating(mdl.ISOValue);
            ExposureProgramResource = GetExposureProgramResource(ExposureProgram);
        }

        private static string GetExposureProgramResource(string program)
        {
            if (Enum.IsDefined(typeof(ExposurePrograms), program))
                return "_" + (int) Enum.Parse(typeof(ExposurePrograms), program) + program;
            else return null;
        }


        private ISORatings GetRating(decimal ISOVal)
        {
            if (ISOVal < 200)
                return ISORatings.NotDefined;
            if (ISOVal < 800)
                return ISORatings.Good;
            if (ISOVal < 1600)
                return ISORatings.Acceptable;
            else
                return ISORatings.Noisey;
        }

        public string Make { get; }

        public decimal FNumber { get; }

        public decimal ExposureTime { get; }

        public decimal ISOValue { get; }

        public bool Flash { get; }

        public string ExposureProgram { get; } 

        public string ExposureProgramResource { get; }

        public ICameraViewModel Camera { get; set; }

        public ISORatings ISORating { get; }

        public string ISORatingResource { get; }
    }
}
