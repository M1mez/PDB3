using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;

namespace BIF.SWE2.UnitTests
{
    [TestFixture]
    public class UEB6 : AbstractTestFixture<IUEB6>
    {
        private IUEB6 ueb;
        public override void TestSetup()
        {
            base.TestSetup();

            IUEB6 ueb = CreateInstance();
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

        #region Pictures
        /// <summary>
        /// Your mock DAL must include exact one file "Blume.jpg"
        /// </summary>
        [Test]       
        public void bl_should_search_Pictures()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IEnumerable<IPictureModel> lst = bl.GetPictures("blume", null, null, null);

            AssertNotNull("bl.GetPictures", lst);
            AssertEquals("bl.GetPictures().count != 1", 1, lst.Count());
        }

        [Test]
        public void bl_should_save_Picture()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IPictureModel obj = ueb.GetEmptyPictureModel();
            AssertNotNull("GetEmptyPictureModel", obj);

            String fileName = "New_Test" + new Random().Next(100) + ".jpg";
            obj.FileName = fileName;

            bl.Save(obj);

            IEnumerable<IPictureModel> lst = bl.GetPictures(null, null, null, null);
            AssertNotNull("bl.GetPictures", lst);
            AssertEquals("bl.GetPictures().count == 1", 1, lst.Count(p => p.FileName == fileName));
        }

        [Test]
        public void bl_should_save_Picture_and_set_new_id()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IPictureModel obj = ueb.GetEmptyPictureModel();
            AssertNotNull("GetEmptyPictureModel", obj);

            String fileName = "New_Test" + new Random().Next(100) + ".jpg";
            obj.FileName = fileName;

            bl.Save(obj);

            AssertTrue("obj.ID > 0", obj.ID > 0);
        }

        [Test]
        public void bl_should_save_Picture_and_set_unique_id()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IPictureModel obj = ueb.GetEmptyPictureModel();
            AssertNotNull("GetEmptyPictureModel", obj);

            String fileName = "New_Test" + new Random().Next(100) + ".jpg";
            obj.FileName = fileName;

            bl.Save(obj);

            IEnumerable<IPictureModel> lst = bl.GetPictures(null, null, null, null);
            AssertNotNull("bl.GetPictures", lst);
            AssertTrue("ID is not unique", lst.GroupBy(k => k.ID).Count(kv => kv.Count() > 1) == 0);
        }

        [Test]
        public void bl_should_get_Picture()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IEnumerable<IPictureModel> lst = bl.GetPictures(null, null, null, null);

            AssertNotNull("bl.GetPictures", lst);
            AssertTrue("bl.GetPictures returned nothing", lst.Count() > 0);

            IPictureModel mdl = lst.First();
            IPictureModel test = bl.GetPicture(mdl.ID);
            AssertNotNull("bl.GetPicture", test);
        }
        #endregion

        #region Photographer
        [Test]
        public void bl_should_save_Photographer()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IPhotographerModel obj = ueb.GetEmptyPhotographerModel();
            AssertNotNull("GetEmptyPictureModel", obj);

            String firstName = "Michael" + new Random().Next(100);
            obj.FirstName = firstName;
            obj.LastName = "Testinger";

            bl.Save(obj);

            IEnumerable<IPhotographerModel> lst = bl.GetPhotographers();
            AssertNotNull("bl.GetPhotographers", lst);
            AssertEquals("bl.GetPhotographers().count == 1", 1, lst.Count(p => p.FirstName == firstName));
        }

        [Test]
        public void bl_should_save_Photographer_and_set_new_id()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IPhotographerModel obj = ueb.GetEmptyPhotographerModel();
            AssertNotNull("GetEmptyPhotographerModel", obj);

            String firstName = "Michael" + new Random().Next(100);
            obj.FirstName = firstName;
            obj.LastName = "Testinger";

            bl.Save(obj);

            AssertTrue("obj.ID > 0", obj.ID > 0);
        }

        [Test]
        public void bl_should_save_Photographer_and_set_unique_id()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IPhotographerModel obj = ueb.GetEmptyPhotographerModel();
            AssertNotNull("GetEmptyPhotographerModel", obj);

            String firstName = "Michael" + new Random().Next(100);
            obj.FirstName = firstName;
            obj.LastName = "Testinger";

            bl.Save(obj);

            IEnumerable<IPhotographerModel> lst = bl.GetPhotographers();
            AssertNotNull("bl.GetPhotographers", lst);
            AssertTrue("ID is not unique", lst.GroupBy(k => k.ID).Count(kv => kv.Count() > 1) == 0);
        }

        [Test]
        public void bl_should_get_Photographer()
        {
            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IEnumerable<IPhotographerModel> lst = bl.GetPhotographers();

            AssertNotNull("bl.GetPhotographers", lst);
            AssertTrue("bl.GetPhotographers returned nothing", lst.Count() > 0);

            IPhotographerModel mdl = lst.First();
            IPhotographerModel test = bl.GetPhotographer(mdl.ID);
            AssertNotNull("bl.GetPhotographer", test);
        }
        #endregion
    }
}
