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
    public partial class CityWindow : Window
    {
        public CityWindow()
        {
            InitializeComponent();
            GetCities();
            Closing += Cities_Closing;
        }

        private void Cities_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminPanel = new AdminMenu();
            adminPanel.Show();
        }

        private async void GetCities()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "Cities", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.City>>(content);
                DataAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void DataAPI_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Table.City table = e.Row.Item as Table.City;
            if (table.IdCity > 0)
            {
                object Object = new
                {
                    IdCity = table.IdCity,
                    NameCity = table.NameCity
                };
                await ApiClass.CRUDAPI(Object, table.IdCity.ToString(), "Cities", ApiClass.Request.Put);
                GetCities();
            }
            else
            {
                object Object = new
                {
                    NameCity = table.NameCity
                };
                await ApiClass.CRUDAPI(Object, "", "Cities", ApiClass.Request.Post);
                GetCities();
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
                {
                    Table.City table = (Table.City)DataAPI.SelectedItems[0];
                    object Object = new
                    { };

                    await ApiClass.CRUDAPI(Object, table.IdCity.ToString(), "Cities", ApiClass.Request.Delete);
                    GetCities();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка", "Уведомление");
            }
        }
    }
}
