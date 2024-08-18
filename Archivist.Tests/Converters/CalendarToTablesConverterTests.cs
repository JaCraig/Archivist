using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class CalendarToTablesConverterTests : TestBaseClass<CalendarToTablesConverter>
    {
        public CalendarToTablesConverterTests()
        {
            _TestClass = new CalendarToTablesConverter();
            TestObject = new CalendarToTablesConverter();
        }

        private readonly CalendarToTablesConverter _TestClass;

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
            var File = new Calendar();

            // Act
            Tables? Result = CalendarToTablesConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Tables>(Result);
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
            _ = File.AddEvent("Event", "Event", "Event", DateTime.Now, DateTime.Now);

            // Act
            Tables? Result = CalendarToTablesConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.Single(Result);
            _ = Assert.Single(Result[0]);
            Assert.Equal(File.Events[0].Summary, Result[0][0]["SUMMARY"].Content);
            Assert.Equal(File.Events[0].Description, Result[0][0]["DESCRIPTION"].Content);
            Assert.Equal(File.Events[0].Location, Result[0][0]["LOCATION"].Content);
            Assert.Equal(File.Events[0].StartDateUtc.ToString("yyyyMMddTHHmm"), Result[0][0]["DTSTART"].Content.Left(13));
            Assert.Equal(File.Events[0].EndDateUtc.ToString("yyyyMMddTHHmm"), Result[0][0]["DTEND"].Content.Left(13));
        }
    }
}