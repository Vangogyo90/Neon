using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace PaintAdmin
{
    public partial class AddProductWindow : Window
    {
        string fileNameImage;
        string fileNameDocument;

        public AddProductWindow()
        {
            InitializeComponent();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            if (fileNameImage != null && fileNameDocument != null)
            {
                object Object = new
                {
                    Certificate = File.ReadAllBytes(fileNameDocument),
                    Photo = File.ReadAllBytes(fileNameImage),
                    Priority = Priority.Value,
                    TypeApplicationId = typeApplication.SelectedValue,
                    TempPulverizationId = tempPulverization.SelectedValue,
                    ShineId = shine.SelectedValue,
                    TypeSurfaceId = typeSurface.SelectedValue,
                    RalCatalogId = RalCatalog.SelectedValue
                };

                await ApiClass.CRUDAPI(Object, "", "Colors", ApiClass.Request.Post);
            }
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
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

        private void AddDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Document (*.pdf)|*.pdf;";
            if (dlg.ShowDialog() == true)
            {
                fileNameDocument = dlg.FileName;
                Image image = new Image();
                image.Source = new BitmapImage(new Uri( "pack://application:,,,/Images/ok.png"));
                documentImage.Source = image.Source;
            }
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
