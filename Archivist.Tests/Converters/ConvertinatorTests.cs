using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class ConvertinatorTests : TestBaseClass<Convertinator>
    {
        public ConvertinatorTests()
        {
            _Converters = new[] { Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>() };
            _TestClass = new Convertinator(_Converters);
            TestObject = new Convertinator(_Converters);
        }

        private readonly IEnumerable<IDataConverter> _Converters;
        private readonly Convertinator _TestClass;

        [Fact]
        public void CanCallConvert_WithInvalidSourceAndDestination_ReturnsEmptyValue()
        {
            // Arrange
            var Source = new StructuredObject();
            Type Destination = typeof(Text);
            var TempConverter = new Convertinator(new[] { new AnythingToTextConverter() });

            // Act
            var Result = TempConverter.Convert(Source, Destination) as Text;

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("{}", Result.Content);
        }

        [Fact]
        public void CanCallConvert_WithNullDestination_ReturnsNull()
        {
            // Arrange
            var Source = new StructuredObject();

            // Act
            var Result = _TestClass.Convert(Source, null);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void CanCallConvert_WithNullSource_ReturnsNull()
        {
            // Arrange
            Type Destination = typeof(Text);

            // Act
            var Result = _TestClass.Convert(null, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void CanCallConvert_WithValidSourceAndDestination_ReturnsConvertedObject()
        {
            // Arrange
            var Source = new StructuredObject();
            Type Destination = typeof(Text);
            var Expected = new Text("{}", "");
            var Converter = new Convertinator(new[] { new AnythingToTextConverter() });

            // Act
            var Result = Converter.Convert(Source, Destination);

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void CanConstruct_WithConverters_ReturnsInstance()
        {
            // Arrange
            IDataConverter[] Converters = new[] { Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>() };

            // Act
            var Instance = new Convertinator(Converters);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstruct_WithNullConverters_ReturnsInstance()
        {
            // Act
            var Instance = new Convertinator(null);

            // Assert
            Assert.NotNull(Instance);
        }
    }
}