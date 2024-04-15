using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paq.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using TLSharp.Core;

namespace Paq.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        static string combinedNumbersEmail;

        public static List<ColorsInDelivery> colorsInDeliveries;

        static string TelephoneNumber;
        static string hash;
        static TelegramClient client;

        [HttpPost]
        public async Task<IActionResult> VerificationCodeNumberTelephones(string Code)
        {
            try
            {
                var userVerify = await client.MakeAuthAsync(TelephoneNumber, hash, Code);
                User user = null;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:7192/api/Users/{User.Identity.Name}", HttpCompletionOption.ResponseHeadersRead))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(apiResponse);
                        user.Verification_Number = true;
                        StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                        using (var responseCode = await httpClient.PutAsync($"http://localhost:7192/api/Users/{User.Identity.Name}", content))
                        {
                            if (responseCode.IsSuccessStatusCode)
                            {
                                return RedirectToAction("MyAccount");
                            }
                        }
                    }
                }
                return RedirectToAction("MyAccount");
            }
            catch
            {
                return RedirectToAction("MyAccount");
            }
        }

        public IActionResult VerificationCodeNumberTelephone()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendCode()
        {
            try
            {
                var charsToRemove = new string[] { "+", "(", ")", "-" };
                foreach (var c in charsToRemove)
                {
                    TelephoneNumber = TelephoneNumber.Replace(c, string.Empty);
                }
                client = new TelegramClient(27601009, "a1ede1cdc438b463ab25e3331142db54");
                await client.ConnectAsync();
                hash = await client.SendCodeRequestAsync(TelephoneNumber);
                return RedirectToAction("VerificationCodeNumberTelephone", "Home");
            }
            catch
            {
                return RedirectToAction("VerificationCodeNumberTelephone", "Home");
            }
        }

        public IActionResult VerificationCodeEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerificationCodeEmails(string Code)
        {
            if (Code.Equals(combinedNumbersEmail))
            {
                User user = null;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:7192/api/Users/{User.Identity.Name}", HttpCompletionOption.ResponseHeadersRead))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(apiResponse);
                        user.Verification = true;
                        StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                        using (var responseCode = await httpClient.PutAsync($"http://localhost:7192/api/Users/{User.Identity.Name}", content))
                        {
                            if (responseCode.IsSuccessStatusCode)
                            {
                                return RedirectToAction("MyAccount");
                            }
                        }
                    }
                }
            }

            return RedirectToAction("MyAccount");
        }

        [HttpPost]
        public IActionResult SendMail(string Email)
        {
            try
            {
                string fromAddress = "neon1271@mail.ru";
                string password = "RUFcAKayUzQzZqz2fxys";
                string toAddress = Email;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(toAddress);
                mail.Subject = "Ваш код для подтверждения";
                Random random = new Random();
                int[] randomNumbers = new int[6];

                for (int i = 0; i < 6; i++)
                {
                    randomNumbers[i] = random.Next(1, 9);
                }
                combinedNumbersEmail = string.Join("", randomNumbers);
                mail.Body = $"Ваш случайный код: {combinedNumbersEmail}.\nНе сообщайте его никому!";

                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromAddress, password);

                smtp.Send(mail);

                return RedirectToAction("VerificationCodeEmail", "Home");
            }
            catch
            {
                return View();
            }
        }

            public async Task<IActionResult> MyAccount()
        {
            User user = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:7192/api/Users/{User.Identity.Name}", HttpCompletionOption.ResponseHeadersRead))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiResponse);
                    TelephoneNumber = user.NumberTelephone;
                }
            }

            return View(user);
        }

        public IActionResult Agreement()
        {
            return View();
        }

        public IActionResult Guide()
        {
            return View();
        }

        public async Task<ViewResult> Cart()
        {
            List<City> cities = await GetCities();
            ViewBag.Cities = cities;
            return View(colorsInDeliveries);
        }

        [HttpGet]
        public async Task<IActionResult> MyDeliveryUpdate(int IdDelivery, string Adress, string Salt, int CityId, int StatusOrderId, int UserId, string Date)
        {
            try
            {
                Delivery delivery = new Delivery();
                delivery.IdDelivery = IdDelivery;
                delivery.Adress = Adress;
                delivery.Salt = Salt;
                delivery.CityId = CityId;
                delivery.StatusOrderId = StatusOrderId;
                delivery.UserId = UserId;
                delivery.Date = Convert.ToDateTime(Date);

                List<ColorDelivery> colorDelivery = null;
                QuantityColor quantityColor = null;

                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(delivery), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"http://localhost:7192/api/Deliveries/{IdDelivery}", content))
                    {
                        using (var responseColor = await httpClient.GetAsync($"http://localhost:7192/api/ColorDeliveries/GetData/{IdDelivery}", HttpCompletionOption.ResponseHeadersRead))
                        {
                            if (responseColor.IsSuccessStatusCode)
                            {
                                string apiResponse = await responseColor.Content.ReadAsStringAsync();
                                colorDelivery = JsonConvert.DeserializeObject<List<ColorDelivery>>(apiResponse);

                                for (int i = 0; colorDelivery.Count() > i; i++)
                                {
                                    using (var responseQuantityColor = await httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/GetDataByColorID/{colorDelivery[i].ColorId}", HttpCompletionOption.ResponseHeadersRead))
                                    {
                                        if (responseQuantityColor.IsSuccessStatusCode)
                                        {
                                            string apiResponseQuantityColor = await responseQuantityColor.Content.ReadAsStringAsync();
                                            quantityColor = JsonConvert.DeserializeObject<QuantityColor>(apiResponseQuantityColor);
                                            quantityColor.Quantity = quantityColor.Quantity - colorDelivery[i].Quantity_Of_Color;
                                            StringContent contentQuantity = new StringContent(JsonConvert.SerializeObject(quantityColor), Encoding.UTF8, "application/json");
                                            using (var responseQuantity = await httpClient.PutAsync($"http://localhost:7192/api/QuantityColors/{quantityColor.IdQuantityColors}", contentQuantity))
                                            {
                                            }
                                        }
                                    }
                                }
                                return RedirectToAction("MyDeliveries");
                            }
                        }
                    }
                }
                return RedirectToAction("MyDeliveries");
            }
            catch
            {
                return RedirectToAction("MyDeliveries");
            }
        }

        [HttpPost]
        public IActionResult RemoveFromCartNotAll(int quantityid, int quantityToRemove)
        {
            try
            {
                var itemToRemove = colorsInDeliveries.FirstOrDefault(item => item.Color.IdQuantityColors == quantityid);

                if (itemToRemove != null)
                {
                    if (quantityToRemove == 0)
                    {
                        colorsInDeliveries.Remove(itemToRemove);
                    }
                    else
                    {
                        if (itemToRemove.Quantity > quantityToRemove)
                        {
                            itemToRemove.Quantity -= quantityToRemove;
                            if (itemToRemove.Quantity <= 0)
                            {
                                colorsInDeliveries.Remove(itemToRemove);
                            }
                        }
                        else
                        {
                            colorsInDeliveries.Remove(itemToRemove);
                        }
                    }
                }

                return RedirectToAction("Cart");
            }
            catch
            {
                return RedirectToAction("Cart");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddCart(int Quantity, int quantityid, int pageCount)
        {
            try
            {
                if (Quantity == 0)
                {
                    Quantity = 1;
                }

                if (colorsInDeliveries == null)
                {
                    colorsInDeliveries = new List<ColorsInDelivery>();
                }

                QuantityColor productDetails = null;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/GetDataByID/{quantityid}", HttpCompletionOption.ResponseHeadersRead))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        productDetails = JsonConvert.DeserializeObject<QuantityColor>(apiResponse);
                    }
                }

                Discount discount = null;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:7192/api/Discounts/GetDiscountByColor/{productDetails.ColorId}", HttpCompletionOption.ResponseHeadersRead))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        discount = JsonConvert.DeserializeObject<Discount>(apiResponse);
                    }
                }

                var existingItem = colorsInDeliveries.FirstOrDefault(item => item.Color.ColorId == productDetails.ColorId);

                if (existingItem != null)
                {
                    existingItem.Quantity += Quantity;
                }
                else
                {
                    var newItem = new ColorsInDelivery
                    {
                        Quantity = Quantity,
                        Color = productDetails,
                        Discount = discount
                    };

                    colorsInDeliveries.Add(newItem);
                }

                return RedirectToAction("Shop", new { pageCount });
            }
            catch
            {
                return RedirectToAction("Shop", new { pageCount });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStocksCart(int Quantity, int quantityid, int pageCount)
        {
            try
            {
                if (Quantity == 0)
                {
                    Quantity = 1;
                }

                if (colorsInDeliveries == null)
                {
                    colorsInDeliveries = new List<ColorsInDelivery>();
                }

                QuantityColor productDetails = null;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/GetDataByID/{quantityid}", HttpCompletionOption.ResponseHeadersRead))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        productDetails = JsonConvert.DeserializeObject<QuantityColor>(apiResponse);
                    }
                }

                Discount discount = null;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:7192/api/Discounts/GetDiscountByColor/{productDetails.ColorId}", HttpCompletionOption.ResponseHeadersRead))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        discount = JsonConvert.DeserializeObject<Discount>(apiResponse);
                    }
                }

                var existingItem = colorsInDeliveries.FirstOrDefault(item => item.Color.ColorId == productDetails.ColorId);

                if (existingItem != null)
                {
                    existingItem.Quantity += Quantity;
                }
                else
                {
                    var newItem = new ColorsInDelivery
                    {
                        Quantity = Quantity,
                        Color = productDetails,
                        Discount = discount
                    };

                    colorsInDeliveries.Add(newItem);
                }

                return RedirectToAction("Stocks", new { pageCount });
            }
            catch
            {
                return RedirectToAction("Stocks", new { pageCount });
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Paint()
        {
            return View();
        }

        public ViewResult CallBack() => View();

        [HttpPost]
        public async Task<IActionResult> CallBack(FeedBack feedback, string messageBytes)
        {
            byte[] bytes = messageBytes.Split(',').Select(byte.Parse).ToArray();
            string message = Encoding.UTF8.GetString(bytes);

            feedback.Message = Encoding.UTF8.GetBytes(message);

            FeedBack feed = new FeedBack();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:7192/api/FeedBacks", content))
                {
                    string apiResonse = await response.Content.ReadAsStringAsync();
                    feed = JsonConvert.DeserializeObject<FeedBack>(apiResonse);
                }
            }
            return View(feed);
        }

        public async Task<List<City>> GetCities()
        {
            List<City> cities = new List<City>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:7192/api/Cities"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cities = JsonConvert.DeserializeObject<List<City>>(apiResponse);
                }
            }

            return cities;
        }

        [HttpPost]
        public async Task<IActionResult> Cart(string Adress, int CityId, int StatusOrderId, int UserId)
        {
            Delivery delivery = new Delivery();
            delivery.Adress = Adress;
            delivery.CityId = CityId;
            delivery.StatusOrderId = StatusOrderId;
            delivery.UserId = UserId;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(delivery), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:7192/api/Deliveries", content))
                {
                    string apiResonse = await response.Content.ReadAsStringAsync();
                    delivery = JsonConvert.DeserializeObject<Delivery>(apiResonse);

                    ColorDelivery colorsInDelivery = new ColorDelivery();
                    colorsInDelivery.DeliveryId = delivery.IdDelivery;

                    for (int i = 0; i < colorsInDeliveries.Count(); i++)
                    {
                        colorsInDelivery.ColorId = colorsInDeliveries[i].Color.ColorId;
                        colorsInDelivery.Quantity_Of_Color = colorsInDeliveries[i].Quantity;
                        StringContent contentColors = new StringContent(JsonConvert.SerializeObject(colorsInDelivery), Encoding.UTF8, "application/json");
                        using (var responseColor = await httpClient.PostAsync("http://localhost:7192/api/ColorDeliveries", contentColors))
                        {
                            string apiResonseColors = await responseColor.Content.ReadAsStringAsync();
                        }
                    }
                    colorsInDeliveries.Clear();
                }
            }
            return View();
        }

        public async Task<IActionResult> Shop(int pageCount)
        {
            Pagination paginations = new Pagination();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/Pagination?pageNumber={pageCount}&pageSize=27", HttpCompletionOption.ResponseHeadersRead))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    paginations = JsonConvert.DeserializeObject<Pagination>(apiResponse);
                }
            }

            return View(paginations);
        }

        public async Task<IActionResult> MyDeliveries()
        {
            List<MyDelivery> myDeliveries = new List<MyDelivery>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:7192/api/Deliveries/GetDataByUser?idUser={User.Identity.Name}", HttpCompletionOption.ResponseHeadersRead))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var Delivery = JsonConvert.DeserializeObject<List<Delivery>>(apiResponse);

                    string apiResponseColor = "";
                    string apiResponsePrice = "";
                    for (int i = 0; i < Delivery.Count(); i++)
                    {
                        using (var responseColor = await httpClient.GetAsync($"http://localhost:7192/api/ColorDeliveries/GetData/{Delivery[i].IdDelivery}", HttpCompletionOption.ResponseHeadersRead))
                        {
                            apiResponseColor = await responseColor.Content.ReadAsStringAsync();
                            var colorDeliveryColor = JsonConvert.DeserializeObject<List<ColorDelivery>>(apiResponseColor);

                            using (var responsePrice = await httpClient.GetAsync($"http://localhost:7192/api/Price/CalculateTotalPriceForDelivery/{Delivery[i].IdDelivery}", HttpCompletionOption.ResponseHeadersRead))
                            {
                                apiResponsePrice = await responsePrice.Content.ReadAsStringAsync();
                                var colorDeliveryPrice = JsonConvert.DeserializeObject<double>(apiResponsePrice);
                                Delivery[i].PriceAll = colorDeliveryPrice;
                            }

                            myDeliveries.Add(new MyDelivery { delivery = Delivery[i], colorDeliveries = colorDeliveryColor });
                        }
                    }
                }
            }
            return View(myDeliveries);
        }

        public async Task<IActionResult> News()
        {
            List<News> news = new List<News>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:7192/api/News", HttpCompletionOption.ResponseHeadersRead))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    news = JsonConvert.DeserializeObject <List<News>>(apiResponse);
                }
            }

            return View(news);
        }

        public async Task<IActionResult> Stocks(int pageCount)
        {
            Pagination paginations = new Pagination();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/PaginationStocks?pageNumber={pageCount}&pageSize=27", HttpCompletionOption.ResponseHeadersRead))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    paginations = JsonConvert.DeserializeObject<Pagination>(apiResponse);
                }
            }

            return View(paginations);
        }

        [HttpGet("ShopByID/{id}")]
        public async Task<IActionResult> ShopByID(int id)
        {
            QuantityColor productDetails = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/GetDataByID/{id}", HttpCompletionOption.ResponseHeadersRead))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productDetails = JsonConvert.DeserializeObject<QuantityColor>(apiResponse);
                }
            }

            if (productDetails == null)
            {
                return NotFound();
            }

            return View(productDetails);
        }

        [HttpGet("ChequeByID/{id}")]
        public async Task<IActionResult> ChequeByID(int id)
        {
            Delivery delivery = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:7192/api/Deliveries/{id}", HttpCompletionOption.ResponseHeadersRead))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    delivery = JsonConvert.DeserializeObject<Delivery>(apiResponse);
                }
            }

            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        [HttpGet("PDFShop/{id}")]
        public async Task<IActionResult> PDFShop(int id)
        {
            Color color = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:7192/api/Colors/{id}", HttpCompletionOption.ResponseHeadersRead))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    color = JsonConvert.DeserializeObject<Color>(apiResponse);
                }
            }

            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        [HttpGet("NewsByID/{id}")]
        public async Task<IActionResult> NewsByID(int id)
        {
            NewsPhoto newsPhoto = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:7192/api/News/NewsPhoto/{id}", HttpCompletionOption.ResponseHeadersRead))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newsPhoto = JsonConvert.DeserializeObject<NewsPhoto>(apiResponse);
                }
            }

            if (newsPhoto == null)
            {
                return NotFound();
            }

            return View(newsPhoto);
        }

        public IActionResult SearchShop(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Shop");
            }

            Pagination searchResults = new Pagination();
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/Search?searchQuery={query}?pageNumber=1&pageSize=10000").Result;

                if (response.IsSuccessStatusCode)
                {
                    searchResults = JsonConvert.DeserializeObject<Pagination>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return View("Shop", searchResults);
        }

        public IActionResult SearchStocks(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Stocks");
            }

            Pagination searchResults = new Pagination();
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/SearchStocks?searchQuery={query}?pageNumber=1&pageSize=10000").Result;

                if (response.IsSuccessStatusCode)
                {
                    searchResults = JsonConvert.DeserializeObject<Pagination>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return View("Stocks", searchResults);
        }

        public IActionResult UpShop()
        {
            Pagination results = new Pagination();
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/UpPrice?pageNumber=1&pageSize=10000").Result;

                if (response.IsSuccessStatusCode)
                {
                    results = JsonConvert.DeserializeObject<Pagination>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return View("Shop", results);
        }

        public IActionResult UpPriceStocks()
        {
            Pagination results = new Pagination();
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/UpPriceStocks?pageNumber=1&pageSize=10000").Result;

                if (response.IsSuccessStatusCode)
                {
                    results = JsonConvert.DeserializeObject<Pagination>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return View("Stocks", results);
        }

        public IActionResult DownShop()
        {
            Pagination results = new Pagination();
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/DownPrice?pageNumber=1&pageSize=10000").Result;

                if (response.IsSuccessStatusCode)
                {
                    results = JsonConvert.DeserializeObject<Pagination>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return View("Shop", results);
        }

        public IActionResult DownPriceStocks()
        {
            Pagination results = new Pagination();
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.GetAsync($"http://localhost:7192/api/QuantityColors/DownPriceStocks?pageNumber=1&pageSize=10000").Result;

                if (response.IsSuccessStatusCode)
                {
                    results = JsonConvert.DeserializeObject<Pagination>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return View("Stocks", results);
        }

        public IActionResult Laborathory()
        {
            return View();
        }

        public async Task<IActionResult> Palette()
        {
            List<RalCatalog> ralCatalogs = new List<RalCatalog>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:7192/api/RalCatalogs", HttpCompletionOption.ResponseHeadersRead))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ralCatalogs = JsonConvert.DeserializeObject<List<RalCatalog>>(apiResponse);
                }
            }

            return View(ralCatalogs);
        }

        public IActionResult Production()
        {
            return View();
        }
    }
}