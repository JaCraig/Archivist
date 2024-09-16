using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class FeedToTablesConverterTests : TestBaseClass<FeedToTablesConverter>
    {
        public FeedToTablesConverterTests()
        {
            _Converter = new FeedToTablesConverter();
            TestObject = new FeedToTablesConverter();
        }

        private readonly FeedToTablesConverter _Converter;

        [Fact]
        public void CanConvert_DestinationTypeNotTables_ReturnsFalse()
        {
            // Arrange
            System.Type SourceType = typeof(Feed);
            System.Type DestinationType = typeof(string);

            // Act
            var Result = _Converter.CanConvert(SourceType, DestinationType);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConvert_SourceTypeFeedAndDestinationTypeTables_ReturnsTrue()
        {
            // Arrange
            System.Type SourceType = typeof(Feed);
            System.Type DestinationType = typeof(Tables);

            // Act
            var Result = _Converter.CanConvert(SourceType, DestinationType);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanConvert_SourceTypeNotFeed_ReturnsFalse()
        {
            // Arrange
            System.Type SourceType = typeof(string);
            System.Type DestinationType = typeof(Tables);

            // Act
            var Result = _Converter.CanConvert(SourceType, DestinationType);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Convert_NullFeed_ReturnsNull()
        {
            // Arrange
            Feed? Feed = null;

            // Act
            Tables? Result = FeedToTablesConverter.Convert(Feed);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ObjectAndDestinationTypeFeed_ReturnsNull()
        {
            // Arrange
            var Source = new object();
            System.Type DestinationType = typeof(Feed);

            // Act
            var Result = _Converter.Convert(Source, DestinationType);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ObjectAndNullDestinationType_ReturnsNull()
        {
            // Arrange
            var Source = new object();
            var DestinationType = default(System.Type);

            // Act
            var Result = _Converter.Convert(Source, DestinationType);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ValidFeed_ReturnsTablesObject()
        {
            // Arrange
            var Feed = new Feed
            {
                Title = "Test Feed"
            };
            Feed.Metadata.Add("Key1", "Value1");
            Feed.Metadata.Add("Key2", "Value2");

            // Act
            Tables? Result = FeedToTablesConverter.Convert(Feed);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(Feed.Title, Result.Title);
            Assert.Equal(Feed.Metadata, Result.Metadata);
        }
    }
}