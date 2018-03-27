using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.ViewModels
{
    public interface IPhotographerListViewModel
    {
        /// <summary>
        /// List of all PhotographerViewModel
        /// </summary>
        IEnumerable<IPhotographerViewModel> List { get; }

        /// <summary>
        /// The currently selected PhotographerViewModel
        /// </summary>
        IPhotographerViewModel CurrentPhotographer { get; }
    }
}
