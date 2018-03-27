using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.Models
{
    public interface IPhotographerModel
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        int ID { get; set; }

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
    }
}
