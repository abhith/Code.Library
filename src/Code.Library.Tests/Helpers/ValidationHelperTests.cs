using Code.Library.Extensions;
using FluentAssertions;
using Xunit;

namespace Code.Library.Tests.Helpers
{
    public class ValidationHelperTests
    {
        [Fact]
        public void IsValidEmailAddressTest()
        {
            "yellowdog@someemail.uk".IsValidEmailAddress().Should().BeTrue();
            "yellow.444@email4u.co.uk".IsValidEmailAddress().Should().BeTrue();
            "adfasdf".IsValidEmailAddress().Should().BeFalse();
            "asd@asdf".IsValidEmailAddress().Should().BeFalse();
        }
    }
}