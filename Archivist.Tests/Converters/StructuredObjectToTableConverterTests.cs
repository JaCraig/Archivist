using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class StructuredObjectToTableConverterTests : TestBaseClass<StructuredObjectToTableConverter>
    {
        public StructuredObjectToTableConverterTests()
        {
            _Converter = new StructuredObjectToTableConverter();
            TestObject = new StructuredObjectToTableConverter();
        }

        private readonly StructuredObjectToTableConverter _Converter;

        [Fact]
        public void CanConvert_SourceAndDestinationTypesDoNotMatch_ReturnsFalse()
        {
            // Arrange
            Type Source = typeof(StructuredObject);
            Type Destination = typeof(string);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConvert_SourceAndDestinationTypesMatch_ReturnsTrue()
        {
            // Arrange
            Type Source = typeof(StructuredObject);
            Type Destination = typeof(Table);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void Convert_NullStructuredObject_ReturnsNullTable()
        {
            // Arrange
            StructuredObject? File = null;

            // Act
            Table? Result = StructuredObjectToTableConverter.Convert(File);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ObjectAndDestinationType_ReturnsConvertedObject()
        {
            // Arrange
            object Source = new StructuredObject();
            Type Destination = typeof(Table);

            // Act
            var Result = _Converter.Convert(Source, Destination);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Table>(Result);
        }

        [Fact]
        public void Convert_ObjectAndNullDestinationType_ReturnsNull()
        {
            // Arrange
            object Source = new StructuredObject();
            Type? Destination = null;

            // Act
            var Result = _Converter.Convert(Source, Destination!);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_StructuredObject_ReturnsConvertedTable()
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
            Table? Result = StructuredObjectToTableConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(File.Title, Result.Title);
            Assert.Equal(File.Keys.Count, Result.Columns.Count);
            Assert.Equal(2, Result.Metadata.Count);
            Assert.Equal(File["Key1"]?.ToString(), Result[0][0].Content);
            Assert.Equal(File["Key2"]?.ToString(), Result[0][1].Content);
            Assert.Equal(File.Metadata["MetadataKey"], Result.Metadata["MetadataKey"]);
        }
    }
}