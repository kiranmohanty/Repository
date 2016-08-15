using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyWeb.Tests
{
    [TestClass]
    public class OOPsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void FullNameTestSuccess()
        {
            BasicUserInfo userInfo = new BasicUserInfo(1, "kiran", "mohanty");
            Assert.AreEqual(userInfo.FullName(), "kiran mohanty");

        }

        [TestMethod]
        public void FullNameTestFailure()
        {
            BasicUserInfo userInfo = new BasicUserInfo(1, "kiran", "mohanty");
            Assert.AreNotEqual(userInfo.FullName(), "kiran mohanty");
        }
    }
}
