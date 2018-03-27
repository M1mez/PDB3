using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.ViewModels
{
    public interface IMainWindowViewModel
    {
        /// <summary>
        /// The current picture ViewModel
        /// </summary>
        IPictureViewModel CurrentPicture { get; }
        /// <summary>
        /// ViewModel with a list of all Pictures
        /// </summary>
        IPictureListViewModel List { get; }
        /// <summary>
        /// Search ViewModel
        /// </summary>
        ISearchViewModel Search { get; }
    }
}
