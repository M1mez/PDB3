using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using BIF.SWE2.Interfaces;
using PicDB.Models;

namespace PicDB.Layers_DA
{
    public static class DTOParser
    {
        internal static IPTCModel ParseIPTCModel(Dictionary<string, object> record)
        {
            if (record["IPTC_ID"] == DBNull.Value) return null;
            //var iptc_ID = (int) record["IPTC_ID"];

            string keywords = null;
            if (record["Keywords"] != DBNull.Value) keywords = (string)record["Keywords"];
            string byLine = null;
            if (record["ByLine"] != DBNull.Value) byLine = (string)record["ByLine"];
            string copyrightNotice = null;
            if (record["CopyrightNotice"] != DBNull.Value) copyrightNotice = (string)record["CopyrightNotice"];
            string headline = null;
            if (record["Headline"] != DBNull.Value) headline = (string)record["Headline"];
            string caption = null;
            if (record["Caption"] != DBNull.Value) caption = (string)record["Caption"];
            int pic_ID = -1;
            if (record["IPTC_FK_Pic_ID"] != DBNull.Value) pic_ID = (int)record["IPTC_FK_Pic_ID"];


            return new IPTCModel
            {
                Pic_ID = pic_ID,
                Keywords = keywords,
                ByLine = byLine,
                CopyrightNotice = copyrightNotice,
                Headline = headline,
                Caption = caption
            };
        }

        internal static EXIFModel ParseEXIFModel(Dictionary<string, object> record)
        {
            if (record["EXIF_ID"] == DBNull.Value) return null;
            //var exif_ID = (int) record["EXIF_ID"];

            string make = null;
            if (record["Make_EXIF"] != DBNull.Value) make = (string)record["Make_EXIF"];
            decimal fNumber = 0;
            if (record["FNumber"] != DBNull.Value) fNumber = (decimal)record["FNumber"];
            decimal exposureTime = 0;
            if (record["ExposureTime"] != DBNull.Value) exposureTime = (decimal)record["ExposureTime"];
            decimal isoValue = 0;
            if (record["ISOValue"] != DBNull.Value) isoValue = (decimal)record["ISOValue"];
            bool flash = false;
            if (record["Flash"] != DBNull.Value) flash = (bool)record["Flash"];
            int exposureProgram = 0;
            if (record["ExposureProgram"] != DBNull.Value) exposureProgram = (int)record["ExposureProgram"];

            return new EXIFModel
            {
                Make = make,
                FNumber = fNumber,
                ExposureTime = exposureTime,
                ISOValue = isoValue,
                Flash = flash,
                ExposureProgram = (ExposurePrograms) exposureProgram
            };
        }
        
        internal static CameraModel ParseCameraModel(Dictionary<string, object> record)
        {
            if (record["Cam_ID"] == DBNull.Value) return null;
            var Cam_ID = (int) record["Cam_ID"];

            string producer = null;
            if (record["Producer"] != DBNull.Value) producer = (string)record["Producer"];
            string make = null;
            if (record["Make_Cam"] != DBNull.Value) make = (string)record["Make_Cam"];
            DateTime? boughtOn = null;
            if (record["BoughtOn"] != DBNull.Value) boughtOn = (DateTime) record["BoughtOn"];
            string notes = null;
            if (record["Notes_Cam"] != DBNull.Value) notes = (string)record["Notes_Cam"];
            decimal isoLimitGood = 0;
            if (record["ISOLimitGood"] != DBNull.Value) isoLimitGood = (decimal)record["ISOLimitGood"];
            decimal isoLimitAcceptable = 0;
            if (record["ISOLimitAcceptable"] != DBNull.Value) isoLimitAcceptable = (decimal)record["ISOLimitAcceptable"];
            
            return new CameraModel
            {
                ID = Cam_ID,
                Producer = producer,
                Make = make,
                BoughtOn = boughtOn,
                Notes = notes,
                ISOLimitGood = isoLimitGood,
                ISOLimitAcceptable = isoLimitAcceptable
            };
        }

        internal static PhotographerModel ParsePhotographerModel(Dictionary<string, object> record)
        {
            if (record["PG_ID"] == DBNull.Value) return null;
            var pg_ID = (int) record["PG_ID"];

            string firstName = null;
            if (record["FirstName"] != DBNull.Value) firstName = (string)record["FirstName"];
            string lastName = null;
            if (record["LastName"] != DBNull.Value) lastName = (string)record["LastName"];
            DateTime? birthDay = null;
            if (record["BirthDay"] != DBNull.Value) birthDay = (DateTime)record["BirthDay"];
            string notes = null;
            if (record["Notes_PG"] != DBNull.Value) notes = (string)record["Notes_PG"];

            return new PhotographerModel
            {
                ID = pg_ID,
                FirstName = firstName,
                LastName = lastName,
                BirthDay = birthDay,
                Notes = notes
            };
        }

        internal static PictureModel ParsePictureModel(Dictionary<string, object> record)
        {
            if (record["Pic_ID"] == DBNull.Value) return null;
            var pic_ID = (int) record["Pic_ID"];

            string fileName = null;
            if (record["FileName"] != DBNull.Value) fileName = (string) record["FileName"];
            int pg_id = 0;
            if (record["PG_ID"] != DBNull.Value) pg_id = (int)record["PG_ID"];


            return new PictureModel
            {
                ID = pic_ID,
                FileName = fileName,
                Camera = ParseCameraModel(record),
                EXIF = ParseEXIFModel(record),
                IPTC = ParseIPTCModel(record),
                PG_ID = pg_id
            };
        }
    }
}
