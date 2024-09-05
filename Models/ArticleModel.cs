using System;

namespace BarangKu.Models
{
    public class Article
    {
        // Atribut atau Properti
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        // Constructor
        public Article(string title, string author, DateTime date, string content)
        {
            Title = title;
            Author = author;
            Date = date;
            Content = content;
        }

        // Default constructor
        public Article() { }
    }
}
