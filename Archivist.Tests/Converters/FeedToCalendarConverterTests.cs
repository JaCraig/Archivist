using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using System.Linq;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class FeedToCalendarConverterTests : TestBaseClass<FeedToCalendarConverter>
    {
        public FeedToCalendarConverterTests()
        {
            _TestClass = new FeedToCalendarConverter();
            TestObject = new FeedToCalendarConverter();
        }

        private readonly FeedToCalendarConverter _TestClass;

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
            var File = new Feed()
            {
                new Archivist.DataTypes.Feeds.Channel()
            };

            // Act
            Calendar? Result = FeedToCalendarConverter.Convert(File);

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

        [Fact]
        public void ConvertWithFilePerformsMapping()
        {
            // Arrange
            var File = new Feed()
            {
                new Archivist.DataTypes.Feeds.Channel()
            };
            File[0].Items.Add(new Archivist.DataTypes.Feeds.FeedItem() { Title = "Test" });

            // Act
            Calendar? Result = FeedToCalendarConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Test", Result.Events.FirstOrDefault()?["Title"].FirstOrDefault()?.Value);
        }
    }
}