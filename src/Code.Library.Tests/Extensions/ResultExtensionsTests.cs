// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtensionTests.cs" company="*">
//  *
// </copyright>
// <summary>
//   Defines the ExtensionTests type.
//  Ref : https://github.com/vkhorikov/CSharpFunctionalExtensions
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Code.Library.Extensions;
using FluentAssertions;

namespace Code.Library.Tests.Extensions
{
    using Xunit;

    /// <summary>
    /// The extension tests.
    /// </summary>
    public class ExtensionTests
    {
        /// <summary>
        /// The _error message.
        /// </summary>
        private const string ErrorMessage = "this failed";

        /// <summary>
        /// The should_execute_action_on_failure.
        /// </summary>
        [Fact]
        public void Should_execute_action_on_failure()
        {
            bool myBool = false;

            Result myResult = Result.Fail(ErrorMessage);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        /// <summary>
        /// The should_execute_action_on_generic_failure.
        /// </summary>
        [Fact]
        public void Should_execute_action_on_generic_failure()
        {
            bool myBool = false;

            Result<MyClass> myResult = Result.Fail<MyClass>(ErrorMessage);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        /// <summary>
        /// The should_exexcute_action_with_result_on_generic_failure.
        /// </summary>
        [Fact]
        public void Should_execute_action_with_result_on_generic_failure()
        {
            string myError = string.Empty;

            Result<MyClass> myResult = Result.Fail<MyClass>(ErrorMessage);
            myResult.OnFailure(error => myError = error);

            myError.Should().Be(ErrorMessage);
        }

        /// <summary>
        /// The my class.
        /// </summary>
        private class MyClass
        {
            /// <summary>
            /// Gets or sets the property.
            /// </summary>
            public string Property { get; set; }
        }
    }
}