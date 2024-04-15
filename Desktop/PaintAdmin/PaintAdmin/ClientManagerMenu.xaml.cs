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
    public partial class ClientManagerMenu : Window
    {
        public ClientManagerMenu()
        {
            InitializeComponent();
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            FeedBackWindow window = new FeedBackWindow();
            window.Show();
            this.Close();
        }

        private void News_Click(object sender, RoutedEventArgs e)
        {
            NewsWindow window = new NewsWindow();
            window.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            RegistryLastUserSettings.DeleteUser();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Email_Click(object sender, RoutedEventArgs e)
        {
            EmailWindow window = new EmailWindow();
            window.Show();
            this.Close();
        }
    }
}
