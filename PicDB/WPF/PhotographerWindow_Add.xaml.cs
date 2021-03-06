﻿using PicDB.Classes;
using PicDB.Models;
using PicDB.ViewModels;
using PicDB;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace PicDB
{
    public partial class PhotographerWindow_Add : Window
    {
        public PhotographerWindow_Add(MainWindow sender, BusinessLayer BL)
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

        public void CreatePhotographer(object sender, RoutedEventArgs e)
        {
            var mdl = new PhotographerModel
            {
                FirstName = _FirstName.Text,
                LastName = _LastName.Text,
                Notes = _Notes.Text,
                BirthDay = _BirthDay.SelectedDate
            };

            var vmdl = new PhotographerViewModel(mdl);

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
                    Sender.UpdatePhotographerList();
                    Close();
                }
            }
            else
            {
                MessageBox.Show(vmdl.ValidationSummary, "Add Photographer", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
