using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class TableToStructuredObjectConverterTests : TestBaseClass<TableToStructuredObjectConverter>
    {
        public TableToStructuredObjectConverterTests()
        {
            _Converter = new TableToStructuredObjectConverter();
            TestObject = new TableToStructuredObjectConverter();
        }

        private readonly TableToStructuredObjectConverter _Converter;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Table);
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallCanConvertWithNullDestination()
        {
            // Arrange
            Type Source = typeof(Table);
            Type? Destination = null;

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallCanConvertWithNullSource()
        {
            // Arrange
            Type? Source = null;
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenDestinationIsNull()
        {
            // Arrange
            var Source = new Table();
            Type? Destination = null;

            // Act
            var Result = _Converter.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenFileIsNull()
        {
            // Arrange
            Table? File = null;

            // Act
            StructuredObject? Result = TableToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenSourceIsNotTable()
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
            var File = new Table();
            File.Columns.AddRange(new List<string> { "Column1", "Column2" });
            File.AddRow().AddRange(new string[] { "Value1", "Value2" });
            File.AddRow().AddRange(new string[] { "Value3", "Value4" });

            // Act
            StructuredObject? Result = TableToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            List<string>? ResultObject = Result.GetValue<List<string>>("Column1");
            Assert.NotNull(ResultObject);
            Assert.Equal("Value1", ResultObject[0]);
            Assert.Equal("Value3", ResultObject[1]);
            ResultObject = Result.GetValue<List<string>>("Column2");
            Assert.NotNull(ResultObject);
            Assert.Equal("Value2", ResultObject[0]);
            Assert.Equal("Value4", ResultObject[1]);
        }

        [Fact]
        public void Convert_ReturnsStructuredObject_WithCorrectMetadata()
        {
            // Arrange
            var File = new Table();
            File.Metadata.Add("Key1", "Value1");
            File.Metadata.Add("Key2", "Value2");

            // Act
            StructuredObject? Result = TableToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Value1", Result.Metadata["Key1"]);
            Assert.Equal("Value2", Result.Metadata["Key2"]);
        }

        [Fact]
        public void Convert_ReturnsStructuredObject_WithCorrectTitle()
        {
            // Arrange
            var File = new Table
            {
                Title = "Test Title"
            };

            // Act
            StructuredObject? Result = TableToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Test Title", Result.Title);
        }
    }
}