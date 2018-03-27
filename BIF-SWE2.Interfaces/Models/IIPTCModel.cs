using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE2.Interfaces.Models
{
    public interface IIPTCModel
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
        /// Summary/Headline of the picture
        /// </summary>
        string Headline { get; set; }
        /// <summary>
        /// Caption/Abstract, a description of the picture
        /// </summary>
        string Caption { get; set; }
    }
}
