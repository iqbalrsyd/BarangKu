using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BarangKu.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BarangKu.Views
{
    public partial class ArticleView : UserControl
    {
        // Koleksi untuk artikel terbaru dan artikel rekomendasi
        public ObservableCollection<Article> RecentArticles { get; set; }
        public ObservableCollection<Article> RecommendedArticles { get; set; }

        public ArticleView()
        {
            InitializeComponent();
            DataContext = this; // Set DataContext ke instance ini untuk binding

            // Data contoh untuk artikel terbaru
            RecentArticles = new ObservableCollection<Article>
            {
                new Article { Title = "Berita OOTD Terkini Dan Terbaru Hari Ini", Description = "Okezone.com - Berita OOTD Terkini Dan ... Selengkapnya", ImageUrl = "/Assets/ootd.png" },
                new Article { Title = "Inspirasi Gaya OOTD Musim Panas", Description = "Detik.com - Ide fashion musim panas... Selengkapnya", ImageUrl = "/Assets/ootd1.png" }
            };

            // Data contoh untuk artikel rekomendasi
            RecommendedArticles = new ObservableCollection<Article>
            {
                new Article { Title = "Trend Fashion Korea Terbaru", Description = "KoreanWave.com - Mode terkini dari Korea... Selengkapnya", ImageUrl = "/Assets/ootd2.png" },
                new Article { Title = "OOTD Untuk Aktivitas Sehari-Hari", Description = "Fashionista.com - Tips untuk tampilan santai... Selengkapnya", ImageUrl = "/Assets/ootd3.png" }
            };
        }

        private void OpenArticleDetail(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag is Article article)
            {
                MessageBox.Show($"Membuka detail untuk artikel: {article.Title}");
                // Ganti MessageBox ini dengan navigasi ke halaman detail atau tindakan lainnya
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    // Model untuk data artikel
    public class Article
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
