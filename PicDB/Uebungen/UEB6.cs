using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using PicDB;
using PicDB.Classes;
using PicDB.Models;

namespace Uebungen
{
    public class UEB6 : IUEB6
    {
        public void HelloWorld()
        {
        }

        public IBusinessLayer GetBusinessLayer()
        {
            return new BusinessLayer(true);
        }

        public void TestSetup(string picturePath)
        {
        }

        public IPictureModel GetEmptyPictureModel()
        {
            PictureModel p = new PictureModel();
            p.ID = p.GetHashCode();
            return p;
        }

        public IPhotographerModel GetEmptyPhotographerModel()
        {
            return new PhotographerModel(){
                    ID = Constants.GetRandomInt()
            };
        }
    }
}
