using Archivist.DataTypes.Feeds;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.DataTypes.Feeds
{
    public class FeedGuidTests : TestBaseClass<FeedGuid>
    {
        public FeedGuidTests()
        {
            _GuidText = "TestValue574952907";
            _IsPermaLink = true;
            _TestClass = new FeedGuid(_GuidText, _IsPermaLink);
            TestObject = new FeedGuid();
        }

        private readonly string _GuidText;
        private readonly bool _IsPermaLink;
        private readonly FeedGuid _TestClass;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new FeedGuid();

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.NotEqual(0, Result);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new FeedGuid();
            var Right = new FeedGuid();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new FeedGuid();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new FeedGuid() == default;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithFeedGuid()
        {
            // Arrange
            var TestClass = new FeedGuid();
            var Other = new FeedGuid();

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
            var Left = new FeedGuid();
            var Right = new FeedGuid();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(FeedGuid) >= new FeedGuid();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new FeedGuid() >= default(FeedGuid);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new FeedGuid();
            var Right = new FeedGuid();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(FeedGuid) > new FeedGuid();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new FeedGuid() > default(FeedGuid);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new FeedGuid();
            var Right = new FeedGuid();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new FeedGuid();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new FeedGuid() != default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new FeedGuid();
            var Right = new FeedGuid();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(FeedGuid) <= new FeedGuid();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new FeedGuid() <= default(FeedGuid);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new FeedGuid();
            var Right = new FeedGuid();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(FeedGuid) < new FeedGuid();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new FeedGuid() < default(FeedGuid);

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
            Assert.Contains(_GuidText, Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new FeedGuid();

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new FeedGuid(_GuidText, _IsPermaLink);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidGuidText(string value)
            => _ = new FeedGuid(value, _IsPermaLink);

        [Fact]
        public void CanSetAndGetGuidText()
        {
            // Arrange
            const string TestValue = "TestValue1553255570";
            var TestObject = new FeedGuid
            {
                // Act
                GuidText = TestValue
            };

            // Assert
            Assert.Equal(TestValue, TestObject.GuidText);
        }

        [Fact]
        public void CanSetAndGetIsPermaLink()
        {
            // Arrange
            const bool TestValue = false;
            var TestObject = new FeedGuid
            {
                // Act
                IsPermaLink = TestValue
            };

            // Assert
            Assert.Equal(TestValue, TestObject.IsPermaLink);
        }

        [Fact]
        public void GuidTextIsInitializedCorrectly() => Assert.Equal(_GuidText, _TestClass.GuidText);

        [Fact]
        public void ImplementsIComparable_FeedGuid()
        {
            // Arrange
            var BaseValue = new FeedGuid("TestValue-1680739824", false);
            var EqualToBaseValue = new FeedGuid("TestValue-1680739824", false);
            var GreaterThanBaseValue = new FeedGuid("TestValue-1680739824", true);

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_FeedGuid()
        {
            // Arrange
            var TestObject = new FeedGuid("TestValue-1680739824", false);
            var Same = new FeedGuid("TestValue-1680739824", false);
            var Different = new FeedGuid("TestValue-1680739824", true);

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
        public void IsPermaLinkIsInitializedCorrectly() => Assert.Equal(_IsPermaLink, _TestClass.IsPermaLink);
    }
}