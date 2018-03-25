using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace PicDB.Classes
{
    class DataAccessLayer : IDataAccessLayer
    {
        private DAL_Conn Conn = DAL_Conn.Instance;

        private static DataAccessLayer _instance;

        private DataAccessLayer() { }

        public static DataAccessLayer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataAccessLayer();
                }
                return _instance;
            }
        }

        public void DeletePhotographer(int ID)
        {
            List<KeyValuePair<string, string>> tempList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("ID", ID.ToString())
            };

            Conn.UspList("usp_DeletePhotographer", tempList);
        }

        public void DeletePicture(int ID)
        {
            List<KeyValuePair<string, string>> tempList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("ID", ID.ToString())
            };

            Conn.UspList("usp_DeletePicture", tempList);
        }
        public void DeletePicture(string fileName)
        {
            List<KeyValuePair<string, string>> tempList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("FileName", fileName)
            };

            Conn.UspList("usp_DeletePicture", tempList);
        }

        public void DeletePictures (List<string> delList)
        {
            delList.ForEach(toDelete => DeletePicture(toDelete));
        }

        private KeyValuePair<string, string> KeyVal (string key, string val){
            return new KeyValuePair<string, string>(key, val);
        }

        public ICameraModel GetCamera(int ID)
        {

            var cam = new CameraModel() { ID = ID };

            var param = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("ID", ID.ToString()) };

            var rows = Conn.UspList("usp_GetCamera", param);
            if (rows.Count == 0 || rows[0][0] != ID.ToString()) return cam;
            var row = rows[0];

            cam.Producer = row[1];
            cam.Make = row[2];
            DateTime.TryParse(row[3], out DateTime dt);
            cam.BoughtOn = dt;
            cam.Notes = row[4];
            Decimal.TryParse(row[5], out Decimal decGood);
            cam.ISOLimitGood = decGood;
            Decimal.TryParse(row[6], out Decimal decAcc);
            cam.ISOLimitAcceptable = decAcc;

            return cam;
        }

        

        public IEnumerable<ICameraModel> GetCameras()
        {
            try
            {
                var list = new List<ICameraModel>();
                foreach (var row in Conn.UspList("usp_GetCameras", new List<KeyValuePair<string, string>>()))
                {
                    DateTime.TryParse(row[3], out DateTime dt);
                    Decimal.TryParse(row[5], out Decimal decGood);
                    Decimal.TryParse(row[6], out Decimal decAcc);
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

        public IPhotographerModel GetPhotographer(int ID)
        {
            var photographer = new PhotographerModel() { ID = ID };

            var param = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("ID", ID.ToString()) };

            var rows = Conn.UspList("usp_GetPhotographer", param);
            if (rows.Count == 0 || rows[0][0] != ID.ToString()) return photographer;
            var row = rows[0];

            photographer.ID = Int32.Parse(row[0]);
            photographer.FirstName = row[1];
            photographer.LastName = row[2];
            DateTime.TryParse(row[3], out DateTime dt);
            photographer.BirthDay = dt;
            photographer.Notes = row[4];

            return photographer;
        }

        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            try
            {
                var list = new List<IPhotographerModel>();
                foreach (var row in Conn.UspList("usp_GetPhotographers", new List<KeyValuePair<string, string>>()))
                {
                    DateTime.TryParse(row[3], out DateTime dt);
                    var pID = Int32.Parse(row[0]);
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

        public IPictureModel GetPicture(int ID)
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

        public IEnumerable<IPictureModel> GetPictures()
        {
            try
            {
                var list = new List<IPictureModel>();
                foreach (var row in Conn.UspList("usp_GetPictures", new List<KeyValuePair<string, string>>()))
                {

                    //row.ForEach(i => Console.WriteLine(i));
                    list.Add(new PictureModel()
                    {
                        ID = Int32.Parse(row[0]),
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

        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts, IEXIFModel exifParts)
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
                        ID = Int32.Parse(row[0]),
                        FileName = row[1]
                    };

                    if (Int32.TryParse(row[2], out int iptcID))
                        pic.IPTC = GetIPTC(iptcID);
                    if (Int32.TryParse(row[3], out int exifID))
                        pic.EXIF = GetEXIF(exifID);
                    if (Int32.TryParse(row[4], out int cameraID))
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

        public void Save(IPictureModel picture)
        {
            Conn.UspList("usp_SavePicture", new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("FileName", picture.FileName)
                });

            if (picture.IPTC != null) Save(picture.IPTC);
            if (picture.EXIF != null) Save(picture.EXIF);
            if (picture.Camera != null) Save(picture.Camera);
        }

        public void Save(IPhotographerModel photographer)
        {
            Conn.UspList("usp_SavePhotographer", new List<KeyValuePair<string, string>>() {
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

        }

        public void Save(IIPTCModel iptc)
        {

        }

        public IIPTCModel GetIPTC(int ID)
        {
            throw new NotImplementedException();
        }

        public void Save(IEXIFModel exif)
        {

        }

        public IEXIFModel GetEXIF(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
