using Archivist.DataTypes.Feeds;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes.Feeds
{
    public class FeedItemTests : TestBaseClass<FeedItem>
    {
        public FeedItemTests()
        {
            _TestClass = new FeedItem();
            TestObject = new FeedItem();
        }

        private readonly FeedItem _TestClass;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            DateTime PubDate = DateTime.UtcNow;
            var TestClass = new FeedItem() { PubDateUtc = PubDate };
            var Other = new FeedItem() { PubDateUtc = PubDate };

            // Act
            var Result = TestClass.CompareTo(Other);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new FeedItem() { PubDateUtc = DateTime.Today };
            var Right = new FeedItem() { PubDateUtc = DateTime.Today };

            // Act
            var Result = Left == Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new FeedItem();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new FeedItem() == default;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithFeedItem()
        {
            // Arrange
            var Other = new FeedItem();

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
            DateTime PubDate = DateTime.UtcNow;
            var Left = new FeedItem() { PubDateUtc = PubDate };
            var Right = new FeedItem() { PubDateUtc = PubDate };

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(FeedItem) >= new FeedItem();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new FeedItem() >= default(FeedItem);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            DateTime PubDate = DateTime.UtcNow;
            var Left = new FeedItem() { PubDateUtc = PubDate };
            var Right = new FeedItem() { PubDateUtc = PubDate };

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(FeedItem) > new FeedItem();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new FeedItem() > default(FeedItem);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new FeedItem() { PubDateUtc = DateTime.Today };
            var Right = new FeedItem() { PubDateUtc = DateTime.Today };

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new FeedItem();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new FeedItem() != default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new FeedItem() { PubDateUtc = DateTime.Today };
            var Right = new FeedItem() { PubDateUtc = DateTime.Today };

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(FeedItem) <= new FeedItem();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new FeedItem() <= default(FeedItem);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new FeedItem() { PubDateUtc = DateTime.Today };
            var Right = new FeedItem() { PubDateUtc = DateTime.Today };

            // Act
            var Result = Left < Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(FeedItem) < new FeedItem();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new FeedItem() < default(FeedItem);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetCategories()
        {
            // Assert
            List<string> Result = Assert.IsType<List<string>>(_TestClass.Categories);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanSetAndGetAuthor()
        {
            // Arrange
            const string TestValue = "TestValue1119964353";

            // Act
            _TestClass.Author = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Author);
        }

        [Fact]
        public void CanSetAndGetDescription()
        {
            // Arrange
            const string TestValue = "TestValue1237641573";

            // Act
            _TestClass.Description = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Description);
        }

        [Fact]
        public void CanSetAndGetEnclosure()
        {
            // Arrange
            var TestValue = new Enclosure();

            // Act
            _TestClass.Enclosure = TestValue;

            // Assert
            Assert.Same(TestValue, _TestClass.Enclosure);
        }

        [Fact]
        public void CanSetAndGetGUID()
        {
            // Arrange
            var TestValue = new FeedGuid();

            // Act
            _TestClass.GUID = TestValue;

            // Assert
            Assert.Same(TestValue, _TestClass.GUID);
        }

        [Fact]
        public void CanSetAndGetLink()
        {
            // Arrange
            const string TestValue = "TestValue2117153398";

            // Act
            _TestClass.Link = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Link);
        }

        [Fact]
        public void CanSetAndGetPubDate()
        {
            // Arrange
            DateTime TestValue = DateTime.UtcNow;

            // Act
            _TestClass.PubDateUtc = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.PubDateUtc);
        }

        [Fact]
        public void CanSetAndGetThumbnail()
        {
            // Arrange
            var TestValue = new Thumbnail();

            // Act
            _TestClass.Thumbnail = TestValue;

            // Assert
            Assert.Same(TestValue, _TestClass.Thumbnail);
        }

        [Fact]
        public void CanSetAndGetTitle()
        {
            // Arrange
            const string TestValue = "TestValue1261347731";

            // Act
            _TestClass.Title = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Title);
        }

        [Fact]
        public void ImplementsIComparable_FeedItem()
        {
            // Arrange
            DateTime PubDate = DateTime.UtcNow;
            var BaseValue = new FeedItem() { Author = "Author", Description = "Description", Link = "Link", PubDateUtc = PubDate, Title = "Title", GUID = new FeedGuid("A"), Enclosure = new Enclosure("A", "B", 10), Thumbnail = new Thumbnail("A") };
            var EqualToBaseValue = new FeedItem() { Author = "Author", Description = "Description", Link = "Link", PubDateUtc = PubDate, Title = "Title", GUID = new FeedGuid("A"), Enclosure = new Enclosure("A", "B", 10), Thumbnail = new Thumbnail("A") };
            var GreaterThanBaseValue = new FeedItem() { Author = "B", Description = "B", Link = "B", PubDateUtc = PubDate.AddSeconds(1), Title = "B", GUID = new FeedGuid("B"), Enclosure = new Enclosure("B", "B", 11), Thumbnail = new Thumbnail("B") };

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_FeedItem()
        {
            // Arrange
            DateTime PubDate = DateTime.UtcNow;
            var TestObject = new FeedItem() { Author = "Author", Description = "Description", Link = "Link", PubDateUtc = PubDate, Title = "Title", GUID = new FeedGuid("A"), Enclosure = new Enclosure("A", "B", 10), Thumbnail = new Thumbnail("A") };
            var Same = new FeedItem() { Author = "Author", Description = "Description", Link = "Link", PubDateUtc = PubDate, Title = "Title", GUID = new FeedGuid("A"), Enclosure = new Enclosure("A", "B", 10), Thumbnail = new Thumbnail("A") };
            var Different = new FeedItem() { Author = "B", Description = "B", Link = "B", PubDateUtc = PubDate.AddSeconds(1), Title = "B", GUID = new FeedGuid("B"), Enclosure = new Enclosure("B", "B", 11), Thumbnail = new Thumbnail("B") };

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
    }
}