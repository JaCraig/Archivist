using Archivist.DataTypes;
using Archivist.Formats.Excel;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Excel
{
    public class ExcelWriterTests : TestBaseClass<ExcelWriter>
    {
        public ExcelWriterTests()
        {
            _TestClass = new ExcelWriter();
            TestObject = new ExcelWriter();
        }

        private readonly ExcelWriter _TestClass;

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
        public async Task WriteAsync_ReturnsFalse_WhenExceptionThrownAsync()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();
            var Stream = new MemoryStream();
            _ = File.ToFileType<Tables>()?.Returns(_ => throw new Exception());

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenFileIsNullAsync()
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
        public async Task WriteAsync_ReturnsFalse_WhenStreamCannotWriteAsync()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();
            Stream Stream = Substitute.For<Stream>();
            _ = Stream.CanWrite.Returns(false);

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }
    }
}