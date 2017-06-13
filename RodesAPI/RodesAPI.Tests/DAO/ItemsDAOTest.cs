using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RodesAPI.DAO;
using System.Data.SqlClient;
using RodesAPI.Infra;
using System.Collections.Generic;
using RodesAPI.ViewModel;
using RodesAPI.Services;

namespace RodesAPI.Tests
{
    [TestClass]
    public class ItemDAOTest
    {
        [TestMethod]
        public void GetItemVMByItemIDTest()
        {
            ItemsVM vm = new ItemsVM();

            using (SqlConnection con = RodesAPIHelper.GetOpenConnection())
            {
                ItemsDAO dao = new ItemsDAO(con);
                vm = dao.GetItemVMByItemID("10000000004895");
            }

            Assert.AreEqual(vm.CodBarra, 10000000004895M);
        }
    }
}
