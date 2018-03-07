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
                    if (_instance == null)
                    {
                        _instance = new BusinessLayer();
                    }
                    return _instance;
                }
            }
        }

        private DataAccessLayer DAL = DataAccessLayer.Instance;

        public void DeletePhotographer(int ID)
        {
            DAL.DeletePhotographer(ID);
        }

        public void DeletePicture(int ID)
        {
            DAL.DeletePicture(ID);
        }

        

        

        public IEXIFModel ExtractEXIF(string filename)
        {           

            var filePath = PersInfo.DeployPath + @"\Pictures\" + filename;
            if (!File.Exists(filePath)) throw new FileNotFoundException();
            Image image = new Bitmap(filePath);
            PropertyItem[] propItems = image.PropertyItems;

            const int MODEL = 0x0110;
            //const int MAKE = 0x010f;
            const int FNUMBER = 0x9202;
            const int EXPTIME = 0x829a;
            const int ISOVALUE = 0x8827;
            const int FLASH = 0x9209;
            Int32[] flashInfo = { 0x0, 0x8, 0x10, 0x14, 0x18, 0x20, 0x30, 0x50, 0x58 };

            var exif = new EXIFModel();
            foreach(var prop in propItems){
                switch (prop.Id){
                    case MODEL:
                        {
                            var encoding = new ASCIIEncoding();
                            exif.Make = (encoding.GetString(prop.Value)).TrimEnd('\0');
                            break;
                        }
                    /*case MAKE:
                        {
                            var encoding = new ASCIIEncoding();
                            make = encoding.GetString(prop.Value);
                            break;
                        }*/
                    case FNUMBER:
                        {
                            exif.FNumber = (decimal)
                                BitConverter.ToUInt32(prop.Value, 0) 
                                / BitConverter.ToUInt32(prop.Value, 4);
                            break;
                        }
                    case EXPTIME:
                        {
                            exif.ExposureTime = (decimal)
                                BitConverter.ToUInt32(prop.Value, 0)
                                / BitConverter.ToUInt32(prop.Value, 4);
                            break;
                        }
                    case ISOVALUE:
                        {
                            exif.ISOValue = BitConverter.ToInt16(prop.Value, 0);
                            break;
                        }
                    case FLASH:
                        {
                            exif.Flash = !flashInfo.Contains(BitConverter.ToUInt16(prop.Value, 0));
                            break;
                        }
                    default:
                        {
                            break;
                        }

                }
            }

            return exif;
        }

        public IIPTCModel ExtractIPTC(string filename)
        {
            var filePath = PersInfo.DeployPath + @"\Pictures\" + filename;
            if (!File.Exists(filePath)) throw new FileNotFoundException();

            var iptc = new IPTCModel();

            // open a filestream for the file we wish to look at
            using (Stream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite))
            {

                BitmapDecoder decoder = BitmapDecoder.Create(fs, BitmapCreateOptions.None, BitmapCacheOption.Default);
                BitmapFrame frame = decoder.Frames[0];

                BitmapMetadata metadata = frame.Metadata as BitmapMetadata;

                if (metadata != null)
                {
                    iptc.Caption = (string)metadata.GetQuery(@"/app13/irb/8bimiptc/iptc\/Caption") ?? iptc.Caption; //Caption/Abstract
                    Console.WriteLine(iptc.Caption);
                    iptc.Keywords = (string)metadata.GetQuery(@"/app13/irb/8bimiptc/iptc\/Keywords") ?? iptc.Keywords;
                    Console.WriteLine(iptc.Keywords);
                    iptc.ByLine = (string)metadata.GetQuery(@"/app13/irb/8bimiptc/iptc\/ByLine") ?? iptc.ByLine;
                    Console.WriteLine(iptc.ByLine);
                    iptc.CopyrightNotice = (string)metadata.GetQuery(@"/app13/irb/8bimiptc/iptc\/Copyrightnotice") ?? iptc.CopyrightNotice;
                    Console.WriteLine(iptc.CopyrightNotice);
                    iptc.Headline = (string)metadata.GetQuery(@"/app13/irb/8bimiptc/iptc\/Headline") ?? iptc.Headline;
                    Console.WriteLine(iptc.Headline);
                }
            }
            return iptc;
        }

            public ICameraModel GetCamera(int ID)
        {
            return DAL.GetCamera(ID);
        }

        public IEnumerable<ICameraModel> GetCameras()
        {
            return DAL.GetCameras();
        }

        public IPhotographerModel GetPhotographer(int ID)
        {
            return DAL.GetPhotographer(ID);
        }

        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            return DAL.GetPhotographers();
        }

        public IPictureModel GetPicture(int ID)
        {
            return DAL.GetPicture(ID);
        }

        public IEnumerable<IPictureModel> GetPictures()
        {
            return DAL.GetPictures();
        }

        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            return DAL.GetPictures(namePart, photographerParts, iptcParts, exifParts);
        }

        public void Save(IPictureModel picture)
        {
            DAL.Save(picture);
        }

        public void Save(IPhotographerModel photographer)
        {
            DAL.Save(photographer);
        }

        public void Sync()
        {
            var dirPics = new List<string>();
            var dbPics = new List<string>();
            var toSave = new List<string>();
            var toDelete = new List<string>();

            foreach (var filePath in Directory.GetFiles(PersInfo.PicPath, "*.jpg"))
            {
                dirPics.Add(Path.GetFileName(filePath));
                Console.WriteLine(Path.GetFileName(filePath));
            }

            dbPics = (from pic in GetPictures() select pic.FileName).ToList();
            toSave = dirPics.Except(dbPics).ToList();
            toDelete = dbPics.Except(dirPics).ToList();

            DAL.Save(toSave);
            DAL.DeletePictures(toDelete);
        }

        public void WriteIPTC(string filename, IIPTCModel iptc)
        {
            throw new NotImplementedException();
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
