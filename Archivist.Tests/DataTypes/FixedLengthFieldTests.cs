using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class FixedLengthFieldTests : TestBaseClass<FixedLengthField>
    {
        public FixedLengthFieldTests()
        {
            _Value = "TestValue667958263";
            _MaxLength = 17;
            _FillerCharacter = 'u';
            _LeftAligned = false;
            _TestClass = new FixedLengthField(_Value, _MaxLength, _FillerCharacter, _LeftAligned);
            TestObject = new FixedLengthField(_Value, _MaxLength, _FillerCharacter, _LeftAligned);
        }

        private readonly char _FillerCharacter;
        private readonly bool _LeftAligned;
        private readonly int _MaxLength;
        private readonly FixedLengthField _TestClass;
        private readonly string _Value;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new FixedLengthField("TestValue1235164331", 71, 'G', true);

            // Act
            var Results = _TestClass.CompareTo(Other);

            // Assert
            Assert.False(Results < 0);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new FixedLengthField("TestValue2076567604", 11, 'y', false);
            var Right = new FixedLengthField("TestValue1729877784", 19, 'D', true);

            // Act
            var Results = Left == Right;

            // Assert
            Assert.False(Results);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new FixedLengthField("TestValue2117395890", 220414409, 'F', false);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthField("TestValue2117395890", 220414409, 'F', false) == default;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithFixedLengthField()
        {
            // Arrange
            var Other = new FixedLengthField("TestValue2006938234", 10, 'C', true);

            // Act
            var Results = _TestClass.Equals(Other);

            // Assert
            Assert.False(Results);
        }

        [Fact]
        public void CanCallEqualsWithObj()
        {
            // Arrange
            var Obj = new object();

            // Act
            var Results = _TestClass.Equals(Obj);

            // Assert
            Assert.False(Results);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var Results = _TestClass.GetHashCode();

            // Assert
            Assert.NotEqual(0, Results);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var Left = new FixedLengthField("TestValue1997389858", 92, 'K', false);
            var Right = new FixedLengthField("TestValue1060731467", 18, 'q', false);

            // Act
            var Results = Left >= Right;

            // Assert
            Assert.False(Results);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(FixedLengthField?) >= new FixedLengthField("TestValue2117395890", 220414409, 'F', false);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthField("TestValue2117395890", 220414409, 'F', false) >= default(FixedLengthField?);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new FixedLengthField("TestValue1187217037", 20, 'w', false);
            var Right = new FixedLengthField("TestValue1230855383", 65, 'Q', true);

            // Act
            var Results = Left > Right;

            // Assert
            Assert.True(Results);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(FixedLengthField?) > new FixedLengthField("TestValue2117395890", 220414409, 'F', false);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthField("TestValue2117395890", 220414409, 'F', false) > default(FixedLengthField?);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new FixedLengthField("TestValue519060352", 10, 'V', false);
            var Right = new FixedLengthField("TestValue955543316", 42, 'M', false);

            // Act
            var Results = Left != Right;

            // Assert
            Assert.True(Results);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new FixedLengthField("TestValue2117395890", 220414409, 'F', false);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthField("TestValue2117395890", 220414409, 'F', false) != default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new FixedLengthField("TestValue1614512103", 12, 'G', false);
            var Right = new FixedLengthField("TestValue1848394499", 98, 'F', true);

            // Act
            var Results = Left <= Right;

            // Assert
            Assert.True(Results);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(FixedLengthField?) <= new FixedLengthField("TestValue2117395890", 220414409, 'F', false);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthField("TestValue2117395890", 220414409, 'F', false) <= default(FixedLengthField?);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new FixedLengthField("TestValue1515964543", 18, 'O', true);
            var Right = new FixedLengthField("TestValue23053656", 10, 'n', true);

            // Act
            var Results = Left < Right;

            // Assert
            Assert.True(Results);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(FixedLengthField?) < new FixedLengthField("TestValue2117395890", 220414409, 'F', false);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthField("TestValue2117395890", 220414409, 'F', false) < default(FixedLengthField?);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            Assert.Equal(_Value.Left(_MaxLength), Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new FixedLengthField(_Value, _MaxLength, _FillerCharacter, _LeftAligned);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidValue(string? value) => _ = new FixedLengthField(value, _MaxLength, _FillerCharacter, _LeftAligned);

        [Fact]
        public void CanSetAndGetValue()
        {
            // Arrange
            const string TestValue = "TestValue1363269916";

            // Act
            _TestClass.Value = TestValue;

            // Assert
            Assert.Equal(TestValue.Left(_MaxLength), _TestClass.Value);
        }

        [Fact]
        public void FillerCharacterIsInitializedCorrectly() => Assert.Equal(_FillerCharacter, _TestClass.FillerCharacter);

        [Fact]
        public void ImplementsIComparable_FixedLengthField()
        {
            // Arrange
            var BaseValue = new FixedLengthField("TestValue2038768348", 48, 'E', false);
            var EqualToBaseValue = new FixedLengthField("TestValue2038768348", 48, 'E', false);
            var GreaterThanBaseValue = new FixedLengthField("TestValue2038768348", 49, 'E', false);

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.False(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.False(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_FixedLengthField()
        {
            // Arrange
            var TestClass = new FixedLengthField("TestValue2038768348", 48, 'E', false);
            var Same = new FixedLengthField("TestValue2038768348", 48, 'E', false);
            var Different = new FixedLengthField("TestValue1512931095", 10, 'g', false);

            // Assert
            Assert.False(TestClass.Equals(default(object)));
            Assert.False(TestClass.Equals(new object()));
            Assert.True(TestClass.Equals((object)Same));
            Assert.False(TestClass.Equals((object)Different));
            Assert.True(TestClass.Equals(Same));
            Assert.False(TestClass.Equals(Different));
            Assert.Equal(Same.GetHashCode(), TestClass.GetHashCode());
            Assert.NotEqual(Different.GetHashCode(), TestClass.GetHashCode());
            Assert.True(TestClass == Same);
            Assert.False(TestClass == Different);
            Assert.False(TestClass != Same);
            Assert.True(TestClass != Different);
        }

        [Fact]
        public void LeftAlignedIsInitializedCorrectly() => Assert.Equal(_LeftAligned, _TestClass.LeftAligned);

        [Fact]
        public void MaxLengthIsInitializedCorrectly() => Assert.Equal(_MaxLength, _TestClass.MaxLength);

        [Fact]
        public void ValueIsInitializedCorrectly() => Assert.Equal(_Value.Left(_MaxLength), new FixedLengthField(_Value, _MaxLength, _FillerCharacter, _LeftAligned).Value);
    }
}