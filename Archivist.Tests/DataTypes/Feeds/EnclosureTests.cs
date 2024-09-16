using Archivist.DataTypes.Feeds;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.DataTypes.Feeds
{
    public class EnclosureTests : TestBaseClass<Enclosure>
    {
        public EnclosureTests()
        {
            _Type = "TestValue574952907";
            _Url = "TestValue1281441158";
            _Length = 1553255570;
            _TestClass = new Enclosure(_Type, _Url, _Length);
            TestObject = new Enclosure();
        }

        private readonly int _Length;
        private readonly Enclosure _TestClass;
        private readonly string _Type;
        private readonly string _Url;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new Enclosure();

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.NotEqual(0, Result);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new Enclosure();
            var Right = new Enclosure();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new Enclosure();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new Enclosure() == default;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithEnclosure()
        {
            // Arrange
            var TestClass = new Enclosure();
            var Other = new Enclosure();

            // Act
            var Result = TestClass.Equals(Other);

            // Assert
            Assert.True(Result);
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
            var Left = new Enclosure();
            var Right = new Enclosure();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(Enclosure) >= new Enclosure();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new Enclosure() >= default(Enclosure);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new Enclosure();
            var Right = new Enclosure();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(Enclosure) > new Enclosure();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new Enclosure() > default(Enclosure);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new Enclosure();
            var Right = new Enclosure();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new Enclosure();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new Enclosure() != default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new Enclosure();
            var Right = new Enclosure();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(Enclosure) <= new Enclosure();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new Enclosure() <= default(Enclosure);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new Enclosure();
            var Right = new Enclosure();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(Enclosure) < new Enclosure();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new Enclosure() < default(Enclosure);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(Result));
            Assert.Contains(_Type, Result);
            Assert.Contains(_Url, Result);
            Assert.Contains(_Length.ToString(), Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new Enclosure();

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new Enclosure(_Type, _Url, _Length);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidUrl(string value)
            => _ = new Enclosure(_Type, value, _Length);

        [Fact]
        public void CanSetAndGetLength()
        {
            // Arrange
            const int TestValue = 1553255570;
            var TestObject = new Enclosure
            {
                // Act
                Length = TestValue
            };

            // Assert
            Assert.Equal(TestValue, TestObject.Length);
        }

        [Fact]
        public void CanSetAndGetType()
        {
            // Arrange
            const string TestValue = "TestValue1553255570";
            var TestObject = new Enclosure
            {
                // Act
                Type = TestValue
            };

            // Assert
            Assert.Equal(TestValue, TestObject.Type);
        }

        [Fact]
        public void CanSetAndGetUrl()
        {
            // Arrange
            const string TestValue = "TestValue1553255570";
            var TestObject = new Enclosure
            {
                // Act
                Url = TestValue
            };

            // Assert
            Assert.Equal(TestValue, TestObject.Url);
        }

        [Fact]
        public void ImplementsIComparable_Enclosure()
        {
            // Arrange
            var BaseValue = new Enclosure("TestValue-1680739824", "TestValue-1680739824", 1553255570);
            var EqualToBaseValue = new Enclosure("TestValue-1680739824", "TestValue-1680739824", 1553255570);
            var GreaterThanBaseValue = new Enclosure("TestValue-1680739824", "TestValue-1680739824", 1553255571);

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_Enclosure()
        {
            // Arrange
            var TestObject = new Enclosure("TestValue-1680739824", "TestValue-1680739824", 1553255570);
            var Same = new Enclosure("TestValue-1680739824", "TestValue-1680739824", 1553255570);
            var Different = new Enclosure("TestValue-1680739824", "TestValue-1680739824", 1553255571);

            // Assert
            Assert.False(TestObject.Equals(default(object)));
            Assert.False(TestObject.Equals(new object()));
            Assert.True(TestObject.Equals((object)Same));
            Assert.False(TestObject.Equals((object)Different));
            Assert.True(TestObject.Equals(Same));
            Assert.False(TestObject.Equals(Different));
            Assert.Equal(Same.GetHashCode(), TestObject.GetHashCode());
            Assert.NotEqual(Different.GetHashCode(), TestObject.GetHashCode());
            Assert.True(TestObject == Same);
            Assert.False(TestObject == Different);
            Assert.False(TestObject != Same);
            Assert.True(TestObject != Different);
        }

        [Fact]
        public void LengthIsInitializedCorrectly()
        {
            // Arrange
            var TestClass = new Enclosure(_Type, _Url, _Length);

            // Assert
            Assert.Equal(_Length, TestClass.Length);
        }

        [Fact]
        public void TypeIsInitializedCorrectly()
        {
            // Arrange
            var TestClass = new Enclosure(_Type, _Url, _Length);

            // Assert
            Assert.Equal(_Type, TestClass.Type);
        }

        [Fact]
        public void UrlIsInitializedCorrectly()
        {
            // Arrange
            var TestClass = new Enclosure(_Type, _Url, _Length);

            // Assert
            Assert.Equal(_Url, TestClass.Url);
        }
    }
}