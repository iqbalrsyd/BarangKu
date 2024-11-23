using BarangKu.Models;
using BarangKu.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BarangKu.ViewModels
{
    public class ArticleViewModel : INotifyPropertyChanged
    {
        private readonly ArticleService _articleService;
        public ObservableCollection<Article> Articles { get; set; }

        public ArticleViewModel()
        {
            _articleService = new ArticleService();
            LoadArticles();
        }

        public void LoadArticles()
        {
            var articlesFromDb = _articleService.GetAllArticles();
            Articles = new ObservableCollection<Article>(articlesFromDb);
            OnPropertyChanged(nameof(Articles));
        }

        public void AddArticle(Article newArticle)
        {
            _articleService.AddArticle(newArticle);
            Articles.Add(newArticle);
            OnPropertyChanged(nameof(Articles));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
