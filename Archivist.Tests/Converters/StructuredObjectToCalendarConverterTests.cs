using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class StructuredObjectToCalendarConverterTests : TestBaseClass<StructuredObjectToCalendarConverter>
    {
        public StructuredObjectToCalendarConverterTests()
        {
            _TestClass = new StructuredObjectToCalendarConverter();
            TestObject = new StructuredObjectToCalendarConverter();
        }

        private readonly StructuredObjectToCalendarConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(string);
            Type Destination = typeof(string);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallConvertWithFile()
        {
            // Arrange
            var File = new StructuredObject();

            // Act
            Calendar? Result = StructuredObjectToCalendarConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Calendar>(Result);
        }

        [Fact]
        public void CanCallConvertWithObjectAndType()
        {
            // Arrange
            var Source = new object();
            Type Destination = typeof(string);

            // Act
            var Result = _TestClass.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void ConvertWithFilePerformsMapping()
        {
            // Arrange
            var File = new StructuredObject();

            // Act
            Calendar? Result = StructuredObjectToCalendarConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(File.Count, Result.Count);
        }
    }
}