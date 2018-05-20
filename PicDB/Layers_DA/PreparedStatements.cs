using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PicDB.Properties;
// ReSharper disable InconsistentNaming

namespace PicDB.Layers_DA
{
    internal partial class DataAccessLayer
    {
        internal class PreparedStatements
        {

            internal PreparedStatements()
            {
                DeletePhotographerId = NewComm(Resources.DeletePhotographer_ID);
                DeletePhotographerId.Parameters.Add("@ID", SqlDbType.Int);

                DeletePictureId = NewComm(Resources.DeletePicture_ID);
                DeletePictureId.Parameters.Add("@ID", SqlDbType.Int);

                DeletePictureFileName = NewComm(Resources.DeletePicture_FileName);
                DeletePictureFileName.Parameters.Add("@FileName", SqlDbType.VarChar);

                GetAllCameras = NewComm(Resources.GetAll_Cameras);

                GetOneCameraId = NewComm(Resources.GetOne_Camera_ID);
                GetOneCameraId.Parameters.Add("@ID", SqlDbType.Int);

                GetAllExif = NewComm(Resources.GetAll_EXIF);

                GetAllIptc = NewComm(Resources.GetAll_IPTC);

                GetOnePhotographerId = NewComm(Resources.GetOne_Photographer_ID);
                GetOnePhotographerId.Parameters.Add("@ID", SqlDbType.Int);

                GetOnePictureId = NewComm(Resources.GetOne_Picture_ID);
                GetOnePictureId.Parameters.Add("@ID", SqlDbType.Int);

                GetAllPhotographers = NewComm(Resources.GetAll_Photographers);

                GetAllPictures = NewComm(Resources.GetAll_Pictures);

                SavePicture = NewComm(Resources.Save_Picture);
                SavePicture.Parameters.Add("@Pic_ID", SqlDbType.Int);
                SavePicture.Parameters.Add("@FileName", SqlDbType.VarChar);
                //SavePicture.Parameters.Add("@PG_ID", SqlDbType.Int);
                SavePicture.Parameters.Add("@Cam_ID", SqlDbType.Int);

                SavePhotographer = NewComm(Resources.Save_Photographer);
                SavePhotographer.Parameters.Add("@FirstName", SqlDbType.VarChar);
                SavePhotographer.Parameters.Add("@LastName", SqlDbType.VarChar);
                SavePhotographer.Parameters.Add("@BirthDay", SqlDbType.DateTime);
                SavePhotographer.Parameters.Add("@Notes", SqlDbType.VarChar);

                AddPictureToPhotographer = NewComm(Resources.Add_Picture_To_Photographer);
                AddPictureToPhotographer.Parameters.Add("@Pic_ID", SqlDbType.Int);
                AddPictureToPhotographer.Parameters.Add("@PG_ID", SqlDbType.Int);

                GetSearchPictures = NewComm(Resources.Get_Search_Pictures);
                GetSearchPictures.Parameters.Add(new SqlParameter("@namePart", SqlDbType.VarChar));
                GetSearchPictures.Parameters.Add(new SqlParameter("@PG_PG_ID", SqlDbType.Int));
                GetSearchPictures.Parameters.Add(new SqlParameter("@PG_Birthday", SqlDbType.DateTime));
                GetSearchPictures.Parameters.Add(new SqlParameter("@PG_FirstName", SqlDbType.VarChar));
                GetSearchPictures.Parameters.Add(new SqlParameter("@PG_LastName", SqlDbType.VarChar));
                GetSearchPictures.Parameters.Add(new SqlParameter("@PG_Notes", SqlDbType.VarChar));
                GetSearchPictures.Parameters.Add(new SqlParameter("@IPTC_Keywords", SqlDbType.VarChar));
                GetSearchPictures.Parameters.Add(new SqlParameter("@IPTC_ByLine", SqlDbType.VarChar));
                GetSearchPictures.Parameters.Add(new SqlParameter("@IPTC_CopyrightNotice", SqlDbType.VarChar));
                GetSearchPictures.Parameters.Add(new SqlParameter("@IPTC_Headline", SqlDbType.VarChar));
                GetSearchPictures.Parameters.Add(new SqlParameter("@IPTC_Caption", SqlDbType.VarChar));
                GetSearchPictures.Parameters.Add(new SqlParameter("@EXIF_Make", SqlDbType.VarChar));
                GetSearchPictures.Parameters.Add(new SqlParameter("@EXIF_FNumber", SqlDbType.Decimal));
                GetSearchPictures.Parameters.Add(new SqlParameter("@EXIF_ExposureTime", SqlDbType.Decimal));
                GetSearchPictures.Parameters.Add(new SqlParameter("@EXIF_ISOValue", SqlDbType.Decimal));
                GetSearchPictures.Parameters.Add(new SqlParameter("@EXIF_Flash", SqlDbType.Bit));
                GetSearchPictures.Parameters.Add(new SqlParameter("@EXIF_ExposureProgram", SqlDbType.Int));

                SaveExif = NewComm(Resources.Save_Exif);
                SaveExif.Parameters.Add(new SqlParameter("@Make", SqlDbType.VarChar));
                SaveExif.Parameters.Add(new SqlParameter("@FNumber", SqlDbType.Decimal));
                SaveExif.Parameters.Add(new SqlParameter("@ExposureTime", SqlDbType.Decimal));
                SaveExif.Parameters.Add(new SqlParameter("@ISOValue", SqlDbType.Decimal));
                SaveExif.Parameters.Add(new SqlParameter("@Flash", SqlDbType.Bit));
                SaveExif.Parameters.Add(new SqlParameter("@ExposureProgram", SqlDbType.Int));
                SaveExif.Parameters.Add(new SqlParameter("@FK_Pic_ID", SqlDbType.Int));

                SaveIptc = NewComm(Resources.Save_Iptc);
                SaveIptc.Parameters.Add(new SqlParameter("@Keywords", SqlDbType.VarChar));
                SaveIptc.Parameters.Add(new SqlParameter("@ByLine", SqlDbType.VarChar));
                SaveIptc.Parameters.Add(new SqlParameter("@CopyrightNotice", SqlDbType.VarChar));
                SaveIptc.Parameters.Add(new SqlParameter("@Headline", SqlDbType.VarChar));
                SaveIptc.Parameters.Add(new SqlParameter("@Caption", SqlDbType.VarChar));
                SaveIptc.Parameters.Add(new SqlParameter("@FK_Pic_ID", SqlDbType.Int));

                SaveCamera = NewComm(Resources.Save_Camera);
                SaveCamera.Parameters.Add(new SqlParameter("@Cam_ID", SqlDbType.Int));
                SaveCamera.Parameters.Add(new SqlParameter("@Producer", SqlDbType.VarChar));
                SaveCamera.Parameters.Add(new SqlParameter("@Make", SqlDbType.VarChar));
                SaveCamera.Parameters.Add(new SqlParameter("@BoughtOn", SqlDbType.DateTime));
                SaveCamera.Parameters.Add(new SqlParameter("@Notes", SqlDbType.VarChar));
                SaveCamera.Parameters.Add(new SqlParameter("@ISOLimitGood", SqlDbType.Decimal));
                SaveCamera.Parameters.Add(new SqlParameter("@ISOLimitAcceptable", SqlDbType.Decimal));

                UpdatePicsCamera = NewComm(Resources.Update_Pictures_Camera);
                UpdatePicsCamera.Parameters.Add(new SqlParameter("@Pic_ID", SqlDbType.Int));
                UpdatePicsCamera.Parameters.Add(new SqlParameter("@Cam_ID", SqlDbType.Int));

                UpdatePicsEXIF = NewComm(Resources.Update_Pictures_EXIF);
                UpdatePicsEXIF.Parameters.Add(new SqlParameter("@Pic_ID", SqlDbType.Int));
                UpdatePicsEXIF.Parameters.Add(new SqlParameter("@EXIF_ID", SqlDbType.Int));

                UpdatePicsIPTC = NewComm(Resources.Update_Pictures_IPTC);
                UpdatePicsIPTC.Parameters.Add(new SqlParameter("@Pic_ID", SqlDbType.Int));
                UpdatePicsIPTC.Parameters.Add(new SqlParameter("@IPTC_ID", SqlDbType.Int));

                UpdatePicsPhotographer = NewComm(Resources.Update_Pictures_Photographer);
                UpdatePicsPhotographer.Parameters.Add(new SqlParameter("@Pic_ID", SqlDbType.Int));
                UpdatePicsPhotographer.Parameters.Add(new SqlParameter("@PG_ID", SqlDbType.Int));

                UpdateIPTC = NewComm(Resources.Update_IPTC);
                UpdateIPTC.Parameters.Add(new SqlParameter("@Pic_ID", SqlDbType.Int));
                UpdateIPTC.Parameters.Add(new SqlParameter("@Keywords", SqlDbType.VarChar));
                UpdateIPTC.Parameters.Add(new SqlParameter("@ByLine", SqlDbType.VarChar));
                UpdateIPTC.Parameters.Add(new SqlParameter("@CopyrightNotice", SqlDbType.VarChar));
                UpdateIPTC.Parameters.Add(new SqlParameter("@Headline", SqlDbType.VarChar));
                UpdateIPTC.Parameters.Add(new SqlParameter("@Caption", SqlDbType.VarChar));
            }

