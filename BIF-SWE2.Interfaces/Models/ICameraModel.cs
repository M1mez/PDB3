using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.Models
{
    public interface ICameraModel
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        int ID { get; set; }
        
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
        /// Max ISO Value for good results. 0 means "not defined"
        /// </summary>
        decimal ISOLimitGood { get; set; }

        /// <summary>
        /// Max ISO Value for acceptable results. 0 means "not defined"
        /// </summary>
        decimal ISOLimitAcceptable { get; set; }
    }
}
