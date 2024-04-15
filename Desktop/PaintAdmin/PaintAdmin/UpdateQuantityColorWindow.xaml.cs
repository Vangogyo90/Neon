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
    public partial class UpdateQuantityColorWindow : Window
    {
        int id;
        DateTime dateTime;
        int Shelf_Life;

        public UpdateQuantityColorWindow(int id)
        {
            InitializeComponent();
            this.id = id;
            GetProduct();
            GetQuantityColor();
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

        private async void GetQuantityColor()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, $"{id}", "QuantityColors", ApiClass.Request.SelectByID);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<Table.QuantityColor>(content);
                ListAPI.SelectedValue = contentData.ColorId;
                warehouse.SelectedValue = contentData.WareHouseId;
                quantity.Text = contentData.Quantity.ToString();
                price.Text = contentData.Price_For_KG.ToString();
                dateTime = contentData.Date;
                Shelf_Life = contentData.Shelf_Life;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
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

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListAPI.SelectedItem != null)
                {
                    Table.Color color = (Table.Color)ListAPI.SelectedItems[0];
                    object Object = new
                    {
                        IdQuantityColors = id,
                        ColorId = color.IdColor,
                        WareHouseId = warehouse.SelectedValue,
                        Quantity = quantity.Text,
                        Price_For_KG = Convert.ToDouble(price.Text),
                        Date = dateTime,
                        Shelf_Life = Shelf_Life
                    };

                    await ApiClass.CRUDAPI(Object, $"{id}", "QuantityColors", ApiClass.Request.Put);
                    this.Close();
                }
            }
            catch { }
        }
    }
}
