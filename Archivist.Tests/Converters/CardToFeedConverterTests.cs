using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class CardToFeedConverterTests : TestBaseClass<CardToFeedConverter>
    {
        public CardToFeedConverterTests()
        {
            _TestClass = new CardToFeedConverter();
            TestObject = new CardToFeedConverter();
        }

        private readonly CardToFeedConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Card);
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
            var File = new Card();

            // Act
            Feed? Result = CardToFeedConverter.Convert(File);

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