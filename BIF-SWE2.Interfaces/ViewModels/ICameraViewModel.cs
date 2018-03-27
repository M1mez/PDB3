using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.ViewModels
{
    public interface ICameraViewModel
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Name of the producer
        /// </summary>
        string Producer { get; set; }
        /// <summary>
        /// Name of camera
        /// </summary>
        string Make { get; set; }
        /// <summary>
        /// Optional: date, when the camera was bought
        /// </summary>
        DateTime? BoughtOn { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        string Notes { get; set; }

        /// <summary>
        /// Returns the number of Pictures
        /// </summary>
        int NumberOfPictures { get; }

        /// <summary>
        /// Returns true, if the model is valid
        /// </summary>
        bool IsValid { get; }
        /// <summary>
        /// Returns a summary of validation errors
        /// </summary>
        string ValidationSummary { get; }

        /// <summary>
        /// returns true if the producer name is valid
        /// </summary>
        bool IsValidProducer { get; }

        /// <summary>
        /// returns true if the make is valid
        /// </summary>
        bool IsValidMake { get; }

        /// <summary>
        /// returns true if the "bought on" date is valid
        /// </summary>
        bool IsValidBoughtOn { get; }

        /// <summary>
        /// Max ISO Value for good results. 0 means "not defined"
        /// </summary>
        decimal ISOLimitGood { get; set; }

        /// <summary>
        /// Max ISO Value for acceptable results. 0 means "not defined"
        /// </summary>
        decimal ISOLimitAcceptable { get; set; }

        /// <summary>
        /// Translates a given ISO value to a ISO rating
        /// </summary>
        /// <param name="iso"></param>
        /// <returns></returns>
        ISORatings TranslateISORating(decimal iso);
    }
}
