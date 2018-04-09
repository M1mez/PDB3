using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;

namespace PicDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Gallery gal = new Gallery();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Gallery();;
        }

        private void OpenNewPhotographerWindow(object sender, RoutedEventArgs e)
        {
            Photographer pw = new Photographer
            {
                ShowInTaskbar = false,
                Owner = Application.Current.MainWindow
            };
            pw.Show();
        }
    }
}
