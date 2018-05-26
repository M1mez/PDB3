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
using BIF.SWE2.Interfaces.ViewModels;
using PicDB.Layers_DA;
using PicDB.ViewModels;

namespace PicDB.Classes
{
    public class BusinessLayer : IBusinessLayer
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BusinessLayer(bool isUnitTest = false)
        {
            _dal = isUnitTest ? new MockDataAccessLayer() : new DataAccessLayer();
        }


        private static DataAccessLayer _dal;

        public void DeletePhotographer(int ID)
        {
            try
            {
                _dal.DeletePhotographer(ID);
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeletePicture(int ID)
        {
            try
            {

                _dal.DeletePicture(ID);
            }
            catch (Exception e)
            {
                log.Error(e);
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
                //FileInformation.WriteIPTC(filename, iptc);
                _dal.Update((IPTCModel)iptc);
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public ICameraModel GetCamera(int ID)
        {
            try
            {
                return _dal.GetCamera(ID);

            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<ICameraModel> GetCameras()
        {
            try
            {
                return _dal.GetCameras();
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public IPhotographerModel GetPhotographer(int ID)
        {
            try
            {
                return _dal.GetPhotographer(ID);

            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            try
            {
                return _dal.GetPhotographers();

            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public IPictureModel GetPicture(int ID)
        {
            try
            {
                return _dal.GetPicture(ID);

            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<IPictureModel> GetPictures()
        {
            try
            {
                return _dal.GetPictures();
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            try
            {
                return _dal.GetPictures(namePart, photographerParts, iptcParts, exifParts);
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        public void Save(IPictureModel picture)
        {
            try
            {
                _dal.Save(picture);
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }

        #region Camera
        public void Save(CameraViewModel cVm) => Save(cVm.CameraModel);
        public void Save(ICameraModel camera)
        {
            try
            {
                _dal.Save(camera);
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void AssignPictureToCamera(int Pic_ID, int Cam_ID)
        {
            try
            {
                _dal.UpdatePicsCamera(Pic_ID, Cam_ID);
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion

        #region Photographer
        public void Save(PhotographerViewModel phVM) => Save(phVM.PhotographerModel);
        public void Save(IPhotographerModel photographer)
        {
            try
            {
                _dal.Save(photographer);
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void AssignPictureToPhotographer(int Pic_ID, int PH_ID)
        {
            try
            {
                _dal.UpdatePicsPhotographer(Pic_ID, PH_ID);
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion

        public void Update(IPhotographerModel photographer)
        {
            try
            {
                _dal.Update(photographer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<PictureModel> GetDirPicModels()
        {
            return _dal.GetPictures().Cast<PictureModel>().ToList();
        }

        public void Sync()
        {
            try
            {
                if (_dal is MockDataAccessLayer mock)
                {
                    mock.SyncTriggered();
                    return;
                }

                var toSave = new List<string>();
                var toDelete = new List<string>();

                var dbPics = GetPictures().ToList();
                var dbPicNames = dbPics.Select(x => x.FileName).ToList();
                _dal.RefreshGallery();
                var dirPics = _dal.DirPics;

                toSave = dirPics.Except(dbPicNames).ToList();
                toDelete = dbPicNames.Except(dirPics).ToList();

                _dal.Save(toSave);
                _dal.DeletePictures(toDelete);

                UpdateExifIptcByPictureList();
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.WriteLine(e.Message);
                
                throw new Exception("Sync", e);
            }
        }

        private void UpdateExifIptcByPictureList()
        {
            var exifList = new List<EXIFModel>();
            //var iptcList = new List<IPTCModel>();
            EXIFModel currexif;
            //IPTCModel curriptc;

            foreach (var pic in GetPictures().ToList())
            {
                var filename = pic.FileName;
                currexif = (EXIFModel)ExtractEXIF(filename);
                currexif.Pic_ID = pic.ID;
                exifList.Add(currexif);
                //curriptc = (IPTCModel)ExtractIPTC(filename);
                //curriptc.Pic_ID = pic.ID;
                //iptcList.Add(curriptc);

                // DAL.UpdatePicsCamera(pic.ID, DAL.GetCameras().ToList()[0].ID);
                // DAL.UpdatePicsPhotographer(pic.ID, DAL.GetPhotographers().ToList()[0].ID);
            }

            exifList.ForEach(_dal.Save);
            //iptcList.ForEach(_dal.Save);
        }

    }
}
