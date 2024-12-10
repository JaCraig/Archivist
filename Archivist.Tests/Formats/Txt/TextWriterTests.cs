using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Txt
{
    public class TextWriterTests : TestBaseClass<Archivist.Formats.Txt.TextWriter>
    {
        public TextWriterTests()
        {
            _TestClass = new Archivist.Formats.Txt.TextWriter(null);
            TestObject = new Archivist.Formats.Txt.TextWriter(null);
        }

        private readonly Archivist.Formats.Txt.TextWriter _TestClass;

        [Fact]
        public void CanCallCanWrite()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();

            // Act
            var Result = _TestClass.CanWrite(File);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();
            await using var Stream = new MemoryStream();
            const string Content = "Test content";
            _ = File.GetContent().Returns(Content);

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.True(Result);
            var Data = Stream.ToArray();
            var WrittenContent = Encoding.UTF8.GetString(Data);
            Assert.Equal(Content, WrittenContent);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenExceptionIsThrownAsync()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();
            var Stream = new MemoryStream();
            _ = File.GetContent().Returns("Test content");
            Stream.Close(); // Close the stream to simulate an exception

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenStreamIsNullAsync()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();

            // Act
            var Result = await _TestClass.WriteAsync(File, null);

            // Assert
            Assert.False(Result);
        }
    }
}