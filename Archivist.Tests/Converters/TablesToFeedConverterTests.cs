using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class TablesToFeedConverterTests : TestBaseClass<TablesToFeedConverter>
    {
        public TablesToFeedConverterTests()
        {
            _TestClass = new TablesToFeedConverter();
            TestObject = new TablesToFeedConverter();
        }

        private readonly TablesToFeedConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Tables);
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
            var File = new Tables() { new Table() };

            // Act
            Feed? Result = TablesToFeedConverter.Convert(File);

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