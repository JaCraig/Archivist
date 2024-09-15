using Archivist.DataTypes.Feeds;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.DataTypes.Feeds
{
    public class ThumbnailTests : TestBaseClass<Thumbnail>
    {
        public ThumbnailTests()
        {
            _Url = "TestValue1281441158";
            _Height = 1969776431;
            _Width = 503444404;
            _TestClass = new Thumbnail(_Url, _Height, _Width);
            TestObject = new Thumbnail();
        }

        private readonly int _Height;
        private readonly Thumbnail _TestClass;
        private readonly string _Url;
        private readonly int _Width;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new Thumbnail();
            var TestObject = new Thumbnail();

            // Act
            var Result = TestObject.CompareTo(Other);

            // Assert
            Assert.True(Result < 0);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new Thumbnail();
            var Right = new Thumbnail();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft() =>
            // Assert
            Assert.False(default == new Thumbnail());

        [Fact]
        public void CanCallEqualityOperatorWithNullRight() =>
            // Assert
            Assert.False(new Thumbnail() == default);

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
        public void CanCallEqualsWithThumbnail()
        {
            // Arrange
            var Other = new Thumbnail();

            // Act
            var Result = _TestClass.Equals(Other);

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
            var Left = new Thumbnail();
            var Right = new Thumbnail();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft() =>
            // Assert
            Assert.False(default(Thumbnail) >= new Thumbnail());

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight() =>
            // Assert
            Assert.False(new Thumbnail() >= default(Thumbnail));

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new Thumbnail();
            var Right = new Thumbnail();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft() =>
            // Assert
            Assert.False(default(Thumbnail) > new Thumbnail());

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight() =>
            // Assert
            Assert.False(new Thumbnail() > default(Thumbnail));

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new Thumbnail();
            var Right = new Thumbnail();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft() =>
            // Assert
            Assert.False(default == new Thumbnail());

        [Fact]
        public void CanCallInequalityOperatorWithNullRight() =>
            // Assert
            Assert.False(new Thumbnail() == default);

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new Thumbnail();
            var Right = new Thumbnail();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft() =>
            // Assert
            Assert.False(default(Thumbnail) <= new Thumbnail());

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight() =>
            // Assert
            Assert.False(new Thumbnail() <= default(Thumbnail));

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new Thumbnail();
            var Right = new Thumbnail();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft() =>
            // Assert
            Assert.False(default(Thumbnail) < new Thumbnail());

        [Fact]
        public void CanCallLessThanOperatorWithNullRight() =>
            // Assert
            Assert.False(new Thumbnail() < default(Thumbnail));

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            Assert.NotEmpty(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new Thumbnail();

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new Thumbnail(_Url, _Height, _Width);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidUrl(string value)
        {
            // Act
            var Instance = new Thumbnail(value, _Height, _Width);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanSetAndGetHeight()
        {
            // Arrange
            const int TestValue = 908466582;
            var TestObj = new Thumbnail
            {
                // Act
                Height = TestValue
            };

            // Assert
            Assert.Equal(TestValue, TestObj.Height);
        }

        [Fact]
        public void CanSetAndGetUrl()
        {
            // Arrange
            const string TestValue = "TestValue1425936163";
            var TestObj = new Thumbnail
            {
                // Act
                Url = _Url
            };

            // Assert
            Assert.Equal(TestValue, TestObj.Url);
        }

        [Fact]
        public void CanSetAndGetWidth()
        {
            // Arrange
            const int TestValue = 1499349718;
            var TestObject = new Thumbnail
            {
                // Act
                Width = _Width
            };

            // Act
            TestObject.Width = TestValue;

            // Assert
            Assert.Equal(TestValue, TestObject.Width);
        }

        [Fact]
        public void HeightIsInitializedCorrectly() => Assert.Equal(_Height, _TestClass.Height);

        [Fact]
        public void ImplementsIComparable_Thumbnail()
        {
            // Arrange
            var BaseValue = new Thumbnail(_Url, _Height, _Width);
            var EqualToBaseValue = new Thumbnail(_Url, _Height, _Width);
            var GreaterThanBaseValue = new Thumbnail("TestValue-Url-1", 1, 1);

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_Thumbnail()
        {
            // Arrange
            var TestObject = new Thumbnail(_Url, _Height, _Width);
            var Same = new Thumbnail(_Url, _Height, _Width);
            var Different = new Thumbnail("TestValue-Url-1", 1, 1);

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
        public void UrlIsInitializedCorrectly() => Assert.Equal(_Url, _TestClass.Url);

        [Fact]
        public void WidthIsInitializedCorrectly() => Assert.Equal(_Width, _TestClass.Width);
    }
}