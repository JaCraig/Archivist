using Archivist.DataTypes;
using Archivist.Formats.YAML;
using Archivist.Tests.BaseClasses;
using BigBook;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.YAML
{
    public class YAMLFormatTests : TestBaseClass<YAMLFormat>
    {
        public YAMLFormatTests()
        {
            _TestClass = new YAMLFormat(null);
            TestObject = new YAMLFormat(null);
        }

        private readonly YAMLFormat _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new YAMLFormat(null);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.Extensions);

            Assert.Equal("YAML", Result[0]);
            Assert.Equal("YML", Result[1]);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.MimeTypes);

            Assert.Equal("TEXT/YAML", Result[0]);
            Assert.Equal("APPLICATION/YAML", Result[1]);
        }

        [Fact]
        public async Task CanReadYAMLFileAsync()
        {
            // Arrange
            var FormatObject = new YAMLFormat(null);
            await using FileStream FileStream = new FileInfo("./TestData/TestYAML.yaml").OpenRead();

            // Act
            var Result = await FormatObject.ReadAsync(FileStream) as StructuredObject;

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("George Washington", Result.GetValue<string>("name"));
        }

        [Fact]
        public async Task CanWriteYAMLFileAsync()
        {
            // Arrange
            var Format = new YAMLFormat(null);
            var OutputStream = new MemoryStream();
            var Data = new StructuredObject();
            _ = Data.SetValue("name", "George Washington");

            // Act
            var Result = await Format.WriteAsync(OutputStream, Data);
            var StreamResult = OutputStream.ReadAll();

            // Assert
            Assert.True(Result);
            Assert.Equal("name: George Washington" + System.Environment.NewLine, StreamResult);
        }
    }
}