using FluentAssertions;
using Xunit;

namespace Code.Library.Tests.Extensions
{
    public class NumberExtensionsTests
    {
        /// <summary>
        /// The get first n digits tests.
        /// </summary>
        [Fact]
        public void GetFirstNDigitsTests()
        {
            int number = 7654321;

            var firstDigit = number.GetFirstNDigits(1);
            var firstTwoDigits = number.GetFirstNDigits(2);
            var firstFourDigits = number.GetFirstNDigits(4);

            firstDigit.Should().Be(7);
            firstTwoDigits.Should().Be(76);
            firstFourDigits.Should().Be(7654);
        }

        /// <summary>
        /// The get nth digit tests.
        /// </summary>
        [Fact]
        public void GetNthDigitTests()
        {
            int number = 7654321;

            var firstDigit = number.GetNthDigit(1);
            var thirdDigit = number.GetNthDigit(3);
            var sixthDigit = number.GetNthDigit(6);
            var seventhDigit = number.GetNthDigit(7);

            firstDigit.Should().Be(1);
            thirdDigit.Should().Be(3);
            sixthDigit.Should().Be(6);
            seventhDigit.Should().Be(7);

            number = 10;

            firstDigit = number.GetNthDigit(1);
            var secondDigit = number.GetNthDigit(2);

            firstDigit.Should().Be(0);
            secondDigit.Should().Be(1);
        }
    }
}