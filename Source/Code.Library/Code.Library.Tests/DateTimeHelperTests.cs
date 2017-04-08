using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace Code.Library.Tests
{
    [TestClass()]
    public class DateTimeHelperTests
    {
        //[Fact]
        public void GetArabianStandardTimeTest()
        {
            // Arrange
            var serverDate = DateTime.Now;
            var uaeTime = serverDate.GetArabianStandardTime();
        }

        [TestMethod()]
        public void GetEachDayTest()
        {
            // Arrange
            var startDate = DateTime.Now;
            var endDate = new DateTime(2017, 05, 11);
            var dates = startDate.GetEachDay(endDate);
            Assert.IsTrue(dates.ToList().Count > 0);
            
        }
    }
}