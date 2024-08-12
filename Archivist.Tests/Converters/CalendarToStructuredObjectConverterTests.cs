using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class CalendarToStructuredObjectConverterTests : TestBaseClass<CalendarToStructuredObjectConverter>
    {
        public CalendarToStructuredObjectConverterTests()
        {
            _TestClass = new CalendarToStructuredObjectConverter();
            TestObject = new CalendarToStructuredObjectConverter();
        }

        private readonly CalendarToStructuredObjectConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Calendar);
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallConvertWithFile()
        {
            // Arrange
            var File = new Calendar();

            // Act
            StructuredObject? Result = CalendarToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<StructuredObject>(Result);
            Assert.Equal(File.Count, Result.Count);
        }

        [Fact]
        public void CanCallConvertWithObjectAndType()
        {
            // Arrange
            var Source = new object();
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _TestClass.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void ConvertWithFilePerformsMapping()
        {
            // Arrange
            var File = new Calendar();

            // Act
            StructuredObject? Result = CalendarToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(File.Count, Result.Count);
        }
    }
}