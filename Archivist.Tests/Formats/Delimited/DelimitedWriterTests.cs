using Archivist.Formats.Delimited;
using Archivist.Tests.BaseClasses;
using BigBook;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Delimited
{
    public class DelimitedWriterTests : TestBaseClass<DelimitedWriter>
    {
        public DelimitedWriterTests()
        {
            _TestClass = new DelimitedWriter();
            TestObject = new DelimitedWriter();
        }

        private readonly DelimitedWriter _TestClass;

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            var Table = new Archivist.DataTypes.Table();
            Table.AddRow().Add("Test");
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(Table, Stream);
            var StreamData = Stream.ToArray();

            // Assert
            Assert.True(Result);
            Assert.True(StreamData.Length > 0);
            Assert.Equal($"\"Test\"{Environment.NewLine}", StreamData.ToString(Encoding.UTF8));
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenFileIsNullAsync()
        {
            // Arrange
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(null, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenStreamIsNullAsync()
        {
            // Arrange
            var Table = new Archivist.DataTypes.Table();

            // Act
            var Result = await _TestClass.WriteAsync(Table, null);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenWriteFailsAsync()
        {
            // Arrange
            var Table = new Archivist.DataTypes.Table();
            Table.AddRow().Add("Test");
            var Stream = new MemoryStream();
            Stream.Close(); // Close the stream to simulate a failure during write

            // Act
            var Result = await _TestClass.WriteAsync(Table, Stream);

            // Assert
            Assert.False(Result);
        }
    }
}