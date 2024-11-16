using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BarangKu.Styles
{
    public class PlaceholderTextBox : TextBox
    {
        // DependencyProperty untuk PlaceholderText
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                nameof(PlaceholderText), typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

        // Properti PlaceholderText
        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        // DependencyProperty untuk warna placeholder
        public static readonly DependencyProperty PlaceholderColorProperty =
            DependencyProperty.Register(
                nameof(PlaceholderColor), typeof(Brush), typeof(PlaceholderTextBox), new PropertyMetadata(Brushes.Gray));

        // Properti untuk warna teks placeholder
        public Brush PlaceholderColor
        {
            get => (Brush)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        // Constructor
        public PlaceholderTextBox()
        {
            // Gunakan template kontrol untuk Placeholder
            this.DefaultStyleKey = typeof(PlaceholderTextBox);

            // Menambahkan handler saat teks berubah dan fokus berubah
            this.TextChanged += OnTextChanged;
            this.GotFocus += OnFocusChanged;
            this.LostFocus += OnFocusChanged;
        }

        // Event handler untuk perubahan teks
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        // Event handler untuk perubahan fokus
        private void OnFocusChanged(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        // Metode untuk memperbarui visibilitas placeholder
        private void UpdatePlaceholderVisibility()
        {
            // Memastikan Placeholder hanya terlihat saat TextBox kosong dan tidak fokus
            if (string.IsNullOrEmpty(this.Text) && !this.IsFocused)
            {
                VisualStateManager.GoToState(this, "NoFocusAndNoText", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "HasFocusOrText", true);
            }
        }
    }
}
