using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    public partial class EmailWindow : Window
    {
        public EmailWindow()
        {
            InitializeComponent();
            Closing += EmailWindow_Closing;
        }

        private void EmailWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClientManagerMenu clientManagerMenu = new ClientManagerMenu();
            clientManagerMenu.Show();
        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            object Object = new
            { };
            var res = await ApiClass.CRUDAPI(Object, "", "Users", ApiClass.Request.Select);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.User>>(content);
                try
                {
                    string fromAddress = "neon1271@mail.ru";
                    string password = "RUFcAKayUzQzZqz2fxys";
                    foreach (Table.User user in contentData)
                    {
                        if (user.Verification)
                        {
                            string toAddress = user.EMail;

                            MailMessage mail = new MailMessage();
                            mail.From = new MailAddress(fromAddress);
                            mail.To.Add(toAddress);
                            mail.Subject = Header.Text;

                            mail.Body = Text.Text;

                            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(fromAddress, password);

                            smtp.Send(mail);
                        }
                    }
                    MessageBox.Show("Рассылка удачно отправлена");
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так!");
                }
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }
    }
}
