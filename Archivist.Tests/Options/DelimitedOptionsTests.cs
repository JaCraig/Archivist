using Archivist.Options;
using Xunit;

namespace Archivist.Tests.Options
{
    public class DelimitedOptionsTests
    {
        public DelimitedOptionsTests()
        {
            _TestClass = new DelimitedOptions();
        }

        private readonly DelimitedOptions _TestClass;

        [Fact]
        public void CanGetDefault()
        {
            // Assert
            DelimitedOptions Result = Assert.IsType<DelimitedOptions>(DelimitedOptions.Default);

            Assert.Equal(",", Result.DefaultSeparator);
            Assert.True(Result.FirstRowIsColumnHeaders);
            Assert.Equal("\"", Result.Quote);
        }

        [Fact]
        public void CanSetAndGetDefaultSeparator()
        {
            // Arrange
            const string TestValue = "TestValue562716268";

            // Act
            _TestClass.DefaultSeparator = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.DefaultSeparator);
        }

        [Fact]
        public void CanSetAndGetFirstRowIsColumnHeaders()
        {
            // Arrange
            const bool TestValue = true;

            // Act
            _TestClass.FirstRowIsColumnHeaders = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.FirstRowIsColumnHeaders);
        }

        [Fact]
        public void CanSetAndGetQuote()
        {
            // Arrange
            const string TestValue = "TestValue2142009546";

            // Act
            _TestClass.Quote = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Quote);
        }
    }
}