using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class TablesToStructuredObjectConverterTests : TestBaseClass<TablesToStructuredObjectConverter>
    {
        public TablesToStructuredObjectConverterTests()
        {
            _ = GetServiceProvider();
            _Converter = new TablesToStructuredObjectConverter();
            TestObject = new TablesToStructuredObjectConverter();
        }

        private readonly TablesToStructuredObjectConverter _Converter;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Tables);
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallCanConvertWithInvalidDestination()
        {
            // Arrange
            Type Source = typeof(Tables);
            Type Destination = typeof(string);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallCanConvertWithInvalidSource()
        {
            // Arrange
            Type Source = typeof(string);
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Convert_ReturnsNotNull_WhenDestinationIsNull()
        {
            // Arrange
            var File = new Tables();

            // Act
            StructuredObject? Result = TablesToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenFileIsNull()
        {
            // Arrange
            Tables? File = null;

            // Act
            StructuredObject? Result = TablesToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenSourceIsNotTables()
        {
            // Arrange
            var Source = new object();
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _Converter.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ReturnsStructuredObject_WithCorrectContent()
        {
            // Arrange
            var File = new Tables();
            var Table1 = new Table { Title = "Table1" };
            Table1.Columns.Add("Column1");
            Table1.Columns.Add("Column2");
            TableRow TableRow = Table1.AddRow();
            TableRow.Add("Cell1");
            TableRow.Add("Cell2");
            File.Add(Table1);

            // Act
            StructuredObject? Result = TablesToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.Single(Result);
            Assert.True(Result.ContainsKey("Table1"));
            var TableObject = (IDictionary<string, object?>?)Result.GetValue<ExpandoObject>("Table1");
            Assert.NotNull(TableObject);
            Assert.Equal(2, TableObject.Count);
            Assert.True(TableObject.ContainsKey("Column1"));
            Assert.True(TableObject.ContainsKey("Column2"));
            Assert.Equal("Cell1", ((List<object>?)TableObject["Column1"])?.FirstOrDefault()?.ToString());
            Assert.Equal("Cell2", ((List<object>?)TableObject["Column2"])?.FirstOrDefault()?.ToString());
        }

        [Fact]
        public void Convert_ReturnsStructuredObject_WithCorrectMetadata()
        {
            // Arrange
            var File = new Tables();
            File.Metadata.Add("Key1", "Value1");
            File.Metadata.Add("Key2", "Value2");

            // Act
            StructuredObject? Result = TablesToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(2, Result.Metadata.Count);
            Assert.True(Result.Metadata.ContainsKey("Key1"));
            Assert.True(Result.Metadata.ContainsKey("Key2"));
            Assert.Equal("Value1", Result.Metadata["Key1"]);
            Assert.Equal("Value2", Result.Metadata["Key2"]);
        }

        [Fact]
        public void Convert_ReturnsStructuredObject_WithCorrectTitle()
        {
            // Arrange
            var File = new Tables
            {
                Title = "Test Title"
            };

            // Act
            StructuredObject? Result = TablesToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Test Title", Result.Title);
        }
    }
}