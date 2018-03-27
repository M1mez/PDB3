using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.ViewModels
{
    public interface IPictureViewModel
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Name of the file
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Full file path, used to load the image
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// The line below the Picture. Format: {IPTC.Headline|FileName} (by {Photographer|IPTC.ByLine}).
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// The IPTC ViewModel
        /// </summary>
        IIPTCViewModel IPTC { get; }

        /// <summary>
        /// The EXIF ViewModel
        /// </summary>
        IEXIFViewModel EXIF { get; }

        /// <summary>
        /// The Photographer ViewModel
        /// </summary>
        IPhotographerViewModel Photographer { get; }

        /// <summary>
        /// The Camera ViewModel
        /// </summary>
        ICameraViewModel Camera { get; }
    }
}
