using Newtonsoft.Json;
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
    public partial class UpdateRalWindow : Window
    {
        int IdRal;
        public UpdateRalWindow(int IdRal)
        {
            InitializeComponent();
            this.IdRal = IdRal;
            Update.IsEnabled = false;
            GetRal();
        }

        private async void GetRal()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, $"{IdRal}", "RalCatalogs", ApiClass.Request.SelectByID);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<Table.RalCatalog>(content);
                Name.Text = contentData.NameRal;
                Ral.Text = contentData.ColorRal;
                HTML.Text = contentData.ColorHtml;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            object Object = new
            {
                IdRalCatalog = IdRal,
                NameRal = Name.Text,
                ColorRal = Ral.Text,
                ColorHtml = HTML.Text
            };
            await ApiClass.CRUDAPI(Object, $"{IdRal}", "RalCatalogs", ApiClass.Request.Put);
            this.Close();
        }

        private void HTML_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update.IsEnabled = false;
            try
            {
                Color color = (Color)ColorConverter.ConvertFromString(HTML.Text);
                ColorLabel.Background = new SolidColorBrush(color);
                Update.IsEnabled = true;
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
