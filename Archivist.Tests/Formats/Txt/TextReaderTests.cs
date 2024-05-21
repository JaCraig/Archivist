using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using BigBook;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Txt
{
    public class TextReaderTests : TestBaseClass<Archivist.Formats.Txt.TextReader>
    {
        private readonly Archivist.Formats.Txt.TextReader _TestClass = new();

        [Fact]
        public async Task CanCallReadAsync()
        {
            // Arrange
            var Stream = new MemoryStream();

            // Act
            Interfaces.IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Text>(Result);
            Assert.Empty(Result.Content ?? "");
            Assert.Empty(Result.Title ?? "");
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            var Result = Assert.IsType<byte[]>(_TestClass.HeaderInfo);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public async Task ReadAsync_Returns_WhenStreamIsNullAsync()
        {
            // Arrange
            Stream? StreamObj = null;
            var TextReaderObj = new Archivist.Formats.Txt.TextReader();

            // Act
            Interfaces.IGenericFile? Result = await TextReaderObj.ReadAsync(StreamObj);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Text>(Result);
            Assert.Empty(Result.Content ?? "");
            Assert.Empty(Result.Title ?? "");
        }

        [Fact]
        public async Task ReadAsync_ReturnsTextFileContentAsync()
        {
            // Arrange
            var Stream = new MemoryStream();
            var TestClass = new Archivist.Formats.Txt.TextReader();
            const string ExpectedContent = "Sample text content that is longer than 30 characters";
            var ExpectedTitle = "Sample text content that is longer than 30 characters"[..30];
            Stream.Write(ExpectedContent.ToByteArray());

            // Act
            Interfaces.IGenericFile? Result = await TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Text>(Result);
            Assert.Equal(ExpectedContent, Result.Content);
            Assert.Equal(ExpectedTitle, Result.Title);
        }
    }
}