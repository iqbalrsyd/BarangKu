using BarangKu.Models;
using BarangKu.Services;
using Npgsql;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace BarangKu.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        public Category Category { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        private readonly DatabaseService _dbService;

        public CategoryViewModel()
        {
            _dbService = new DatabaseService();
        }



        public List<Category> GetCategory()
        {
            List<Category> categories = new List<Category>();
            var conn = _dbService.GetConnection();

            try
            {
                string getCategory = "SELECT categoryid, name, description FROM category";
                using (var cmd = new NpgsqlCommand(getCategory, conn))
                {
                    using(var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category
                            {
                                CategoryID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                            };

                            categories.Add(category);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _dbService.CloseConnection(conn);
            }

            return categories;

        }
        public void LoadCategory()
        {
            var categories = GetCategory();
            Categories = new ObservableCollection<Category>(categories);
        }

        private int _selectedCategoryId;
        public int SelectedCategoryId
        {
            get { return _selectedCategoryId; }
            set
            {
                _selectedCategoryId = value;
                OnPropertyChanged(nameof(SelectedCategoryId));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
