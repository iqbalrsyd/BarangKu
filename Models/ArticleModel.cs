using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Windows.Media.Imaging;

namespace BarangKu.Models
{
    [Table("articles")] // Menentukan nama tabel di database
    public class Article
    {
        [Key] // Menandai ArticleId sebagai primary key
        public int ArticleId { get; set; }

        [Required] // Menentukan UserId tidak boleh null
        public int UserId { get; set; }

        [Required] // Menentukan Title tidak boleh null
        [MaxLength(255)] // Membatasi panjang title seperti pada definisi tabel
        public string Title { get; set; } = string.Empty;

        [Required] // Menentukan Content tidak boleh null
        public string Content { get; set; } = string.Empty;

        public string ImageUrl { get; set; }
        public byte[] ImageBinary { get; set; }

        public bool IsRead { get; set; } = false; // Default value
        public BitmapImage ImageSource
        {
            get
            {
                if (ImageBinary == null || ImageBinary.Length == 0)
                    return null;

                using (var stream = new MemoryStream(ImageBinary))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                    return image;
                }
            }
        }
    }
}