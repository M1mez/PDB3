using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.ViewModels
{
    public interface ISearchViewModel
    {
        /// <summary>
        /// The search text
        /// </summary>
        string SearchText { get; set; }

        /// <summary>
        /// True, if a search is active
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Number of photos found.
        /// </summary>
        int ResultCount { get; }
    }
}
