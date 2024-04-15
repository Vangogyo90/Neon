using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
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
    public partial class RalColorsWindow : Window
    {
        public RalColorsWindow()
        {
            InitializeComponent();
            GetRalColors();
            Closing += RalColorsWindow_Closing;
        }

        private void RalColorsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu window = new AdminMenu();
            window.Show();
        }

        private async void GetRalColors()
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "RalCatalogs", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.RalCatalog>>(content);
                ListAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddRalColorsWindow window = new AddRalColorsWindow();
            window.Closed += Window_Closed;
            window.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GetRalColors();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                Table.RalCatalog table = (Table.RalCatalog)ListAPI.SelectedItems[0];
                object Object = new
                { };

                await ApiClass.CRUDAPI(Object, table.IdRalCatalog.ToString(), "RalCatalogs", ApiClass.Request.Delete);
                GetRalColors();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                Table.RalCatalog table = (Table.RalCatalog)ListAPI.SelectedItems[0];
                UpdateRalWindow window = new UpdateRalWindow(table.IdRalCatalog);
                window.Closed += Window_Closed;
                window.ShowDialog();
            }
        }
    }
}
