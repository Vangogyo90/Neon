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
    public partial class UpdateUserWindow : Window
    {
        int IdUser;
        List<Table.Token> contentData;
        public UpdateUserWindow(int IdUser)
        {
            InitializeComponent();
            this.IdUser = IdUser;
            GetUser();
        }

        private async void GetUser()
        {
            var res = await ApiClass.CustomGet("Tokens", $"GetDataByID/{IdUser}");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                contentData = JsonConvert.DeserializeObject<List<Table.Token>>(content);
                Roles.SelectedValue = contentData[0].Users.RoleId;
                Verify.IsChecked = contentData[0].Users.Verification;
                if (contentData[0].Token1.Equals("ban"))
                    Ban.IsChecked = true;
                else
                    Ban.IsChecked = false;

            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            object ObjectUser = new
            {
                IdUser = contentData[0].Users.IdUser,
                Login = contentData[0].Users.Login,
                Password = contentData[0].Users.Password,
                Name = contentData[0].Users.Name,
                Surname = contentData[0].Users.Surname,
                Patromymic = contentData[0].Users.Patromymic,
                EMail = contentData[0].Users.EMail,
                NumberTelephone = contentData[0].Users.NumberTelephone,
                Priority = contentData[0].Users.Priority,
                Salt = contentData[0].Users.Salt,
                Verification = Verify.IsChecked,
                Photo = contentData[0].Users.Photo,
                RoleId = Roles.SelectedValue
            };
            await ApiClass.CRUDAPI(ObjectUser, $"{IdUser}", "Users", ApiClass.Request.Put);
            if (Ban.IsChecked.Value)
            {
                object ObjectToken = new
                {
                    IdToken = contentData[0].IdToken,
                    Token1 = "ban",
                    UserId = contentData[0].UserId
                };
                await ApiClass.CRUDAPI(ObjectToken, $"{contentData[0].IdToken}", "Tokens", ApiClass.Request.Put);
            }
            else
            {
                object ObjectToken = new
                {};
                await ApiClass.CRUDAPI(ObjectToken, $"{contentData[0].IdToken}", "Tokens", ApiClass.Request.Delete);
            }
            this.Close();
        }

        private async void Roles_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "Roles", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.Role>>(content);
                cmb.ItemsSource = contentData;
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }
    }
}
