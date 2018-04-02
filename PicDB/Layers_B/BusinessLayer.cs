using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using PicDB.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using PicDB.Layers_DA;

namespace PicDB.Classes
{
    class BusinessLayer : IBusinessLayer
    {
        private BusinessLayer()
        {
        }
        private static BusinessLayer _instance;
        private static readonly object padlock = new object();
        public static BusinessLayer Instance
        {
            get
            {
                lock (padlock)
                {
                    return _instance ?? (_instance = new BusinessLayer());
                }
            }
        }

        private static DataAccessLayer DAL = DataAccessLayer.Instance;

        public void DeletePhotographer(int ID)
        {
            try
            {

                DAL.DeletePhotographer(ID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeletePicture(int ID)
        {
            try
            {

                DAL.DeletePicture(ID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEXIFModel ExtractEXIF(string filename) => FileInformation.ExtractEXIF(filename);

        public IIPTCModel ExtractIPTC(string filename) => FileInformation.ExtractIPTC(filename);

        public void WriteIPTC(string filename, IIPTCModel iptc)
        {
            try
            {
                FileInformation.WriteIPTC(filename, iptc);
                DAL.Save((IPTCModel) iptc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ICameraModel GetCamera(int ID)
        {
            try
            {
                return DAL.GetCamera(ID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<ICameraModel> GetCameras()
        {
            try
            {
                return DAL.GetCameras();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IPhotographerModel GetPhotographer(int ID)
        {
            try
            {
                return DAL.GetPhotographer(ID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            try
            {
                return DAL.GetPhotographers();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IPictureModel GetPicture(int ID)
        {
            try
            {
                return DAL.GetPicture(ID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<IPictureModel> GetPictures()
        {
            try
            {
                return DAL.GetPictures();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            try
            {
                return DAL.GetPictures(namePart, photographerParts, iptcParts, exifParts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public void Save(IPictureModel picture)
        {
            try
            {
                DAL.Save(picture);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Save(IPhotographerModel photographer)
        {
            try
            {
                DAL.Save(photographer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public void Sync()
        {
            try
            {
                if (DAL is MockDataAccessLayer mock)
                {
                    mock.SyncTriggered();
                    return;
                }

                var toSave = new List<string>();
                var toDelete = new List<string>();
                var exifList = new List<EXIFModel>();
                var iptcList = new List<IPTCModel>();
                EXIFModel currexif;
                IPTCModel curriptc;

                var dbPics = GetPictures().ToList();
                var dbPicNames = dbPics.Select(x => x.FileName).ToList();
                var dirPics = Directory.GetFiles(Constants.PicPath, "*.jpg").Select(Path.GetFileName).ToList();

                toSave = dirPics.Except(dbPicNames).ToList();
                toDelete = dbPicNames.Except(dirPics).ToList();

                DAL.Save(toSave);
                DAL.DeletePictures(toDelete);

                /*DAL.Save(new CameraModel()
                {
                    BoughtOn = new DateTime(1999,5,1),
                    ISOLimitAcceptable = (decimal) 1/16,
                    ISOLimitGood = (decimal) 1/70,
                    Make = "LAL",
                    Notes = "MEH",
                    Producer = "Mama+Papa"
                });

                DAL.Save(new PhotographerModel()
                {
                    BirthDay = new DateTime(1989, 9, 2),
                    FirstName = "Johannes",
                    LastName = "Fessler",
                    Notes = "WHAT"
                });*/

                foreach (var pic in GetPictures().ToList())
                {
                    var filename = pic.FileName;
                    currexif = (EXIFModel) ExtractEXIF(filename);
                    currexif.Pic_ID = pic.ID;
                    exifList.Add(currexif);
                    curriptc = (IPTCModel) ExtractIPTC(filename);
                    curriptc.Pic_ID = pic.ID;
                    iptcList.Add(curriptc);

                   // DAL.UpdatePicsCamera(pic.ID, DAL.GetCameras().ToList()[0].ID);
                   // DAL.UpdatePicsPhotographer(pic.ID, DAL.GetPhotographers().ToList()[0].ID);
                }

                exifList.ForEach(DAL.Save);
                iptcList.ForEach(DAL.Save);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Sync", e);
            }
           
        }
    }
}
