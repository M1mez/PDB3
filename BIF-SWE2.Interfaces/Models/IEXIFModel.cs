using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.Models
{
    public interface IEXIFModel
    {
        /// <summary>
        /// Name of camera
        /// </summary>
        string Make { get; set; }
        /// <summary>
        /// Aperture number
        /// </summary>
        decimal FNumber { get; set; }
        /// <summary>
        /// Exposure time
        /// </summary>
        decimal ExposureTime { get; set; }
        /// <summary>
        /// ISO value
        /// </summary>
        decimal ISOValue { get; set; }
        /// <summary>
        /// Flash yes/no
        /// </summary>
        bool Flash { get; set; }

        /// <summary>
        /// The exposure program
        /// </summary>
        ExposurePrograms ExposureProgram { get; set; }
    }
}
