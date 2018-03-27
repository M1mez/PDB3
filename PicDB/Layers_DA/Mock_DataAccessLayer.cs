using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using PicDB.Models;
using PicDB.Properties;

namespace PicDB.Layers_DA
{
    class Mock_DataAccessLayer : IDataAccessLayer
    {
        public Mock_DataAccessLayer()
        {
        }

        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts,
            IEXIFModel exifParts)
        {
            var list = new List<IPictureModel>();
            if (isFirstCall) list.Add(new PictureModel(){ID = 1});
            isFirstCall = false;
            return list;
        }

        public IPictureModel GetPicture(int ID)
        {
            return new PictureModel()
            {
                ID = ID
            };
        }

        public void Save(IPictureModel picture)
        {
            throw new NotImplementedException();
        }

        public void DeletePicture(int ID)
        {
        }

        private bool isFirstCall = true;

        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            var list = new List<IPhotographerModel>();
            if (isFirstCall) list.Add( new PhotographerModel() { ID = 1 });
            isFirstCall = false;
            return list;
        }

        public IPhotographerModel GetPhotographer(int ID)
        {
            return new PhotographerModel(){ID = ID};
        }

        public void Save(IPhotographerModel photographer)
        {
            throw new NotImplementedException();
        }

        public void DeletePhotographer(int ID)
        {
        }

        public IEnumerable<ICameraModel> GetCameras()
        {
            return new List<ICameraModel>(){ new CameraModel() };
        }

        public ICameraModel GetCamera(int ID)
        {
            return new CameraModel(){ID = ID};
        }
    }
}
