using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
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
            _ = File.AddEvent("Event", "Event", "Event", DateTime.Now, DateTime.Now);

            // Act
            Table? Result = CalendarToTableConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Table>(Result);
            _ = Assert.Single(Result);
            Assert.Equal(File.Events[0].Summary, Result[0]["SUMMARY"].Content);
            Assert.Equal(File.Events[0].Description, Result[0]["DESCRIPTION"].Content);
            Assert.Equal(File.Events[0].Location, Result[0]["LOCATION"].Content);
            Assert.Equal(File.Events[0].StartDateUtc.ToString("yyyyMMddTHHmm"), Result[0]["DTSTART"].Content.Left(13));
            Assert.Equal(File.Events[0].EndDateUtc.ToString("yyyyMMddTHHmm"), Result[0]["DTEND"].Content.Left(13));
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
            Table? Result = CalendarToTableConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.Single(Result);
            Assert.Equal(File.Events[0].Summary, Result[0]["SUMMARY"].Content);
            Assert.Equal(File.Events[0].Description, Result[0]["DESCRIPTION"].Content);
            Assert.Equal(File.Events[0].Location, Result[0]["LOCATION"].Content);
            Assert.Equal(File.Events[0].StartDateUtc.ToString("yyyyMMddTHHmm"), Result[0]["DTSTART"].Content.Left(13));
            Assert.Equal(File.Events[0].EndDateUtc.ToString("yyyyMMddTHHmm"), Result[0]["DTEND"].Content.Left(13));
        }
    }
}