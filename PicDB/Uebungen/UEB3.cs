using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB;
using PicDB.Classes;
using PicDB.Layers_DA;
using PicDB.ViewModels;

namespace Uebungen
{
    public class UEB3 : IUEB3
    {
        public void HelloWorld()
        {
        }

        public IBusinessLayer GetBusinessLayer()
        {
            return BusinessLayer.Instance;
        }

        public void TestSetup(string picturePath)
        {
        }

        public IDataAccessLayer GetDataAccessLayer()
        {
            if (Constants.IsUnitTest) return new MockDataAccessLayer();
            return DataAccessLayer.Instance;
        }

        public ISearchViewModel GetSearchViewModel()
        {
            return new SearchViewModel();
        }
    }
}
