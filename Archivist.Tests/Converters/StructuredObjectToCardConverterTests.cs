using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using System.Linq;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class StructuredObjectToCardConverterTests : TestBaseClass<StructuredObjectToCardConverter>
    {
        public StructuredObjectToCardConverterTests()
        {
            _TestClass = new StructuredObjectToCardConverter();
            TestObject = new StructuredObjectToCardConverter();
        }

        private readonly StructuredObjectToCardConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(string);
            Type Destination = typeof(string);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallConvertWithFile()
        {
            // Arrange
            var File = new StructuredObject();
            _ = File.SetValue("N", "V");
            _ = File.SetValue("N2", "V2");

            // Act
            Card? Result = StructuredObjectToCardConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(2, Result.Fields.Count);
            Assert.Equal("V", Result.Name?.Value);
            Assert.Equal("V2", Result["N2"].FirstOrDefault()?.Value);
        }

        [Fact]
        public void CanCallConvertWithObjectAndType()
        {
            // Arrange
            var Source = new object();
            Type Destination = typeof(string);

            // Act
            var Result = _TestClass.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void ConvertWithFilePerformsMapping()
        {
            // Arrange
            var File = new StructuredObject();

            // Act
            Card? Result = StructuredObjectToCardConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(File.Count, Result.Count);
        }
    }
}