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
    public partial class WareHouseMenu : Window
    {
        public WareHouseMenu()
        {
            InitializeComponent();
            GetWareHousesProduct();
        }

        private async void GetWareHousesProduct()
        {
            var res = await ApiClass.CustomGet("QuantityColors", "GetData");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.QuantityColor>>(content);
                ListAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddQuantityColorWindow window = new AddQuantityColorWindow();
            window.Closed += Window_Closed;
            window.ShowDialog();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                Table.QuantityColor table = (Table.QuantityColor)ListAPI.SelectedItems[0];
                object Object = new
                { };

                await ApiClass.CRUDAPI(Object, table.IdQuantityColors.ToString(), "QuantityColors", ApiClass.Request.Delete);
                GetWareHousesProduct();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                Table.QuantityColor table = (Table.QuantityColor)ListAPI.SelectedItems[0];
                UpdateQuantityColorWindow window = new UpdateQuantityColorWindow(table.IdQuantityColors);
                window.Closed += Window_Closed;
                window.ShowDialog();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GetWareHousesProduct();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            RegistryLastUserSettings.DeleteUser();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
