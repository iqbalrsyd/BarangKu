using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Title { get; set; }

        [Required] // Menentukan Content tidak boleh null
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public bool IsRead { get; set; } = false; // Default value
    }
}
