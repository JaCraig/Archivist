using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class FeedToTableConverterTests : TestBaseClass<FeedToTableConverter>
    {
        public FeedToTableConverterTests()
        {
            _Converter = new FeedToTableConverter();
            TestObject = new FeedToTableConverter();
        }

        private static readonly string[] _Columns = new string[] { "Title", "Description", "Author", "Categories", "Description", "Enclosure", "GUID", "Link", "PubDate", "Thumbnail" };
        private readonly FeedToTableConverter _Converter;

        [Fact]
        public void CanConvert_WithSourceFeedAndDestinationNotTable_ReturnsFalse()
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
        public void CanConvert_WithSourceFeedAndDestinationTable_ReturnsTrue()
        {
            // Arrange
            System.Type Source = typeof(Feed);
            System.Type Destination = typeof(Table);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanConvert_WithSourceNotFeedAndDestinationNotTable_ReturnsFalse()
        {
            // Arrange
            System.Type Source = typeof(string);
            System.Type Destination = typeof(int);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConvert_WithSourceNotFeedAndDestinationTable_ReturnsFalse()
        {
            // Arrange
            System.Type Source = typeof(string);
            System.Type Destination = typeof(Table);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Convert_WithEmptyFeed_ReturnsEmptyObject()
        {
            // Arrange
            var Feed = new Feed();

            // Act
            Table? Result = FeedToTableConverter.Convert(Feed);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(_Columns, Result.Columns);
            Assert.Empty(Result);
        }

        [Fact]
        public void Convert_WithFeed_CopiesMetadataToResultTable()
        {
            // Arrange
            var Feed = new Feed
            {
                new Archivist.DataTypes.Feeds.Channel()
            };
            Feed.Metadata.Add("Key1", "Value1");
            Feed.Metadata.Add("Key2", "Value2");

            // Act
            Table? Result = FeedToTableConverter.Convert(Feed);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(3, Result.Metadata.Count);
            Assert.Equal(Feed.Metadata["Key1"], Result.Metadata["Key1"]);
            Assert.Equal(Feed.Metadata["Key2"], Result.Metadata["Key2"]);
            Assert.Equal(",", Result.Metadata["Delimiter"]);
        }

        [Fact]
        public void Convert_WithFeed_ReturnsTable()
        {
            // Arrange
            var Feed = new Feed
            {
                new Archivist.DataTypes.Feeds.Channel()
            };
            Feed[0].Add(new Archivist.DataTypes.Feeds.FeedItem() { Title = "Test" });
            Feed[0].Add(new Archivist.DataTypes.Feeds.FeedItem() { Title = "Test2" });

            // Act
            Table? Result = FeedToTableConverter.Convert(Feed);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(Feed[0].Count, Result.Count);
            Assert.Contains("Title", Result.Columns);
            Assert.Equal(Feed[0][0].Title, Result[0]["Title"].Content);
            Assert.Equal(Feed[0][1].Title, Result[1]["Title"].Content);
        }

        [Fact]
        public void Convert_WithFeed_SetsTitleOfResultTable()
        {
            // Arrange
            var Feed = new Feed
            {
                new Archivist.DataTypes.Feeds.Channel()
            };
            Feed.Title = "Test Title";

            // Act
            Table? Result = FeedToTableConverter.Convert(Feed);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(Feed.Title, Result.Title);
        }

        [Fact]
        public void Convert_WithNullFeed_ReturnsNull()
        {
            // Arrange
            Feed? Feed = null;

            // Act
            Table? Result = FeedToTableConverter.Convert(Feed);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_WithNullSource_ReturnsNull()
        {
            // Arrange
            object? Source = null;
            System.Type Destination = typeof(Table);

            // Act
            var Result = _Converter.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_WithSourceNotFeed_ReturnsNull()
        {
            // Arrange
            object Source = "test";
            System.Type Destination = typeof(Table);

            // Act
            var Result = _Converter.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }
    }
}