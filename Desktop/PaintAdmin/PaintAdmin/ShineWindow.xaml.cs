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
    public partial class ShineWindow : Window
    {
        public ShineWindow()
        {
            InitializeComponent();
            GetShine();
            Closing += ShineWindow_Closing;
        }

        private void ShineWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminPanel = new AdminMenu();
            adminPanel.Show();
        }

        private async void GetShine()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "Shines", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.Shine>>(content);
                DataAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void DataAPI_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Table.Shine table = e.Row.Item as Table.Shine;
            if (table.FirstProcent > 0 && table.EndProcent > 0 && table.NameShine != null)
            {
                if (table.IdShine > 0)
                {
                    object Object = new
                    {
                        IdShine = table.IdShine,
                        FirstProcent = table.FirstProcent,
                        EndProcent = table.EndProcent,
                        NameShine = table.NameShine
                    };
                    await ApiClass.CRUDAPI(Object, table.IdShine.ToString(), "Shines", ApiClass.Request.Put);
                    GetShine();
                }
                else
                {
                    object Object = new
                    {
                        FirstProcent = table.FirstProcent,
                        EndProcent = table.EndProcent,
                        NameShine = table.NameShine
                    };
                    await ApiClass.CRUDAPI(Object, "", "Shines", ApiClass.Request.Post);
                    GetShine();
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
                    Table.Shine table = (Table.Shine)DataAPI.SelectedItems[0];
                    object Object = new
                    { };

                    await ApiClass.CRUDAPI(Object, table.IdShine.ToString(), "Shines", ApiClass.Request.Delete);
                    GetShine();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка", "Уведомление");
            }
        }
    }
}
