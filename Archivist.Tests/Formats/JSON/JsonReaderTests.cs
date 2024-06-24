using Archivist.Formats.JSON;
using Archivist.Tests.BaseClasses;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.JSON
{
    public class JsonReaderTests : TestBaseClass<JsonReader>
    {
        public JsonReaderTests()
        {
            _Options = new JsonSerializerOptions();
            _TestClass = new JsonReader(_Options);
            TestObject = new JsonReader(_Options);
        }

        private readonly JsonSerializerOptions _Options;
        private readonly JsonReader _TestClass;

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
            const string JsonData = "{ \"TestProperty\": \"TestValue\" }";
            var Data = System.Text.Encoding.UTF8.GetBytes(JsonData);
            Stream.Write(Data, 0, Data.Length);
            _ = Stream.Seek(0, SeekOrigin.Begin);

            // Act
            Interfaces.IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new JsonReader(_Options);

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
    }
}