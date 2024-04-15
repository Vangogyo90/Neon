using System;
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
using System.Windows.Shapes;

namespace PaintAdmin
{
    public partial class AddRalColorsWindow : Window
    {
        public AddRalColorsWindow()
        {
            InitializeComponent();
            Add.IsEnabled = false;
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            object Object = new
            {
                NameRal = Name.Text,
                ColorRal = Ral.Text,
                ColorHtml = HTML.Text
            };
            await ApiClass.CRUDAPI(Object, "", "RalCatalogs", ApiClass.Request.Post);
        }

        private void HTML_TextChanged(object sender, TextChangedEventArgs e)
        {
            Add.IsEnabled = false;
            try
            {
                Color color = (Color)ColorConverter.ConvertFromString(HTML.Text);
                ColorLabel.Background = new SolidColorBrush(color);
                Add.IsEnabled = true;
            }
            catch { }
        }

        private void Ral_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            KeyClass.DontDelete(e, Ral, 3);
        }

        private void HTML_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            KeyClass.DontDelete(e, HTML, 1);
        }
    }
}
