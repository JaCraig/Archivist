using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System.Linq;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class FeedToCardConverterTests : TestBaseClass<FeedToCardConverter>
    {
        public FeedToCardConverterTests()
        {
            _Converter = new FeedToCardConverter();
            TestObject = new FeedToCardConverter();
        }

        private readonly FeedToCardConverter _Converter;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            System.Type Source = typeof(Feed);
            System.Type Destination = typeof(Card);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallCanConvert_WithInvalidDestination()
        {
            // Arrange
            System.Type Source = typeof(Feed);
            System.Type Destination = typeof(string);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallCanConvert_WithInvalidSource()
        {
            // Arrange
            System.Type Source = typeof(string);
            System.Type Destination = typeof(Card);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Convert_ReturnsCardWithMetadataAndTitle()
        {
            // Arrange
            var File = new Feed
            {
                Title = "Sample Title"
            };
            File.Metadata.Add("Key1", "Value1");
            File.Metadata.Add("Key2", "Value2");
            File.Channels.Add(new Archivist.DataTypes.Feeds.Channel());
            File.Channels[0].Add(new Archivist.DataTypes.Feeds.FeedItem() { Title = "Test1", Description = "Description1" });
            File.Channels[0].Add(new Archivist.DataTypes.Feeds.FeedItem() { Title = "Test2", Description = "Description2" });

            // Act
            Card? Result = FeedToCardConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(2, Result.Metadata.Count);
            Assert.Equal("Value1", Result.Metadata["Key1"]);
            Assert.Equal("Value2", Result.Metadata["Key2"]);
            Assert.Equal(File.Title, Result.Title);
            Assert.Equal(8, Result.Fields.Count);
            Assert.Equal("Test1", Result["Title"].FirstOrDefault()?.Value);
            Assert.Equal("Test2", Result["Title"].Skip(1).FirstOrDefault()?.Value);
            Assert.Equal("Description1", Result["Description"].FirstOrDefault()?.Value);
            Assert.Equal("Description2", Result["Description"].Skip(1).FirstOrDefault()?.Value);
        }

        [Fact]
        public void Convert_ReturnsEmptyValue_WhenFileIsEmpty()
        {
            // Arrange
            var File = new Feed();

            // Act
            Card? Result = FeedToCardConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Fields);
            Assert.Empty(Result.Metadata);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenFileIsNull()
        {
            // Arrange
            Feed? File = null;

            // Act
            Card? Result = FeedToCardConverter.Convert(File);

            // Assert
            Assert.Null(Result);
        }
    }
}