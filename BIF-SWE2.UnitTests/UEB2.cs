using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using System.Text.RegularExpressions;
using System.IO;

namespace BIF.SWE2.UnitTests
{
    [TestFixture]
    public class UEB2 : AbstractTestFixture<IUEB2>
    {
        private IUEB2 ueb;
        public override void TestSetup()
        {
            base.TestSetup();

            IUEB2 ueb = CreateInstance();
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

        #region MainWindowViewModel
        [Test]
        public void MainWindowViewModel_should_return_SearchViewModel()
        {
            IMainWindowViewModel vmdl = ueb.GetMainWindowViewModel();
            AssertNotNull("GetMainWindowViewModel", vmdl);
            AssertNotNull("vmdl.Search", vmdl.Search);
        }
        [Test]
        public void MainWindowViewModel_should_return_ListViewModel()
        {
            IMainWindowViewModel vmdl = ueb.GetMainWindowViewModel();
            AssertNotNull("GetMainWindowViewModel", vmdl);
            AssertNotNull("vmdl.List", vmdl.List);
        }
        [Test]
        public void MainWindowViewModel_should_return_CurrentPictureViewModel()
        {
            IMainWindowViewModel vmdl = ueb.GetMainWindowViewModel();
            AssertNotNull("GetMainWindowViewModel", vmdl);
            // May return null, but must not fail
            var p = vmdl.CurrentPicture;
            Log("{0}", p != null ? "current picture available" : "no current picture");
        }
        #endregion

        #region PictureViewModel
        [Test]
        public void PictureViewModel_should_reflect_Model()
        {
            IPictureModel mdl = ueb.GetPictureModel("test.jpg");
            AssertNotNull("GetPictureModel", mdl);
            IPictureViewModel vmdl = ueb.GetPictureViewModel(mdl);
            AssertNotNull("GetPictureViewModel", vmdl);
            AssertEquals("test.jpg", vmdl.FileName);
        }

        [Test]
        public void PictureViewModel_should_return_EXIFViewModel()
        {
            IPictureModel mdl = ueb.GetPictureModel("test.jpg");
            AssertNotNull("GetPictureModel", mdl);
            IPictureViewModel vmdl = ueb.GetPictureViewModel(mdl);
            AssertNotNull("GetPictureViewModel", vmdl);
            AssertNotNull("vmdl.EXIF", vmdl.EXIF);
        }

        [Test]
        public void PictureViewModel_should_return_IPTCViewModel()
        {
            IPictureModel mdl = ueb.GetPictureModel("test.jpg");
            AssertNotNull("GetPictureModel", mdl);
            IPictureViewModel vmdl = ueb.GetPictureViewModel(mdl);
            AssertNotNull("GetPictureViewModel", vmdl);
            AssertNotNull("vmdl.IPTC", vmdl.IPTC);
        }

        [Test]
        public void PictureViewModel_should_return_DisplayName()
        {
            IPictureModel mdl = ueb.GetPictureModel("test.jpg");
            AssertNotNull("GetPictureModel", mdl);
            IPictureViewModel vmdl = ueb.GetPictureViewModel(mdl);
            AssertNotNull("GetPictureViewModel", vmdl);
            AssertTrue("DisplayName ~ ^.* (by .*)$", new Regex("^.* \\(by .*\\)$").Match(vmdl.DisplayName).Success);
        }

        [Test]
        public void PictureViewModel_should_reflect_Camera()
        {
            IPictureModel mdl = ueb.GetPictureModel("test.jpg");
            AssertNotNull("GetPictureModel", mdl);
            IPictureViewModel vmdl = ueb.GetPictureViewModel(mdl);
            AssertNotNull("GetPictureViewModel", vmdl);

            ICameraModel c_mdl = ueb.GetCameraModel("Canon", "EOS 20D");
            AssertNotNull("GetCameraModel", c_mdl);

            mdl.Camera = c_mdl;
            AssertNotNull("vmdl.Camera", vmdl.Camera);
            AssertEquals("Canon", mdl.Camera.Producer);
            AssertEquals("EOS 20D", mdl.Camera.Make);
        }
        #endregion

        #region PictureModel
        [Test]
        public void PictureModel_should_reflect_Camera()
        {
            IPictureModel mdl = ueb.GetPictureModel("test.jpg");
            AssertNotNull("GetPictureModel", mdl);
            ICameraModel c = ueb.GetCameraModel("Canon", "EOS 20D");
            AssertNotNull("GetCameraModel", c);
            mdl.Camera = c;
            AssertNotNull("mdl.Camera", mdl.Camera);
            AssertEquals(c, mdl.Camera);
        }
        #endregion

        #region CameraViewModel
        [Test]
        public void CameraViewModel_should_reflect_Model()
        {
            ICameraModel mdl = ueb.GetCameraModel("Canon", "EOS 80D");
            AssertNotNull("GetCameraModel", mdl);
            ICameraViewModel vmdl = ueb.GetCameraViewModel(mdl);
            AssertNotNull("GetCameraViewModel", vmdl);
            AssertEquals("Canon", vmdl.Producer);
            AssertEquals("EOS 80D", vmdl.Make);
        }
        #endregion

        #region Basic Business Layer
        [Test]
        public void BL_should_sync()
        {
            EnsureTestImages();

            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            bl.Sync();

            AssertEquals(Constants.PictureCount, bl.GetPictures().Count());
        }
        #endregion
    }
}
