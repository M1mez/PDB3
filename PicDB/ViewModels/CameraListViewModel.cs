using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB.ViewModels
{
    class CameraListViewModel : ICameraListViewModel
    {
        public IEnumerable<ICameraViewModel> List { get;}

        public ICameraViewModel CurrentCamera { get; }
    }
}
