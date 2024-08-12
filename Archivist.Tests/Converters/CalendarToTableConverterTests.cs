using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class CalendarToTableConverterTests : TestBaseClass<CalendarToTableConverter>
    {
        public CalendarToTableConverterTests()
        {
            _TestClass = new CalendarToTableConverter();
            TestObject = new CalendarToTableConverter();
        }

        private readonly CalendarToTableConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Calendar);
            Type Destination = typeof(Table);

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
            Table? Result = CalendarToTableConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Table>(Result);
            Assert.Equal(File.Count, Result.Count);
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
            var File = new Calendar();

            // Act
            Table? Result = CalendarToTableConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(File.Count, Result.Count);
        }
    }
}