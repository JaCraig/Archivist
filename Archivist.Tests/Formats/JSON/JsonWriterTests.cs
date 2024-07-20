using Archivist.DataTypes;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.JSON
{
    public class JsonWriterTests : TestBaseClass<Archivist.Formats.JSON.JsonWriter>
    {
        public JsonWriterTests()
        {
            _Options = new JsonSerializerSettings();
            _TestClass = new Archivist.Formats.JSON.JsonWriter(_Options);
            TestObject = new Archivist.Formats.JSON.JsonWriter(_Options);
        }

        private readonly JsonSerializerSettings _Options;
        private readonly Archivist.Formats.JSON.JsonWriter _TestClass;

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
            var Instance = new Archivist.Formats.JSON.JsonWriter(_Options);

            // Assert
            Assert.NotNull(Instance);
        }
    }
}