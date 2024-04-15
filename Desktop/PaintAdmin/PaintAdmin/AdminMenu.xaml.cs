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
    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void Cities_Click(object sender, RoutedEventArgs e)
        {
            CityWindow window = new CityWindow();
            window.Show();
            this.Close();
        }

        private void Ral_Colors_Click(object sender, RoutedEventArgs e)
        {
            RalColorsWindow window = new RalColorsWindow();
            window.Show();
            this.Close();
        }

        private void Shine_Click(object sender, RoutedEventArgs e)
        {
            ShineWindow window = new ShineWindow();
            window.Show();
            this.Close();
        }

        private void StatusDelivery_Click(object sender, RoutedEventArgs e)
        {
            StatusDeliveryWindow window = new StatusDeliveryWindow();
            window.Show();
            this.Close();
        }

        private void TempPulvirization_Click(object sender, RoutedEventArgs e)
        {
            TempPulvirizationWindow window = new TempPulvirizationWindow();
            window.Show();
            this.Close();
        }

        private void TypeSurface_Click(object sender, RoutedEventArgs e)
        {
            TypeSurfaceWindow window = new TypeSurfaceWindow();
            window.Show();
            this.Close();
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            UserWindow window = new UserWindow();
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

        private void TypeApplication_Click(object sender, RoutedEventArgs e)
        {
            TypeApplicationWindow window = new TypeApplicationWindow();
            window.Show();
            this.Close();
        }

        private void WareHouse_Click(object sender, RoutedEventArgs e)
        {
            WareHouseWindow window = new WareHouseWindow();
            window.Show();
            this.Close();
        }

        private void BackUp_Click(object sender, RoutedEventArgs e)
        {
            BackUpWindow window = new BackUpWindow();
            window.Show();
            this.Close();
        }

        private void RestoreBackUp_Click(object sender, RoutedEventArgs e)
        {
            RestoreBackUpWindow window = new RestoreBackUpWindow();
            window.Show();
            this.Close();
        }
    }
}
