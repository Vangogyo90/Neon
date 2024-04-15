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
    public partial class AboutNewsWindow : Window
    {
        int IdNews;
        public AboutNewsWindow(int IdNews)
        {
            InitializeComponent();
            this.IdNews = IdNews;
            GetNews();
        }

        private void ChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            ChangePhotoWindow window = new ChangePhotoWindow(IdNews);
            window.ShowDialog();
        }

        private async void GetNews()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, $"{IdNews}", "News", ApiClass.Request.SelectByID);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<Table.News>(content);
                Header.Text = contentData.HeadingNews;
                TextNews.Text = Encoding.UTF8.GetString(contentData.TextNews);
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void ChangeNews_Click(object sender, RoutedEventArgs e)
        {
            if (Header != null && TextNews != null)
            {
                object Object = new
                {
                    IdNews = IdNews,
                    HeadingNews = Header.Text,
                    TextNews = Encoding.UTF8.GetBytes(TextNews.Text),
                    UserId = RegistryLastUserSettings.GetUserID(),
                    Date = DateTime.Now
                };

                await ApiClass.CRUDAPI(Object, $"{IdNews}", "News", ApiClass.Request.Put);
                this.Close();
            }
        }
    }
}
