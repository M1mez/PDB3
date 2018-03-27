using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.ViewModels
{
    public interface IEXIFViewModel
    {
        /// <summary>
        /// Name of camera
        /// </summary>
        string Make { get; }
        /// <summary>
        /// Aperture number
        /// </summary>
        decimal FNumber { get; }
        /// <summary>
        /// Exposure time
        /// </summary>
        decimal ExposureTime { get; }
        /// <summary>
        /// ISO value
        /// </summary>
        decimal ISOValue { get; }
        /// <summary>
        /// Flash yes/no
        /// </summary>
        bool Flash { get; }

        /// <summary>
        /// The Exposure Program as string
        /// </summary>
        string ExposureProgram { get; }

        /// <summary>
        /// The Exposure Program as image resource
        /// </summary>
        string ExposureProgramResource { get; }

        /// <summary>
        /// Gets or sets a optional camera view model
        /// </summary>
        ICameraViewModel Camera { get; set; }

        /// <summary>
        /// Returns a ISO rating if a camera is set.
        /// </summary>
        ISORatings ISORating { get; }

        /// <summary>
        /// Returns a image resource of a ISO rating if a camera is set.
        /// </summary>
        string ISORatingResource { get; }
    }
}
