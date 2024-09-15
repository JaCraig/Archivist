using Archivist.DataTypes;
using Archivist.DataTypes.Feeds;
using Archivist.Formats.RSS;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.RSS
{
    public class RSSWriterTests : TestBaseClass<RSSWriter>
    {
        public RSSWriterTests()
        {
            _TestClass = new RSSWriter();
            TestObject = new RSSWriter();
        }

        private readonly RSSWriter _TestClass;

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            IGenericFile File = new Feed();
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.True(Result);
            Assert.True(Stream.Length > 0);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithNullFileAsync()
        {
            // Arrange
            IGenericFile? File = null;
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithNullStreamAsync()
        {
            // Arrange
            IGenericFile File = new Feed();
            Stream? Stream = null;

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithValuesAndMultipleItemsAsync()
        {
            // Arrange
            IGenericFile File = new Feed()
            {
                new Channel()
            };
            var Stream = new MemoryStream();
            var Feed = File as Feed;
            Assert.NotNull(Feed);
            Feed.Title = "Test Title";
            Feed.Channels[0].Description = "Test Description";
            Feed.Channels[0].Link = "http://www.example.com";
            Feed.Channels[0].Items.Add(new FeedItem { Title = "Item Title", Description = "Item Description", Link = "http://www.example.com/item" });
            Feed.Channels[0].Items.Add(new FeedItem { Title = "Item Title 2", Description = "Item Description 2", Link = "http://www.example.com/item2" });

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.True(Result);
            Assert.True(Stream.Length > 0);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithValuesAsync()
        {
            // Arrange
            IGenericFile File = new Feed
            {
                new Channel()
            };
            var Stream = new MemoryStream();
            var Feed = File as Feed;
            Assert.NotNull(Feed);
            Feed.Title = "Test Title";
            Feed.Channels[0].Description = "Test Description";
            Feed.Channels[0].Link = "http://www.example.com";
            Feed.Channels[0].Items.Add(new FeedItem { Title = "Item Title", Description = "Item Description", Link = "http://www.example.com/item" });

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.True(Result);
            Assert.True(Stream.Length > 0);
        }
    }
}