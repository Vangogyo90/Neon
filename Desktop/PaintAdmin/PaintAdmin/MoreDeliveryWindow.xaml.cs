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
    public partial class MoreDeliveryWindow : Window
    {
        int DeliveryID;
        public MoreDeliveryWindow(int DeliveryID)
        {
            InitializeComponent();
            this.DeliveryID = DeliveryID;
            GetDeliveryProduct();
        }

        private async void GetDeliveryProduct()
        {
            var res = await ApiClass.CustomGet("ColorDeliveries", $"GetData/{DeliveryID}");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.ColorDelivery>>(content);
                if (contentData.Count > 0)
                    ListAPI.ItemsSource = contentData;
                else
                {
                    ListAPI.Visibility = Visibility.Hidden;
                    No.Visibility = Visibility.Visible;
                }
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }
    }
}
