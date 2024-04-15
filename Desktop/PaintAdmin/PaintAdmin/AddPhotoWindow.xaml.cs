using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AddPhotoWindow : Window
    {
        public ObservableCollection<AddNewsWindow.Photo> photos = new ObservableCollection<AddNewsWindow.Photo>();
        public AddPhotoWindow()
        {
            InitializeComponent();
            GetPhotos();
            Closing += AddPhotoWindow_Closing;
        }

        private void AddPhotoWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AddPhotoWindow window = new AddPhotoWindow();
            window.photos = photos;
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png;";

            if (dlg.ShowDialog() == true)
            {
                AddNewsWindow.Photo photo = new AddNewsWindow.Photo();
                photo.ImageData = File.ReadAllBytes(dlg.FileName);
                photos.Add(photo);
                GetPhotos();
            }
        }

        public void GetPhotos()
        {
            ListAPI.ItemsSource = photos;
        }

        private void DeletePhoto_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                AddNewsWindow.Photo table = (AddNewsWindow.Photo)ListAPI.SelectedItems[0];
                photos.Remove(table);
                GetPhotos();
            }
        }
    }
}
