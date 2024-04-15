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
    public partial class UpdateDiscountWindow : Window
    {
        int IdDiscount;
        int ColorId;
        public UpdateDiscountWindow(int IdDiscount)
        {
            InitializeComponent();
            this.IdDiscount = IdDiscount;
            GetDiscount();
        }

        private async void GetDiscount()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, $"{IdDiscount}", "Discounts", ApiClass.Request.SelectByID);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<Table.Discount>(content);
                DiscountSlider.Value = contentData.SizeDicsount;
                DiscountText.Text = contentData.SizeDicsount.ToString();
                ColorId = contentData.ColorId;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private void DiscountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DiscountText.Text = Math.Round(DiscountSlider.Value).ToString();
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DiscountText.Text != "0")
            {
                object Object = new
                {
                    IdDiscount = IdDiscount,
                    ColorId = ColorId,
                    SizeDicsount = DiscountText.Text
                };

                await ApiClass.CRUDAPI(Object, $"{IdDiscount}", "Discounts", ApiClass.Request.Put);
                this.Close();
            }
        }
    }
}
