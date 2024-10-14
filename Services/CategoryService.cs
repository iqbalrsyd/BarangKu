using BarangKu.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarangKu.Services
{
    public class CategoryService
    {
        private readonly List<Category> _categories; // Simulasi database

        public CategoryService()
        {
            _categories = new List<Category>();
        }

        public void AddCategory(Category category)
        {
            // Logic to add category to the database or in-memory list
            _categories.Add(category);
            Console.WriteLine($"Category {category.Name} added.");
        }

        public void EditCategory(Category category)
        {
            // Logic to update category in the database or in-memory list
            var existingCategory = _categories.FirstOrDefault(c => c.CategoryID == category.CategoryID);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                Console.WriteLine($"Category {category.CategoryID} updated.");
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }

        public void DeleteCategory(int categoryId)
        {
            // Logic to delete category from the database or in-memory list
            var category = _categories.FirstOrDefault(c => c.CategoryID == categoryId);
            if (category != null)
            {
                _categories.Remove(category);
                Console.WriteLine($"Category {categoryId} deleted.");
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }
    }
}
