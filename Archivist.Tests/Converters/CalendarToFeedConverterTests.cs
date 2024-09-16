using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class CalendarToFeedConverterTests : TestBaseClass<CalendarToFeedConverter>
    {
        public CalendarToFeedConverterTests()
        {
            _TestClass = new CalendarToFeedConverter();
            TestObject = new CalendarToFeedConverter();
        }

        private readonly CalendarToFeedConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Calendar);
            Type Destination = typeof(Feed);

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
            Feed? Result = CalendarToFeedConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
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
    }
}