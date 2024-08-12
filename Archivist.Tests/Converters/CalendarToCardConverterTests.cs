using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class CalendarToCardConverterTests : TestBaseClass<CalendarToCardConverter>
    {
        public CalendarToCardConverterTests()
        {
            _TestClass = new CalendarToCardConverter();
            TestObject = new CalendarToCardConverter();
        }

        private readonly CalendarToCardConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(CalendarComponent);
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
            var File = new CalendarComponent();

            // Act
            Card? Result = CalendarToCardConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Card>(Result);
        }

        [Fact]
        public void CanCallConvertWithObjectAndType()
        {
            // Arrange
            var Source = new object();
            Type Destination = typeof(Card);

            // Act
            var Result = _TestClass.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void ConvertWithFilePerformsMapping()
        {
            // Arrange
            var File = new CalendarComponent();

            // Act
            Card? Result = CalendarToCardConverter.Convert(File);

            // Assert
            Assert.Equal(File.Count, Result.Count);
            Assert.Same(File.Fields, Result.Fields);
        }
    }
}