using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class TableToCardConverterTests : TestBaseClass<TableToCardConverter>
    {
        public TableToCardConverterTests()
        {
            _Converter = new TableToCardConverter();
            TestObject = new TableToCardConverter();
        }

        private readonly TableToCardConverter _Converter;

        [Fact]
        public void CanConvert_InvalidDestination_ReturnsFalse()
        {
            // Arrange
            Type Source = typeof(Table);
            Type Destination = typeof(string);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConvert_InvalidSource_ReturnsFalse()
        {
            // Arrange
            Type Source = typeof(string);
            Type Destination = typeof(Card);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConvert_ValidSourceAndDestination_ReturnsTrue()
        {
            // Arrange
            Type Source = typeof(Table);
            Type Destination = typeof(Card);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void Convert_EmptyTable_ReturnsCardWithTitle()
        {
            // Arrange
            var Table = new Table
            {
                Title = "Test Table"
            };

            // Act
            Card? Result = TableToCardConverter.Convert(Table);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Test Table", Result.Title);
            Assert.Empty(Result.Fields);
            _ = Assert.Single(Result.Metadata);
            Assert.Equal(",", Result.Metadata["Delimiter"]);
        }

        [Fact]
        public void Convert_NullSource_ReturnsNull()
        {
            // Arrange
            object? Source = null;
            Type Destination = typeof(Card);

            // Act
            var Result = _Converter.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_NullTable_ReturnsNull()
        {
            // Arrange
            Table? Table = null;

            // Act
            Card? Result = TableToCardConverter.Convert(Table);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ObjectAndDestinationType_ReturnsConvertedObject()
        {
            // Arrange
            object Source = new Table();
            Type Destination = typeof(Card);

            // Act
            var Result = _Converter.Convert(Source, Destination);

            // Assert
            _ = Assert.IsType<Card>(Result);
        }

        [Fact]
        public void Convert_TableWithColumns_ReturnsCardWithFields()
        {
            // Arrange
            var Table = new Table
            {
                Title = "Test Table"
            };
            Table.Columns.AddRange(new List<string> { "Column1", "Column2", "Column3" });
            Table.AddRow().AddRange(new List<string> { "Value1", "Value2", "Value3" });

            // Act
            Card? Result = TableToCardConverter.Convert(Table);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Test Table", Result.Title);
            Assert.Equal(3, Result.Fields.Count);
            Assert.Equal("Column1", Result.Fields[0]?.Property);
            Assert.Equal("Value1", Result.Fields[0]?.Value);
            Assert.Equal("Column2", Result.Fields[1]?.Property);
            Assert.Equal("Value2", Result.Fields[1]?.Value);
            Assert.Equal("Column3", Result.Fields[2]?.Property);
            Assert.Equal("Value3", Result.Fields[2]?.Value);
            _ = Assert.Single(Result.Metadata);
            Assert.Equal(",", Result.Metadata["Delimiter"]);
        }

        [Fact]
        public void Convert_TableWithMetadata_ReturnsCardWithMetadata()
        {
            // Arrange
            var Table = new Table
            {
                Title = "Test Table",
            };
            Table.Metadata.Add("Key1", "Value1");
            Table.Metadata.Add("Key2", "Value2");

            // Act
            Card? Result = TableToCardConverter.Convert(Table);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Test Table", Result.Title);
            Assert.Empty(Result.Fields);
            Assert.Equal(3, Result.Metadata.Count);
            Assert.Equal("Value1", Result.Metadata["Key1"]);
            Assert.Equal("Value2", Result.Metadata["Key2"]);
        }
    }
}