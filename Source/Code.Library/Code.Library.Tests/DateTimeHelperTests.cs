using System;

namespace Code.Library.Tests
{
    public class DateTimeHelperTests
    {
        //[Fact]
        public void GetArabianStandardTimeTest()
        {
            // Arrange
            var serverDate = DateTime.Now;
            var uaeTime = serverDate.GetArabianStandardTime();
        }
    }
}