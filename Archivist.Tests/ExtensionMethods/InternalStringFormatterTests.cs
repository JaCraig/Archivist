using Archivist.ExtensionMethods;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.ExtensionMethods
{
    public class InternalStringFormatterTests : TestBaseClass
    {
        protected override Type? ObjectType { get; } = typeof(InternalStringFormatter);

        [Fact]
        public void CanCallFormat()
        {
            // Arrange
            const string Input = "TestValue1493286982";
            const string FormatPattern = "@@@####";

            // Act
            var Result = InternalStringFormatter.Format(Input, FormatPattern);

            // Assert
            Assert.Equal("Tes1493", Result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanCallFormatWithInvalidInput(string value)
        {
            // Act
            var Result = InternalStringFormatter.Format(value, "####");

            // Assert
            Assert.Equal("", Result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallFormatWithInvalidFormatPattern(string? value) =>
            // Act
            Assert.Throws<ArgumentException>(() => InternalStringFormatter.Format("TestValue247102728", value));
    }
}