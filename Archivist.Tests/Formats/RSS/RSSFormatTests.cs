using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Formats.RSS;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using Mecha.xUnit;
using NSubstitute;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.RSS
{
    public class RSSFormatTests : TestBaseClass<RSSFormat>
    {
        public RSSFormatTests()
        {
            _Converter = new Convertinator(new[] { Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>() });
            _TestClass = new RSSFormat(_Converter);
            TestObject = new RSSFormat(_Converter);
        }

        private static readonly string[] _ExpectedExtensions = new string[] { "RSS" };
        private static readonly string[] _ExpectedMimeTypes = new string[] { "APPLICATION/RSS+XML" };
        private readonly Convertinator _Converter;
        private readonly RSSFormat _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new RSSFormat(_Converter);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.Extensions);
            Assert.NotEmpty(Result);
            Assert.Equal(_ExpectedExtensions, Result);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.MimeTypes);
            Assert.NotEmpty(Result);
            Assert.Equal(_ExpectedMimeTypes, Result);
        }

        [Fact]
        public void CanGetOrder()
        {
            // Assert
            var Result = Assert.IsType<int>(_TestClass.Order);
            Assert.Equal(200, Result);
        }

        [Property]
        public void CanRead([Required] string fileName, [Required] string extension)
        {
            var Result = _TestClass.CanRead(fileName + ".rss");
            Assert.True(Result);

            Result = _TestClass.CanRead(fileName + "." + extension);
            var ExpectedResult = string.Equals(extension, "RSS", System.StringComparison.OrdinalIgnoreCase);
            Assert.Equal(ExpectedResult, Result);
        }

        [Fact]
        public async Task CanReadAsync()
        {
            // Arrange
            var TestData = new FileStream("./TestData/TestRSS.rss", FileMode.Open);
            var TestObject = new RSSFormat(_Converter);

            // Act
            Feed Result = Assert.IsType<Feed>(await TestObject.ReadAsync(TestData));

            // Assert
            Assert.NotNull(Result);
            Archivist.DataTypes.Feeds.Channel Channel = Assert.Single(Result.Channels);
            Assert.Equal(10, Channel.Count);
            Assert.Equal("The Underfold", Channel.Title);
            Assert.Equal("http://theunderfold.com", Channel.Link);
            Assert.Equal("A comic by Brian Russell where Brian and his bag-faced friend, JB embark on misadventures!", Channel.Description);
            Assert.Equal("en-US", Channel.Language);
        }

        [Property]
        public void CanWrite([Required] string fileName, [Required] string extension)
        {
            var Result = _TestClass.CanWrite(fileName + ".rss");
            Assert.True(Result);

            Result = _TestClass.CanWrite(fileName + "." + extension);
            var ExpectedResult = string.Equals(extension, "RSS", System.StringComparison.OrdinalIgnoreCase);
            Assert.Equal(ExpectedResult, Result);
        }

        [Fact]
        public async Task CanWriteAsync()
        {
            // Arrange
            var TestData = new FileStream("./TestData/TestRSS.rss", FileMode.Open);
            Feed Feed = Assert.IsType<Feed>(await _TestClass.ReadAsync(TestData));
            var NewStream = new MemoryStream();

            // Act
            Assert.True(await _TestClass.WriteAsync(NewStream, Feed));
            _ = NewStream.Seek(0, SeekOrigin.Begin);
            var StreamContent = NewStream.ReadAll();
            Assert.NotNull(StreamContent);
            _ = NewStream.Seek(0, SeekOrigin.Begin);
            Feed NewFeed = Assert.IsType<Feed>(await _TestClass.ReadAsync(NewStream));

            // Assert
            Assert.NotNull(NewFeed);
            Assert.Equal(Feed.GetContent(), NewFeed.GetContent());
            Assert.Equal(Feed, NewFeed);
        }
    }
}