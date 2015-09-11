using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Code.Library.Tests
{
    [TestClass()]
    public class ValidationHelperTests
    {
        [TestMethod()]
        public void IsValidEmailTest()
        {
            Assert.IsTrue("test@test.com".IsValidEmailAddressV1());
            Assert.IsFalse("test".IsValidEmailAddressV1());
            Assert.IsTrue("test@test.com.in".IsValidEmailAddressV1());
        }

        [TestMethod()]
        public void IsValidEmailAddressTest()
        {
            Assert.IsTrue("yellowdog@someemail.uk".IsValidEmailAddressV2());
            Assert.IsTrue("yellow.444@email4u.co.uk".IsValidEmailAddressV2());
            Assert.IsFalse("adfasdf".IsValidEmailAddressV2());
            Assert.IsFalse("asd@asdf".IsValidEmailAddressV2());
        }
    }
}