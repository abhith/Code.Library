using Code.Library;
using System;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Code.Library.Tests
{
    [TestClass()]
    public class StringHelperTests
    {
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

        public void UrlAvailableTest()
        {
            Assert.IsTrue("www.codeproject.com".UrlAvailable());
            Assert.IsFalse("www.asjdfalskdfjalskdf.com".UrlAvailable());
        }

        public void ReverseTest()
        {
            string input = "yellow dog";
            string expected = "god wolley";
            string actual = input.Reverse();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReduceTest()
        {
            string input = "The quick brown fox jumps over the lazy dog";
            int count = 10;
            string endings = "...";
            string expected = "The qui...";
            string actual = input.Reduce(count, endings);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RemoveSpacesTest()
        {
            string input = "yellow dog" + Environment.NewLine + "black cat";
            string expected = "yellowdog" + Environment.NewLine + "blackcat";
            string actual = input.RemoveSpaces();
            Assert.AreEqual(expected, actual);
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
        public void Nl2BrTest()
        {
            string input = "yellow dog" + Environment.NewLine + "black cat";
            string expected = "yellow dog<br />black cat";
            string actual = input.Nl2Br();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MD5Test()
        {
            string input = "The quick brown fox jumps over the lazy dog";
            string expected = "9e107d9d372bb6826bd81d3542a419d6";
            string actual = input.MD5();
            Assert.AreEqual(expected, actual);
        }
    }
}