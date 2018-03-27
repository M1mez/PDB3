using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.ViewModels
{
    public interface IIPTCViewModel
    {
        /// <summary>
        /// A list of keywords
        /// </summary>
        string Keywords { get; set; }
        /// <summary>
        /// Name of the photographer
        /// </summary>
        string ByLine { get; set; }
        /// <summary>
        /// copyright noties. 
        /// </summary>
        string CopyrightNotice { get; set; }
        /// <summary>
        /// A list of common copyright noties. e.g. All rights reserved, CC-BY, CC-BY-SA, CC-BY-ND, CC-BY-NC, CC-BY-NC-SA, CC-BY-NC-ND
        /// </summary>
        IEnumerable<string> CopyrightNotices { get; }
        /// <summary>
        /// Summary/Headline of the picture
        /// </summary>
        string Headline { get; set; }
        /// <summary>
        /// Caption/Abstract, a description of the picture
        /// </summary>
        string Caption { get; set; }
    }
}
