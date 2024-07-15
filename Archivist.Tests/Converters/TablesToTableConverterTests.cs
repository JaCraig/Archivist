using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class TablesToTableConverterTests : TestBaseClass<TablesToTableConverter>
    {
        public TablesToTableConverterTests()
        {
            _Converter = new TablesToTableConverter();
            TestObject = new TablesToTableConverter();
        }

        private readonly TablesToTableConverter _Converter;

        [Fact]
        public void CanConvert_WithSourceNotTablesAndDestinationNotTable_ReturnsFalse()
        {
            // Arrange
            System.Type Source = typeof(string);
            System.Type Destination = typeof(int);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConvert_WithSourceNotTablesAndDestinationTable_ReturnsFalse()
        {
            // Arrange
            System.Type Source = typeof(string);
            System.Type Destination = typeof(Table);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConvert_WithSourceTablesAndDestinationNotTable_ReturnsFalse()
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
        public void CanConvert_WithSourceTablesAndDestinationTable_ReturnsTrue()
        {
            // Arrange
            System.Type Source = typeof(Tables);
            System.Type Destination = typeof(Table);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void Convert_WithEmptyTables_ReturnsNull()
        {
            // Arrange
            var Tables = new Tables();

            // Act
            Table? Result = TablesToTableConverter.Convert(Tables);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_WithNullSource_ReturnsNull()
        {
            // Arrange
            object? Source = null;
            System.Type Destination = typeof(Table);

            // Act
            var Result = _Converter.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_WithNullTables_ReturnsNull()
        {
            // Arrange
            Tables? Tables = null;

            // Act
            Table? Result = TablesToTableConverter.Convert(Tables);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_WithSourceNotTables_ReturnsNull()
        {
            // Arrange
            object Source = "test";
            System.Type Destination = typeof(Table);

            // Act
            var Result = _Converter.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_WithTables_CopiesMetadataToResultTable()
        {
            // Arrange
            var Table1 = new Table
            {
                Title = "Table 1"
            };
            Table1.Metadata.Add("Key1", "Value1");
            Table1.Metadata.Add("Key2", "Value2");

            var Table2 = new Table
            {
                Title = "Table 2"
            };
            Table2.Metadata.Add("Key3", "Value3");
            Table2.Metadata.Add("Key4", "Value4");

            var Tables = new Tables
            {
                Table1,
                Table2
            };

            // Act
            Table? Result = TablesToTableConverter.Convert(Tables);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(Table1.Metadata, Result.Metadata);
        }

        [Fact]
        public void Convert_WithTables_ReturnsFirstTable()
        {
            // Arrange
            var Table1 = new Table
            {
                Title = "Table 1"
            };
            Table1.Metadata.Add("Key1", "Value1");
            Table1.Metadata.Add("Key2", "Value2");

            var Table2 = new Table
            {
                Title = "Table 2",
            };
            Table2.Metadata.Add("Key3", "Value3");
            Table2.Metadata.Add("Key4", "Value4");

            var Tables = new Tables
            {
                Table1,
                Table2
            };

            // Act
            Table? Result = TablesToTableConverter.Convert(Tables);

            // Assert
            Assert.Equal(Table1, Result);
        }

        [Fact]
        public void Convert_WithTables_SetsTitleOfResultTable()
        {
            // Arrange
            var Table1 = new Table
            {
                Title = "Table 1",
            };
            Table1.Metadata.Add("Key1", "Value1");
            Table1.Metadata.Add("Key2", "Value2");

            var Table2 = new Table
            {
                Title = "Table 2"
            };
            Table2.Metadata.Add("Key3", "Value3");
            Table2.Metadata.Add("Key4", "Value4");

            var Tables = new Tables
            {
                Table1,
                Table2
            };

            // Act
            Table? Result = TablesToTableConverter.Convert(Tables);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(Table1.Title, Result.Title);
        }
    }
}