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
    public partial class DiscountWindow : Window
    {
        public DiscountWindow()
        {
            InitializeComponent();
            Closing += DiscountWindow_Closing;
            GetDiscount();
        }

        private async void GetDiscount()
        {
            var res = await ApiClass.CustomGet("Discounts", "GetData");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.Discount>>(content);
                ListAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private void DiscountWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ProductWindow window = new ProductWindow();
            window.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddDiscountWindow window = new AddDiscountWindow();
            window.Closed += Window_Closed;
            window.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GetDiscount();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                Table.Discount table = (Table.Discount)ListAPI.SelectedItems[0];
                object Object = new
                { };

                await ApiClass.CRUDAPI(Object, table.IdDiscount.ToString(), "Discounts", ApiClass.Request.Delete);
                GetDiscount();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                Table.Discount table = (Table.Discount)ListAPI.SelectedItems[0];
                UpdateDiscountWindow window = new UpdateDiscountWindow(table.IdDiscount);
                window.Closed += Window_Closed;
                window.ShowDialog();
            }
        }
    }
}
