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
    public partial class TypeApplicationWindow : Window
    {
        public TypeApplicationWindow()
        {
            InitializeComponent();
            GetTypeApplication();
            Closing += TypeApplicationWindow_Closing;
        }

        private void TypeApplicationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminPanel = new AdminMenu();
            adminPanel.Show();
        }

        private async void GetTypeApplication()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "TypeApplications", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.TypeApplication>>(content);
                DataAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void DataAPI_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Table.TypeApplication table = e.Row.Item as Table.TypeApplication;
            if (table.IdTypeApplication > 0)
            {
                object Object = new
                {
                    IdTypeApplication = table.IdTypeApplication,
                    NameTypeApplication = table.NameTypeApplication
                };
                await ApiClass.CRUDAPI(Object, table.IdTypeApplication.ToString(), "TypeApplications", ApiClass.Request.Put);
                GetTypeApplication();
            }
            else
            {
                object Object = new
                {
                    NameTypeApplication = table.NameTypeApplication
                };
                await ApiClass.CRUDAPI(Object, "", "TypeApplications", ApiClass.Request.Post);
                GetTypeApplication();
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
                {
                    Table.TypeApplication table = (Table.TypeApplication)DataAPI.SelectedItems[0];
                    object Object = new
                    { };

                    await ApiClass.CRUDAPI(Object, table.IdTypeApplication.ToString(), "TypeApplications", ApiClass.Request.Delete);
                    GetTypeApplication();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка", "Уведомление");
            }
        }
    }
}
