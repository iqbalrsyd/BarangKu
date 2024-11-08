using System;
using System.ComponentModel;

namespace BarangKu.Models
{
    public class Category : INotifyPropertyChanged
    {
        private int categoryId; 

        public int CategoryID
        {
            get { return categoryId; }
            set
            {
                if (categoryId != value)
                {
                    categoryId = value;
                    OnPropertyChanged(nameof(CategoryID));
                }
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public Category()
        {
            
        }

        public Category(int categoryId, string name, string description)
        {
            CategoryID = categoryId;
            Name = name;
            Description = description;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
