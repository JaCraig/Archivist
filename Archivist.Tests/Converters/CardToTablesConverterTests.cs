using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Enums;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class CardToTablesConverterTests : TestBaseClass<CardToTablesConverter>
    {
        public CardToTablesConverterTests()
        {
            _TestClass = new CardToTablesConverter();
            TestObject = new CardToTablesConverter();
        }

        private readonly CardToTablesConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Card);
            Type Destination = typeof(Tables);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallCanConvertWithNullDestination()
        {
            // Act
            var Result = _TestClass.CanConvert(typeof(string), default);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallCanConvertWithNullSource()
        {
            // Act
            var Result = _TestClass.CanConvert(default, typeof(string));

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallConvertWithFile()
        {
            // Arrange
            var File = new Card();

            // Act
            Tables? Result = CardToTablesConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Table Table = Assert.Single(Result);
            Assert.NotNull(Table);
            TableRow Row = Assert.Single(Table);
            Assert.NotNull(Row);
            Assert.Empty(Row);
        }

        [Fact]
        public void CanCallConvertWithObjectAndType()
        {
            // Arrange
            var Source = new object();
            Type Destination = typeof(Tables);

            // Act
            var Result = _TestClass.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void CanCallConvertWithObjectAndTypeWithNullDestination()
        {
            // Act
            var Result = _TestClass.Convert(new object(), default);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void ConvertWithFilePerformsMapping()
        {
            // Arrange
            var File = new Card
            {
                FirstName = "Test",
                LastName = "User",
                Prefix = "Mr",
                Suffix = "Jr",
                Title = "Test User"
            };
            File.Fields.Add(new CardField(CommonCardFields.Title, null, "Test User Title"));

            // Act
            Tables? Result = CardToTablesConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("User;Test;;Mr;Jr", Result[0][0][0].Content);
            Assert.Equal("Test User Title", Result[0][0][1].Content);
            Assert.Equal(File.Title, Result.Title);
        }
    }
}