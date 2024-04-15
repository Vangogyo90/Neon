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
    public partial class WareHouseWindow : Window
    {
        public WareHouseWindow()
        {
            InitializeComponent();
            GetWareHouse();
            Closing += WareHouseWindow_Closing;
        }

        private void WareHouseWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminPanel = new AdminMenu();
            adminPanel.Show();
        }

        private async void Cites_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "Cities", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.City>>(content);
                cmb.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void DataAPI_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Table.WareHouse table = e.Row.Item as Table.WareHouse;
            if (table.CityId > 0 && table.Adress != null)
            {
                if (table.IdWareHouse > 0)
                {
                    object Object = new
                    {
                        IdWareHouse = table.IdWareHouse,
                        CityId = table.CityId,
                        Adress = table.Adress
                    };
                    await ApiClass.CRUDAPI(Object, table.IdWareHouse.ToString(), "WareHouses", ApiClass.Request.Put);
                    GetWareHouse();
                }
                else
                {
                    object Object = new
                    {
                        CityId = table.CityId,
                        Adress = table.Adress
                    };
                    await ApiClass.CRUDAPI(Object, "", "WareHouses", ApiClass.Request.Post);
                    GetWareHouse();
                }
            }
            else
                e.Cancel = true;
        }

        private async void GetWareHouse()
        {
            var res = await ApiClass.CustomGet("WareHouses", "GetData");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.WareHouse>>(content);
                DataAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
                {
                    Table.WareHouse table = (Table.WareHouse)DataAPI.SelectedItems[0];
                    object Object = new
                    { };

                    await ApiClass.CRUDAPI(Object, table.IdWareHouse.ToString(), "WareHouses", ApiClass.Request.Delete);
                    GetWareHouse();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка", "Уведомление");
            }
        }
    }
}
