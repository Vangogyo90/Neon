using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AddNewsWindow : Window
    {
        public ObservableCollection<Photo> photos = new ObservableCollection<Photo>();
        public AddNewsWindow()
        {
            InitializeComponent();
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            AddPhotoWindow window = new AddPhotoWindow();
            window.photos = photos;
            window.GetPhotos();
            window.ShowDialog();
        }

        private async void AddNews_Click(object sender, RoutedEventArgs e)
        {
            if (Header != null && TextNews != null)
            {
                object Object = new
                {
                    HeadingNews = Header.Text,
                    TextNews = Encoding.UTF8.GetBytes(TextNews.Text),
                    UserId = RegistryLastUserSettings.GetUserID()
                };
                
                var res = await ApiClass.CRUDAPI(Object, "", "News", ApiClass.Request.Post);
                var content = await res.Content.ReadAsStringAsync();
                if (photos.Count > 0)
                {
                    for (int i = 0; i < photos.Count; i++)
                    {
                        var contentData = JsonConvert.DeserializeObject<Table.News>(content);
                        object ObjectPhoto = new
                        {
                            NewsId = contentData.IdNews,
                            Photo = photos[i].ImageData
                        };
                        await ApiClass.CRUDAPI(ObjectPhoto, "", "PhotoNews", ApiClass.Request.PostWithoutMessage);
                    }
                }
            }
        }

        public class Photo
        {
            public byte[] ImageData { get; set; }
        }
    }
}
