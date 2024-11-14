using BarangKu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using BarangKu.Services;

namespace BarangKu.Views
{
    public partial class EditProfileView : UserControl
    {
        private readonly DatabaseService _databaseService = new DatabaseService();
        private byte[] _profileImageData; 
        private readonly int _userId = 1; 
        public EditProfileView()
        {
            InitializeComponent();
            DataContext = new EditProfileViewModel();

        }

        private void NavigateToProfileView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow.DataContext as NavigationServices;
            navigationService?.NavigateToProfileView();
        }

        private void RoundedButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Data telah tersimpan");
        }
    }
}
