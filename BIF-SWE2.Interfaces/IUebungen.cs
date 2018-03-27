using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;

namespace BIF.SWE2.Interfaces 
{
	public interface IUEB1
	{
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();

        /// <summary>
        /// This method is called before any test is run. Use it to prepare your application for unit-testing.
        /// </summary>
        /// <param name="picturePath"></param>
        void TestSetup(string picturePath);
        /// <summary>
        /// Returns a business layer instance
        /// </summary>
        /// <returns></returns>
        IBusinessLayer GetBusinessLayer();

        /// <summary>
        /// Returns a data access layer instance. It does not matter if it's a real one or the MockDAL for unit-tests.
        /// </summary>
        /// <returns></returns>
        IDataAccessLayer GetAnyDataAccessLayer();

        /// <summary>
        /// Returns a instance of the WPF application class.
        /// </summary>
        /// <returns></returns>
        IApplication GetApplication();

        /// <summary>
        /// Returns a empty model.
        /// </summary>
        /// <returns></returns>
        IPictureModel GetEmptyPictureModel();
        /// <summary>
        /// Returns a empty model.
        /// </summary>
        /// <returns></returns>
        IPhotographerModel GetEmptyPhotographerModel();
        /// <summary>
        /// Returns a empty model.
        /// </summary>
        /// <returns></returns>
        ICameraModel GetEmptyCameraModel();
        /// <summary>
        /// Returns a empty model.
        /// </summary>
        /// <returns></returns>
        IIPTCModel GetEmptyIPTCModel();
        /// <summary>
        /// Returns a empty model.
        /// </summary>
        /// <returns></returns>
        IEXIFModel GetEmptyEXIFModel();

        /// <summary>
        /// Returns a empty ViewModel.
        /// </summary>
        /// <returns></returns>
        IMainWindowViewModel GetEmptyMainWindowViewModel();
        /// <summary>
        /// Returns a empty ViewModel.
        /// </summary>
        /// <returns></returns>
        ISearchViewModel GetEmptySearchViewModel();
        /// <summary>
        /// Returns a empty ViewModel.
        /// </summary>
        /// <returns></returns>
        IPictureListViewModel GetEmptyPictureListViewModel();
        /// <summary>
        /// Returns a empty ViewModel.
        /// </summary>
        /// <returns></returns>
        IPictureViewModel GetEmptyPictureViewModel();
        /// <summary>
        /// Returns a empty ViewModel.
        /// </summary>
        /// <returns></returns>
        IIPTCViewModel GetEmptyIPTCViewModel();
        /// <summary>
        /// Returns a empty ViewModel.
        /// </summary>
        /// <returns></returns>
        IEXIFViewModel GetEmptyEXIFViewModel();
        /// <summary>
        /// Returns a empty ViewModel.
        /// </summary>
        /// <returns></returns>
        IPhotographerListViewModel GetEmptyPhotographerListViewModel();
        /// <summary>
        /// Returns a empty ViewModel.
        /// </summary>
        /// <returns></returns>
        IPhotographerViewModel GetEmptyPhotographerViewModel();
        /// <summary>
        /// Returns a empty ViewModel.
        /// </summary>
        /// <returns></returns>
        ICameraListViewModel GetEmptyCameraListViewModel();
        /// <summary>
        /// Returns a empty ViewModel.
        /// </summary>
        /// <returns></returns>
        ICameraViewModel GetEmptyCameraViewModel();
    }
    public interface IUEB2
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();
        /// <summary>
        /// This method is called before any test is run. Use it to prepare your application for unit-testing.
        /// </summary>
        /// <param name="picturePath"></param>
        void TestSetup(string picturePath);

        /// <summary>
        /// Returns a business layer instance
        /// </summary>
        /// <returns></returns>
        IBusinessLayer GetBusinessLayer();

        /// <summary>
        /// Returns a picture model.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        IPictureModel GetPictureModel(string filename);

        /// <summary>
        /// Returns a picture ViewModel.
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        IPictureViewModel GetPictureViewModel(IPictureModel mdl);

