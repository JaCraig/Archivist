using Archivist.Formats.Excel;
using Archivist.Tests.BaseClasses;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Excel
{
    public class ExcelFormatTests : TestBaseClass<ExcelFormat>
    {
        public ExcelFormatTests()
        {
            _TestClass = new ExcelFormat();
        }

        private readonly ExcelFormat _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new ExcelFormat();

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            var Results = Assert.IsType<string[]>(_TestClass.Extensions);

            _ = Assert.Single(Results);
            Assert.Equal("XLSX", Results[0]);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            var Results = Assert.IsType<string[]>(_TestClass.MimeTypes);

            _ = Assert.Single(Results);
            Assert.Equal("APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT.SPREADSHEETML.SHEET", Results[0]);
        }

        [Fact]
        public async Task CanReadAndWriteAsync()
        {
            //Arrange
            var Tables = new Archivist.DataTypes.Tables();
            Archivist.DataTypes.Table Table = Tables.AddTable();
            Table.Columns.AddRange(new[] { "Col1", "Col2", "Col3" });
            Archivist.DataTypes.TableRow Row = Table.AddRow();
            Row.AddRange(new[] { "Test", "Data", "Row1" });
            Row = Table.AddRow();
            Row.AddRange(new[] { "Testing", "Data", "Row2" });

            await using var Stream = new System.IO.MemoryStream();

            // Act
            var WriteResult = await _TestClass.WriteAsync(Stream, Tables);
            Interfaces.IGenericFile? ReadResult = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.True(WriteResult);
            Assert.NotNull(ReadResult);
            Assert.Equal(Tables, ReadResult);
        }
    }
}