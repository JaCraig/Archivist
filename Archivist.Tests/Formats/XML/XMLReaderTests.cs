using Archivist.DataTypes;
using Archivist.Formats.XML;
using Archivist.Tests.BaseClasses;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.XML
{
    public class XMLReaderTests : TestBaseClass<XMLReader>
    {
        public XMLReaderTests()
        {
            _Options = new JsonSerializerSettings();
            _TestClass = new XMLReader(_Options);
            TestObject = new XMLReader(_Options);
        }

        private readonly JsonSerializerSettings _Options;
        private readonly XMLReader _TestClass;

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
            var Instance = new XMLReader(_Options);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            var Result = Assert.IsType<byte[]>(_TestClass.HeaderInfo);

            Assert.Equal(new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22 }, Result);
        }

        [Fact]
        public async Task CanReadAsync()
        {
            // Arrange
            FileStream Stream = new FileInfo("./TestData/TestXML.xml").OpenRead();

            // Act
            var Result = (StructuredObject?)await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            var ResultVal = Result.GetContent();
            Assert.Equal("{\"whmcsapi\":{\"@version\":\"4.1.2\",\"action\":\"getstaffonline\",\"result\":\"success\",\"totalresults\":\"1\",\"staffonline\":{\"staff\":{\"adminusername\":\"Admin\",\"logintime\":\"2010-03-03 18:29:12\",\"ipaddress\":\"127.0.0.1\",\"lastvisit\":\"2010-03-03 18:30:43\"}}}}", ResultVal);
            Assert.Empty(Result.Metadata);
        }

        [Fact]
        public async Task ReadAsync_ReturnsEmptyStructuredObject_WhenDataIsInvalidAsync()
        {
            // Arrange
            var Reader = new XMLReader(_Options);
            Stream Stream = new MemoryStream();
            Stream.Write(new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22 }, 0, 19);
            Stream.Write(new byte[] { 0x3E }, 0, 1);
            Stream.Close();

            // Act
            Interfaces.IGenericFile? Result = await Reader.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            var ResultVal = Result.GetContent();
            Assert.Equal("{}", ResultVal);
            Assert.Empty(Result.Metadata);
        }

        [Fact]
        public async Task ReadAsync_ReturnsEmptyStructuredObject_WhenDataIsNotStructuredObjectAsync()
        {
            // Arrange
            var Reader = new XMLReader(_Options);
            Stream Stream = new MemoryStream();
            Stream.Write(new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22 }, 0, 19);
            Stream.Write(new byte[] { 0x3E, 0x3C, 0x2F, 0x3F, 0x3E }, 0, 5);
            Stream.Close();

            // Act
            Interfaces.IGenericFile? Result = await Reader.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            var ResultVal = Result.GetContent();
            Assert.Equal("{}", ResultVal);
            Assert.Empty(Result.Metadata);
        }

        [Fact]
        public async Task ReadAsync_ReturnsEmptyStructuredObject_WhenDataIsNullAsync()
        {
            // Arrange
            var Reader = new XMLReader(_Options);
            Stream Stream = new MemoryStream();
            Stream.Write(new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22 }, 0, 19);
            Stream.Close();

            // Act
            Interfaces.IGenericFile? Result = await Reader.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            var ResultVal = Result.GetContent();
            Assert.Equal("{}", ResultVal);
            Assert.Empty(Result.Metadata);
        }

        [Fact]
        public async Task ReadAsync_ReturnsEmptyStructuredObject_WhenStreamCannotReadAsync()
        {
            // Arrange
            var Reader = new XMLReader(_Options);
            Stream Stream = new MemoryStream();
            Stream.Close();

            // Act
            Interfaces.IGenericFile? Result = await Reader.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            var ResultVal = Result.GetContent();
            Assert.Equal("{}", ResultVal);
            Assert.Empty(Result.Metadata);
        }

        [Fact]
        public async Task ReadAsync_ReturnsEmptyStructuredObject_WhenStreamDataIsNullOrEmptyAsync()
        {
            // Arrange
            var Reader = new XMLReader(_Options);
            Stream Stream = new MemoryStream();

            // Act
            Interfaces.IGenericFile? Result = await Reader.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            var ResultVal = Result.GetContent();
            Assert.Equal("{}", ResultVal);
            Assert.Empty(Result.Metadata);
        }
    }
}