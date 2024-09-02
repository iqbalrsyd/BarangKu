using System;
using System.Collections.Generic;
using System.Linq;


namespace BarangKu
{
    public class Category
    {
        public int CategoryID { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public void AddCategory(string name, string description)
        {
            Name = name;
            Description = description;
            // Logic to add category
        }


        public void EditCategory(Category category)
        {
            Name = category.Name;
            Description = category.Description;
            // Logic to edit category
        }


        public void DeleteCategory(Category category)
        {
            // Logic to delete category
        }
    }
}