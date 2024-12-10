using Archivist.Tests.BaseClasses;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.JSON
{
    public class JsonReaderTests : TestBaseClass<Archivist.Formats.JSON.JsonReader>
    {
        public JsonReaderTests()
        {
            _Options = new JsonSerializerSettings();
            _TestClass = new Archivist.Formats.JSON.JsonReader(_Options, null, null);
            TestObject = new Archivist.Formats.JSON.JsonReader(_Options, null, null);
        }

        private readonly JsonSerializerSettings _Options;
        private readonly Archivist.Formats.JSON.JsonReader _TestClass;

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
            var Instance = new Archivist.Formats.JSON.JsonReader(_Options, null, null);

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