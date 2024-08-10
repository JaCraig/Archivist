namespace Archivist.Tests.ExtensionMethods
{
    using Archivist.ExtensionMethods;
    using System;
    using Xunit;

    public static class InternalStringFormatterTests
    {
        [Fact]
        public static void CanCallFormat()
        {
            // Arrange
            var input = "TestValue1493286982";
            var formatPattern = "TestValue1887807456";

            // Act
            var result = InternalStringFormatter.Format(input, formatPattern);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public static void CannotCallFormatWithInvalidInput(string value)
        {
            Assert.Throws<ArgumentNullException>(() => InternalStringFormatter.Format(value, "TestValue860197991"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public static void CannotCallFormatWithInvalidFormatPattern(string? value)
        {
            Assert.Throws<ArgumentNullException>(() => InternalStringFormatter.Format("TestValue247102728", value));
        }
    }
}