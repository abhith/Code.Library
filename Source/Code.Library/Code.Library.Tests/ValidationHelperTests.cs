using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Code.Library.Tests
{
    [TestClass()]
    public class ValidationHelperTests
    {
        //[TestMethod()]
        //public void IsValidEmailTest()
        //{
        //    Assert.IsTrue("test@test.com".IsValidEmailAddress());
        //    Assert.IsFalse("test".IsValidEmailAddress());
        //    Assert.IsTrue("test@test.com.in".IsValidEmailAddress());
        //}

        [TestMethod()]
        public void IsValidEmailAddressTest()
        {
            Assert.IsTrue("yellowdog@someemail.uk".IsValidEmailAddress());
            Assert.IsTrue("yellow.444@email4u.co.uk".IsValidEmailAddress());
            Assert.IsFalse("adfasdf".IsValidEmailAddress());
            Assert.IsFalse("asd@asdf".IsValidEmailAddress());
        }
    }
}