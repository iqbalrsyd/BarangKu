using BarangKu.Models;
using BarangKu.Services;
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
        int productid { get; set; }
        public MyProductView(int productid)
        {
            InitializeComponent();
            DataContext = new MyProductViewModel(productid);
            this.productid = productid;
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            // Mendapatkan nilai saat ini dari StockTextBox
            if (int.TryParse(StockTextBox.Text, out int currentStock))
            {
                // Mengurangi nilai, tetapi menjaga agar tidak kurang dari 1
                currentStock = Math.Max(1, currentStock - 1);
                StockTextBox.Text = currentStock.ToString();
            }
            else
            {
                // Jika parsing gagal, set nilai default ke 1
                StockTextBox.Text = "1";
            }
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            // Mendapatkan nilai saat ini dari StockTextBox
            if (int.TryParse(StockTextBox.Text, out int currentStock))
            {
                // Menambah nilai
                currentStock++;
                StockTextBox.Text = currentStock.ToString();
            }
            else
            {
                // Jika parsing gagal, set nilai default ke 1
                StockTextBox.Text = "1";
            }
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string description = DescriptionTextBox.Text;
            decimal price;
            int stock;
            string duration = string.Empty;

            // Ambil categoryId dari ComboBox
            int categoryId = (int)CategoryComboBox.SelectedValue;

            // Validasi harga
            if (!decimal.TryParse(PriceTextBox.Text, out price) || price <= 0 || price.ToString("F0").Length > 10)
            {
                MessageBox.Show("Harga produk harus valid dan lebih besar dari 0.");
                return;
            }

            // Validasi stok
            if (!int.TryParse(StockTextBox.Text, out stock) || stock < 1)
            {
                MessageBox.Show("Stok harus angka valid dan lebih dari 0.");
                return;
            }

            object productViewModel;


            if (string.IsNullOrEmpty(name))
            {
                duration = "0";
                productViewModel = new NewProductModel();
            }
            else if (categoryId == 2) // Preloved
            {
                duration = "Custom Duration";
                productViewModel = new PrelovedProductModel();
            }
            else
            {
                MessageBox.Show("Kategori tidak valid.");
                return;
            }

            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Deskripsi produk tidak boleh kosong.");
                return;
            }

            // Mengambil nilai stok dari StockTextBox
            if (!int.TryParse(StockTextBox.Text, out stock) || stock < 1)
            {
                MessageBox.Show("Stok harus angka valid dan lebih dari 0.");
                return;
            }

            // Validasi harga, pastikan harga adalah angka yang valid, lebih besar dari 0, dan tidak melebihi 8 digit sebelum desimal dan 2 digit setelah desimal
            if (!decimal.TryParse(PriceTextBox.Text, out price) || price <= 0 || price.ToString("F0").Length > 10)
            {
                MessageBox.Show("Harga produk harus valid dan lebih besar dari 0.");
                return;
            }

            string condition = string.Empty;

            byte[] imageBytes = null;

            // Validate image upload: Check if either _selectedImage or ImagePreview exists
            if (_selectedImage == null && ImagePreview == null)
            {
                MessageBox.Show("Silakan pilih gambar untuk produk.");
                return;
            }

            // If the image is selected by the user (_selectedImage), convert it to byte array
            if (_selectedImage != null)
            {
                imageBytes = ImageToByteArray(_selectedImage);
            }
            // If ImagePreview is not null (image preview exists but not selected by the user), convert it to byte array
            else if (ImagePreview != null)
            {
                // Extract BitmapImage from Image control (ImagePreview is of type System.Windows.Controls.Image)
                var bitmapImage = ImagePreview.Source as BitmapImage;

                // Check if the source is actually a BitmapImage
                if (bitmapImage != null)
                {
                    imageBytes = ImageToByteArray(bitmapImage);
                }
                else
                {
                    MessageBox.Show("Gambar pratinjau tidak valid.");
                    return;
                }
            }


            // Determine product condition and select correct StoreViewModel
            StoreViewModel storeViewModel;
            if (ConditionComboBox.SelectedValue != null)
            {
                // Convert SelectedValue to int
                if (int.TryParse(ConditionComboBox.SelectedValue.ToString(), out int conditionId))
                {
                    if (conditionId == 1) // New Product
                    {
                        condition = "Baru";
                        storeViewModel = new NewProductModel();
                    }
                    else if (conditionId == 2) // Preloved Product
                    {
                        condition = "Preloved";
                        duration = DurationTextBox.Text;

                        if (string.IsNullOrWhiteSpace(duration))
                        {
                            MessageBox.Show("Silakan isi durasi untuk produk Preloved.");
                            return;
                        }

                        storeViewModel = new PrelovedProductModel();
                    }
                    else
                    {
                        MessageBox.Show("Kondisi tidak valid.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Kondisi produk tidak valid.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Pilih kondisi produk terlebih dahulu.");
                return;
            }

            if (productid == 0)
            {
                // Add new product
                StoreViewModel store = new StoreViewModel();
                Product newProduct = store.AddProduct(categoryId, name, description, price, stock, condition, duration, imageBytes);

                if (newProduct != null)
                {
                    MessageBox.Show("Produk berhasil disimpan.");
                    var mainWindow = Window.GetWindow(this) as MainWindow;
                    var navigationService = mainWindow?.DataContext as NavigationServices;
                    navigationService?.NavigateToStoreView();
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan produk.");
                }
            }
            else
            {
                // Update existing product
                MyProductViewModel myProductViewModel = new MyProductViewModel(productid);
                Product updatedProduct = myProductViewModel.EditProduk(productid, categoryId, name, description, price, stock, condition, duration, imageBytes);

                if (updatedProduct != null)
                {
                    MessageBox.Show("Produk berhasil diperbarui.");
                    var mainWindow = Window.GetWindow(this) as MainWindow;
                    var navigationService = mainWindow?.DataContext as NavigationServices;
                    navigationService?.NavigateToStoreView();
                }
                else
                {
                    MessageBox.Show("Gagal memperbarui produk.");
                }
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

        private void ConditionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConditionComboBox.SelectedValue.ToString() == "1") // Barang Baru
            {
                DurationTextBox.Text = "produk baru";
                DurationTextBox.IsEnabled = false; // Disable input
            }
            else // Preloved
            {
                DurationTextBox.Text = "";
                DurationTextBox.IsEnabled = true; 
            }
        }
    }
}
