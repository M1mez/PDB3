using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.ViewModels
{
    public interface ICameraListViewModel
    {
        /// <summary>
        /// List of all CameraListViewModel
        /// </summary>
        IEnumerable<ICameraViewModel> List { get; }

        /// <summary>
        /// The currently selected CameraListViewModel
        /// </summary>
        ICameraViewModel CurrentCamera { get; }
    }
}
