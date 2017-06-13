using Microsoft.VisualStudio.TestTools.UnitTesting;
using RodesAPI.Controllers;
using RodesAPI.Services;
using RodesAPI.ViewModel;
using System;
using System.Net.Http;

namespace RodesAPI.Tests.Controller
{
    [TestClass]
    public class AuthControllerTest
    {
        [TestMethod]
        public void GetTest()
        {
            AuthController controller = new AuthController();
            UserVM vm = new UserVM();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:60808/rodesapi/auth" );

            request.Headers.Add("login", "teste");
            request.Headers.Add("pass","");

            controller.Request = request;
            vm = controller.Get();

            Assert.AreEqual(vm.UserName, "TESTE");
        }

    }
}
