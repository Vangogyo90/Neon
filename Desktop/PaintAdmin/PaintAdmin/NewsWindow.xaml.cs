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
    public partial class NewsWindow : Window
    {
        public NewsWindow()
        {
            InitializeComponent();
            Closing += NewsWindow_Closing;
            GetNews();
        }

        private async void GetNews()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "News", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.News>>(content);
                DataAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private void NewsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClientManagerMenu window = new ClientManagerMenu();
            window.Show();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
                {
                    Table.News table = (Table.News)DataAPI.SelectedItems[0];
                    object Object = new
                    { };

                    await ApiClass.CustomDelete("PhotoNews", $"DeletePhotoNewsByNewsID/{table.IdNews}");
                    await ApiClass.CRUDAPI(Object, table.IdNews.ToString(), "News", ApiClass.Request.Delete);
                    GetNews();

                }
            }
            catch
            {
                MessageBox.Show("Ошибка", "Уведомление");
            }
        }

        private void More_Click(object sender, RoutedEventArgs e)
        {
            if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
            {
                Table.News table = (Table.News)DataAPI.SelectedItems[0];
                AboutNewsWindow window = new AboutNewsWindow(table.IdNews);
                window.Closed += Window_Closed;
                window.ShowDialog();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddNewsWindow window = new AddNewsWindow();
            window.Closed += Window_Closed;
            window.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GetNews();
        }

        private void Prewiew_Click(object sender, RoutedEventArgs e)
        {
            if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
            {
                Table.News table = (Table.News)DataAPI.SelectedItems[0];
                PrewiewWindow window = new PrewiewWindow(table.IdNews);
                window.Closed += Window_Closed;
                window.ShowDialog();
            }
        }
    }
}
