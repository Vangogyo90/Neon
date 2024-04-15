using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestClass
{
    [TestClass]
    public class UnitClass
    {
        [TestMethod]
        public async Task SelectByID_ShouldReturnSuccessStatusCode()
        {
            var request = ApiClass.Request.SelectByID;
            var nameTable = "Cities";

            object Object = new
            {

            };

            var response = await ApiClass.CRUDAPI(Object, "5", nameTable, request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Select_ShouldReturnSuccessStatusCode()
        {
            var request = ApiClass.Request.Select;
            var nameTable = "Cities";

            object Object = new
            {

            };

            var response = await ApiClass.CRUDAPI(Object, "", nameTable, request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task RoleByLogin_ShouldReturnSuccessStatusCode()
        {
            var nameTable = "Users";
            var login = "qwerty12345";

            object Object = new
            {

            };

            var response = await ApiClass.RoleByLogin(nameTable, login);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task CustomGet_ShouldReturnSuccessStatusCode()
        {
            var nameTable = "WareHouses";
            var nameCommand = "GetData";

            object Object = new
            {

            };

            var response = await ApiClass.CustomGet(nameTable, nameCommand);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task SignIn_ShouldReturnSuccessStatusCode()
        {
            var nameTable = "Users";
            var login = "qwerty12345";
            var password = "qwerty12345";

            object Object = new
            {

            };

            var response = await ApiClass.SignIn(nameTable, login, password);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task SignIn_ShouldReturnFalseStatusCode()
        {
            var nameTable = "Users";
            var login = "qwerty12345";
            var password = "qwerty12345";

            object Object = new
            {

            };

            var response = await ApiClass.SignIn(nameTable, "", "");

            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Post_ShouldReturnSuccessStatusCode()
        {
            var request = ApiClass.Request.Post;
            var nameTable = "Cities";

            object Object = new
            {
                nameCity = "Набережные челны"
            };

            var response = await ApiClass.CRUDAPI(Object, "", nameTable, request);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Post_ShouldReturnFalseStatusCode()
        {
            var request = ApiClass.Request.Post;
            var nameTable = "Cities";

            object Object = new
            {
                nameCity = "Набережные челны"
            };

            var response = await ApiClass.CRUDAPI(Object, "", nameTable, request);

            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Put_ShouldReturnSuccessStatusCode()
        {
            var request = ApiClass.Request.Put;
            var nameTable = "Cities";

            object Object = new
            {
                idCity = "26",
                nameCity = "Набережные челны"
            };

            var response = await ApiClass.CRUDAPI(Object, "26", nameTable, request);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnSuccessStatusCode()
        {
            var request = ApiClass.Request.Delete;
            var nameTable = "Cities";

            object Object = new
            {

            };

            var response = await ApiClass.CRUDAPI(Object, "26", nameTable, request);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
