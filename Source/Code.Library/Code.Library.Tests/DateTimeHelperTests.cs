using Code.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Library.Tests
{
    [TestClass()]
    public class DateTimeHelperTests
    {
        [TestMethod()]
        public void GetArabianStandardTimeTest()
        {
            // Arrange
            var serverDate = DateTime.Now;
            var uaeTime = serverDate.GetArabianStandardTime();
            Assert.AreNotEqual(serverDate, uaeTime);
        }
    }
}