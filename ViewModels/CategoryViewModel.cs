using BarangKu.Models;
using BarangKu.Services;
using System.Windows.Input;

namespace BarangKu.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        private readonly CategoryService _categoryService;

        public ICommand AddCategoryCommand { get; }
        public ICommand EditCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }

        public CategoryViewModel(Category category)
        {
            Category = category;
            _categoryService = new CategoryService();
            AddCategoryCommand = new RelayCommand(AddCategory);
            EditCategoryCommand = new RelayCommand(EditCategory);
            DeleteCategoryCommand = new RelayCommand(DeleteCategory);
        }

        private void AddCategory()
        {
            _categoryService.AddCategory(Category);
        }

        private void EditCategory()
        {
            _categoryService.EditCategory(Category);
        }

        private void DeleteCategory()
        {
            _categoryService.DeleteCategory(Category.CategoryID);
        }
    }
}