        /// <summary>
        /// Returns the MainWindowViewModel
        /// </summary>
        /// <returns></returns>
        IMainWindowViewModel GetMainWindowViewModel();

        /// <summary>
        /// Returns a camera model
        /// </summary>
        /// <param name="producer"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        ICameraModel GetCameraModel(string producer, string make);

        /// <summary>
        /// Returns a camera view model
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        ICameraViewModel GetCameraViewModel(ICameraModel mdl);
    }
    public interface IUEB3
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();
        /// <summary>
        /// This method is called before any test is run. Use it to prepare your application for unit-testing.
        /// </summary>
        /// <param name="picturePath"></param>
        void TestSetup(string picturePath);
        /// <summary>
        /// Returns a business layer instance
        /// </summary>
        /// <returns></returns>
        IBusinessLayer GetBusinessLayer();

        /// <summary>
        /// Returns a mock data access layer
        /// </summary>
        /// <returns></returns>
        IDataAccessLayer GetDataAccessLayer();

        /// <summary>
        /// Returns a SearchViewModel
        /// </summary>
        /// <returns></returns>
        ISearchViewModel GetSearchViewModel();
    }
    public interface IUEB4
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();
        /// <summary>
        /// This method is called before any test is run. Use it to prepare your application for unit-testing.
        /// </summary>
        /// <param name="picturePath"></param>
        void TestSetup(string picturePath);
        /// <summary>
        /// Returns a business layer instance
        /// </summary>
        /// <returns></returns>
        IBusinessLayer GetBusinessLayer();

        /// <summary>
        /// Returns a empty EXIF model
        /// </summary>
        /// <returns></returns>
        IEXIFModel GetEmptyEXIFModel();

        /// <summary>
        /// Returns a EXIF ViewModel representing the given model
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        IEXIFViewModel GetEXIFViewModel(IEXIFModel mdl);

        /// <summary>
        /// Returns a empty IPTC model
        /// </summary>
        /// <returns></returns>
        IIPTCModel GetEmptyIPTCModel();

        /// <summary>
        /// Returns a IPTC ViewModel representing the given model
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        IIPTCViewModel GetIPTCViewModel(IIPTCModel mdl);

        /// <summary>
        /// Returns a camera model
        /// </summary>
        /// <param name="producer"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        ICameraModel GetCameraModel(string producer, string make);

        /// <summary>
        /// Returns a camera view model
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        ICameraViewModel GetCameraViewModel(ICameraModel mdl);
    }
    public interface IUEB5
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();
        /// <summary>
        /// This method is called before any test is run. Use it to prepare your application for unit-testing.
        /// </summary>
        /// <param name="picturePath"></param>
        void TestSetup(string picturePath);
        /// <summary>
        /// Returns a business layer instance
        /// </summary>
        /// <returns></returns>
        IBusinessLayer GetBusinessLayer();

        /// <summary>
        /// Returns a empty PhotographerModel
        /// </summary>
        /// <returns></returns>
        IPhotographerModel GetEmptyPhotographerModel();

        /// <summary>
        /// Returns a Photographer View Model from the given Model.
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        IPhotographerViewModel GetPhotographerViewModel(IPhotographerModel mdl);

        /// <summary>
        /// Returns a empty CameraModel
        /// </summary>
        /// <returns></returns>
        ICameraModel GetEmptyCameraModel();
        /// <summary>
        /// Returns a Camera View Model from the given Model.
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        ICameraViewModel GetCameraViewModel(ICameraModel mdl);
    }
    public interface IUEB6
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();
        /// <summary>
        /// This method is called before any test is run. Use it to prepare your application for unit-testing.
        /// </summary>
        /// <param name="picturePath"></param>
        void TestSetup(string picturePath);
        /// <summary>
        /// Returns a business layer instance
        /// </summary>
        /// <returns></returns>
        IBusinessLayer GetBusinessLayer();

        /// <summary>
        /// Returns a empty picture model
        /// </summary>
        /// <returns></returns>
        IPictureModel GetEmptyPictureModel();

        /// <summary>
        /// Returns a empty photographer model
        /// </summary>
        /// <returns></returns>
        IPhotographerModel GetEmptyPhotographerModel();
    }
}
