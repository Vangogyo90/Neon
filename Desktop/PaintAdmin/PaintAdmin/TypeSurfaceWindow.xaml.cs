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
    public partial class TypeSurfaceWindow : Window
    {
        public TypeSurfaceWindow()
        {
            InitializeComponent();
            GetTypeSurface();
            Closing += TypeSurfaceWindow_Closing;
        }

        private void TypeSurfaceWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminPanel = new AdminMenu();
            adminPanel.Show();
        }

        private async void GetTypeSurface()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "TypeSurfaces", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.TypeSurface>>(content);
                DataAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void DataAPI_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Table.TypeSurface table = e.Row.Item as Table.TypeSurface;
            if (table.IdTypeSurface > 0)
            {
                object Object = new
                {
                    IdTypeSurface = table.IdTypeSurface,
                    NameTypeSurface = table.NameTypeSurface
                };
                await ApiClass.CRUDAPI(Object, table.IdTypeSurface.ToString(), "TypeSurfaces", ApiClass.Request.Put);
                GetTypeSurface();
            }
            else
            {
                object Object = new
                {
                    NameTypeSurface = table.NameTypeSurface
                };
                await ApiClass.CRUDAPI(Object, "", "TypeSurfaces", ApiClass.Request.Post);
                GetTypeSurface();
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataAPI.Items.Count != 0 & DataAPI.SelectedItems.Count != 0)
                {
                    Table.TypeSurface table = (Table.TypeSurface)DataAPI.SelectedItems[0];
                    object Object = new
                    { };

                    await ApiClass.CRUDAPI(Object, table.IdTypeSurface.ToString(), "TypeSurfaces", ApiClass.Request.Delete);
                    GetTypeSurface();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка", "Уведомление");
            }
        }
    }
}
