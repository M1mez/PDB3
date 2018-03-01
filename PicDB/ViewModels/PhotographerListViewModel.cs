using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB.ViewModels
{
    class PhotographerListViewModel : IPhotographerListViewModel
    {
        public IEnumerable<IPhotographerViewModel> List { get; }

        public IPhotographerViewModel CurrentPhotographer { get; }
    }
}
