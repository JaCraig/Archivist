using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class StructuredObjectToFeedConverterTests : TestBaseClass<StructuredObjectToFeedConverter>
    {
        public StructuredObjectToFeedConverterTests()
        {
            _TestClass = new StructuredObjectToFeedConverter();
            TestObject = new StructuredObjectToFeedConverter();
        }

        private readonly StructuredObjectToFeedConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(StructuredObject);
            Type Destination = typeof(Feed);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallConvertWithFile()
        {
            // Arrange
            var File = new StructuredObject();

            // Act
            Feed? Result = StructuredObjectToFeedConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
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
    }
}