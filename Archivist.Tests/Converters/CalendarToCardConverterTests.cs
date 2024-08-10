namespace Archivist.Tests.Converters
{
    using Archivist.Converters;
    using Archivist.DataTypes;
    using System;
    using Xunit;

    public class CalendarToCardConverterTests
    {
        private CalendarToCardConverter _testClass;

        public CalendarToCardConverterTests()
        {
            _testClass = new CalendarToCardConverter();
        }

        [Fact]
        public void CanCallConvertWithFile()
        {
            // Arrange
            var @file = new Calendar();

            // Act
            var result = CalendarToCardConverter.Convert(file);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void ConvertWithFilePerformsMapping()
        {
            // Arrange
            var @file = new Calendar();

            // Act
            var result = CalendarToCardConverter.Convert(file);

            // Assert
            Assert.Equal(file.Count, result.Count);
            Assert.Same(file.Fields, result.Fields);
            Assert.Same(file.TimeZones, result.TimeZones);
        }

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            var source = typeof(string);
            var destination = typeof(string);

            // Act
            var result = _testClass.CanConvert(source, destination);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallConvertWithObjectAndType()
        {
            // Arrange
            var source = new object();
            var destination = typeof(string);

            // Act
            var result = _testClass.Convert(source, destination);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }
    }
}