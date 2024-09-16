using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class TableToFeedConverterTests : TestBaseClass<TableToFeedConverter>
    {
        public TableToFeedConverterTests()
        {
            _TestClass = new TableToFeedConverter();
            TestObject = new TableToFeedConverter();
        }

        private readonly TableToFeedConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Table);
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
            var File = new Table();

            // Act
            Feed? Result = TableToFeedConverter.Convert(File);

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