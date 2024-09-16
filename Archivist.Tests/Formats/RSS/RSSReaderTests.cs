using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Formats.RSS;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System;
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
            Assert.False(Result);
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
            Archivist.DataTypes.Feeds.Channel Channel = Assert.Single(Feed.Channels);
            Assert.Equal(50, Feed.Channels[0].Count);
            Assert.Equal("Virginia Resources Page Feed", Channel.Title);
            Assert.Equal("Content for display on Virginia Practice Area Page.", Channel.Description);
            Assert.Equal("http://app.vable.com/rss/feed/5e86172399324124f0668641", Channel.Link);
            Assert.Equal("", Channel.Language);
            Assert.Equal(DateTime.UtcNow.Date.ToString(), Channel.PubDateUtc.Date.ToString());
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
            Archivist.DataTypes.Feeds.Channel Channel = Assert.Single(Feed.Channels);
            Assert.Equal(10, Channel.Count);
            Assert.Equal(DateTime.UtcNow.Date.ToString(), Channel.PubDateUtc.Date.ToString());
            Assert.Equal("The Underfold", Channel.Title);
            Assert.Equal("http://theunderfold.com", Channel.Link);
            Assert.Equal("A comic by Brian Russell where Brian and his bag-faced friend, JB embark on misadventures!", Channel.Description);
            Assert.Equal("en-US", Channel.Language);
        }
    }
}