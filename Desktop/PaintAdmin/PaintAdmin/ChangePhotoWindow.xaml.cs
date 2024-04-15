using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class ChangePhotoWindow : Window
    {
        int IdNews;
        public ChangePhotoWindow(int IdNews)
        {
            InitializeComponent();
            this.IdNews = IdNews;
            GetPhotos();
        }

        private async void GetPhotos()
        {
            var res = await ApiClass.CustomGet("PhotoNews", $"GetPhotoNewsByNewsID/{IdNews}");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.PhotoNews>>(content);
                if (contentData.Count > 0)
                {
                    No.Visibility = Visibility.Hidden;
                    ListAPI.Visibility = Visibility.Visible;
                    ListAPI.ItemsSource = contentData;
                }
                else
                {
                    No.Visibility = Visibility.Visible;
                    ListAPI.Visibility = Visibility.Hidden;
                }
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void DeletePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListAPI.SelectedItem != null)
                {
                    Table.PhotoNews table = (Table.PhotoNews)ListAPI.SelectedItems[0];
                    object Object = new
                    { };
                    await ApiClass.CRUDAPI(Object, table.IdPhotoNews.ToString(), "PhotoNews", ApiClass.Request.Delete);
                    GetPhotos();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка", "Уведомление");
            }
        }

        private async void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png;";

            if (dlg.ShowDialog() == true)
            {
                object Object = new
                {
                    NewsId = IdNews,
                    Photo = File.ReadAllBytes(dlg.FileName),
                };

                await ApiClass.CRUDAPI(Object, "", "PhotoNews", ApiClass.Request.Post);
                GetPhotos();
            }
        }
    }
}
