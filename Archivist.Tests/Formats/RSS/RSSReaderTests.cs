using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Formats.RSS;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.RSS
{
    public class RSSReaderTests : TestBaseClass<RSSReader>
    {
        public RSSReaderTests()
        {
            _Converter = new Convertinator(new[] { Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>() });
            _TestClass = new RSSReader(_Converter);
            TestObject = new RSSReader(_Converter);
        }

        private static readonly byte[] _HeaderInfo = new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x20 };
        private readonly Convertinator _Converter;
        private readonly RSSReader _TestClass;

        [Fact]
        public void CanCallInternalCanRead()
        {
            // Arrange
            var Stream = new MemoryStream();

            // Act
            var Result = _TestClass.InternalCanRead(Stream);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public async Task CanCallReadAsync()
        {
            // Arrange
            var Stream = new MemoryStream();

            // Act
            IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new RSSReader(_Converter);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            var Result = Assert.IsType<byte[]>(_TestClass.HeaderInfo);
            Assert.NotEmpty(Result);
            Assert.Equal(_HeaderInfo, Result);
        }

        [Fact]
        public async Task Read_From_File2Async()
        {
            // Arrange
            var Stream = new FileStream("./TestData/TestRSS2.rss", FileMode.Open);

            // Act
            IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Feed Feed = Assert.IsType<Feed>(Result);
            Assert.NotNull(Feed);
            _ = Assert.Single(Feed.Channels);
            Assert.Equal(10, Feed.Channels[0].Count);
            Assert.Equal("Test RSS Feed", Feed.Title);
            Assert.Equal("This is a test RSS feed", Feed.Channels[0].Description);
            Assert.Equal("https://example.com/rss", Feed.Channels[0].Link);
            Assert.Equal("en-US", Feed.Channels[0].Language);
            Assert.Equal("2021-01-01T00:00:00Z", Feed.Channels[0].PubDate.ToUniversalTime().ToString());
        }

        [Fact]
        public async Task Read_From_FileStreamAsync()
        {
            // Arrange
            var Stream = new FileStream("./TestData/TestRSS.rss", FileMode.Open);

            // Act
            IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Feed Feed = Assert.IsType<Feed>(Result);
            Assert.NotNull(Feed);
            _ = Assert.Single(Feed.Channels);
            Assert.Equal(10, Feed.Channels[0].Count);
            Assert.Equal("Test RSS Feed", Feed.Title);
            Assert.Equal("This is a test RSS feed", Feed.Channels[0].Description);
            Assert.Equal("https://example.com/rss", Feed.Channels[0].Link);
            Assert.Equal("en-US", Feed.Channels[0].Language);
            Assert.Equal("2021-01-01T00:00:00Z", Feed.Channels[0].PubDate.ToUniversalTime().ToString());
        }
    }
}