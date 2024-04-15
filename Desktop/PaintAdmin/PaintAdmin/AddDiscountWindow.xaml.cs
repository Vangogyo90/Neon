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
    public partial class AddDiscountWindow : Window
    {
        public AddDiscountWindow()
        {
            InitializeComponent();
            GetProduct();
            DiscountText.Text = "0";
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
            if (ListAPI.SelectedValue != null && DiscountText.Text != "0")
            {
                Table.Color table = (Table.Color)ListAPI.SelectedItems[0];
                object Object = new
                {
                    ColorId = table.IdColor,
                    SizeDicsount = DiscountText.Text
                };

                await ApiClass.CRUDAPI(Object, "", "Discounts", ApiClass.Request.Post);
            }
        }

        private void DiscountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DiscountText.Text = Math.Round(DiscountSlider.Value).ToString();
        }
    }
}
