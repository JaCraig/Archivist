using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class CardToStructuredObjectConverterTests : TestBaseClass<CardToStructuredObjectConverter>
    {
        public CardToStructuredObjectConverterTests()
        {
            _TestClass = new CardToStructuredObjectConverter();
        }

        private readonly CardToStructuredObjectConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Card);
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallCanConvertWithNullDestination()
        {
            var Result = _TestClass.CanConvert(typeof(string), default);

            Assert.False(Result);
        }

        [Fact]
        public void CanCallConvertWithFile()
        {
            // Arrange
            var File = new Card();

            // Act
            StructuredObject? Result = CardToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallConvertWithObjectAndType()
        {
            // Arrange
            var Source = new object();
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _TestClass.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void CannotCallCanConvertWithNullSource()
        {
            var Result = _TestClass.CanConvert(default, typeof(string));

            Assert.False(Result);
        }

        [Fact]
        public void CannotCallConvertWithObjectAndTypeWithNullDestination()
        {
            var Result = _TestClass.Convert(new object(), default);

            Assert.Null(Result);
        }

        [Fact]
        public void ConvertWithFilePerformsMapping()
        {
            // Arrange
            var File = new Card();

            // Act
            StructuredObject? Result = CardToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(File.Count, Result.Count);
        }
    }
}