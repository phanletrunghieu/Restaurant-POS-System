using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;

namespace UnitTest
{
    [TestClass]
    public class UnitTestArea
    {
        [TestMethod]
        public void TestAddArea()
        {
            string name = "BanVIP";
            AreaBLL areaBLL = new AreaBLL();
            Area area = areaBLL.CreateArea(name);
            Assert.AreEqual(name, area.Name);
        }
    }
}
