using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.ViewModels
{
    public interface IPictureListViewModel
    {
        /// <summary>
        /// ViewModel of the current picture
        /// </summary>
        IPictureViewModel CurrentPicture { get; }
        /// <summary>
        /// List of all PictureViewModels
        /// </summary>
        IEnumerable<IPictureViewModel> List { get; }

        /// <summary>
        /// All prev. pictures to the current selected picture.
        /// </summary>
        IEnumerable<IPictureViewModel> PrevPictures { get; }
        /// <summary>
        /// All next pictures to the current selected picture.
        /// </summary>
        IEnumerable<IPictureViewModel> NextPictures { get; }

        /// <summary>
        /// Number of all images
        /// </summary>
        int Count { get; }
        /// <summary>
        /// The current Index, 1 based
        /// </summary>
        int CurrentIndex { get; }

        /// <summary>
        /// {CurrentIndex} of {Cout}
        /// </summary>
        string CurrentPictureAsString { get; }
    }
}
