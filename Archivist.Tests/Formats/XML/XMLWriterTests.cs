using Archivist.DataTypes;
using Archivist.Formats.XML;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.XML
{
    public class XMLWriterTests : TestBaseClass<XMLWriter>
    {
        public XMLWriterTests()
        {
            _TestClass = new XMLWriter();
            TestObject = new XMLWriter();
        }

        private readonly XMLWriter _TestClass;

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenFileIsNotStructuredObjectAsync()
        {
            // Arrange
            var Writer = new XMLWriter();
            IGenericFile File = Substitute.For<IGenericFile>();
            Stream Stream = new MemoryStream();
            _ = File.ToFileType<StructuredObject>().Returns((StructuredObject?)null);

            // Act
            var Result = await Writer.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenFileIsNullAsync()
        {
            // Arrange
            var Writer = new XMLWriter();
            Stream Stream = new MemoryStream();

            // Act
            var Result = await Writer.WriteAsync(null, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenStreamCannotWriteAsync()
        {
            // Arrange
            var Writer = new XMLWriter();
            IGenericFile File = Substitute.For<IGenericFile>();
            Stream Stream = Substitute.For<Stream>();
            _ = Stream.CanWrite.Returns(false);

            // Act
            var Result = await Writer.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenStreamIsNullAsync()
        {
            // Arrange
            var Writer = new XMLWriter();
            IGenericFile File = Substitute.For<IGenericFile>();

            // Act
            var Result = await Writer.WriteAsync(File, null);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsTrue_WhenContentIsNullAsync()
        {
            // Arrange
            var Writer = new XMLWriter();
            Stream Stream = new MemoryStream();
            var StructuredObject = new StructuredObject();

            // Act
            var Result = await Writer.WriteAsync(StructuredObject, Stream);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsTrue_WhenWriteObjectIsSuccessfulAsync()
        {
            // Arrange
            var Writer = new XMLWriter();
            Stream Stream = new MemoryStream();
            var StructuredObject = new StructuredObject();
            _ = StructuredObject.SetValue("TestProperty", "TestValue");

            // Act
            var Result = await Writer.WriteAsync(StructuredObject, Stream);

            // Assert
            Assert.True(Result);
        }
    }
}