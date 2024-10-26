using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class ImageToAnythingConverterTests : TestBaseClass<ImageToAnythingConverter>
    {
        public ImageToAnythingConverterTests()
        {
            _TestClass = new ImageToAnythingConverter();
            TestObject = new ImageToAnythingConverter();
        }

        private readonly ImageToAnythingConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Image);
            Type Destination = typeof(Text);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallConvertWithFileAndDestination()
        {
            // Arrange
            var File = new Image();
            Type Destination = typeof(Calendar);

            // Act
            var Result = _TestClass.Convert(File, Destination);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Calendar>(Result);
        }

        [Fact]
        public void CanCallConvertWithObjectAndType()
        {
            // Arrange
            var Source = new Image();
            Type Destination = typeof(Table);

            // Act
            var Result = _TestClass.Convert(Source, Destination);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.IsType<Table>(Result);
        }
    }
}