            internal SqlCommand UpdatePicsEXIF;
            internal SqlCommand UpdatePicsPhotographer;
            internal SqlCommand UpdatePicsIPTC;
            internal SqlCommand UpdatePicsCamera;
            internal SqlCommand SaveCamera;
            internal SqlCommand SaveIptc;
            internal SqlCommand SaveExif;
            internal SqlCommand GetSearchPictures;
            internal SqlCommand AddPictureToPhotographer;
            internal SqlCommand SavePhotographer;
            internal SqlCommand SavePicture;
            internal SqlCommand GetAllPictures;
            internal SqlCommand GetOnePictureId;
            internal SqlCommand GetAllPhotographers;
            internal SqlCommand GetOnePhotographerId;
            internal SqlCommand GetAllIptc;
            internal SqlCommand GetAllExif;
            internal SqlCommand GetOneCameraId;
            internal SqlCommand GetAllCameras;
            internal SqlCommand DeletePictureFileName;
            internal SqlCommand DeletePhotographerId;
            internal SqlCommand DeletePictureId;
            internal SqlCommand UpdateIPTC;

            internal static SqlCommand GetNextIdTableName
            {
                get
                {
                    var cmd = new SqlCommand(Resources.GetNextID_TableName, Conn);
                    cmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.VarChar));
                    return cmd;
                }
            }

            private static SqlCommand NewComm(string str) => new SqlCommand(str, Conn);

            //private static SqlCommand fillParameters
        }
    }
}
