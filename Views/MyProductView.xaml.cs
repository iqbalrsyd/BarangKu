using BarangKu.Models;
using BarangKu.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace BarangKu.Views
{
    /// <summary>
    /// Interaction logic for MyProductView.xaml
    /// </summary>
    public partial class MyProductView : UserControl
    {
        private BitmapImage _selectedImage;
        public MyProductView()
        {
            InitializeComponent();
            DataContext = new Category();
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            

            string name = NameTextBox.Text;
            string description = DescriptionTextBox.Text;
            int stock = int.Parse(StockTextBox.Text);
            int duration;
            decimal price = decimal.Parse(PriceTextBox.Text);

            int categoryId = 0;
            if (CategoryComboBox.SelectedValue != null)
            {
                categoryId = Convert.ToInt32(CategoryComboBox.SelectedValue);
            }

            byte[] imageBytes = _selectedImage != null ? ImageToByteArray(_selectedImage) : null;

            ProductViewModel productViewModel;

            if (categoryId == 1) // Barang Baru
            {
                duration = 0;
                productViewModel = new NewProductModel();
            }
            else if (categoryId == 2) // Preloved
            {
                duration = int.Parse(DurationTextBox.Text);
                productViewModel = new PrelovedProductModel();
            }
            else
            {
                MessageBox.Show("Kategori tidak valid.");
                return;
            }

            ProductViewModel product = new ProductViewModel();

            Product newProduct = product.AddProduct(categoryId, name, description, price, stock, duration, imageBytes);

            if (newProduct != null)
            {
                MessageBox.Show("Produk berhasil disimpan.");
            }
            else
            {
                MessageBox.Show("Gagal menyimpan produk.");
            }

        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _selectedImage = new BitmapImage(new Uri(openFileDialog.FileName));

                ImagePreview.Source = _selectedImage;
            }
        }

        private byte[] ImageToByteArray(BitmapImage image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
