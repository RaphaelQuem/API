using Microsoft.VisualStudio.TestTools.UnitTesting;
using RodesAPI.Controllers;
using RodesAPI.Services;
using RodesAPI.ViewModel;
using System;
using System.Net.Http;

namespace RodesAPI.Tests.Controller
{
    [TestClass]
    public class ItemsControllerTest
    {
        string token="";
        [TestInitialize]
        public void Init()
        {
            token = TokenService.Tokenize("TESTE");
        }
        [TestMethod]
        public void GetTest()
        {
            ItemsController controller = new ItemsController();
            ItemsVM vm = new ItemsVM();

            vm = controller.Get("10000000004895");

            Assert.AreEqual(vm.CodBarra, 10000000004895M);
        }

    }
}
