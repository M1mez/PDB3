using System.Collections.Generic;
using BIF.SWE2.Interfaces.Models;
using PicDB.Classes;
using PicDB.Models;

namespace PicDB.Layers_DA
{
    class MockDataAccessLayer : DataAccessLayer
    {
        private List<IPictureModel> _mockPictureModelList = new List<IPictureModel>(){new PictureModel()};
        private List<IPhotographerModel> _mockPhotographerModelList = new List<IPhotographerModel>(){new PhotographerModel(){ID=_serialId++}};
        private bool _mockSyncTriggered = false;

        public void SyncTriggered() { _mockSyncTriggered = true; }

        public override IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts,
            IEXIFModel exifParts)
        {
            if (namePart == "blume") return new List<IPictureModel>() { new PictureModel() };
            return _mockPictureModelList;
        }

        public override IPictureModel GetPicture(int ID) => _mockPictureModelList[0];

        public override IEnumerable<IPictureModel> GetPictures()
        {
            if (_mockSyncTriggered) return new List<IPictureModel>(new PictureModel[5]);
            return _mockPictureModelList;
        }

        public override void Save(IPictureModel picture)
        {
            _mockPictureModelList.Add((PictureModel)picture);
        }

        public override void DeletePicture(int ID)
        {
            _mockPictureModelList = new List<IPictureModel>();
        }

        public override IEnumerable<IPhotographerModel> GetPhotographers() => _mockPhotographerModelList;

        public override IPhotographerModel GetPhotographer(int ID) => new PhotographerModel() { ID = ID };

        public override void Save(IPhotographerModel photographer)
        {
            _mockPhotographerModelList.Add((PhotographerModel)photographer);
        }

        public override void DeletePhotographer(int ID)
        {
            _mockPhotographerModelList = new List<IPhotographerModel>();
        }

        public override IEnumerable<ICameraModel> GetCameras() => new List<ICameraModel>() { new CameraModel() };

        public override ICameraModel GetCamera(int ID) => new CameraModel() { ID = ID };

        private static int _serialId;
        //public int GetNextId(string tableName) => _serialId++;
    }
}
