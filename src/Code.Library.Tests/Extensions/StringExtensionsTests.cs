using Code.Library.Extensions;
using FluentAssertions;
using System;
using System.Globalization;
using System.Threading;
using Xunit;

namespace Code.Library.Tests.Extensions
{
    public class StringExtensionsTests
    {
        /// <summary>
        /// The clean content test.
        /// </summary>
        [Fact]
        public void CleanTest()
        {
            var input = "$7.42 billion";
            var output = input.Clean(true);
            output.Should().Be(input);

            input = "<IMG SRC=&#14; javascript:alert(XSS);>";
            output = input.Clean();
            output.Should().BeEmpty();

            input = "</style><script>a=eval;b=alert;a(b(/XSS/ .source));</script>";
            output = input.Clean();
            output.Should().Be("a=eval;b=alert;ab/XSS/ .source;");

            input = string.Empty;
            output = input.Clean();
            output.Should().Be(input);
        }

        [Fact]
        public void IsNumberOnlyTest()
        {
            "12345".IsNumberOnly(false).Should().BeTrue();
            "   12345".IsNumberOnly(false).Should().BeTrue();
            "12.345".IsNumberOnly(true).Should().BeTrue();
            "   12,345 ".IsNumberOnly(true).Should().BeTrue();
            "12 345".IsNumberOnly(false).Should().BeFalse();
            "tractor".IsNumberOnly(true).Should().BeFalse();
        }

        [Fact]
        public void IsNumberTest()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            "12345".IsNumber(false).Should().BeTrue();
            "   12345".IsNumber(false).Should().BeTrue();
            "12.345".IsNumber(true).Should().BeTrue();
            "   12,345 ".IsNumber(true).Should().BeTrue();
            "12 345".IsNumber(false).Should().BeTrue();
            "tractor".IsNumber(true).Should().BeFalse();
        }

        /// <summary>
        ///A test for IsValidUrl
        ///</summary>
        [Fact]
        public void IsValidUrlTest()
        {
            "http://www.codeproject.com".IsValidUrl().Should().BeTrue();
            "https://www.codeproject.com/#some_anchor".IsValidUrl().Should().BeTrue();
            "https://localhost".IsValidUrl().Should().BeTrue();
            "http://www.abcde.nf.net/signs-banners.jpg".IsValidUrl().Should().BeTrue();
            "http://aa-bbbb.cc.bla.com:80800/test/test/test.aspx?dd=dd&id=dki".IsValidUrl().Should().BeTrue();
            "http:wwwcodeprojectcom".IsValidUrl().Should().BeFalse();
            "http://www.code project.com".IsValidUrl().Should().BeFalse();
        }

        [Fact]
        public void MaskTests()
        {
            "123456789".Mask(3, "****").Should().Be("123****89");
            "123456789".Mask(3, "****---").Should().Be("123****---");
        }

        [Fact]
        public void MD5Test()
        {
            string input = "The quick brown fox jumps over the lazy dog";
            string expected = "9e107d9d372bb6826bd81d3542a419d6";
            string actual = input.MD5();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Nl2BrTest()
        {
            string input = "yellow dog" + Environment.NewLine + "black cat";
            string expected = "yellow dog<br />black cat";
            string actual = input.Nl2Br();
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        ///A test for RemoveDiacritics
        ///</summary>
        [Fact]
        public void RemoveDiacriticsTest()
        {
            //contains all czech accents
            var input = "Příliš žluťoučký kůň úpěl ďábelské ódy.";
            var expected = "Prilis zlutoucky kun upel dabelske ody.";
            string actual = input.RemoveDiacritics();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void RemoveSpacesTest()
        {
            string input = "yellow dog" + Environment.NewLine + "black cat";
            string expected = "yellowdog" + Environment.NewLine + "blackcat";
            string actual = input.RemoveSpaces();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ReverseTest()
        {
            string input = "yellow dog";
            string expected = "god wolley";
            string actual = input.Reverse();
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// The to friendly url test.
        /// </summary>
        [Fact]
        public void ToFriendlyUrlTest()
        {
            var input = "New Text Document";
            var expected = "new-text-document";
            input.ToFriendlyUrl().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TruncateAtWordTests()
        {
            "This is a long sentence".TruncateAtWord(6).Should().Be("This");
            "This is a long sentence".TruncateAtWord(7).Should().Be("This is");
        }

        [Fact]
        public void UrlAvailableTest()
        {
            "www.abhith.net".UrlAvailable().Should().BeTrue();
            "www.asjdfalskdfjalskdf.com".UrlAvailable().Should().BeFalse();
        }
    }
}