using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    public partial class FeedBackWindow : Window
    {
        public FeedBackWindow()
        {
            InitializeComponent();
            cbCategory.SelectedIndex = 0;
            Closing += FeedBackWindow_Closing;
        }

        private void FeedBackWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClientManagerMenu window = new ClientManagerMenu();
            window.Show();
        }

        private async Task GetFeedbacks(string category)
        {
            var res = await ApiClass.CustomGet("FeedBacks", $"GetFeedBacksByCategory/{(category.Equals("Все") ? "All" : category)}");
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<List<Table.FeedBack>>(content);
                if (contentData.Count > 0)
                {
                    No.Visibility = Visibility.Hidden;
                    ListAPI.Visibility = Visibility.Visible;
                    foreach (Table.FeedBack feedback in contentData)
                    {
                        feedback.Mes = Encoding.UTF8.GetString(feedback.Message);
                        if (feedback.Date < DateTime.Now.AddDays(-3) && !feedback.End)
                        {
                            feedback.IsDateExpired = true;
                        }
                    }
                    ListAPI.ItemsSource = contentData;
                }
                else
                {
                    No.Visibility = Visibility.Visible;
                    ListAPI.Visibility = Visibility.Hidden;
                }
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Table.FeedBack table = (Table.FeedBack)button.DataContext;
            table.End = true;
            UpdateFeedback(table);
            ListAPI.SelectedItem = table;
        }

        private void UnCheck_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Table.FeedBack table = (Table.FeedBack)button.DataContext;
            table.End = false;
            UpdateFeedback(table);
            ListAPI.SelectedItem = table;
        }

        private async void UpdateFeedback(Table.FeedBack feedback)
        {
            object Object = new
            {
                IdFeedBack = feedback.IdFeedBack,
                Number_Or_E_mail = feedback.Number_Or_E_mail,
                End = feedback.End,
                NameUser = feedback.NameUser,
                Message = feedback.Message,
                UserId = feedback.UserId,
                Date = feedback.Date,
                Category = feedback.Category
            };

            await ApiClass.CRUDAPI(Object, $"{feedback.IdFeedBack}", "FeedBacks", ApiClass.Request.Put);
            await GetFeedbacks(cbCategory.Text);
        }

        private async void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            string selectedCategory = (string)selectedItem.Content;
            await GetFeedbacks(selectedCategory);
        }
    }
}
