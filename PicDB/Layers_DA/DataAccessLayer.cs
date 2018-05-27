﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using PicDB.Classes;
using PicDB.Models;
using PicDB.Properties;

namespace PicDB.Layers_DA
{
    partial class DataAccessLayer : IDataAccessLayer
    {
        //TODO: Konfigurationsfile
        private static SqlConnection Conn = new SqlConnection() { ConnectionString = Constants.ConnString };
        private readonly PreparedStatements PS;
        private static log4net.ILog log => FileInformation.Logger;
        private List<string> _dirPics = null;
        
        public DataAccessLayer() => PS = new PreparedStatements();
        
        #region Picture
        public virtual void DeletePicture(int ID)
        {
            var output = $"Delete Picture with ID: {ID}";
            log.Info(output);
            try
            {
                log.Debug(ID);
                if (ID <= 0) log.Warn("ID <= 0");
                Conn.Open();
                PS.DeletePictureId.Parameters["@ID"].Value = ID;
                PS.DeletePictureId.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public void DeletePicture(string fileName)
        {
            var output = $"DeletePicture with FileName: {fileName}";
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.DeletePictureFileName.Parameters["@FileName"].Value = fileName;
                Console.WriteLine(PS.DeletePictureFileName.Parameters["@FileName"].Value + "isvalue");
                PS.DeletePictureFileName.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public void DeletePictures(List<string> delList)
        {
            delList.ForEach(DeletePicture);
        }
        public virtual IPictureModel GetPicture(int ID)
        {
            var output = $"Get picture with ID: {ID}";
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.GetOnePictureId.Parameters["@ID"].Value = ID;
                PictureModel picture;
                using (var reader = PS.GetOnePictureId.ExecuteReader())
                {
                    reader.Read();
                    picture = DTOParser.ParsePictureModel(RecordToDictionary(reader));
                }
                Conn.Close();
                return picture;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public virtual IEnumerable<IPictureModel> GetPictures()
        {
            var output = "Get all pictures";
            Console.WriteLine(output);
            var pictures = new List<IPictureModel>();
            try
            {
                Conn.Open();
                using (var reader = PS.GetAllPictures.ExecuteReader())
                {
                    while (reader.Read()) pictures.Add(DTOParser.ParsePictureModel(RecordToDictionary(reader)));
                }
                Conn.Close();
                Console.WriteLine($"Returned {pictures.Count} pictures!");
                return pictures;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public virtual IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            var output = "Search Pictures";
            Console.WriteLine(output);

            var pictures = new List<IPictureModel>();
            try
            {
                PS.GetSearchPictures.Parameters["@namePart"].Value = (object)namePart ?? DBNull.Value;

                // Photographer
                if (photographerParts?.ID == 0 || photographerParts?.ID == null)
                    PS.GetSearchPictures.Parameters["@PG_PG_ID"].Value = DBNull.Value;
                else PS.GetSearchPictures.Parameters["@PG_PG_ID"].Value = (object)photographerParts.ID;
                PS.GetSearchPictures.Parameters["@PG_Birthday"].Value = (object)photographerParts?.BirthDay ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@PG_FirstName"].Value = (object)photographerParts?.FirstName ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@PG_LastName"].Value = (object)photographerParts?.LastName ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@PG_Notes"].Value = (object)photographerParts?.Notes ?? DBNull.Value;

                // IPTC
                if (iptcParts?.Keywords == "" || iptcParts?.Keywords == null)
                    PS.GetSearchPictures.Parameters["@IPTC_Keywords"].Value = DBNull.Value;
                else PS.GetSearchPictures.Parameters["@IPTC_Keywords"].Value = iptcParts.Keywords;

                if (iptcParts?.ByLine == "" || iptcParts?.ByLine == null)
                    PS.GetSearchPictures.Parameters["@IPTC_ByLine"].Value = DBNull.Value;
                else PS.GetSearchPictures.Parameters["@IPTC_ByLine"].Value = iptcParts.ByLine;

                if (iptcParts?.CopyrightNotice == "" || iptcParts?.CopyrightNotice == null)
                    PS.GetSearchPictures.Parameters["@IPTC_CopyrightNotice"].Value = DBNull.Value;
                else PS.GetSearchPictures.Parameters["@IPTC_CopyrightNotice"].Value = iptcParts.CopyrightNotice;

                if (iptcParts?.Headline == "" || iptcParts?.Headline == null)
                    PS.GetSearchPictures.Parameters["@IPTC_Headline"].Value = DBNull.Value;
                else PS.GetSearchPictures.Parameters["@IPTC_Headline"].Value = iptcParts.Headline;

                if (iptcParts?.Caption == "" || iptcParts?.Caption == null)
                    PS.GetSearchPictures.Parameters["@IPTC_Caption"].Value = DBNull.Value;
                else PS.GetSearchPictures.Parameters["@IPTC_Caption"].Value = iptcParts.Caption;

                /*PS.GetSearchPictures.Parameters["@IPTC_Keywords"].Value = (object)iptcParts?.Keywords ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@IPTC_ByLine"].Value = (object)iptcParts?.ByLine ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@IPTC_CopyrightNotice"].Value = (object)iptcParts?.CopyrightNotice ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@IPTC_Headline"].Value = (object)iptcParts?.Headline ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@IPTC_Caption"].Value = (object)iptcParts?.Caption ?? DBNull.Value;*/

                // EXIF
                PS.GetSearchPictures.Parameters["@EXIF_Make"].Value = (object)exifParts?.Make ?? DBNull.Value;
                //TODO cant search for value 0 because of interface, what to do?
                PS.GetSearchPictures.Parameters["@EXIF_FNumber"].Value =
                    exifParts?.FNumber == 0 || exifParts?.FNumber == null
                        ? DBNull.Value
                        : (object)exifParts.FNumber; //(object)exifParts?.FNumber ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@EXIF_ExposureTime"].Value =
                    exifParts?.ExposureTime == 0 || exifParts?.ExposureTime == null
                        ? DBNull.Value
                        : (object)exifParts.ExposureTime; //(object)exifParts?.ExposureTime ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@EXIF_ISOValue"].Value =
                    exifParts?.ISOValue == 0 || exifParts?.ISOValue == null
                        ? DBNull.Value
                        : (object)exifParts.ISOValue; //(object)exifParts?.ISOValue ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@EXIF_Flash"].Value = (object)exifParts?.Flash ?? DBNull.Value;
                PS.GetSearchPictures.Parameters["@EXIF_ExposureProgram"].Value = (object)exifParts?.ExposureProgram ?? DBNull.Value;

                Conn.Open();
                using (var reader = PS.GetSearchPictures.ExecuteReader())
                {
                    Console.WriteLine("FieldCount" + reader.FieldCount);
                    while (reader.Read())
                    {
                        pictures.Add(DTOParser.ParsePictureModel(RecordToDictionary(reader)));
                    }
                }
                Conn.Close();
                return pictures;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public virtual void Save(IPictureModel picture)
        {
            var output = $"Save picture with ID: {picture.ID}";
            if (picture == null) throw new ArgumentNullException(nameof(picture));
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.SavePicture.Parameters["@Pic_ID"].Value = picture.ID;
                PS.SavePicture.Parameters["@FileName"].Value = picture.FileName;
                //PS.SavePicture.Parameters["@Cam_ID"].Value = (object)picture.Camera?.ID ?? DBNull.Value;
                PS.SavePicture.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public void Save(List<string> picList)
        {
            picList.ForEach(fileName => Save(new PictureModel() { FileName = fileName }));
        }
        #endregion

        #region Photographer
        public virtual void Save(IPhotographerModel photographer)
        {
            var output = $"Save photographer {photographer.LastName} {photographer.FirstName}, born on {photographer.BirthDay}";
            if (photographer == null) throw new ArgumentNullException(nameof(photographer));
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.SavePhotographer.Parameters["@FirstName"].Value = photographer.FirstName;
                PS.SavePhotographer.Parameters["@LastName"].Value = photographer.LastName;
                PS.SavePhotographer.Parameters["@BirthDay"].Value =
                    photographer.BirthDay != null ? (object)photographer.BirthDay : DBNull.Value;
                PS.SavePhotographer.Parameters["@Notes"].Value = photographer.Notes;
                PS.SavePhotographer.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public void Update(IPhotographerModel photographer)
        {
            var output = $"Update photographer with ID \"{photographer.ID}\": {photographer.LastName} {photographer.FirstName}, born on {photographer.BirthDay}";
            if (photographer == null) throw new ArgumentNullException(nameof(photographer));
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.UpdatePhotographer.Parameters["@PG_ID"].Value = photographer.ID;
                PS.UpdatePhotographer.Parameters["@FirstName"].Value = photographer.FirstName;
                PS.UpdatePhotographer.Parameters["@LastName"].Value = photographer.LastName;
                PS.UpdatePhotographer.Parameters["@BirthDay"].Value =
                    photographer.BirthDay != null ? (object)photographer.BirthDay : DBNull.Value;
                PS.UpdatePhotographer.Parameters["@Notes"].Value = photographer.Notes;
                PS.UpdatePhotographer.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public void UpdatePicsPhotographer(int pic_ID, int pg_ID)
        {
            log.Info("Update Picture with ID of Photographer");
            try
            {
                log.Debug($"Update picture {pic_ID} with photographer {pg_ID}");

                string warning = "";
                if (pic_ID <= 0) warning += $"Pic ID = {pic_ID}, ";
                if (pg_ID <= 0) warning += $"PG ID = {pg_ID}, ";
                if (pic_ID <= 0 || pg_ID <= 0) log.Warn(warning.TrimEnd(','));

                Conn.Open();
                PS.UpdatePicsPhotographer.Parameters["@Pic_ID"].Value = pic_ID;
                PS.UpdatePicsPhotographer.Parameters["@PG_ID"].Value = pg_ID;
                PS.UpdatePicsPhotographer.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
            finally
            {
                Conn.Close();
            }
        }
        public virtual IPhotographerModel GetPhotographer(int ID)
        {
            var output = $"Get Photographer with ID: {ID}";
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.GetOnePhotographerId.Parameters["@ID"].Value = ID;
                PhotographerModel photographer;
                using (var reader = PS.GetOnePhotographerId.ExecuteReader())
                {
                    reader.Read();
                    photographer = DTOParser.ParsePhotographerModel(RecordToDictionary(reader));
                }
                Conn.Close();
                return photographer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public virtual IEnumerable<IPhotographerModel> GetPhotographers()
        {
            var output = "Get all photographers";
            Console.WriteLine(output);
            var photographers = new List<IPhotographerModel>();
            try
            {
                Conn.Open();
                using (var reader = PS.GetAllPhotographers.ExecuteReader())
                {
                    while (reader.Read())
                        photographers.Add(DTOParser.ParsePhotographerModel(RecordToDictionary(reader)));
                }
                Conn.Close();
                return photographers;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public virtual void DeletePhotographer(int ID)
        {
            var output = $"Delete Photographer with ID {ID}";
            log.Info(output);
            try
            {
                log.Debug(ID);
                if (ID <= 0) log.Warn("ID <= 0");

                Conn.Open();
                PS.DeletePhotographerId.Parameters["@ID"].Value = ID;
                PS.DeletePhotographerId.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        #endregion

        #region Camera
        public void Save(ICameraModel camera)
        {
            var output = $"Save camera {camera.Producer} {camera.Make}, bought on {camera.BoughtOn}";
            if (camera == null) throw new ArgumentNullException(nameof(camera));
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                //PS.SaveCamera.Parameters["@Cam_ID"].Value = camera.ID <= 0 ? DBNull.Value : (object)camera.ID;
                PS.SaveCamera.Parameters["@Producer"].Value = camera.Producer;
                PS.SaveCamera.Parameters["@Make"].Value = camera.Make;
                PS.SaveCamera.Parameters["@BoughtOn"].Value =
                    camera.BoughtOn!= null ? (object)camera.BoughtOn : DBNull.Value;
                PS.SaveCamera.Parameters["@Notes"].Value = camera.Notes;
                PS.SaveCamera.Parameters["@ISOLimitGood"].Value = camera.ISOLimitGood;
                PS.SaveCamera.Parameters["@ISOLimitAcceptable"].Value = camera.ISOLimitAcceptable;
                PS.SaveCamera.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public void Update(ICameraModel camera)
        {
            var output = $"Update camera with ID \"{camera.ID}\": {camera.Producer} {camera.Make}, bought on {camera.BoughtOn}";
            if (camera == null) throw new ArgumentNullException(nameof(camera));
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.UpdateCamera.Parameters["@Cam_ID"].Value = camera.ID;
                PS.UpdateCamera.Parameters["@Producer"].Value = camera.Producer;
                PS.UpdateCamera.Parameters["@Make"].Value = camera.Make;
                PS.UpdateCamera.Parameters["@BoughtOn"].Value =
                    camera.BoughtOn != null ? (object)camera.BoughtOn : DBNull.Value;
                PS.UpdateCamera.Parameters["@Notes"].Value = camera.Notes;
                PS.UpdateCamera.Parameters["@ISOLimitGood"].Value = camera.ISOLimitGood;
                PS.UpdateCamera.Parameters["@ISOLimitAcceptable"].Value = camera.ISOLimitAcceptable;
                PS.UpdateCamera.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public void UpdatePicsCamera(int pic_ID, int cam_ID)
        {
            var output = $"Update picture {pic_ID} with camera {cam_ID}";
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.UpdatePicsCamera.Parameters["@Pic_ID"].Value = pic_ID;
                PS.UpdatePicsCamera.Parameters["@Cam_ID"].Value = cam_ID;
                PS.UpdatePicsCamera.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public virtual ICameraModel GetCamera(int ID)
        {
            var output = $"Get Camera with ID: {ID}";
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.GetOneCameraId.Parameters["@ID"].Value = ID;
                CameraModel camera;
                using (var reader = PS.GetOneCameraId.ExecuteReader())
                {
                    reader.Read();
                    camera = DTOParser.ParseCameraModel(RecordToDictionary(reader));
                }
                Conn.Close();
                return camera;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public virtual IEnumerable<ICameraModel> GetCameras()
        {
            var output = "Get all cameras";
            Console.WriteLine(output);
            var cameras = new List<ICameraModel>();
            try
            {
                Conn.Open();
                using (var reader = PS.GetAllCameras.ExecuteReader())
                {
                    while (reader.Read()) cameras.Add(DTOParser.ParseCameraModel(RecordToDictionary(reader)));
                }
                Conn.Close();
                return cameras;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public virtual void DeleteCamera(int ID)
        {
            var output = $"Delete Camera with ID {ID}";
            log.Info(output);
            try
            {
                log.Debug(ID);
                if (ID <= 0) log.Warn("ID <= 0");

                Conn.Open();
                PS.DeleteCameraId.Parameters["@ID"].Value = ID;
                PS.DeleteCameraId.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        #endregion

        #region IPTC
        public void Update(IPTCModel iptc)
        {
            var output = $"Update IPTC model for picture with ID: {iptc.Pic_ID}";
            if (iptc == null) throw new ArgumentNullException(nameof(iptc));
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.UpdateIPTC.Parameters["@Pic_ID"].Value = iptc.Pic_ID;
                PS.UpdateIPTC.Parameters["@Keywords"].Value = iptc.Keywords;
                PS.UpdateIPTC.Parameters["@ByLine"].Value = iptc.ByLine;
                PS.UpdateIPTC.Parameters["@CopyrightNotice"].Value = iptc.CopyrightNotice;
                PS.UpdateIPTC.Parameters["@Headline"].Value = iptc.Headline;
                PS.UpdateIPTC.Parameters["@Caption"].Value = iptc.Caption;
                PS.UpdateIPTC.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public void Save(IPTCModel iptc)
        {
            var output = $"Save IPTC model for picture with ID: {iptc.Pic_ID}";
            if (iptc == null) throw new ArgumentNullException(nameof(iptc));
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.SaveIptc.Parameters["@Keywords"].Value = iptc.Keywords;
                PS.SaveIptc.Parameters["@ByLine"].Value = iptc.ByLine;
                PS.SaveIptc.Parameters["@CopyrightNotice"].Value = iptc.CopyrightNotice;
                PS.SaveIptc.Parameters["@Headline"].Value = iptc.Headline;
                PS.SaveIptc.Parameters["@Caption"].Value = iptc.Caption;
                PS.SaveIptc.Parameters["@FK_Pic_ID"].Value = iptc.Pic_ID;
                PS.SaveIptc.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        public void UpdatePicsIPTC(int pic_ID, int iptc_ID)
        {
            var output = $"Update picture {pic_ID} with iptc {iptc_ID}";
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.UpdatePicsIPTC.Parameters["@Pic_ID"].Value = pic_ID;
                PS.UpdatePicsIPTC.Parameters["@IPTC_ID"].Value = iptc_ID;
                PS.UpdatePicsIPTC.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        #endregion

        #region EXIF
        public void Save(EXIFModel exif)
        {
            var output = $"Save EXIF model for picture with ID: {exif.Pic_ID}";
            if (exif == null) throw new ArgumentNullException(nameof(exif));
            Console.WriteLine(output);
            try
            {
                Conn.Open();
                PS.SaveExif.Parameters["@Make"].Value = exif.Make != null ? (object)exif.Make : DBNull.Value;
                PS.SaveExif.Parameters["@FNumber"].Value = exif.FNumber;
                PS.SaveExif.Parameters["@ExposureTime"].Value = exif.ExposureTime;
                PS.SaveExif.Parameters["@ISOValue"].Value = exif.ISOValue;
                PS.SaveExif.Parameters["@Flash"].Value = exif.Flash;
                PS.SaveExif.Parameters["@ExposureProgram"].Value = exif.ExposureProgram;
                PS.SaveExif.Parameters["@FK_Pic_ID"].Value = exif.Pic_ID;
                PS.SaveExif.ExecuteNonQuery();
                Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(output, e);
            }
            finally
            {
                Conn.Close();
            }
        }
        #endregion

        /// <summary>
        /// Get a List of Names of current Files in Picture Folder
        /// </summary>
        public List<string> DirPics
        {
            get
            {
                if (_dirPics == null) RefreshGallery();
                return _dirPics;
            }
        }
        public void RefreshGallery() => _dirPics = Directory.GetFiles(Constants.PicPath, "*.jpg").Select(Path.GetFileName).ToList();
        private static Dictionary<string, object> RecordToDictionary(IDataRecord record)
        {
            return Enumerable.Range(0, record.FieldCount)
                .ToDictionary(record.GetName, record.GetValue);
        }

    }
}
