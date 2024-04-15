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
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            GetUsers();
            Closing += UserWindow_Closing;
        }

        private void UserWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu window = new AdminMenu();
            window.Show();
        }

        private async void GetUsers()
        {
            var res = await ApiClass.CustomGet("Tokens", $"GetData/{RegistryLastUserSettings.GetUserID()}");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.Token>>(content);
                foreach (Table.Token token in contentData)
                {
                    if (token.Token1.Equals("ban"))
                        token.Token1 = "БАН";
                    else
                        token.Token1 = "";
                }
                ListAPI.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                Table.Token table = (Table.Token)ListAPI.SelectedItems[0];
                object Object = new
                { };

                await ApiClass.CRUDAPI(Object, table.IdToken.ToString(), "Tokens", ApiClass.Request.Delete);
                await ApiClass.CRUDAPI(Object, table.UserId.ToString(), "Users", ApiClass.Request.Delete);
                GetUsers();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ListAPI.SelectedItem != null)
            {
                Table.Token table = (Table.Token)ListAPI.SelectedItems[0];
                UpdateUserWindow window = new UpdateUserWindow(table.UserId);
                window.Closed += Window_Closed;
                window.ShowDialog();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GetUsers();
        }
    }
}
