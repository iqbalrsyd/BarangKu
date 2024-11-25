
using BarangKu.Models;
using Npgsql;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BarangKu.Services
{
    public class ArticleService
    {
        private readonly DatabaseService _databaseService;

        public ArticleService()
        {
            _databaseService = new DatabaseService();
        }

        public List<Article> GetAllArticles()
        {
            var articles = new List<Article>();

            using (var conn = _databaseService.GetConnection())
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM articles", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        articles.Add(new Article
                        {
                            ArticleId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            Title = reader.GetString(2),
                            Content = reader.GetString(3),
                            ImageUrl = reader.IsDBNull(4) ? null : reader.GetString(4),
                            IsRead = reader.GetBoolean(5)
                        });
                    }
                }
            }

            return articles;
        }

        public void AddArticle(Article article)
        {
            using (var conn = _databaseService.GetConnection())
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO articles (userid, title, content, imageurl, isread) VALUES (@userid, @title, @content, @imageurl, @isread)";
                    cmd.Parameters.AddWithValue("@userid", article.UserId);
                    cmd.Parameters.AddWithValue("@title", article.Title);
                    cmd.Parameters.AddWithValue("@content", article.Content);
                    cmd.Parameters.AddWithValue("@imageurl", article.ImageUrl ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@isread", article.IsRead);
                    cmd.Parameters.AddWithValue("@ImageBinary", article.ImageBinary ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
