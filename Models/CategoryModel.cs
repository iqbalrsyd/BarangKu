using System;

namespace BarangKu.Models
{
    public class Category
    {
        public int CategoryID { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Category()
        {
            // Default constructor
        }

        public Category(int categoryId, string name, string description)
        {
            CategoryID = categoryId;
            Name = name;
            Description = description;
        }
    }
}
