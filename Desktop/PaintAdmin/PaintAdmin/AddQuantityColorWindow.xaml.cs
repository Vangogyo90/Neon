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
    public partial class AddQuantityColorWindow : Window
    {
        public AddQuantityColorWindow()
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

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListAPI.SelectedItem != null)
                {
                    Table.Color color = (Table.Color)ListAPI.SelectedItems[0];
                    object Object = new
                    {
                        ColorId = color.IdColor,
                        WareHouseId = warehouse.SelectedValue,
                        Quantity = quantity.Text,
                        Price_For_KG = Convert.ToDouble(price.Text)
                    };

                    await ApiClass.CRUDAPI(Object, "", "QuantityColors", ApiClass.Request.Post);
                }
            }
            catch { }
        }

        private async void warehouse_Loaded(object sender, RoutedEventArgs e)
        {
            var res = await ApiClass.CustomGet("WareHouses", "GetData");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.WareHouse>>(content);
                foreach (Table.WareHouse wareHouse in contentData)
                {
                    wareHouse.AdressCity = wareHouse.Adress + ", " + wareHouse.City.NameCity;
                }
                warehouse.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }
    }
}
