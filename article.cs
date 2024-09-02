using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarangKu
{
    public class article
    {
        // Atribut atau Properti
        public string Judul { get; set; }
        public string Penulis { get; set; }
        public DateTime Tanggal { get; set; }
        public string IsiKonten { get; set; }

        // Constructor
        public article(string judul, string penulis, DateTime tanggal, string isiKonten)
        {
            Judul = judul;
            Penulis = penulis;
            Tanggal = tanggal;
            IsiKonten = isiKonten;
        }

        // Metode untuk menampilkan informasi artikel
        public void TampilkanArtikel()
        {
            Console.WriteLine("Judul: " + Judul);
            Console.WriteLine("Penulis: " + Penulis);
            Console.WriteLine("Tanggal: " + Tanggal.ToString("dd MMMM yyyy"));
            Console.WriteLine("Isi Konten: " + IsiKonten);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Membuat objek Artikel
            article artikel = new article(
                "Panduan Belajar C# untuk Pemula",
                "John Doe",
                DateTime.Now,
                "Ini adalah konten artikel yang berisi panduan belajar C# dari dasar hingga mahir."
            );

            // Menampilkan artikel
            artikel.TampilkanArtikel();
        }
    }
}