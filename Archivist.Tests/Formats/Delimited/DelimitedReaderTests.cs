using Archivist.Formats.Delimited;
using Archivist.Options;
using Archivist.Tests.BaseClasses;
using Archivist.Tests.Utils;
using BigBook.ExtensionMethods;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Delimited
{
    public class DelimitedReaderTests : TestBaseClass<DelimitedReader>
    {
        public DelimitedReaderTests()
        {
            _TestClass = new DelimitedReader(DelimitedOptions.Default, null, null);
            TestObject = new DelimitedReader(DelimitedOptions.Default, null, null);
        }

        private readonly DelimitedReader _TestClass;

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
            var Instance = new DelimitedReader(DelimitedOptions.Default, null, null);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            var Result = Assert.IsType<byte[]>(_TestClass.HeaderInfo);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public async Task CanReadAsync()
        {
            // Arrange
            var Stream = new MemoryStream();
            Stream.Write(CSVFileData.ExampleCSV1.ToByteArray());
            Stream.Position = 0;

            // Act
            var Result = (Archivist.DataTypes.Table?)await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            Assert.NotEmpty(Result.Metadata);
            Assert.Equal(",", Result.Delimiter);
            Assert.Equal(6, Result.Columns.Count);

            Assert.Contains("Header 1", Result.Columns);
            Assert.Contains("Header 2", Result.Columns);
            Assert.Contains("Header 3", Result.Columns);
            Assert.Contains("Header 4", Result.Columns);
            Assert.Contains("Header 5", Result.Columns);
            Assert.Contains("Header 6", Result.Columns);

            Assert.Equal(3, Result.Count);
            Assert.Equal("This", Result[0]["Header 1"].Content);
            Assert.Equal("is", Result[0]["Header 2"].Content);
            Assert.Equal("a", Result[0]["Header 3"].Content);
            Assert.Equal("test", Result[0]["Header 4"].Content);
            Assert.Equal("CSV", Result[0]["Header 5"].Content);
            Assert.Equal("file", Result[0]["Header 6"].Content);

            Assert.Equal("Tons", Result[1]["Header 1"].Content);
            Assert.Equal("of", Result[1]["Header 2"].Content);
            Assert.Equal("data", Result[1]["Header 3"].Content);
            Assert.Equal("in here", Result[1]["Header 4"].Content);
            Assert.Equal("is", Result[1]["Header 5"].Content);
            Assert.Equal("super", Result[1]["Header 6"].Content);

            Assert.Equal("important", Result[2]["Header 1"].Content);
        }
    }
}