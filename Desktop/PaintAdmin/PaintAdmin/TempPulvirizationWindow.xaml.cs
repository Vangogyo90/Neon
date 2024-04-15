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
    public partial class TempPulvirizationWindow : Window
    {
        public TempPulvirizationWindow()
        {
            InitializeComponent();
            GetTempPulverization();
            Closing += TempPulvirizationWindow_Closing;
        }

        private void TempPulvirizationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminPanel = new AdminMenu();
            adminPanel.Show();
        }

        private async void GetTempPulverization()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "TempPulverizations", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.TempPulverization>>(content);
                DataAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void DataAPI_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Table.TempPulverization table = e.Row.Item as Table.TempPulverization;
            if (table.Degree > 0 && table.Time > 0)
            {
                if (table.IdTempPulverization > 0)
                {
                    object Object = new
                    {
                        IdTempPulverization = table.IdTempPulverization,
                        Degree = table.Degree,
                        Time = table.Time
                    };
                    await ApiClass.CRUDAPI(Object, table.IdTempPulverization.ToString(), "TempPulverizations", ApiClass.Request.Put);
                    GetTempPulverization();
                }
                else
                {
                    object Object = new
                    {
                        Degree = table.Degree,
                        Time = table.Time
                    };
                    await ApiClass.CRUDAPI(Object, "", "TempPulverizations", ApiClass.Request.Post);
                    GetTempPulverization();
                }
            }
            else
                e.Cancel = true;
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
                {
                    Table.TempPulverization table = (Table.TempPulverization)DataAPI.SelectedItems[0];
                    object Object = new
                    { };

                    await ApiClass.CRUDAPI(Object, table.IdTempPulverization.ToString(), "TempPulverizations", ApiClass.Request.Delete);
                    GetTempPulverization();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка", "Уведомление");
            }
        }
    }
}
