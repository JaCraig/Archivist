using Archivist.DataTypes;
using Archivist.Formats.YAML;
using Archivist.Tests.BaseClasses;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.YAML
{
    public class YAMLReaderTests : TestBaseClass<YAMLReader>
    {
        public YAMLReaderTests()
        {
            _TestClass = new YAMLReader();
            TestObject = new YAMLReader();
        }

        private readonly YAMLReader _TestClass;

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
            Interfaces.IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new YAMLReader();

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            var Result = Assert.IsType<byte[]>(_TestClass.HeaderInfo);

            Assert.Empty(Result);
        }

        [Fact]
        public void CanReadTestFile()
        {
            // Arrange
            using FileStream FileStream = new FileInfo("./TestData/TestYAML.yaml").OpenRead();

            // Act
            var Result = _TestClass.InternalCanRead(FileStream);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public async Task CanReadTestFileAsync()
        {
            // Arrange
            await using FileStream FileStream = new FileInfo("./TestData/TestYAML.yaml").OpenRead();

            // Act
            var Result = await _TestClass.ReadAsync(FileStream) as StructuredObject;

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("George Washington", Result.GetValue<string>("name"));
        }

        [Fact]
        public void CanReadTestStream()
        {
            // Arrange
            using var Stream = new MemoryStream();
            using StreamWriter Writer = new(Stream);
            Writer.WriteLine("name: George Washington");
            Writer.Flush();
            _ = Stream.Seek(0, SeekOrigin.Begin);

            // Act
            var Result = _TestClass.InternalCanRead(Stream);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public async Task CanReadTestStreamAsync()
        {
            // Arrange
            using var Stream = new MemoryStream();
            using StreamWriter Writer = new(Stream);
            Writer.WriteLine("name: George Washington");
            Writer.Flush();
            _ = Stream.Seek(0, SeekOrigin.Begin);

            // Act
            var Result = await _TestClass.ReadAsync(Stream) as StructuredObject;

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("George Washington", Result.GetValue<string>("name"));
        }

        [Fact]
        public void CanReadTestString()
        {
            // Arrange
            const string TestString = "name: George Washington";

            // Act
            var Result = _TestClass.InternalCanRead(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(TestString)));

            // Assert
            Assert.True(Result);
        }
    }
}