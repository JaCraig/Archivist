using Archivist.DataTypes;
using Archivist.Formats.JSON;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.JSON
{
    public class JsonWriterTests : TestBaseClass<JsonWriter>
    {
        public JsonWriterTests()
        {
            _Options = new JsonSerializerOptions();
            _TestClass = new JsonWriter(_Options);
            TestObject = new JsonWriter(_Options);
        }

        private readonly JsonSerializerOptions _Options;
        private readonly JsonWriter _TestClass;

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            IGenericFile File = new StructuredObject();
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new JsonWriter(_Options);

            // Assert
            Assert.NotNull(Instance);
        }
    }
}