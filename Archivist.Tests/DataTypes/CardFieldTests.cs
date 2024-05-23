using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class CardFieldTests : TestBaseClass<CardField>
    {
        public CardFieldTests()
        {
            _Property = "TestValue1888918131";
            _Parameters = new[] { new CardFieldParameter("TestValue685308228", "TestValue180072147"), new CardFieldParameter("TestValue90202416", "TestValue350054617"), new CardFieldParameter("TestValue2036030522", "TestValue437133569") };
            _Value = "TestValue1524892779";
            _TestClass = new CardField(_Property, _Parameters, _Value);
            TestObject = new CardField(_Property, _Parameters, _Value);
        }

        private readonly IEnumerable<CardFieldParameter> _Parameters;
        private readonly string _Property;
        private readonly CardField _TestClass;
        private readonly string _Value;

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Results = _TestClass.ToString();

            // Assert
            Assert.Equal($"{_Property} ({string.Join(';', _Parameters)}): {_Value}", Results);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new CardField(_Property, _Parameters, _Value);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidProperty(string? value) => _ = new CardField(value, _Parameters, _Value);

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidValue(string value) => _ = new CardField(_Property, _Parameters, value);

        [Fact]
        public void CanSetAndGetProperty()
        {
            // Arrange
            const string TestValue = "TestValue1187036884";

            // Act
            _TestClass.Property = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Property);
        }

        [Fact]
        public void CanSetAndGetValue()
        {
            // Arrange
            const string TestValue = "TestValue1517126714";

            // Act
            _TestClass.Value = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Value);
        }

        [Fact]
        public void ParametersIsInitializedCorrectly() => Assert.Equal(_Parameters, new CardField(_Property, _Parameters, _Value).Parameters);

        [Fact]
        public void PropertyIsInitializedCorrectly() => Assert.Equal(_Property, new CardField(_Property, _Parameters, _Value).Property);

        [Fact]
        public void ValueIsInitializedCorrectly() => Assert.Equal(_Value, new CardField(_Property, _Parameters, _Value).Value);
    }
}