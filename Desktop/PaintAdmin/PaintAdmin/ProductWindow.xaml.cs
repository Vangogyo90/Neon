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
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
            GetProduct();
        }

        private async void GetProduct()
        {
            var res = await ApiClass.CustomGet("Colors", "GetData");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.Color>>(content);
                ListAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow window = new AddProductWindow();
            window.Closed += Window_Closed;
            window.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GetProduct();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                switch (MessageBox.Show("Если вы удалите товар, то он больше не будет отображаться нигде, даже в доставках!", "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        Table.Color table = (Table.Color)ListAPI.SelectedItems[0];
                        object Object = new
                        { };


                        await ApiClass.CustomDelete("ColorDeliveries", $"DeleteColorDeliveryByColorId/{table.IdColor}");
                        await ApiClass.CustomDelete("QuantityColors", $"DeleteQuantityColorByColorId/{table.IdColor}");
                        await ApiClass.CustomDelete("Discounts", $"DeleteDiscountByColorId/{table.IdColor}");
                        await ApiClass.CRUDAPI(Object, table.IdColor.ToString(), "Colors", ApiClass.Request.Delete);
                        GetProduct();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                Table.Color table = (Table.Color)ListAPI.SelectedItems[0];
                UpdateProductWindow window = new UpdateProductWindow(table.IdColor);
                window.Closed += Window_Closed;
                window.ShowDialog();
            }
        }

        private void Discount_Click(object sender, RoutedEventArgs e)
        {
            DiscountWindow window = new DiscountWindow();
            window.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            RegistryLastUserSettings.DeleteUser();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                Table.Color table = (Table.Color)ListAPI.SelectedItems[0];
                SaveFileDialog svdlg = new SaveFileDialog();
                svdlg.Filter = "Document (*.pdf)|*.pdf;";
                svdlg.FileName = $"{table.RalCatalog.ColorRal} {table.RalCatalog.NameRal} {table.Shines.NameShine} {table.TypeSurfaces.NameTypeSurface}.pdf";
                if (svdlg.ShowDialog() == true)
                {
                    ConvertByteArrayToPdfFile(table.Certificate, svdlg.FileName);
                }
            }
        }

        private void ConvertByteArrayToPdfFile(byte[] byteArray, string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                fileStream.Write(byteArray, 0, byteArray.Length);
            }
        }
    }
}
