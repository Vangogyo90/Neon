using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class DeliveryWindow : Window
    {
        public DeliveryWindow()
        {
            InitializeComponent();
            DataAPI.BeginningEdit += DataAPI_BeginningEdit;
        }

        private void DataAPI_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var delivery = e.Row.Item as Table.Delivery;
            if (delivery != null && delivery.StatusDeliveres.NameStatusOrder == "Доставлено")
            {
                e.Cancel = true;
                MessageBox.Show("Невозможно редактировать доставленные заказы.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async Task GetDelivery(string status)
        {
            var res = await ApiClass.CustomGet("Deliveries", $"GetDeliveryByStatus/{(status.Equals("Все") ? "All" : status)}");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.Delivery>>(content);
                foreach (Table.Delivery delivery in contentData)
                {
                    delivery.Adress = Encrypt(delivery.Adress);
                    if (delivery.StatusDeliveres.NameStatusOrder.Equals("Доставлено"))
                        delivery.IsDelivired = true;
                    else if (delivery.StatusDeliveres.NameStatusOrder.Equals("Оплачено"))
                        delivery.IsPay = true;
                    else if (delivery.StatusDeliveres.NameStatusOrder.Equals("Не оплачено"))
                        delivery.IsNotPay = true;
                    else if (delivery.StatusDeliveres.NameStatusOrder.Equals("Отменено"))
                        delivery.IsCancel = true;
                    else if (delivery.StatusDeliveres.NameStatusOrder.Equals("Самовывоз"))
                        delivery.IsPickUp = true;
                    else if (delivery.StatusDeliveres.NameStatusOrder.Equals("Доставка до пункта"))
                        delivery.IsPoint = true;
                }
                DataAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void DataAPI_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Table.Delivery table = e.Row.Item as Table.Delivery;
            object Object = new
            {
                IdDelivery = table.IdDelivery,
                Adress = Decrypt(table.Adress),
                Salt = table.Salt,
                Cheque = table.Cheque,
                CityId = table.CityId,
                StatusOrderId = table.StatusOrderId,
                UserId = table.UserId,
                Date = table.Date
            };
            await ApiClass.CRUDAPI(Object, table.IdDelivery.ToString(), "Deliveries", ApiClass.Request.Put);
            await GetDelivery(cbStatus.Text);
        }

        private async void StatusDelivey_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "StatusDeliveries", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.StatusDelivery>>(content);
                cmb.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void Cheque_Click(object sender, RoutedEventArgs e)
        {
            if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
            {
                Table.Delivery table = (Table.Delivery)DataAPI.SelectedItems[0];
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Document (*.pdf)|*.pdf;";
                if (dlg.ShowDialog() == true)
                {
                    object Object = new
                    {
                        IdDelivery = table.IdDelivery,
                        Adress = Decrypt(table.Adress),
                        Salt = table.Salt,
                        Cheque = File.ReadAllBytes(dlg.FileName),
                        CityId = table.CityId,
                        StatusOrderId = table.StatusOrderId,
                        UserId = table.UserId,
                        Date = table.Date
                    };

                    await ApiClass.CRUDAPI(Object, table.IdDelivery.ToString(), "Deliveries", ApiClass.Request.Put);
                    await GetDelivery(cbStatus.Text);
                }
            }
        }

        private void More_Click(object sender, RoutedEventArgs e)
        {
            if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
            {
                Table.Delivery table = (Table.Delivery)DataAPI.SelectedItems[0];
                MoreDeliveryWindow window = new MoreDeliveryWindow(table.IdDelivery);
                window.ShowDialog();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            RegistryLastUserSettings.DeleteUser();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public static string Decrypt(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        private string Encrypt(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private async void StatusDelivey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Table.StatusDelivery selectedItem = (Table.StatusDelivery)comboBox.SelectedItem;

            if (selectedItem.NameStatusOrder.Equals("Доставлено"))
            {
                switch (MessageBox.Show("Если вы выберите - 'Доставлено' заказ закроется и его нельзя будет больше изменить", "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
                        {
                            Table.Delivery table = (Table.Delivery)DataAPI.SelectedItems[0];
                            var res = await ApiClass.CustomGet("Price", $"CalculateTotalPriceForDelivery/{table.IdDelivery}");
                            var content = await res.Content.ReadAsStringAsync();
                            if (res.IsSuccessStatusCode)
                            {
                                var contentData = JsonConvert.DeserializeObject<string>(content);
                                object Object = new
                                {
                                    Delivery_ID = table.IdDelivery,
                                    Price_Delivery = contentData
                                };

                                await ApiClass.CRUDAPI(Object, "", "SoldColors", ApiClass.Request.PostWithoutMessage);
                            }
                            else
                                MessageBox.Show("Ошибка", "Уведомление");
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow();
            statisticsWindow.Show();
            this.Close();
        }

        private async void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Table.StatusDelivery selectedItem = (Table.StatusDelivery)comboBox.SelectedItem;
            string selectedCategory = (string)selectedItem.NameStatusOrder;
            await GetDelivery(selectedCategory);
        }

        private async void cbStatus_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "StatusDeliveries", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.StatusDelivery>>(content);
                Table.StatusDelivery statusDelivery = new Table.StatusDelivery { IdStatusOrder = 0, NameStatusOrder = "Все" };
                contentData.Add(statusDelivery);
                cmb.ItemsSource = contentData;
                cmb.SelectedItem = statusDelivery;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }
    }
}
