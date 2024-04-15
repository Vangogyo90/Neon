using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;

namespace PaintAdmin
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (RegistryLastUserSettings.CheckUser())
            {
                this.Visibility = Visibility.Hidden;
                EnterMethod(RegistryLastUserSettings.GetLogin(), RegistryLastUserSettings.GetPassword());
            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            EnterMethod(Login.Text, Password.Text);
        }

        private async void EnterMethod(string Login, string Password)
        {
            try
            {
                Enter.IsEnabled = false;
                var resUser = await ApiClass.SignIn("Users", Login, Password);
                var UserID = await ApiClass.UserIDByLogin("Users", Login);

                if (resUser.IsSuccessStatusCode)
                {
                    ApiClass.APIKey = await resUser.Content.ReadAsStringAsync();
                    ApiClass.GetClearApiKey();
                    var resRole = await ApiClass.RoleByLogin("Users", Login);
                    if (await resRole.Content.ReadAsStringAsync() == "1")
                    {
                        RegistryLastUserSettings.DeleteUser();
                        RegistryLastUserSettings.AddUser(Login, Password, await resRole.Content.ReadAsStringAsync(), await UserID.Content.ReadAsStringAsync());
                        AdminMenu window = new AdminMenu();
                        window.Show();
                        this.Close();
                    }
                    else if (await resRole.Content.ReadAsStringAsync() == "2")
                    {
                        RegistryLastUserSettings.DeleteUser();
                        RegistryLastUserSettings.AddUser(Login, Password, await resRole.Content.ReadAsStringAsync(), await UserID.Content.ReadAsStringAsync());
                        DeliveryWindow window = new DeliveryWindow();
                        window.Show();
                        this.Close();
                    }
                    else if (await resRole.Content.ReadAsStringAsync() == "3")
                    {
                        RegistryLastUserSettings.DeleteUser();
                        RegistryLastUserSettings.AddUser(Login, Password, await resRole.Content.ReadAsStringAsync(), await UserID.Content.ReadAsStringAsync());
                        ProductWindow window = new ProductWindow();
                        window.Show();
                        this.Close();
                    }
                    else if (await resRole.Content.ReadAsStringAsync() == "4")
                    {
                        RegistryLastUserSettings.DeleteUser();
                        RegistryLastUserSettings.AddUser(Login, Password, await resRole.Content.ReadAsStringAsync(), await UserID.Content.ReadAsStringAsync());
                        ClientManagerMenu window = new ClientManagerMenu();
                        window.Show();
                        this.Close();
                    }
                    else if (await resRole.Content.ReadAsStringAsync() == "5")
                    {
                        RegistryLastUserSettings.DeleteUser();
                        RegistryLastUserSettings.AddUser(Login, Password, await resRole.Content.ReadAsStringAsync(), await UserID.Content.ReadAsStringAsync());
                        WareHouseMenu window = new WareHouseMenu();
                        window.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя не существует в системе", "Уведомление");
                    }
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                    this.Visibility = Visibility.Visible;
                }
                Enter.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show($"Ошибка подключения! Проверьте сервер", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
                this.Close();
            }
        }

        private void PDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog svdlg = new SaveFileDialog();
            svdlg.Filter = "Document (*.pdf)|*.pdf;";
            svdlg.FileName = $"Руководство пользователя.pdf";
            if (svdlg.ShowDialog() == true)
            {
                string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string filePath = Path.Combine(projectDirectory, "PDF", "Guide.pdf");
                string destinationFilePath = svdlg.FileName;
                File.Copy(filePath, destinationFilePath, true);
            }
        }
    }
}
