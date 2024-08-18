using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class KeyValueParameterTests : TestBaseClass<KeyValueParameter>
    {
        public KeyValueParameterTests()
        {
            _Name = "TestValue293008716";
            _Value = "TestValue683907540";
            _TestClass = new KeyValueParameter(_Name, _Value);
            TestObject = new KeyValueParameter(_Name, _Value);
        }

        private readonly string _Name;
        private readonly KeyValueParameter _TestClass;
        private readonly string _Value;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new KeyValueParameter("TestValue22992092", "TestValue1579698638");

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.True(Result > 0);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new KeyValueParameter("TestValue1517759331", "TestValue266124303");
            var Right = new KeyValueParameter("TestValue1009878074", "TestValue1348750668");

            // Act
            var Result = Left == Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new KeyValueParameter("TestValue31692556", "TestValue46177473");

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new KeyValueParameter("TestValue31692556", "TestValue46177473") == default;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithCardFieldParameter()
        {
            // Arrange
            var Other = new KeyValueParameter("TestValue951797604", "TestValue1844731192");

            // Act
            var Result = _TestClass.Equals(Other);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithObj()
        {
            // Arrange
            var Obj = new object();

            // Act
            var Result = _TestClass.Equals(Obj);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var Result = _TestClass.GetHashCode();

            // Assert
            Assert.NotEqual(0, Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var Left = new KeyValueParameter("TestValue1923441644", "TestValue1921017293");
            var Right = new KeyValueParameter("TestValue1977938827", "TestValue2014314808");

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = null >= new KeyValueParameter("TestValue31692556", "TestValue46177473");

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new KeyValueParameter("TestValue31692556", "TestValue46177473") >= null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new KeyValueParameter("TestValue398445081", "TestValue235020733");
            var Right = new KeyValueParameter("TestValue1611362084", "TestValue716955240");

            // Act
            var Result = Left > Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = null > new KeyValueParameter("TestValue31692556", "TestValue46177473");

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new KeyValueParameter("TestValue31692556", "TestValue46177473") > null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new KeyValueParameter("TestValue583298174", "TestValue2058693616");
            var Right = new KeyValueParameter("TestValue870096427", "TestValue1327964166");

            // Act
            var Result = Left != Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new KeyValueParameter("TestValue31692556", "TestValue46177473");

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new KeyValueParameter("TestValue31692556", "TestValue46177473") != default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new KeyValueParameter("TestValue2101187830", "TestValue1570687676");
            var Right = new KeyValueParameter("TestValue1656331521", "TestValue279464845");

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = null <= new KeyValueParameter("TestValue31692556", "TestValue46177473");

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new KeyValueParameter("TestValue31692556", "TestValue46177473") <= null;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new KeyValueParameter("TestValue886324691", "TestValue1018763552");
            var Right = new KeyValueParameter("TestValue31082410", "TestValue770128656");

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = null < new KeyValueParameter("TestValue31692556", "TestValue46177473");

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new KeyValueParameter("TestValue31692556", "TestValue46177473") < null;

            // Assert
            Assert.False(Result);
        }

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
            var Instance = new KeyValueParameter(_Name, _Value);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidName(string value) => _ = new KeyValueParameter(value, _Value);

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidValue(string value) => _ = new KeyValueParameter(_Name, value);

        [Fact]
        public void CanSetAndGetName()
        {
            // Arrange
            const string TestValue = "TestValue877727010";

            // Act
            _TestClass.Name = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Name);
        }

        [Fact]
        public void CanSetAndGetValue()
        {
            // Arrange
            const string TestValue = "TestValue1953516492";

            // Act
            _TestClass.Value = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Value);
        }

        [Fact]
        public void ImplementsIComparable_CardFieldParameter()
        {
            // Arrange
            var BaseValue = new KeyValueParameter("TestValue1529159469", "TestValue979813989");
            var EqualToBaseValue = new KeyValueParameter("TestValue1529159469", "TestValue979813989");
            var GreaterThanBaseValue = new KeyValueParameter("TestValue261425000", "TestValue1423806828");

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_CardFieldParameter()
        {
            // Arrange
            var TestClass = new KeyValueParameter("TestValue1529159469", "TestValue979813989");
            var Same = new KeyValueParameter("TestValue1529159469", "TestValue979813989");
            var Different = new KeyValueParameter("TestValue261425000", "TestValue1423806828");

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
        public void NameIsInitializedCorrectly() => Assert.Equal(_Name, new KeyValueParameter(_Name, _Value).Name);

        [Fact]
        public void ValueIsInitializedCorrectly() => Assert.Equal(_Value, new KeyValueParameter(_Name, _Value).Value);
    }
}