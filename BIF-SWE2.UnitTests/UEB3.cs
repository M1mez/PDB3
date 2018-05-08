using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;

namespace BIF.SWE2.UnitTests
{
    [TestFixture]
    public class UEB3 : AbstractTestFixture<IUEB3>
    {
        private IUEB3 ueb;
        public override void TestSetup()
        {
            base.TestSetup();

            IUEB3 ueb = CreateInstance();
            ueb.TestSetup(System.IO.Path.Combine(WorkingDirectory, Constants.PicturePath));
        }
        public override void Setup()
        {
            base.Setup();
            ueb = CreateInstance();
        }

        [Test]
        public void HelloWorld()
        {
            ueb.HelloWorld();
        }

        #region Data Access Layer
        [Test]
        public void mock_dal_should_return_fake_Pictures_data()
        {
            IDataAccessLayer dal = ueb.GetDataAccessLayer();
            AssertNotNull("GetDataAccessLayer", dal);

            IEnumerable<IPictureModel> lst = dal.GetPictures(null, null, null, null);

            AssertNotNull("dal.GetPictures", lst);
            AssertTrue("dal.GetPictures returned nothing", lst.Count() > 0);
        }

        [Test]
        public void mock_dal_should_return_fake_Pictures_data_by_ID()
        {
            IDataAccessLayer dal = ueb.GetDataAccessLayer();
            AssertNotNull("GetDataAccessLayer", dal);

            IPictureModel obj = dal.GetPicture(1234);
            AssertNotNull("dal.GetPicture", obj);
        }

        [Test]
        public void mock_dal_should_return_fake_Photographers_data()
        {
            IDataAccessLayer dal = ueb.GetDataAccessLayer();
            AssertNotNull("GetDataAccessLayer", dal);

            IEnumerable<IPhotographerModel> lst = dal.GetPhotographers();

            AssertNotNull("dal.GetPhotographers", lst);
            AssertTrue("dal.GetPhotographers returned nothing", lst.Count() > 0);
        }

        [Test]
        public void mock_dal_should_return_fake_Photographers_data_by_ID()
        {
            IDataAccessLayer dal = ueb.GetDataAccessLayer();
            AssertNotNull("GetDataAccessLayer", dal);

            IPhotographerModel obj = dal.GetPhotographer(1234);
            AssertNotNull("dal.GetPhotographer", obj);
        }

        [Test]
        public void mock_dal_should_delete_Picture()
        {
            IDataAccessLayer dal = ueb.GetDataAccessLayer();
            AssertNotNull("GetDataAccessLayer", dal);

            IEnumerable<IPictureModel> lst = dal.GetPictures(null, null, null, null);

            AssertNotNull("dal.GetPictures", lst);
            AssertTrue("dal.GetPictures returned nothing", lst.Count() > 0);

            IPictureModel mdl = lst.First();
            dal.DeletePicture(mdl.ID);
            IEnumerable<IPictureModel> lst2 = dal.GetPictures(null, null, null, null);

            AssertFalse("Picture was not deleted", lst2.Contains(mdl));
        }

        [Test]
        public void mock_dal_should_delete_Photographer()
        {
            IDataAccessLayer dal = ueb.GetDataAccessLayer();
            AssertNotNull("GetDataAccessLayer", dal);

            IEnumerable<IPhotographerModel> lst = dal.GetPhotographers();

            AssertNotNull("dal.GetPhotographers", lst);
            AssertTrue("dal.GetPhotographers returned nothing", lst.Count() > 0);

            IPhotographerModel mdl = lst.First();
            dal.DeletePhotographer(mdl.ID);
            IEnumerable<IPhotographerModel> lst2 = dal.GetPhotographers();

            AssertFalse("Photographer was not deleted", lst2.Contains(mdl));
        }

        [Test]
        public void mock_dal_should_return_fake_Camera_data()
        {
            IDataAccessLayer dal = ueb.GetDataAccessLayer();
            AssertNotNull("GetDataAccessLayer", dal);

            IEnumerable<ICameraModel> lst = dal.GetCameras();

            AssertNotNull("dal.GetCameras", lst);
            AssertTrue("dal.GetCameras returned nothing", lst.Count() > 0);
        }

