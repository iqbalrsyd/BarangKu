using BarangKu.Models;
using System;
using System.Collections.Generic;

namespace BarangKu.Services
{
    public class ArticleService
    {
        private readonly List<Article> _articles; // Simulasi database

        public ArticleService()
        {
            _articles = new List<Article>();
        }

        public void AddArticle(Article article)
        {
            _articles.Add(article);
            Console.WriteLine($"Article '{article.Title}' added.");
        }

        public List<Article> GetAllArticles()
        {
            return _articles;
        }

        public Article GetArticleByTitle(string title)
        {
            return _articles.Find(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public void DeleteArticle(string title)
        {
            var article = GetArticleByTitle(title);
            if (article != null)
            {
                _articles.Remove(article);
                Console.WriteLine($"Article '{title}' removed.");
            }
            else
            {
                Console.WriteLine("Article not found.");
            }
        }
    }
}
