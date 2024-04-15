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
    public partial class BackUpWindow : Window
    {
        public BackUpWindow()
        {
            InitializeComponent();
            Closing += BackUpWindow_Closing;
        }

        private void BackUpWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdminMenu window = new AdminMenu();
            window.Show();
        }

        private async void GetBackUP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = $"Data Source={NameServer.Text};Initial Catalog={NameBD.Text};Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"BACKUP DATABASE [{NameBD.Text}] TO DISK = '{Path.Text}'";
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show("BackUp создан");
                    connection.Close();
                }
            }
            catch
            {
                MessageBox.Show("Что-то не так! Проверьте данные");
            }
        }
    }
}
