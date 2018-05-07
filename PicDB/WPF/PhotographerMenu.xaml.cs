using PicDB.Classes;
using PicDB.Models;
using PicDB.ViewModels;
using System.Windows;

namespace PicDB
{
    public partial class Photographer : Window
    {
        BusinessLayer bl = new BusinessLayer();

        public Photographer()
        {
            InitializeComponent();
        }

        public void AddPhotographer(object sender, RoutedEventArgs e)
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
                bl.Save(mdl);
                Close();
            }
            else
            {
                MessageBox.Show(vmdl.ValidationSummary, "Add Photographer", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
