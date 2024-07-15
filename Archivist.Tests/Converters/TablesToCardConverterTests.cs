using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System.Linq;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class TablesToCardConverterTests : TestBaseClass<TablesToCardConverter>
    {
        public TablesToCardConverterTests()
        {
            _Converter = new TablesToCardConverter();
            TestObject = new TablesToCardConverter();
        }

        private readonly TablesToCardConverter _Converter;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            System.Type Source = typeof(Tables);
            System.Type Destination = typeof(Card);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallCanConvert_WithInvalidDestination()
        {
            // Arrange
            System.Type Source = typeof(Tables);
            System.Type Destination = typeof(string);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallCanConvert_WithInvalidSource()
        {
            // Arrange
            System.Type Source = typeof(string);
            System.Type Destination = typeof(Card);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Convert_ReturnsCardWithMetadataAndTitle()
        {
            // Arrange
            var File = new Tables
            {
                Title = "Sample Title"
            };
            File.Metadata.Add("Key1", "Value1");
            File.Metadata.Add("Key2", "Value2");
            Table Table = File.AddTable();
            Table.Columns.Add("Column1");
            Table.Columns.Add("Column2");
            TableRow TableRow = Table.AddRow();
            TableRow.Add("Cell1");
            TableRow.Add("Cell2");

            // Act
            Card? Result = TablesToCardConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(3, Result.Metadata.Count);
            Assert.Equal("Value1", Result.Metadata["Key1"]);
            Assert.Equal("Value2", Result.Metadata["Key2"]);
            Assert.Equal(File.Title, Result.Title);
            Assert.Equal(2, Result.Fields.Count);
            Assert.Equal("Cell1", Result["Column1"].FirstOrDefault()?.Value);
            Assert.Equal("Cell2", Result["Column2"].FirstOrDefault()?.Value);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenFileIsEmpty()
        {
            // Arrange
            var File = new Tables();

            // Act
            Card? Result = TablesToCardConverter.Convert(File);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenFileIsNull()
        {
            // Arrange
            Tables? File = null;

            // Act
            Card? Result = TablesToCardConverter.Convert(File);

            // Assert
            Assert.Null(Result);
        }
    }
}