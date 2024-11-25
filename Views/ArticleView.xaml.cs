using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BarangKu.Models;
using BarangKu.Services;

namespace BarangKu.Views
{
    public partial class ArticleView : UserControl, INotifyPropertyChanged
    {
        private readonly ArticleService _articleService;
        public ObservableCollection<Article> Articles { get; set; }

        public ArticleView()
        {
            InitializeComponent();
            _articleService = new ArticleService();
            DataContext = this;
            LoadArticles();
        }

        private void LoadArticles()
        {
            Articles = new ObservableCollection<Article>(_articleService.GetAllArticles());
            OnPropertyChanged(nameof(Articles));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NavigateToSeeMore_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var articleId = button?.CommandParameter as int?; 

            if (articleId.HasValue)
            {
                var mainWindow = Window.GetWindow(this) as MainWindow;
                var navigationService = mainWindow?.DataContext as NavigationServices;

                navigationService?.NavigateToSeeMoreView(articleId.Value); 
            }
        }


        private void NavigateToCartView_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var navigationService = mainWindow.DataContext as NavigationServices;
            navigationService?.NavigateToCartView();
        }
    }
}
