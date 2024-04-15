using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public partial class RestoreBackUpWindow : Window
    {
        public RestoreBackUpWindow()
        {
            InitializeComponent();
            Closing += RestoreBackUpWindow_Closing;
        }

        private void RestoreBackUpWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
        }

        private async void GetBackUP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = $"Data Source={NameServer.Text};Initial Catalog=master;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"RESTORE DATABASE [{NameBD.Text}] FROM DISK = '{Path.Text}' WITH REPLACE";
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show("Резервная копия восстановлена успешно");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при восстановлении резервной копии: {ex.Message}");
            }
        }
    }
}
