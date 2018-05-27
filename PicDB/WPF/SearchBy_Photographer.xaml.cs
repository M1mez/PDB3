using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicDB.WPF
{
    /// <summary>
    /// Interaction logic for SearchBy_Photographer.xaml
    /// </summary>
    public partial class SearchBy_Photographer : UserControl
    {
        private static log4net.ILog log => FileInformation.Logger;

        public SearchBy_Photographer()
        {
            DataContextChanged += SearchBy_Photographer_DataContextChanged;
            InitializeComponent();
        }

        private void SearchBy_Photographer_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            log.Debug("newval" + e.NewValue.GetType());
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
