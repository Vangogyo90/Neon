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
    public partial class PrewiewWindow : Window
    {
        int IdNews;
        public PrewiewWindow(int IdNews)
        {
            InitializeComponent();
            this.IdNews = IdNews;
            GetNews();
        }

        private async void GetNews()
        {
            object Object = new { };
            var res = await ApiClass.CRUDAPI(Object, $"{IdNews}", "News", ApiClass.Request.SelectByID);
            var content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                var contentData = JsonConvert.DeserializeObject<Table.News>(content);
                string TextNews = Encoding.UTF8.GetString(contentData.TextNews);
                LoadHtml($"<!DOCTYPE html>" +
                         $"<html lang='en'>" +
                         $"<head>" +
                         $"<meta charset='UTF-8'>" +
                         $"<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
                         $"<link href='https://fonts.googleapis.com/css2?family=Commissioner:wght@200&display=swap' rel='stylesheet'>" +
                         $"<link href='https://fonts.googleapis.com/css2?family=Ubuntu&display=swap' rel='stylesheet'>" +
                         $"<style>" +
                         $".Main_information div {{" +
                         $"    padding: 5px;" +
                         $"    margin: 15px;" +
                         $"}}" +
                 "body {" +
         "  background-color: #fff;" +
         "  margin: 0;" +
         "  padding: 0;" +
         "}" +
         ".Main_information {" +
         "    background-color: #F2F2F2;" +
         "    flex-wrap: nowrap;" +
         "    margin: auto;" +
         "    box-shadow: 12px 8px 0px 0px #c21f20;" +
         "    font-family: 'Ubuntu', sans-serif;" +
         "}" +
         "</style>" +
         "</head>" +
         "<body>" +
         "<div class='Main_information'>" +
         $"<p style='white-space: pre-wrap;'>{TextNews}</p>" +
         "</div>" +
         "</body>" +
         "</html>");
            }
            else
                MessageBox.Show("Ошибка", "Уведомление");
        }

        private void LoadHtml(string html)
        {
            webBrowser.NavigateToString(html);
        }

    }
}
