using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class CardToTableConverterTests : TestBaseClass<CardToTableConverter>
    {
        public CardToTableConverterTests()
        {
            _TestClass = new CardToTableConverter();
            TestObject = new CardToTableConverter();
        }

        private readonly CardToTableConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Card);
            Type Destination = typeof(Table);

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
        public void CanCallCanConvertWithNullSource()
        {
            var Result = _TestClass.CanConvert(default, typeof(string));

            Assert.False(Result);
        }

        [Fact]
        public void CanCallConvertWithFile()
        {
            // Arrange
            var File = new Card();

            // Act
            Table? Result = CardToTableConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Columns);
            Assert.Equal(",", Result.Metadata["Delimiter"]);
            _ = Assert.Single(Result);
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
        public void CanCallConvertWithObjectAndTypeWithNullDestination()
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
            Table? Result = CardToTableConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            _ = Assert.Single(Result);
            Assert.Empty(Result.Columns);
            Assert.Equal(",", Result.Metadata["Delimiter"]);
        }
    }
}