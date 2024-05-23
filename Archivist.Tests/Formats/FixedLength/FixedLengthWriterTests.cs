using Archivist.DataTypes;
using Archivist.Formats.FixedLength;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.FixedLength
{
    public class FixedLengthWriterTests : TestBaseClass<FixedLengthWriter>
    {
        public FixedLengthWriterTests()
        {
            TestObject = new FixedLengthWriter();
        }

        private readonly FixedLengthWriter _TestClass = new();

        [Fact]
        public void CanCallCanWrite()
        {
            // Arrange
            IGenericFile File = new FixedLengthFile();

            // Act
            var Results = _TestClass.CanWrite(File);

            // Assert
            Assert.True(Results);
        }

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();
            var Stream = new MemoryStream();

            // Act
            var Results = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Results);
        }

        [Fact]
        public async Task WriteAsync_ShouldReturnFalse_WhenFileIsNullAsync()
        {
            // Arrange
            var Stream = new MemoryStream();
            var Writer = new FixedLengthWriter();

            // Act
            var Result = await Writer.WriteAsync(null, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ShouldReturnFalse_WhenStreamIsNullAsync()
        {
            // Arrange
            var File = new FixedLengthFile();
            var Writer = new FixedLengthWriter();

            // Act
            var Result = await Writer.WriteAsync(File, null);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ShouldWriteFileContentToStreamAsync()
        {
            // Arrange
            var File = new FixedLengthFile();
            File.Records.Add(new FixedLengthRecord());
            File.Records[0].Fields.Add(new FixedLengthField("Field 1", 10));
            File.Records[0].Fields.Add(new FixedLengthField("Field 2", 10));
            var Stream = new MemoryStream();
            var Writer = new FixedLengthWriter();
            const string ExpectedContent = "Field 1   Field 2   ";

            // Act
            var Result = await Writer.WriteAsync(File, Stream);

            // Assert
            Assert.True(Result);
            Stream.Position = 0;
            using var Reader = new StreamReader(Stream, Encoding.UTF8);
            var WrittenContent = await Reader.ReadToEndAsync();
            Assert.Equal(ExpectedContent, WrittenContent);
        }
    }
}