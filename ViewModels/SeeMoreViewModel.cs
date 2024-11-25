using BarangKu.Models;
using BarangKu.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarangKu.ViewModels
{
    public class SeeMoreViewModel : INotifyPropertyChanged
    {
        private readonly ArticleService _articleService;
        private Article _currentArticle;

        public event PropertyChangedEventHandler PropertyChanged;

        public Article CurrentArticle
        {
            get => _currentArticle;
            set
            {
                _currentArticle = value;
                OnPropertyChanged(nameof(CurrentArticle));
            }
        }

        public SeeMoreViewModel(int articleId)
        {
            _articleService = new ArticleService();
            
        }

        

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
