using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class CardFieldParameterTests : TestBaseClass<CardFieldParameter>
    {
        public CardFieldParameterTests()
        {
            _Name = "TestValue293008716";
            _Value = "TestValue683907540";
            _TestClass = new CardFieldParameter(_Name, _Value);
        }

        private readonly string _Name;
        private readonly CardFieldParameter _TestClass;
        private readonly string _Value;

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Results = _TestClass.ToString();

            // Assert
            Assert.Equal($"{_Name}={_Value}", Results);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new CardFieldParameter(_Name, _Value);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidName(string value) => new CardFieldParameter(value, _Value);

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidValue(string value) => new CardFieldParameter(_Name, value);

        [Fact]
        public void CanSetAndGetName()
        {
            // Arrange
            var TestValue = "TestValue877727010";

            // Act
            _TestClass.Name = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Name);
        }

        [Fact]
        public void CanSetAndGetValue()
        {
            // Arrange
            var TestValue = "TestValue1953516492";

            // Act
            _TestClass.Value = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Value);
        }

        [Fact]
        public void NameIsInitializedCorrectly() => Assert.Equal(_Name, new CardFieldParameter(_Name, _Value).Name);

        [Fact]
        public void ValueIsInitializedCorrectly() => Assert.Equal(_Value, new CardFieldParameter(_Name, _Value).Value);
    }
}