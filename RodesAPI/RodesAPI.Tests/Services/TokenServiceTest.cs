using Microsoft.VisualStudio.TestTools.UnitTesting;
using RodesAPI.Services;

namespace RodesAPI.Tests
{
    [TestClass]
    public class TokenServiceTest
    {
        [TestMethod]
        public void TokenizeTest()
        {
            string token = "";

            token = TokenService.Tokenize("ABCDEFG01012001");

            Assert.AreNotEqual(token, "");
            Assert.AreEqual(TokenService.ValidTokens.Count, 1);
        }
    }
}
