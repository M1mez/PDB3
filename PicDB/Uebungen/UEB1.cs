using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB;
using PicDB.Classes;
using PicDB.Layers_DA;
using PicDB.Models;
using PicDB.ViewModels;

namespace Uebungen
{
    public class UEB1 : IUEB1
    {
        public IApplication GetApplication()
        {
            return new App();
        }

        public void HelloWorld()
        {
            // I'm fine
        }

        public IDataAccessLayer GetAnyDataAccessLayer()
        {
            return DataAccessLayer.Instance;
        }

        public IBusinessLayer GetBusinessLayer()
        {
            return BusinessLayer.Instance;
        }

        public BIF.SWE2.Interfaces.Models.IEXIFModel GetEmptyEXIFModel()
        {
            return new EXIFModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.IEXIFViewModel GetEmptyEXIFViewModel()
        {
            return new EXIFViewModel();
        }

        public BIF.SWE2.Interfaces.Models.IIPTCModel GetEmptyIPTCModel()
        {
            return new IPTCModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.IIPTCViewModel GetEmptyIPTCViewModel()
        {
            return new IPTCViewModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.IMainWindowViewModel GetEmptyMainWindowViewModel()
        {
            return new MainWindowViewModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.IPhotographerListViewModel GetEmptyPhotographerListViewModel()
        {
            return new PhotographerListViewModel();
        }

        public BIF.SWE2.Interfaces.Models.IPhotographerModel GetEmptyPhotographerModel()
        {
            return new PhotographerModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.IPhotographerViewModel GetEmptyPhotographerViewModel()
        {
            return new PhotographerViewModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.IPictureListViewModel GetEmptyPictureListViewModel()
        {
            return new PictureListViewModel();
        }

        public BIF.SWE2.Interfaces.Models.IPictureModel GetEmptyPictureModel()
        {
            return new PictureModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.IPictureViewModel GetEmptyPictureViewModel()
        {
            return new PictureViewModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.ISearchViewModel GetEmptySearchViewModel()
        {
            return new SearchViewModel();
        }

        public void TestSetup(string picturePath)
        {
            
        }

        public ICameraModel GetEmptyCameraModel()
        {
            return new CameraModel();
        }

        public ICameraListViewModel GetEmptyCameraListViewModel()
        {
            return new CameraListViewModel();
        }

        public ICameraViewModel GetEmptyCameraViewModel()
        {
            return new CameraViewModel();
        }
    }
}
