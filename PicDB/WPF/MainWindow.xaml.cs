using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using PicDB.ViewModels;
using PicDB.Classes;

namespace PicDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessLayer bl = BusinessLayer.Instance;

        public MainWindow()
        {
            InitializeComponent();
            bl.Sync();
            this.DataContext = new MainWindowViewModel();
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
