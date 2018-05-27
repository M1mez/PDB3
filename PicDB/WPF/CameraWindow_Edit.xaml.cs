﻿using System;
using PicDB.Classes;
using PicDB.Models;
using PicDB.ViewModels;
using PicDB;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Helper;

namespace PicDB
{
    public partial class CameraWindow_Edit : Window
    {
        public CameraWindow_Edit(MainWindow sender, BusinessLayer BL)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            if (mainWindow != null)
            {
                this.Left = mainWindow.Left + (mainWindow.ActualWidth - this.ActualWidth) / 2;
                this.Top = mainWindow.Top + (mainWindow.ActualWidth - this.ActualHeight) / 2;
            }

            this.SizeToContent = SizeToContent.Height;
            this.Sender = sender;
            this.BL = BL;

            InitializeComponent();
        }

        private MainWindow Sender { get; }
        private BusinessLayer BL { get; }
        public CameraListViewModel CameraList => Sender.CameraList;

        private void TextBox_PreviewTextInputDecimal(object sender, TextCompositionEventArgs e)
        {
            //check for decimal input
            e.Handled = !Constants.dec.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        public void CreateCamera(object sender, RoutedEventArgs e)
        {
            
            var mdl = new CameraModel
            {
                Producer = _Producer.Text,
                Make = _Make.Text,
                BoughtOn = _BoughtOn.SelectedDate,
                Notes = _Notes.Text,
                ISOLimitAcceptable = Decimal.Parse(_ISOLimitAcceptable.Text),
                ISOLimitGood = Decimal.Parse(_ISOLimitGood.Text)
            };

            var vmdl = new CameraViewModel(mdl);

            if(vmdl.IsValid)
            {
                try
                {
                    BL.Save(mdl);
                }
                catch (System.Exception)
                {
                    //show error
                    throw;
                } finally
                {
                    Sender.UpdateCameraList();
                    Close();
                }
            }
            else
            {
                MessageBox.Show(vmdl.ValidationSummary, "Add Camera", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void EditCamera(object sender, RoutedEventArgs e)
        {
            var mdl = new CameraModel
            {
                ID = CameraList.CurrentCamera.ID,
                Producer = _Producer.Text,
                Make = _Make.Text,
                BoughtOn = _BoughtOn.SelectedDate,
                Notes = _Notes.Text,
                ISOLimitAcceptable = Decimal.Parse(_ISOLimitAcceptable.Text),
                ISOLimitGood = Decimal.Parse(_ISOLimitGood.Text)
            };

            var vmdl = new CameraViewModel(mdl);

            if (!vmdl.IsValid)
                MessageBoxEx.Show(vmdl.ValidationSummary, "Edit Camera", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            else
                try
                {
                    BL.Update(mdl);
                }
                catch (System.Exception)
                {
                    //show error
                    throw;
                }
                finally
                {
                    Sender.UpdateCameraList();
                    Close();
                }
        }

        public void DeleteCamera(object sender, RoutedEventArgs e)
        {
            if (CameraList.CurrentCamera != null)
            {
                MessageBoxResult messageBoxResult = MessageBoxEx.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        BL.DeleteCamera(CameraList.CurrentCamera.ID);
                        Sender.UpdatePictureList();
                        Sender.UpdateCameraList();
                        CameraList.CurrentCamera = null;
                    }
                    catch (System.Exception)
                    {
                        //show error
                        throw;
                    }
                    finally
                    {
                        Sender.UpdateCameraList();
                        Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("No Camera selected", "Delete Camera", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
