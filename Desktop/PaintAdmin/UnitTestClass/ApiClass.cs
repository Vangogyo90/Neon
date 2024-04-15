using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UnitTestClass
{
    public class ApiClass
    {
        private const string APP_PATH = "http://localhost:5179/api/";
        public static string APIKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicXdlcnR5MTIzNDUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiIxIiwiaXNzIjoiTXlBdXRoU2VydmVyIiwiYXVkIjoiTXlBdXRoQ2xpZW50In0.emGMzvxVhNzFUmfRsqydgkJR7k9bFzjmJ5JCQoXY08o";

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
                           return resultPost;
                        case Request.PostWithoutMessage:
                            var resultPostWithoutMessage = await client.PostAsync($"{nameTable}", byteContent).ConfigureAwait(false);
                            return resultPostWithoutMessage;
                        case Request.Put:
                            var resultPut = await client.PutAsync($"{nameTable}/{id}", byteContent).ConfigureAwait(false);
                            return resultPut;
                        case Request.Delete:
                            var resultDelete = await client.DeleteAsync($"{nameTable}/{id}").ConfigureAwait(false);
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
