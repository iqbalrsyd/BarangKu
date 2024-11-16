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
        public int CategoryID { get; private set; }
        public MyProductView()
        {
            InitializeComponent();
            var viewModel = new CategoryViewModel();
            viewModel.LoadCategory();
            DataContext = viewModel;
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

            // Mengambil nilai stok dari StockTextBox
            int stock = int.Parse(StockTextBox.Text);

            string duration = DurationTextBox.Text;

            decimal price = decimal.Parse(PriceTextBox.Text);

            string condition = string.Empty;

            byte[] imageBytes = _selectedImage != null ? ImageToByteArray(_selectedImage) : null;

            StoreViewModel storeViewModel;
            if (ConditionComboBox.SelectedValue != null)
            {
                // Konversi SelectedValue menjadi int
                if (int.TryParse(ConditionComboBox.SelectedValue.ToString(), out int conditionId))
                {
                    if (conditionId == 1) // Barang Baru
                    {
                        condition = "Baru";
                        storeViewModel = new NewProductModel();
                    }
                    else if (conditionId == 2) // Preloved
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

            StoreViewModel product = new StoreViewModel();

            Product newProduct = product.AddProduct(CategoryID, name, description, price, stock, condition, duration, imageBytes);

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
                DurationTextBox.IsEnabled = true; // Enable input
            }
        }
    }
}
