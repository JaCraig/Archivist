using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Formats.YAML;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.YAML
{
    public class YAMLWriterTests : TestBaseClass<YAMLWriter>
    {
        public YAMLWriterTests()
        {
            _TestClass = new YAMLWriter();
            TestObject = new YAMLWriter();
        }

        private readonly YAMLWriter _TestClass;

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            var File = new StructuredObject();
            _ = File.SetValue("Test", "Value");
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);
            Stream.Position = 0;
            var ResultContent = Stream.ReadAll();

            // Assert
            Assert.True(Result);
            Assert.NotEmpty(ResultContent);
            Assert.Equal("test: Value" + System.Environment.NewLine, ResultContent);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new YAMLWriter();

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenFileIsNotStructuredObjectAsync()
        {
            // Arrange
            var File = new Text("", "");
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
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
        public async Task WriteAsync_ReturnsFalse_WhenStreamCannotWriteAsync()
        {
            // Arrange
            var File = new StructuredObject();
            var Stream = new MemoryStream();
            Stream.Close();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenStreamIsNullAsync()
        {
            // Arrange
            var File = new StructuredObject();

            // Act
            var Result = await _TestClass.WriteAsync(File, null);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenStructuredObjectIsNullAsync()
        {
            // Arrange
            IGenericFile? File = null;
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }
    }
}