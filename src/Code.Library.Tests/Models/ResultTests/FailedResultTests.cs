// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FailedResultTests.cs" company="*">
//   *
// </copyright>
// <summary>
//   Defines the FailedResultTests type.
//  Ref : https://github.com/vkhorikov/CSharpFunctionalExtensions
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FluentAssertions;

namespace Code.Library.Tests.Models
{
    using Xunit;

    /// <summary>
    /// The failed result tests.
    /// </summary>
    public class FailedResultTests
    {
        [Fact]
        public void Can_create_a_generic_version()
        {
            Result<MyClass> result = Result.Fail<MyClass>("Error message");

            result.Error.Should().Be("Error message");
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Can_create_a_non_generic_version()
        {
            Result result = Result.Fail("Error message");

            result.Error.Should().Be("Error message");
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Cannot_create_without_error_message()
        {
            Action action1 = () => { Result.Fail(null); };
            Action action2 = () => { Result.Fail(string.Empty); };
            Action action3 = () => { Result.Fail<MyClass>(null); };
            Action action4 = () => { Result.Fail<MyClass>(string.Empty); };

            action1.Should().Throw<ArgumentNullException>();
            action2.Should().Throw<ArgumentNullException>();
            action3.Should().Throw<ArgumentNullException>();
            action4.Should().Throw<ArgumentNullException>();
        }

        #region Private Classes

        private class MyClass
        {
        }

        #endregion Private Classes
    }
}