using System;
using System.Collections.Generic;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using PicDB.Classes;
using PicDB.Models;

namespace PicDB.Layers_DA
{
    class DataAccessLayer : IDataAccessLayer
    {
        private DAL_Conn Conn = DAL_Conn.Instance;

        private static DataAccessLayer _instance;
        private static MockDataAccessLayer _mockInstance;
        private static readonly object padlock = new object();

        protected DataAccessLayer() { }

        public static DataAccessLayer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (DAL_Conn.IsUnitTest)
                    {
                        return _mockInstance ?? (_mockInstance = new MockDataAccessLayer());
                    }

                    return _instance ?? (_instance = new DataAccessLayer());
                }
            }
        }

        public virtual void DeletePhotographer(int ID)
        {
            Conn.OneWaySingleSql("usp_DeletePhotographer", "ID", ID.ToString());
        }

        public virtual void DeletePicture(int ID)
        {
            Conn.OneWaySingleSql("usp_DeletePicture", "ID", ID.ToString());
        }

        public void DeletePicture(string fileName)
        {
            Conn.OneWaySingleSql("usp_DeletePicture", "FileName", fileName);
        }

        public void DeletePictures (List<string> delList)
        {
            delList.ForEach(DeletePicture);
        }

        private static KeyValuePair<string, string> KeyVal (string key, string val) =>
            new KeyValuePair<string, string>(key, val);
        

        public virtual ICameraModel GetCamera(int ID)
        {
            var cam = new CameraModel() { ID = ID };

            var param = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("ID", ID.ToString()) };

            var rows = Conn.UspList("usp_GetCamera", param);
            if (rows.Count == 0 || rows[0][0] != ID.ToString()) return cam;
            var row = rows[0];

            cam.Producer = row[1];
            cam.Make = row[2];
            DateTime.TryParse(row[3], out var dt);
            cam.BoughtOn = dt;
            cam.Notes = row[4];
            decimal.TryParse(row[5], out var decGood);
            cam.ISOLimitGood = decGood;
            decimal.TryParse(row[6], out var decAcc);
            cam.ISOLimitAcceptable = decAcc;

            return cam;
        }

        

        public virtual IEnumerable<ICameraModel> GetCameras()
        {
            try
            {
                var list = new List<ICameraModel>();
                foreach (var row in Conn.UspList("usp_GetCameras", new List<KeyValuePair<string, string>>()))
                {
                    DateTime.TryParse(row[3], out var dt);
                    decimal.TryParse(row[5], out var decGood);
                    decimal.TryParse(row[6], out var decAcc);
                    var cam = new CameraModel
                    {
                        Producer = row[1],
                        Make = row[2],
                        BoughtOn = dt,
                        Notes = row[4],
                        ISOLimitGood = decGood,
                        ISOLimitAcceptable = decAcc
                    };

                    list.Add(cam);
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught in GetCameras: " + e.Message);
            }
            return new List<ICameraModel>();
        }

        public virtual IPhotographerModel GetPhotographer(int ID)
        {
            var photographer = new PhotographerModel() { ID = ID };

            var param = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("ID", ID.ToString()) };

            var rows = Conn.UspList("usp_GetPhotographer", param);
            if (rows.Count == 0 || rows[0][0] != ID.ToString()) return photographer;
            var row = rows[0];

            photographer.ID = int.Parse(row[0]);
            photographer.FirstName = row[1];
            photographer.LastName = row[2];
            DateTime.TryParse(row[3], out var dt);
            photographer.BirthDay = dt;
            photographer.Notes = row[4];

            return photographer;
        }

        public virtual IEnumerable<IPhotographerModel> GetPhotographers()
        {
            try
            {
                var list = new List<IPhotographerModel>();
                foreach (var row in Conn.UspList("usp_GetPhotographers", new List<KeyValuePair<string, string>>()))
                {
                    DateTime.TryParse(row[3], out var dt);
                    var pID = int.Parse(row[0]);
                    var photographer = new PhotographerModel
                    {
                        ID = pID,
                        FirstName = row[1],
                        LastName = row[2],
                        BirthDay = dt,
                        Notes = row[4]
                    };

                    list.Add(photographer);
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught in GetPhotographers: " + e.Message);
            }
            return new List<IPhotographerModel>();
        }

        public virtual IPictureModel GetPicture(int ID)
        {
            var pic = new PictureModel() { ID = ID };

            var param = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("ID", ID.ToString()) };

            var rows = Conn.UspList("usp_GetPicture", param);
            if (rows.Count == 0 || rows[0][0] != ID.ToString()) return pic;
            var row = rows[0];

            pic.FileName = row[1];

            return pic;
        }

        public virtual IEnumerable<IPictureModel> GetPictures()
        {
            try
            {
                var list = new List<IPictureModel>();
                foreach (var row in Conn.UspList("usp_GetPictures", new List<KeyValuePair<string, string>>()))
                {

                    //row.ForEach(i => Console.WriteLine(i));
                    list.Add(new PictureModel()
                    {
                        ID = int.Parse(row[0]),
                        FileName = row[1]
                    });
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught in GetPictures (no params) : " + e.Message);
            }
            return new List<IPictureModel>();
        }

        public virtual IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            
            var paramList = new List<KeyValuePair<string, string>>();

            if (namePart != null)
                paramList.AddRange(new List<KeyValuePair<string, string>>()
                {
                    KeyVal("NamePart", namePart)
                });

            if (photographerParts != null)
                paramList.AddRange(new List<KeyValuePair<string, string>>()
                {
                    KeyVal("FirstName", photographerParts.FirstName),
                    KeyVal("LastName", photographerParts.LastName),
                    KeyVal("Birthday", photographerParts.BirthDay.ToString()),
                    KeyVal("Notes", photographerParts.Notes)
                });

            if (iptcParts != null)
                paramList.AddRange(new List<KeyValuePair<string, string>>()
                {
                    KeyVal("Keywords", iptcParts.Keywords),
                    KeyVal("ByLine", iptcParts.ByLine),
                    KeyVal("CopyrightNotice", iptcParts.CopyrightNotice),
                    KeyVal("Headline", iptcParts.Headline),
                    KeyVal("Caption", iptcParts.Caption)
                });

            if (exifParts != null)
                paramList.AddRange(new List<KeyValuePair<string, string>>()
                {
                    KeyVal("Make", exifParts.Make),
                    KeyVal("FNumber", exifParts.FNumber.ToString()),
                    KeyVal("ExposureTime", exifParts.ExposureTime.ToString()),
                    KeyVal("ISOValue", exifParts.ISOValue.ToString()),
                    KeyVal("Flash", exifParts.Flash.ToString())
                });

            try
            {
                var list = new List<IPictureModel>();
                foreach (var row in Conn.UspList("usp_GetPictures", paramList))
                {

                    var pic = new PictureModel()
                    {
                        ID = int.Parse(row[0]),
                        FileName = row[1]
                    };

                    if (int.TryParse(row[2], out var iptcID))
                        pic.IPTC = GetIPTC(iptcID);
                    if (int.TryParse(row[3], out var exifID))
                        pic.EXIF = GetEXIF(exifID);
                    if (int.TryParse(row[4], out var cameraID))
                        pic.Camera = GetCamera(cameraID);

                    list.Add(pic);
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught in GetPictures (no params) : " + e.Message);
            }
            return new List<IPictureModel>();
        }

        public virtual void Save(IPictureModel picture)
        {
            Conn.OneWaySingleSql("usp_SavePicture", "FileName", picture.FileName);

            if (picture.IPTC != null) Save(picture.IPTC);
            if (picture.EXIF != null) Save(picture.EXIF);
            if (picture.Camera != null) Save(picture.Camera);
        }

        public virtual void Save(IPhotographerModel photographer)
        {
            Conn.OneWayListSql("usp_SavePhotographer", new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("FirstName", photographer.FirstName),
                    new KeyValuePair<string, string>("LastName", photographer.LastName),
                    new KeyValuePair<string, string>("Birthday", photographer.BirthDay.ToString()),
                    new KeyValuePair<string, string>("Notes", photographer.Notes)
                });
        }

        public void Save (List<string> picList)
        {
            picList.ForEach(fileName => Save(new PictureModel() { FileName = fileName }));
        }

        public void Save(ICameraModel camera)
        {
            throw new NotImplementedException();
        }

        public void Save(IIPTCModel iptc)
        {
            throw new NotImplementedException();
        }

        public IIPTCModel GetIPTC(int ID)
        {
            throw new NotImplementedException();
        }

        public void Save(IEXIFModel exif)
        {
            throw new NotImplementedException();
        }

        public IEXIFModel GetEXIF(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
