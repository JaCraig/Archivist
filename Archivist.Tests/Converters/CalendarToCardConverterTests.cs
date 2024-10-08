using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
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
            Type Source = typeof(Calendar);
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
            var File = new Calendar();

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
            var File = new Calendar();
            _ = File.AddEvent("Summary", "Description", "Location", DateTime.Now, DateTime.Now);

            // Act
            Card? Result = CalendarToCardConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(File.Events[0].Count, Result.Count);
            Assert.Equal(File.Events[0].Summary, Result.Fields[0]?.Value);
            Assert.Equal(File.Events[0].Description, Result.Fields[1]?.Value);
            Assert.Equal(File.Events[0].Location, Result.Fields[2]?.Value);
            Assert.Equal(File.Events[0].StartDateUtc.ToString("yyyyMMddTHHmm"), Result.Fields[3]?.Value.Left(13));
            Assert.Equal(File.Events[0].EndDateUtc.ToString("yyyyMMddTHHmm"), Result.Fields[4]?.Value.Left(13));
        }
    }
}