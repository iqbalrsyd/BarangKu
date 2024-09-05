using BarangKu.Models;
using BarangKu.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BarangKu.ViewModels
{
	public class ArticleViewModel
	{
		public ObservableCollection<Article> Articles { get; set; }
		private readonly ArticleService _articleService;

		public ICommand AddArticleCommand { get; }
		public ICommand DeleteArticleCommand { get; }
		public ICommand LoadArticlesCommand { get; }

		public ArticleViewModel()
		{
			_articleService = new ArticleService();
			Articles = new ObservableCollection<Article>();
			AddArticleCommand = new RelayCommand(AddArticle);
			DeleteArticleCommand = new RelayCommand(DeleteArticle);
			LoadArticlesCommand = new RelayCommand(LoadArticles);
		}

		private void AddArticle()
		{
			// Example article addition
			var newArticle = new Article
			{
				Title = "New Article Title",
				Author = "Author Name",
				Date = DateTime.Now,
				Content = "Content of the article."
			};
			_articleService.AddArticle(newArticle);
			LoadArticles(); // Refresh list
		}

		private void DeleteArticle()
		{
			// Example deletion
			_articleService.DeleteArticle("New Article Title");
			LoadArticles(); // Refresh list
		}

		private void LoadArticles()
		{
			Articles.Clear();
			var articles = _articleService.GetAllArticles();
			foreach (var article in articles)
			{
				Articles.Add(article);
			}
		}
	}
}