        [Test]
        public void mock_dal_should_return_fake_Camera_data_by_ID()
        {
            IDataAccessLayer dal = ueb.GetDataAccessLayer();
            AssertNotNull("GetDataAccessLayer", dal);

            ICameraModel obj = dal.GetCamera(1234);
            AssertNotNull("dal.GetCamera", obj);
        }
        #endregion

        #region Business Layer
        [Test]
        public void bl_should_return_Pictures()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IEnumerable<IPictureModel> lst = bl.GetPictures(null, null, null, null);

            AssertNotNull("bl.GetPictures", lst);
            AssertTrue("bl.GetPictures returned nothing", lst.Count() > 0);
        }

        [Test]
        public void bl_should_return_Picture_by_ID()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IPictureModel obj = bl.GetPicture(1234);
            AssertNotNull("bl.GetPicture", obj);
        }

        [Test]
        public void bl_should_return_Photographers()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IEnumerable<IPhotographerModel> lst = bl.GetPhotographers();

            AssertNotNull("bl.GetPhotographers", lst);
            AssertTrue("bl.GetPhotographers returned nothing", lst.Count() > 0);
        }

        [Test]
        public void bl_should_return_Photographer_by_ID()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IPhotographerModel obj = bl.GetPhotographer(1234);
            AssertNotNull("bl.GetPhotographer", obj);
        }

        [Test]
        public void bl_should_simulate_EXIF_extraction()
        {
            EnsureTestImages();

            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IEXIFModel mdl = bl.ExtractEXIF("Img1.jpg");

            AssertNotNull("bl.ExtractEXIF", mdl);
            AssertTrue("ExposureTime != 0", mdl.ExposureTime != 0);
            AssertTrue("FNumber != 0", mdl.FNumber != 0);
            AssertTrue("ISOValue != 0", mdl.ISOValue != 0);
            AssertNotEmptyOrNull(mdl.Make);
        }

        [Test]
        public void bl_should_return_Cameras()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IEnumerable<ICameraModel> lst = bl.GetCameras();

            AssertNotNull("bl.GetCameras", lst);
            AssertTrue("bl.GetCameras returned nothing", lst.Count() > 0);
        }

        [Test]
        public void bl_should_return_Camera_by_ID()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            ICameraModel obj = bl.GetCamera(1234);
            AssertNotNull("bl.GetCamera", obj);
        }

        #endregion

        #region SearchViewModel
        [Test]
        public void SearchViewModel_should_reflect_active_search()
        {
            ISearchViewModel vmdl = ueb.GetSearchViewModel();
            AssertTrue("IsActive == false", vmdl.IsActive == false);
            vmdl.SearchText = "Hallo";
            AssertTrue("IsActive == true", vmdl.IsActive == true);
        }

        [Test]
        public void SearchViewModel_should_reflect_active_search_2()
        {
            ISearchViewModel vmdl = ueb.GetSearchViewModel();
            AssertTrue("IsActive == false", vmdl.IsActive == false);
            vmdl.SearchText = "Hallo";
            AssertTrue("IsActive == true", vmdl.IsActive == true);
            vmdl.SearchText = "";
            AssertTrue("IsActive == false", vmdl.IsActive == false);
        }

        [Test]
        public void SearchViewModel_should_reflect_search()
        {
            ISearchViewModel vmdl = ueb.GetSearchViewModel();
            AssertEmptyOrNull(vmdl.SearchText);
            vmdl.SearchText = "Hallo";
            AssertEquals("Hallo", vmdl.SearchText);
        }

        [Test]
        public void SearchViewModel_should_reflect_search_2()
        {
            ISearchViewModel vmdl = ueb.GetSearchViewModel();
            AssertEmptyOrNull(vmdl.SearchText);
            vmdl.SearchText = "Hallo";
            AssertEquals("Hallo", vmdl.SearchText);
            vmdl.SearchText = "";
            AssertEmptyOrNull(vmdl.SearchText);
        }
        #endregion

        #region Using log4net
        // This is not possible as log4j does not allow programmatically configuration
        #endregion
    }
}
