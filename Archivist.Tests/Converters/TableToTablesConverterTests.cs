using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class TableToTablesConverterTests : TestBaseClass<TableToTablesConverter>
    {
        public TableToTablesConverterTests()
        {
            _Converter = new TableToTablesConverter();
            TestObject = new TableToTablesConverter();
        }

        private readonly TableToTablesConverter _Converter;

        [Fact]
        public void CanConvert_DestinationTypeNotTables_ReturnsFalse()
        {
            // Arrange
            System.Type SourceType = typeof(Table);
            System.Type DestinationType = typeof(string);

            // Act
            var Result = _Converter.CanConvert(SourceType, DestinationType);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConvert_SourceTypeNotTable_ReturnsFalse()
        {
            // Arrange
            System.Type SourceType = typeof(string);
            System.Type DestinationType = typeof(Tables);

            // Act
            var Result = _Converter.CanConvert(SourceType, DestinationType);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConvert_SourceTypeTableAndDestinationTypeTables_ReturnsTrue()
        {
            // Arrange
            System.Type SourceType = typeof(Table);
            System.Type DestinationType = typeof(Tables);

            // Act
            var Result = _Converter.CanConvert(SourceType, DestinationType);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void Convert_NullTable_ReturnsNull()
        {
            // Arrange
            Table? Table = null;

            // Act
            Tables? Result = TableToTablesConverter.Convert(Table);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ObjectAndDestinationTypeTable_ReturnsNull()
        {
            // Arrange
            var Source = new object();
            System.Type DestinationType = typeof(Table);

            // Act
            var Result = _Converter.Convert(Source, DestinationType);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ObjectAndNullDestinationType_ReturnsNull()
        {
            // Arrange
            var Source = new object();
            var DestinationType = default(System.Type);

            // Act
            var Result = _Converter.Convert(Source, DestinationType!);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ValidTable_ReturnsTablesObject()
        {
            // Arrange
            var Table = new Table
            {
                Title = "Test Table"
            };
            Table.Metadata.Add("Key1", "Value1");
            Table.Metadata.Add("Key2", "Value2");

            // Act
            Tables? Result = TableToTablesConverter.Convert(Table);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(Table.Title, Result.Title);
            Assert.Equal(Table.Metadata, Result.Metadata);
        }
    }
}