using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PaintAdmin
{
    public class ApiClass
    {
        private const string APP_PATH = "http://localhost:5179/api/";
        public static string APIKey;

        public enum Request
        {
            Post,
            Delete,
            Put,
            Select,
            SelectByID,
            PostWithoutMessage
        }

        public static async Task<HttpResponseMessage> CRUDAPI(object Object, string id, string nameTable, Request request)
        {
            using (var client = new HttpClient())
            {
                string authorization = APIKey;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);

                client.BaseAddress = new Uri(APP_PATH);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();

                var MyContent = JsonConvert.SerializeObject(Object);

                var buffer = Encoding.UTF8.GetBytes(MyContent);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (response.IsSuccessStatusCode)
                {
                    switch (request)
                    {
                        case Request.Select:
                            response = await client.GetAsync($"{nameTable}").ConfigureAwait(false);
                            return response;
                        case Request.SelectByID:
                            response = await client.GetAsync($"{nameTable}/{id}").ConfigureAwait(false);
                            return response;
                        case Request.Post:
                            var resultPost = await client.PostAsync($"{nameTable}", byteContent).ConfigureAwait(false);
                            if (!resultPost.IsSuccessStatusCode) MessageBox.Show($"Ошибка добавления", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
                            else MessageBox.Show($"Удачное добавление", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
                            return resultPost;
                        case Request.PostWithoutMessage:
                            var resultPostWithoutMessage = await client.PostAsync($"{nameTable}", byteContent).ConfigureAwait(false);
                            if (!resultPostWithoutMessage.IsSuccessStatusCode) MessageBox.Show($"Ошибка добавления", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
                            return resultPostWithoutMessage;
                        case Request.Put:
                            var resultPut = await client.PutAsync($"{nameTable}/{id}", byteContent).ConfigureAwait(false);
                            if (!resultPut.IsSuccessStatusCode) MessageBox.Show($"Ошибка изменения", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
                            return resultPut;
                        case Request.Delete:
                            var resultDelete = await client.DeleteAsync($"{nameTable}/{id}").ConfigureAwait(false);
                            if (!resultDelete.IsSuccessStatusCode) MessageBox.Show($"Ошибка удаления, возможно данные связаны", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
                            return resultDelete;
                    }
                }
                else
                {
                    return response;
                }
                return response;
            }
        }

        public static void GetClearApiKey()
        {
            var charsToRemove = new string[] { "\"" };
            foreach (var c in charsToRemove)
            {
                APIKey = APIKey.Replace(c, string.Empty);
            }
        }

        public static async Task<HttpResponseMessage> RoleByLogin(string nameTable, string Login)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APP_PATH);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();

                response = await client.GetAsync($"{nameTable}/RoleByLogin/{Login}").ConfigureAwait(false);
                return response;
            }
        }

        public static async Task<HttpResponseMessage> CustomGet(string nameTable, string nameCommand)
        {
            using (var client = new HttpClient())
            {
                string authorization = APIKey;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);

                client.BaseAddress = new Uri(APP_PATH);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();

                response = await client.GetAsync($"{nameTable}/{nameCommand}").ConfigureAwait(false);
                return response;
            }
        }

        public static async Task<HttpResponseMessage> CustomDelete(string nameTable, string nameCommand)
        {
            using (var client = new HttpClient())
            {
                string authorization = APIKey;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);

                client.BaseAddress = new Uri(APP_PATH);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();

                response = await client.DeleteAsync($"{nameTable}/{nameCommand}").ConfigureAwait(false);
                return response;
            }
        }

        public static async Task<HttpResponseMessage> UserIDByLogin(string nameTable, string Login)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APP_PATH);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();

                response = await client.GetAsync($"{nameTable}/UserIDByLogin/{Login}").ConfigureAwait(false);
                return response;
            }
        }

        public static async Task<HttpResponseMessage> VerificationByLogin(string nameTable, string Login)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APP_PATH);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();

                response = await client.GetAsync($"{nameTable}/VerificationbyLogin/{Login}").ConfigureAwait(false);
                return response;
            }
        }

        public static async Task<HttpResponseMessage> SignIn(string nameTable, string Login, string Password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(APP_PATH);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();

                response = await client.GetAsync($"{nameTable}/{Login}/{Password}").ConfigureAwait(false);

                return response;
            }
        }
    }
}
