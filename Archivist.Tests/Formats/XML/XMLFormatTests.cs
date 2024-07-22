using Archivist.DataTypes;
using Archivist.Formats.XML;
using Archivist.Tests.BaseClasses;
using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.XML
{
    public class XMLFormatTests : TestBaseClass<XMLFormat>
    {
        public XMLFormatTests()
        {
            _Options = new JsonSerializerSettings();
            _TestClass = new XMLFormat(_Options);
            TestObject = new XMLFormat(_Options);
        }

        private static readonly string[] _ExpectedExtensions = new[] { "XML" };
        private static readonly string[] _ExpectedMimeTypes = new[] { "TEXT/XML", "APPLICATION/XML" };
        private readonly JsonSerializerSettings _Options;
        private readonly XMLFormat _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new XMLFormat(_Options);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullOptions() => _ = new XMLFormat(default);

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.Extensions);

            Assert.Equal(_ExpectedExtensions, Result);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.MimeTypes);

            Assert.Equal(_ExpectedMimeTypes, Result);
        }

        [Fact]
        public async Task CanReadAsync()
        {
            // Arrange
            await using var Stream = new MemoryStream();

            // Act
            Interfaces.IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public async Task CanWriteAsync()
        {
            // Arrange
            await using var Stream = new MemoryStream();
            var File = new StructuredObject(new ExpandoObject());

            // Act
            var Result = await _TestClass.WriteAsync(Stream, File);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public async Task ReadAndWrite_AreCompatibleAsync()
        {
            // Arrange
            await using var Stream = new MemoryStream();
            var File = new StructuredObject(new ExpandoObject());
            _ = File.SetValue("TestValue", 10)
                .SetValue("TestString", "Test");

            // Act
            var WriteResult = await _TestClass.WriteAsync(Stream, File);
            Stream.Position = 0;
            StructuredObject? ReadResult = (await _TestClass.ReadAsync(Stream))?.ToFileType<StructuredObject>();
            var ReadValue = ReadResult?.GetValue<int>("TestValue");
            var ReadString = ReadResult?.GetValue<string>("TestString");

            // Assert
            Assert.NotNull(ReadResult);
            Assert.True(WriteResult);
            Assert.Equal(10, ReadValue);
            Assert.Equal("Test", ReadString);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenFileIsNullAsync()
        {
            // Arrange
            await using var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(Stream, null);

            // Assert
            Assert.False(Result);
        }
    }
}