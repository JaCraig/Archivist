using Archivist.DataTypes;
using Archivist.Formats.JSON;
using Archivist.Tests.BaseClasses;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.JSON
{
    public class JsonFormatTests : TestBaseClass<JsonFormat>
    {
        public JsonFormatTests()
        {
            _Options = new JsonSerializerSettings();
            _TestClass = new JsonFormat(_Options, null, null);
            TestObject = new JsonFormat(_Options, null, null);
        }

        private static readonly string[] _Expected = new[] { "JSON" };
        private static readonly string[] _ExpectedArray = new[] { "APPLICATION/JSON" };
        private readonly JsonSerializerSettings _Options;
        private readonly JsonFormat _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new JsonFormat(_Options, null, null);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullOptions()
        {
            // Act
            var Result = new JsonFormat(default, null, null);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.Extensions);

            Assert.NotNull(Result);
            Assert.Equal(_Expected, Result);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.MimeTypes);

            Assert.NotNull(Result);
            Assert.Equal(_ExpectedArray, Result);
        }

        [Fact]
        public async Task CanReadAndWriteAsync()
        {
            // Arrange
            await using var Stream = new System.IO.MemoryStream();
            var File = new StructuredObject()
            {
                ["Property1"] = "Value1",
                ["Property2"] = "Value2",
                ["Property3"] = "Value3"
            };

            // Act
            var WriteResult = await _TestClass.WriteAsync(Stream, File);
            _ = Stream.Seek(0, System.IO.SeekOrigin.Begin);
            StructuredObject? ReadResult = (await _TestClass.ReadAsync(Stream))?.ToFileType<StructuredObject>();

            // Assert
            Assert.True(WriteResult);
            Assert.NotNull(ReadResult);
            Assert.Equal("Value1", ReadResult.GetValue<string>("Property1"));
            Assert.Equal("Value2", ReadResult.GetValue<string>("Property2"));
            Assert.Equal("Value3", ReadResult.GetValue<string>("Property3"));
        }
    }
}