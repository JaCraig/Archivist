using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class AnythingToTextConverterTests : TestBaseClass<AnythingToTextConverter>
    {
        public AnythingToTextConverterTests()
        {
            _TestClass = new AnythingToTextConverter();
        }

        private readonly AnythingToTextConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(IGenericFile);
            Type Destination = typeof(Text);

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
            IGenericFile File = Substitute.For<IGenericFile>();

            // Act
            Text? Result = AnythingToTextConverter.Convert(File);

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

        [Fact]
        public void CanCallConvertWithObjectAndTypeWithNullDestination()
        {
            var Result = _TestClass.Convert(new object(), default);

            Assert.Null(Result);
        }
    }
}