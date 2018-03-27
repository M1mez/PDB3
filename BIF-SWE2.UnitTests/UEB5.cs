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
    /* Placeholder */
    [TestFixture]
    public class UEB5 : AbstractTestFixture<IUEB5>
    {
        private IUEB5 ueb;
        public override void TestSetup()
        {
            base.TestSetup();

            IUEB5 ueb = CreateInstance();
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

        #region Photographer
        [Test]
        public void PhotographerViewModel_should_reflect_Model()
        {
            IPhotographerModel mdl = ueb.GetEmptyPhotographerModel();
            AssertNotNull("GetEmptyPhotographerModel", mdl);

            var dt = DateTime.Today.AddYears(-36);

            mdl.FirstName = "Bernhard";
            mdl.LastName = "Testinger";
            mdl.BirthDay = dt;

            IPhotographerViewModel vmdl = ueb.GetPhotographerViewModel(mdl);
            AssertNotNull("GetPhotographerViewModel", vmdl);
            AssertEquals("Bernhard", vmdl.FirstName);
            AssertEquals("Testinger", vmdl.LastName);
            AssertEquals(dt, vmdl.BirthDay);
        }

        private IPhotographerViewModel GetViewModel()
        {
            IPhotographerModel mdl = ueb.GetEmptyPhotographerModel();
            AssertNotNull("GetEmptyPhotographerModel", mdl);

            IPhotographerViewModel vmdl = ueb.GetPhotographerViewModel(mdl);
            AssertNotNull("GetPhotographerViewModel", vmdl);

            return vmdl;
        }

        [Test]
        public void PhotographerViewModel_should_validate_LastName()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = "Testinger";
            AssertTrue("IsValidLastName == true", vmdl.IsValidLastName);
        }

        [Test]
        public void PhotographerViewModel_should_fail_validate_on_empty_LastName()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = "";
            AssertFalse("IsValidLastName == false", vmdl.IsValidLastName);
        }

        [Test]
        public void PhotographerViewModel_should_fail_validate_on_null_LastName()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = null;
            AssertFalse("IsValidLastName == false", vmdl.IsValidLastName);
        }

        [Test]
        public void PhotographerViewModel_should_validate_BirthDay()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.BirthDay = DateTime.Today.AddYears(-36);
            AssertTrue("IsValidBirthDay == true", vmdl.IsValidBirthDay);
        }

        [Test]
        public void PhotographerViewModel_should_validate_null_BirthDay()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.BirthDay = null;
            AssertTrue("IsValidBirthDay == true", vmdl.IsValidBirthDay);
        }

        [Test]
        public void PhotographerViewModel_should_fail_validate_future_BirthDay()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.BirthDay = DateTime.Today.AddYears(1);
            AssertFalse("IsValidBirthDay == false", vmdl.IsValidBirthDay);
        }

        [Test]
        public void PhotographerViewModel_should_validate()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = "Gruber";
            vmdl.BirthDay = DateTime.Today.AddYears(-1);
            AssertTrue("IsValid == true", vmdl.IsValid);
        }

        [Test]
        public void PhotographerViewModel_should_validate_2()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = "Gruber";
            vmdl.BirthDay = null;
            AssertTrue("IsValid == true", vmdl.IsValid);
        }

        [Test]
        public void PhotographerViewModel_should_fail_validate()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = "";
            vmdl.BirthDay = DateTime.Today.AddYears(-1);
            AssertFalse("IsValid == false", vmdl.IsValid);
        }

        [Test]
        public void PhotographerViewModel_should_fail_validate_2()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = "Gruber";
            vmdl.BirthDay = DateTime.Today.AddYears(1);
            AssertFalse("IsValid == false", vmdl.IsValid);
        }

        [Test]
        public void PhotographerViewModel_should_fail_with_summary()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = "";
            vmdl.BirthDay = DateTime.Today.AddYears(-1);
            AssertNotEmptyOrNull(vmdl.ValidationSummary);
        }

        [Test]
        public void PhotographerViewModel_should_fail_with_summary_2()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = "Gruber";
            vmdl.BirthDay = DateTime.Today.AddYears(1);
            AssertNotEmptyOrNull(vmdl.ValidationSummary);
        }

        [Test]
        public void PhotographerViewModel_should_fail_with_summary_3()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = "";
            vmdl.BirthDay = DateTime.Today.AddYears(1);
            AssertNotEmptyOrNull(vmdl.ValidationSummary);
        }

        [Test]
        public void PhotographerViewModel_should_return_empty_summary_on_valid_state()
        {
            IPhotographerViewModel vmdl = GetViewModel();
            vmdl.LastName = "Gruber";
            vmdl.BirthDay = DateTime.Today.AddYears(-1);
            AssertEmptyOrNull(vmdl.ValidationSummary);
        }
        #endregion

        #region Challenge
        private ICameraViewModel GetCameraViewModel()
        {
            ICameraModel mdl = ueb.GetEmptyCameraModel();
            AssertNotNull("GetEmptyCameraModel", mdl);

            ICameraViewModel vmdl = ueb.GetCameraViewModel(mdl);
            AssertNotNull("GetCameraViewModel", vmdl);

            return vmdl;
        }

        [Test]
        public void CameraViewModel_should_validate_Producer()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = "Canon";
            AssertTrue("IsValidProducer == true", vmdl.IsValidProducer);
        }

        [Test]
        public void CameraViewModel_should_fail_validate_on_empty_Producer()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = "";
            AssertFalse("IsValidProducer == false", vmdl.IsValidProducer);
        }

        [Test]
        public void CameraViewModel_should_fail_validate_on_null_Producer()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = null;
            AssertFalse("IsValidProducer == false", vmdl.IsValidProducer);
        }

        [Test]
        public void CameraViewModel_should_validate_Make()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Make = "EOS 80D";
            AssertTrue("IsValidMake == true", vmdl.IsValidMake);
        }

        [Test]
        public void CameraViewModel_should_fail_validate_on_empty_Make()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Make = "";
            AssertFalse("IsValidMake == false", vmdl.IsValidMake);
        }

        [Test]
        public void CameraViewModel_should_fail_validate_on_null_Make()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Make = null;
            AssertFalse("IsValidMake == false", vmdl.IsValidMake);
        }

        [Test]
        public void CameraViewModel_should_validate_BoughtOn()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.BoughtOn = DateTime.Today.AddYears(-36);
            AssertTrue("IsValidBoughtOn == true", vmdl.IsValidBoughtOn);
        }

        [Test]
        public void CameraViewModel_should_validate_null_BoughtOn()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.BoughtOn = null;
            AssertTrue("IsValidBoughtOn == true", vmdl.IsValidBoughtOn);
        }

        [Test]
        public void CameraViewModel_should_fail_validate_future_BoughtOn()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.BoughtOn = DateTime.Today.AddYears(1);
            AssertFalse("IsValidBoughtOn == false", vmdl.IsValidBoughtOn);
        }

        [Test]
        public void CameraViewModel_should_validate()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = "Canon";
            vmdl.Make = "EOS 80D";
            vmdl.BoughtOn = DateTime.Today.AddYears(-1);
            AssertTrue("IsValid == true", vmdl.IsValid);
        }

        [Test]
        public void CameraViewModel_should_validate_2()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = "Canon";
            vmdl.Make = "EOS 80D";
            vmdl.BoughtOn = null;
            AssertTrue("IsValid == true", vmdl.IsValid);
        }

        [Test]
        public void CameraViewModel_should_fail_validate()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = "";
            vmdl.Make = "";
            vmdl.BoughtOn = DateTime.Today.AddYears(-1);
            AssertFalse("IsValid == false", vmdl.IsValid);
        }

        [Test]
        public void CameraViewModel_should_fail_validate_2()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = "Canon";
            vmdl.Make = "EOS 80D";
            vmdl.BoughtOn = DateTime.Today.AddYears(1);
            AssertFalse("IsValid == false", vmdl.IsValid);
        }

        [Test]
        public void CameraViewModel_should_fail_with_summary()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = "";
            vmdl.Make = "";
            vmdl.BoughtOn = DateTime.Today.AddYears(-1);
            AssertNotEmptyOrNull(vmdl.ValidationSummary);
        }

        [Test]
        public void CameraViewModel_should_fail_with_summary_2()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = "Canon";
            vmdl.Make = "EOS 80D";
            vmdl.BoughtOn = DateTime.Today.AddYears(1);
            AssertNotEmptyOrNull(vmdl.ValidationSummary);
        }

        [Test]
        public void CameraViewModel_should_fail_with_summary_3()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = "";
            vmdl.Make = "";
            vmdl.BoughtOn = DateTime.Today.AddYears(1);
            AssertNotEmptyOrNull(vmdl.ValidationSummary);
        }

        [Test]
        public void CameraViewModel_should_return_empty_summary_on_valid_state()
        {
            ICameraViewModel vmdl = GetCameraViewModel();
            vmdl.Producer = "Canon";
            vmdl.Make = "EOS 80D";
            vmdl.BoughtOn = DateTime.Today.AddYears(-1);
            AssertEmptyOrNull(vmdl.ValidationSummary);
        }
        #endregion
    }
}
