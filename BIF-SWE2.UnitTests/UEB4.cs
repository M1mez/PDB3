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
    public class UEB4 : AbstractTestFixture<IUEB4>
    {
        private IUEB4 ueb;
        public override void TestSetup()
        {
            base.TestSetup();

            IUEB4 ueb = CreateInstance();
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

        #region IPTC & EXIF Handling
        [Test]
        public void bl_should_extract_IPTC_Info_from_existing_picture()
        {
            EnsureTestImages();

            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IIPTCModel iptc = bl.ExtractIPTC("Img1.jpg");
            AssertNotNull("ExtractIPTC", iptc);
        }

        [Test]
        public void bl_should_extract_fake_IPTC_Info_from_existing_picture()
        {
            EnsureTestImages();

            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            IIPTCModel iptc = bl.ExtractIPTC("Img1.jpg");
            AssertNotNull("ExtractIPTC", iptc);
            AssertNotEmptyOrNull(iptc.ByLine);
            AssertNotEmptyOrNull(iptc.Caption);
            AssertNotEmptyOrNull(iptc.CopyrightNotice);
            AssertNotEmptyOrNull(iptc.Headline);
            AssertNotEmptyOrNull(iptc.Keywords);
        }

        [Test]
        public void bl_should_fail_extracting_IPTC_Info_from_missing_picture()
        {
            EnsureTestImages();

            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            Assert.That(() => bl.ExtractIPTC("Missing_Imgage.jpg"), Throws.Exception);
        }

        [Test]
        public void bl_should_extract_fake_EXIF_Info_from_missing_picture()
        {
            EnsureTestImages();

            IBusinessLayer bl = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", bl);

            Assert.That(() => bl.ExtractEXIF("Missing_Imgage.jpg"), Throws.Exception);
        }
        #endregion

        #region EXIFViewModel
        [Test]
        public void EXIFViewModel_should_reflect_Model()
        {
            IEXIFModel mdl = ueb.GetEmptyEXIFModel();
            AssertNotNull("GetEmptyEXIFModel", mdl);

            mdl.Make = "Canon";
            mdl.ISOValue = 200;
            mdl.FNumber = 8;
            mdl.ExposureTime = 0.008m;

            IEXIFViewModel vmdl = ueb.GetEXIFViewModel(mdl);
            AssertNotNull("GetEXIFViewModel", vmdl);
            AssertEquals("Canon", vmdl.Make);
            AssertEquals(200, vmdl.ISOValue);
            AssertEquals(8, vmdl.FNumber);
            AssertEquals(0.008m, vmdl.ExposureTime);
        }

        [Test]
        public void EXIFViewModel_should_return_ExposureProgramResource()
        {
            IEXIFModel mdl = ueb.GetEmptyEXIFModel();
            AssertNotNull("GetEmptyEXIFModel", mdl);

            mdl.ExposureProgram = ExposurePrograms.Manual;

            IEXIFViewModel vmdl = ueb.GetEXIFViewModel(mdl);
            AssertNotNull("GetEXIFViewModel", vmdl);

            AssertNotEmptyOrNull(vmdl.ExposureProgramResource);
        }
        #endregion

        #region Challenge 4
        [Test]
        public void CameraViewModel_should_reflect_Model()
        {
            ICameraModel mdl = ueb.GetCameraModel("Canon", "EOS 80D");
            AssertNotNull("GetCameraModel", mdl);
            mdl.ISOLimitGood = 400;
            mdl.ISOLimitAcceptable = 800;

            ICameraViewModel vmdl = ueb.GetCameraViewModel(mdl);

            AssertNotNull("GetCameraViewModel", vmdl);
            AssertEquals(400, vmdl.ISOLimitGood);
            AssertEquals(800, vmdl.ISOLimitAcceptable);
        }

        private IEXIFViewModel SetupISORatingTest(decimal iso, bool setupCamera = true)
        {
            IEXIFModel exif = ueb.GetEmptyEXIFModel();
            AssertNotNull("GetEmptyEXIFModel", exif);

            exif.Make = "Canon";
            exif.ISOValue = iso;
            exif.FNumber = 8;
            exif.ExposureTime = 0.008m;

            IEXIFViewModel vmdl = ueb.GetEXIFViewModel(exif);
            AssertNotNull("GetEXIFViewModel", vmdl);

            if (setupCamera)
            {
                ICameraModel camera = ueb.GetCameraModel("Canon", "EOS 80D");
                AssertNotNull("GetCameraModel", camera);
                camera.ISOLimitGood = 400;
                camera.ISOLimitAcceptable = 800;
                vmdl.Camera = ueb.GetCameraViewModel(camera);
            }
            return vmdl;
        }

        [Test]
        public void ExifViewModel_should_reflect_ISORating_200()
        {
            IEXIFViewModel vmdl = SetupISORatingTest(200);
            AssertEquals(ISORatings.Good, vmdl.ISORating);
        }
        [Test]
        public void ExifViewModel_should_reflect_ISORating_400()
        {
            IEXIFViewModel vmdl = SetupISORatingTest(400);
            AssertEquals(ISORatings.Good, vmdl.ISORating);
        }
        [Test]
        public void ExifViewModel_should_reflect_ISORating_800()
        {
            IEXIFViewModel vmdl = SetupISORatingTest(800);
            AssertEquals(ISORatings.Acceptable, vmdl.ISORating);
        }
        [Test]
        public void ExifViewModel_should_reflect_ISORating_1600()
        {
            IEXIFViewModel vmdl = SetupISORatingTest(1600);
            AssertEquals(ISORatings.Noisey, vmdl.ISORating);
        }
        [Test]
        public void ExifViewModel_should_reflect_ISORating_0()
        {
            IEXIFViewModel vmdl = SetupISORatingTest(0);
            AssertEquals(ISORatings.NotDefined, vmdl.ISORating);
        }
        [Test]
        public void ExifViewModel_should_reflect_ISORating_without_camera()
        {
            IEXIFViewModel vmdl = SetupISORatingTest(0, false);
            AssertEquals(ISORatings.NotDefined, vmdl.ISORating);
        }
        #endregion

        #region IPTCViewModel
        [Test]
        public void IPTCViewModel_should_reflect_Model()
        {
            IIPTCModel mdl = ueb.GetEmptyIPTCModel();
            AssertNotNull("GetEmptyIPTCModel", mdl);

            mdl.ByLine = "me";
            mdl.Caption = "Very cool!";
            mdl.CopyrightNotice = "all rights reverved";
            mdl.Headline = "Cool!";
            mdl.Keywords = "cool, nice, great";

            IIPTCViewModel vmdl = ueb.GetIPTCViewModel(mdl);
            AssertNotNull("GetIPTCViewModel", vmdl);
            AssertEquals("me", vmdl.ByLine);
            AssertEquals("Very cool!", vmdl.Caption);
            AssertEquals("all rights reverved", vmdl.CopyrightNotice);
            AssertEquals("Cool!", vmdl.Headline);
            AssertEquals("cool, nice, great", vmdl.Keywords);
        }

        [Test]
        public void IPTCViewModel_should_return_CopyrightNotices()
        {
            IIPTCModel mdl = ueb.GetEmptyIPTCModel();
            AssertNotNull("GetEmptyIPTCModel", mdl);

            IIPTCViewModel vmdl = ueb.GetIPTCViewModel(mdl);
            AssertNotNull("GetIPTCViewModel", vmdl);

            AssertNotNull("GetIPTCViewModel.CopyrightNotices", vmdl.CopyrightNotices);
            AssertTrue("GetIPTCViewModel.CopyrightNotices.Count()  > 2", vmdl.CopyrightNotices.Count() > 2);
        }
        #endregion
    }
}
