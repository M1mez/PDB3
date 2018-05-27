using System.Linq;
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
    public partial class PhotographerWindow_Edit : Window
    {
        public PhotographerWindow_Edit(MainWindow sender, BusinessLayer BL)
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
        public PhotographerListViewModel PhotographerList => Sender.PhotographerList;

        public void EditPhotographer(object sender, RoutedEventArgs e)
        {
            var mdl = new PhotographerModel
            {
                ID = PhotographerList.CurrentPhotographer.ID,
                FirstName = _FirstName.Text,
                LastName = _LastName.Text,
                BirthDay = _BirthDay.SelectedDate,
                Notes = _Notes.Text
            };

            var vmdl = new PhotographerViewModel(mdl);

            if (!vmdl.IsValid)
                MessageBoxEx.Show(vmdl.ValidationSummary, "Edit Photographer", MessageBoxButton.OK,
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
                    Sender.UpdatePhotographerList();
                    Close();
                }
        }

        public void DeletePhotographer(object sender, RoutedEventArgs e)
        {
            if (PhotographerList.CurrentPhotographer != null)
            {
                MessageBoxResult messageBoxResult = MessageBoxEx.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        BL.DeletePhotographer(PhotographerList.CurrentPhotographer.ID);
                        Sender.UpdatePictureList();
                        Sender.UpdatePhotographerList();
                        PhotographerList.CurrentPhotographer = null;
                    }
                    catch (System.Exception)
                    {
                        //show error
                        throw;
                    }
                    finally
                    {
                        Sender.UpdatePhotographerList();
                        Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("No Photographer selected", "Delete Photographer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
