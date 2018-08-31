using Code.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading;

namespace Code.Library.Tests
{
    using Shouldly;

    using Xunit;

    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

    [TestClass()]
    public class StringHelperTests
    {
        #region Public Methods

        [TestMethod()]
        public void IsNumberOnlyTest()
        {
            Assert.IsTrue("12345".IsNumberOnly(false));
            Assert.IsTrue("   12345".IsNumberOnly(false));
            Assert.IsTrue("12.345".IsNumberOnly(true));
            Assert.IsTrue("   12,345 ".IsNumberOnly(true));
            Assert.IsFalse("12 345".IsNumberOnly(false));
            Assert.IsFalse("tractor".IsNumberOnly(true));
        }

        [TestMethod()]
        public void IsNumberTest()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            Assert.IsTrue("12345".IsNumber(false));
            Assert.IsTrue("   12345".IsNumber(false));
            Assert.IsTrue("12.345".IsNumber(true));
            Assert.IsTrue("   12,345 ".IsNumber(true));
            Assert.IsTrue("12 345".IsNumber(false));
            Assert.IsFalse("tractor".IsNumber(true));
        }

        /// <summary>
        ///A test for IsValidUrl
        ///</summary>
        [TestMethod()]
        public void IsValidUrlTest()
        {
            Assert.IsTrue("http://www.codeproject.com".IsValidUrl());
            Assert.IsTrue("https://www.codeproject.com/#some_anchor".IsValidUrl());
            Assert.IsTrue("https://localhost".IsValidUrl());
            Assert.IsTrue("http://www.abcde.nf.net/signs-banners.jpg".IsValidUrl());
            Assert.IsTrue("http://aa-bbbb.cc.bla.com:80800/test/test/test.aspx?dd=dd&id=dki".IsValidUrl());
            Assert.IsFalse("http:wwwcodeprojectcom".IsValidUrl());
            Assert.IsFalse("http://www.code project.com".IsValidUrl());
        }

        [TestMethod()]
        public void MD5Test()
        {
            string input = "The quick brown fox jumps over the lazy dog";
            string expected = "9e107d9d372bb6826bd81d3542a419d6";
            string actual = input.MD5();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Nl2BrTest()
        {
            string input = "yellow dog" + Environment.NewLine + "black cat";
            string expected = "yellow dog<br />black cat";
            string actual = input.Nl2Br();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RemoveDiacritics
        ///</summary>
        [TestMethod()]
        public void RemoveDiacriticsTest()
        {
            ////contains all czech accents
            // input:  "Příliš žluťoučký kůň úpěl ďábelské ódy."
            //result: "Prilis zlutoucky kun upel dabelske ody."
            //string actual = input.RemoveDiacritics();
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RemoveSpacesTest()
        {
            string input = "yellow dog" + Environment.NewLine + "black cat";
            string expected = "yellowdog" + Environment.NewLine + "blackcat";
            string actual = input.RemoveSpaces();
            Assert.AreEqual(expected, actual);
        }

        public void ReverseTest()
        {
            string input = "yellow dog";
            string expected = "god wolley";
            string actual = input.Reverse();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// The to friendly url test.
        /// </summary>
        [TestMethod]
        public void ToFriendlyUrlTest()
        {
            var input = "New Text Document";
            var expected = "new-text-document";
            Assert.AreEqual(expected, input.ToFriendlyUrl());
        }

        public void UrlAvailableTest()
        {
            Assert.IsTrue("www.codeproject.com".UrlAvailable());
            Assert.IsFalse("www.asjdfalskdfjalskdf.com".UrlAvailable());
        }

        #endregion Public Methods

        /// <summary>
        /// The clean content test.
        /// </summary>
        [Fact]
        public void CleanTest()
        {
            var input = "$7.42 billion";

            var output = input.Clean(true);

            output.ShouldBe(input);

            input = "SELECT accountNumber, balance FROM accounts WHERE account_owner_id = 0 OR 1=1";

            output = input.Clean(true);

            output.ShouldNotBe(input);
        }
    }
}