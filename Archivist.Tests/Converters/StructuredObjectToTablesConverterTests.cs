using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class StructuredObjectToTablesConverterTests : TestBaseClass<StructuredObjectToTablesConverter>
    {
        public StructuredObjectToTablesConverterTests()
        {
            _TestClass = new StructuredObjectToTablesConverter();
            TestObject = new StructuredObjectToTablesConverter();
        }

        private readonly StructuredObjectToTablesConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(StructuredObject);
            Type Destination = typeof(Tables);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallCanConvertWithInvalidDestination()
        {
            // Arrange
            Type Source = typeof(StructuredObject);
            Type Destination = typeof(string);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallCanConvertWithInvalidSource()
        {
            // Arrange
            Type Source = typeof(string);
            Type Destination = typeof(Tables);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenFileIsNull()
        {
            // Arrange
            StructuredObject? File = null;

            // Act
            Tables? Result = StructuredObjectToTablesConverter.Convert(File);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenSourceIsNotStructuredObject()
        {
            // Arrange
            object Source = "Test";
            Type Destination = typeof(Tables);

            // Act
            var Result = _TestClass.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenSourceIsNull()
        {
            // Arrange
            object? Source = null;
            Type Destination = typeof(Tables);

            // Act
            var Result = _TestClass.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ReturnsTablesObject_WhenFileIsNotNull()
        {
            // Arrange
            var File = new StructuredObject
            {
                Title = "Test Title"
            };
            File.Add("Key1", "Value1");
            File.Add("Key2", "Value2");
            File.Metadata.Add("MetadataKey", "MetadataValue");

            // Act
            Tables? Result = StructuredObjectToTablesConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Test Title", Result.Title);
            _ = Assert.Single(Result);
            Assert.Equal(2, Result[0].Columns.Count);
            _ = Assert.Single(Result[0]);
            Assert.Equal("Key1", Result[0].Columns[0]);
            Assert.Equal("Key2", Result[0].Columns[1]);
            Assert.Equal("Value1", Result[0][0][0].Content);
            Assert.Equal("Value2", Result[0][0][1].Content);
            Assert.Equal("MetadataValue", Result.Metadata["MetadataKey"]);
        }
    }
}