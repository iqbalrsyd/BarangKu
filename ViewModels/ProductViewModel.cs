using BarangKu.Models;
using BarangKu.Services;
using System.ComponentModel;
using BarangKu.ViewModels;

namespace BarangKu.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private Product _product;
        private readonly ProductService _productService;

        public ProductViewModel()
        {
            _product = new Product();
            _productService = new ProductService();
        }

        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        public void AddProduct()
        {
            _productService.AddProduct(_product);
            OnPropertyChanged("Product");
        }

        public void EditProduct()
        {
            _productService.EditProduct(_product);
            OnPropertyChanged("Product");
        }

        public void DeleteProduct()
        {
            _productService.DeleteProduct(_product);
            OnPropertyChanged("Product");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
