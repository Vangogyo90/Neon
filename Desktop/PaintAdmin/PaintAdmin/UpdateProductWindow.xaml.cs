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
    public partial class UpdateProductWindow : Window
    {
        string fileNameImage;
        string fileNameDocument;
        int IdColor;
        byte[] img;
        byte[] document;

        public UpdateProductWindow(int IdColor)
        {
            InitializeComponent();
            this.IdColor = IdColor;
            GetColor();
        }

        private async void Change_Click(object sender, RoutedEventArgs e)
        {
            byte[] img = { };
            byte[] document = { };
            if (fileNameImage != null && fileNameDocument != null)
            {
                img = File.ReadAllBytes(fileNameImage);
                document = File.ReadAllBytes(fileNameDocument);
            }
            else if (fileNameDocument != null)
            {
                img = this.img;
                document = File.ReadAllBytes(fileNameDocument);
            }
            else if (fileNameImage != null)
            {
                img = File.ReadAllBytes(fileNameImage);
                document = this.document;
            }
            else
            {
                img = this.img;
                document = this.document;
            }

            object Object = new
            {
                IdColor = IdColor,
                Certificate = document,
                Photo = img,
                Priority = Priority.Value,
                TypeApplicationId = typeApplication.SelectedValue,
                TempPulverizationId = tempPulverization.SelectedValue,
                ShineId = shine.SelectedValue,
                TypeSurfaceId = typeSurface.SelectedValue,
                RalCatalogId = RalCatalog.SelectedValue
            };

            await ApiClass.CRUDAPI(Object, $"{IdColor}", "Colors", ApiClass.Request.Put);
            this.Close();
        }

        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png;";

            if (dlg.ShowDialog() == true)
            {
                fileNameImage = dlg.FileName;
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(fileNameImage));
                imgColor.Source = image.Source;
            }
        }

        private void ChangeDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Document (*.pdf)|*.pdf;";
            if (dlg.ShowDialog() == true)
            {
                fileNameDocument = dlg.FileName;
            }
        }

        private async void GetColor()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, $"{IdColor}", "Colors", ApiClass.Request.SelectByID);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<Table.Color>(content);
                Priority.Value = contentData.Priority;
                RalCatalog.SelectedValue = contentData.RalCatalogId;
                tempPulverization.SelectedValue = contentData.TempPulverizationId;
                shine.SelectedValue = contentData.ShineId;
                typeApplication.SelectedValue = contentData.TypeApplicationId;
                typeSurface.SelectedValue = contentData.TypeSurfaceId;
                imgColor.Source = GetImageByByte(contentData.Photo);
                img = contentData.Photo;
                document = contentData.Certificate;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private BitmapImage GetImageByByte(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(bytes))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void RalCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RalCatalog.Items.Count != 0 & RalCatalog.SelectedItem != null)
            {
                Table.RalCatalog data = (Table.RalCatalog)RalCatalog.SelectedItem;
                Color color = (Color)ColorConverter.ConvertFromString(data.ColorHtml);
                ColorLabel.Background = new SolidColorBrush(color);
            }
        }

        private async void typeApplication_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "TypeApplications", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.TypeApplication>>(content);
                cmb.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void tempPulverization_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "TempPulverizations", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.TempPulverization>>(content);
                cmb.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void shine_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "Shines", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.Shine>>(content);
                cmb.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void typeSurface_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "TypeSurfaces", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.TypeSurface>>(content);
                cmb.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void RalCatalog_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "RalCatalogs", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.RalCatalog>>(content);
                cmb.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }
    }
}
