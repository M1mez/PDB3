using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.ViewModels
{
    public interface IPhotographerViewModel
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Firstname, including middle name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// Lastname
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// Birthday
        /// </summary>
        DateTime? BirthDay { get; set; }
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
        /// returns true if the last name is valid
        /// </summary>
        bool IsValidLastName { get; }

        /// <summary>
        /// returns true if the birthday is valid
        /// </summary>
        bool IsValidBirthDay { get; }
    }
}
