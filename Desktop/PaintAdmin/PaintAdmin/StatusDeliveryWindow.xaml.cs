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
    public partial class StatusDeliveryWindow : Window
    {
        public StatusDeliveryWindow()
        {
            InitializeComponent();
            GetStatusDelivery();
            Closing += StatusDeliveryWindow_Closing;
        }

        private void StatusDeliveryWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminPanel = new AdminMenu();
            adminPanel.Show();
        }

        private async void GetStatusDelivery()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "StatusDeliveries", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.StatusDelivery>>(content);
                DataAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void DataAPI_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Table.StatusDelivery table = e.Row.Item as Table.StatusDelivery;
            if (table.IdStatusOrder > 0)
            {
                object Object = new
                {
                    IdStatusOrder = table.IdStatusOrder,
                    NameStatusOrder = table.NameStatusOrder
                };
                await ApiClass.CRUDAPI(Object, table.IdStatusOrder.ToString(), "StatusDeliveries", ApiClass.Request.Put);
                GetStatusDelivery();
            }
            else
            {
                object Object = new
                {
                    NameStatusOrder = table.NameStatusOrder
                };
                await ApiClass.CRUDAPI(Object, "", "StatusDeliveries", ApiClass.Request.Post);
                GetStatusDelivery();
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
                {
                    Table.StatusDelivery table = (Table.StatusDelivery)DataAPI.SelectedItems[0];
                    object Object = new
                    { };

                    await ApiClass.CRUDAPI(Object, table.IdStatusOrder.ToString(), "StatusDeliveries", ApiClass.Request.Delete);
                    GetStatusDelivery();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка", "Уведомление");
            }
        }
    }
}
