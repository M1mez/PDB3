using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.Models
{
    public interface IPictureModel
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Filename of the picture, without path.
        /// </summary>
        string FileName { get; set; }
        /// <summary>
        /// IPTC information
        /// </summary>
        IIPTCModel IPTC { get; set; }
        /// <summary>
        /// EXIF information
        /// </summary>
        IEXIFModel EXIF { get; set; }

        /// <summary>
        /// The camera (optional)
        /// </summary>
        ICameraModel Camera { get; set; }
    }
}
