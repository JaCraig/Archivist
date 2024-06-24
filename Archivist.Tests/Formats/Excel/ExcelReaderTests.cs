using Archivist.DataTypes;
using Archivist.Formats.Excel;
using Archivist.Options;
using Archivist.Tests.BaseClasses;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Excel
{
    public class ExcelReaderTests : TestBaseClass<ExcelReader>
    {
        public ExcelReaderTests()
        {
            _TestClass = new ExcelReader(ExcelOptions.Default);
            TestObject = new ExcelReader(ExcelOptions.Default);
        }

        private readonly ExcelReader _TestClass;

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
        public void CanCallInternalCanReadWithNullStream()
        {
            // Act
            var Result = _TestClass.InternalCanRead(default);

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
            _ = Assert.IsType<Tables>(Result);
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            var Result = Assert.IsType<byte[]>(_TestClass.HeaderInfo);

            Assert.NotNull(Result);
            Assert.Equal(new byte[] { 0x50, 0x4B, 0x03, 0x04 }, Result);
        }

        [Fact]
        public async Task CanReadAsync()
        {
            // Arrange
            FileStream Stream = new FileInfo("./TestData/TestXLSX.xlsx").OpenRead();

            // Act
            var Result = (Tables?)await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            Assert.Equal("Test Data\nGoes here\n 1", Result.GetContent());
            Assert.NotEmpty(Result.Metadata);
            _ = Assert.Single(Result);
            Assert.Equal(2, Result[0].Columns.Count);
            Assert.Equal("Test", Result[0].Columns[0]);
            Assert.Equal("Data", Result[0].Columns[1]);
            Assert.Equal(2, Result[0].Count);
            Assert.Equal("Goes", Result[0][0][0].Content);
            Assert.Equal("here", Result[0][0][1].Content);
            Assert.Equal("", Result[0][1][0].Content);
            Assert.Equal("1", Result[0][1][1].Content);
        }
    }
}