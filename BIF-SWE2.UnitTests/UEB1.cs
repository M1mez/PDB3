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
    public class UEB1 : AbstractTestFixture<IUEB1>
    {
        private IUEB1 ueb;
        #region Test setup

        public override void TestSetup()
        {
            base.TestSetup();

            IUEB1 ueb = CreateInstance();
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

        [Test]
        public void Hello_Application()
        {
            IApplication obj = ueb.GetApplication();
            AssertNotNull("GetApplication", obj);

            obj.HelloWorld();
        }
        #endregion

        #region Check existence of all model classes
        [Test]
        public void Hello_Empty_PictureModel()
        {
            IPictureModel obj = ueb.GetEmptyPictureModel();
            AssertNotNull("GetEmptyPictureModel", obj);
        }
        [Test]
        public void Hello_Empty_PhotographerModel()
        {
            IPhotographerModel obj = ueb.GetEmptyPhotographerModel();
            AssertNotNull("GetEmptyPhotographerModel", obj);
        }
        [Test]
        public void Hello_Empty_IPTCModel()
        {
            IIPTCModel obj = ueb.GetEmptyIPTCModel();
            AssertNotNull("GetEmptyIPTCModel", obj);
        }
        [Test]
        public void Hello_Empty_EXIFModel()
        {
            IEXIFModel obj = ueb.GetEmptyEXIFModel();
            AssertNotNull("GetEmptyEXIFModel", obj);
        }
        #endregion

        #region Check existence of all view model classes
        [Test]
        public void Hello_Empty_MainWindowViewModel()
        {
            IMainWindowViewModel obj = ueb.GetEmptyMainWindowViewModel();
            AssertNotNull("GetEmptyMainWindowViewModel", obj);
        }
        [Test]
        public void Hello_Empty_SearchViewModelViewModel()
        {
            ISearchViewModel obj = ueb.GetEmptySearchViewModel();
            AssertNotNull("GetEmptySearchViewModel", obj);
        }
        [Test]
        public void Hello_Empty_PictureListViewModel()
        {
            IPictureListViewModel obj = ueb.GetEmptyPictureListViewModel();
            AssertNotNull("GetEmptyPictureListViewModel", obj);
        }
        [Test]
        public void Hello_Empty_PictureViewModel()
        {
            IPictureViewModel obj = ueb.GetEmptyPictureViewModel();
            AssertNotNull("GetEmptyPictureViewModel", obj);
        }
        [Test]
        public void Hello_Empty_IPTCViewModel()
        {
            IIPTCViewModel obj = ueb.GetEmptyIPTCViewModel();
            AssertNotNull("GetEmptyIPTCViewModel", obj);
        }
        [Test]
        public void Hello_Empty_EXIFViewModel()
        {
            IEXIFViewModel obj = ueb.GetEmptyEXIFViewModel();
            AssertNotNull("GetEmptyEXIFViewModel", obj);
        }
        [Test]
        public void Hello_Empty_PhotographerListViewModel()
        {
            IPhotographerListViewModel obj = ueb.GetEmptyPhotographerListViewModel();
            AssertNotNull("GetEmptyPhotographerListViewModel", obj);
        }
        [Test]
        public void Hello_Empty_PhotographerViewModel()
        {
            IPhotographerViewModel obj = ueb.GetEmptyPhotographerViewModel();
            AssertNotNull("GetEmptyPhotographerViewModel", obj);
        }
        #endregion

        #region Check existence of BL & DAL classes
        [Test]
        public void Hello_BusinessLayer()
        {
            IBusinessLayer obj = ueb.GetBusinessLayer();
            AssertNotNull("GetBusinessLayer", obj);
        }

        [Test]
        public void Hello_Any_DataAccessLayer()
        {
            IDataAccessLayer obj = ueb.GetAnyDataAccessLayer();
            AssertNotNull("GetAnyDataAccessLayer", obj);
        }
        #endregion

        #region Challenge 1
        [Test]
        public void Hello_Empty_CameraModel()
        {
            ICameraModel obj = ueb.GetEmptyCameraModel();
            AssertNotNull("GetEmptyCameraModel", obj);
        }
        [Test]
        public void Hello_Empty_CameraListViewModel()
        {
            ICameraListViewModel obj = ueb.GetEmptyCameraListViewModel();
            AssertNotNull("GetEmptyCameraListViewModel", obj);
        }
        [Test]
        public void Hello_Empty_CameraViewModel()
        {
            ICameraViewModel obj = ueb.GetEmptyCameraViewModel();
            AssertNotNull("GetEmptyCameraViewModel", obj);
        }
        #endregion
    }
}
