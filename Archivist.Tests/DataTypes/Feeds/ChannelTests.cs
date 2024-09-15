using Archivist.DataTypes.Feeds;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes.Feeds
{
    public class ChannelTests : TestBaseClass<Channel>
    {
        public ChannelTests()
        {
            _TestClass = new Channel();
            TestObject = new Channel();
        }

        private readonly Channel _TestClass;

        [Fact]
        public void CanCallAdd()
        {
            // Arrange
            var TestObject = new Channel();
            var Item = new FeedItem();

            // Act
            TestObject.Add(Item);

            // Assert
            Assert.Contains(Item, TestObject);
        }

        [Fact]
        public void CanCallAddRange()
        {
            // Arrange
            var TestObject = new Channel();
            var Items = new[] { new FeedItem(), new FeedItem(), new FeedItem() };

            // Act
            TestObject.AddRange(Items);

            // Assert
            Assert.Equal(Items.Length, TestObject.Count);
            Assert.Contains(Items[0], TestObject);
            Assert.Contains(Items[1], TestObject);
            Assert.Contains(Items[2], TestObject);
        }

        [Fact]
        public void CanCallAddRangeWithNullItems()
        {
            // Arrange
            var TestObject = new Channel();
            // Act
            TestObject.AddRange(null);

            // Assert
            Assert.Empty(TestObject);
        }

        [Fact]
        public void CanCallAddWithNullItem()
        {
            // Arrange
            var TestObject = new Channel
            {
                // Act
                null
            };

            // Assert
            Assert.Empty(TestObject);
        }

        [Fact]
        public void CanCallClear()
        {
            // Arrange
            var TestObject = new Channel() { new FeedItem() };

            // Act
            TestObject.Clear();

            // Assert
            Assert.Empty(TestObject);
        }

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new Channel();

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.True(Result < 0);
        }

        [Fact]
        public void CanCallContains()
        {
            // Arrange
            var Item = new FeedItem();

            // Act
            var Result = _TestClass.Contains(Item);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallContainsWithNullItem()
        {
            // Act
            var Result = _TestClass.Contains(null);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallCopyTo()
        {
            // Arrange
            var TestObject = new Channel() { new FeedItem(), new FeedItem(), new FeedItem() };
            var Array = new[] { new FeedItem(), new FeedItem(), new FeedItem() };
            const int ArrayIndex = 0;

            // Act
            TestObject.CopyTo(Array, ArrayIndex);

            // Assert
            Assert.Contains(Array[0], TestObject);
        }

        [Fact]
        public void CanCallCopyToWithNullArray()
        {
            // Arrange
            var TestObject = new Channel() { new FeedItem(), new FeedItem(), new FeedItem() };
            const int ArrayIndex = 0;

            // Act
            TestObject.CopyTo(null, ArrayIndex);

            // Assert
            Assert.NotEmpty(TestObject);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var PubDate = DateTime.UtcNow;
            var Left = new Channel() { PubDate = PubDate };
            var Right = new Channel() { PubDate = PubDate };

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new Channel();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new Channel() == default;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithChannel()
        {
            // Arrange
            var Other = new Channel()
            {
                Title = "TestValue494963059"
            };

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
        public void CanCallGetEnumeratorForIEnumerableWithNoParameters()
        {
            // Act
            var Result = ((IEnumerable)_TestClass).GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Act
            var Result = _TestClass.GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var Result = _TestClass.GetHashCode();

            // Assert
            Assert.IsType<int>(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var Left = new Channel();
            var Right = new Channel();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(Channel) >= new Channel();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new Channel() >= default(Channel);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new Channel();
            var Right = new Channel();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(Channel) > new Channel();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new Channel() > default(Channel);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallIndexOf()
        {
            // Arrange
            var Item = new FeedItem();

            // Act
            var Result = _TestClass.IndexOf(Item);

            // Assert
            Assert.Equal(-1, Result);
        }

        [Fact]
        public void CanCallIndexOfWithNullItem()
        {
            // Act
            var Result = _TestClass.IndexOf(null);

            // Assert
            Assert.Equal(-1, Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new Channel();
            var Right = new Channel();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new Channel();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new Channel() != default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInsert()
        {
            // Arrange
            var TestObject = new Channel() { new FeedItem() { Title = "A" }, new FeedItem() { Title = "B" }, new FeedItem() { Title = "C" } };
            const int Index = 1;
            var Item = new FeedItem() { Title = "D" };

            // Act
            TestObject.Insert(Index, Item);

            // Assert
            Assert.Equal(Item, TestObject[Index]);
        }

        [Fact]
        public void CanCallInsertWithNullItem()
        {
            // Arrange
            var TestObject = new Channel() { new FeedItem(), new FeedItem(), new FeedItem() };
            const int Index = 1;

            // Act
            TestObject.Insert(Index, null);

            // Assert
            Assert.Equal(3, TestObject.Count);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new Channel();
            var Right = new Channel();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(Channel) <= new Channel();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new Channel() <= default(Channel);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new Channel();
            var Right = new Channel();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(Channel) < new Channel();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new Channel() < default(Channel);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallRemove()
        {
            // Arrange
            var TestObject = new Channel() { new FeedItem() };
            var Item = new FeedItem();
            TestObject.Add(Item);

            // Act
            var Result = TestObject.Remove(Item);

            // Assert
            Assert.True(Result);
            Assert.DoesNotContain(Item, TestObject);
        }

        [Fact]
        public void CanCallRemoveAt()
        {
            // Arrange
            var Item = new FeedItem();
            var TestObject = new Channel() { new FeedItem(), Item, new FeedItem() };
            const int Index = 1;

            // Act
            TestObject.RemoveAt(Index);

            // Assert
            Assert.DoesNotContain(Item, TestObject);
        }

        [Fact]
        public void CanCallRemoveWithNullItem()
        {
            // Arrange
            var TestObject = new Channel() { new FeedItem() };

            // Act
            var Result = TestObject.Remove(null);

            // Assert
            Assert.False(Result);
            Assert.NotEmpty(TestObject);
        }

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            Assert.IsType<string>(Result);
        }

        [Fact]
        public void CanGetCategories()
        {
            // Assert
            Assert.IsType<List<string>>(_TestClass.Categories);

            Assert.Empty(_TestClass.Categories);
        }

        [Fact]
        public void CanGetCount()
        {
            // Assert
            Assert.IsType<int>(_TestClass.Count);

            Assert.Empty(_TestClass);
        }

        [Fact]
        public void CanGetIsReadOnly()
        {
            // Assert
            Assert.IsType<bool>(_TestClass.IsReadOnly);

            Assert.False(_TestClass.IsReadOnly);
        }

        [Fact]
        public void CanGetItems()
        {
            // Assert
            Assert.IsType<List<FeedItem>>(_TestClass.Items);

            Assert.Empty(_TestClass.Items);
        }

        [Fact]
        public void CanSetAndGetCloud()
        {
            // Arrange
            const string TestValue = "TestValue1305003830";

            // Act
            _TestClass.Cloud = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Cloud);
        }

        [Fact]
        public void CanSetAndGetCopyright()
        {
            // Arrange
            const string TestValue = "TestValue683259290";

            // Act
            _TestClass.Copyright = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Copyright);
        }

        [Fact]
        public void CanSetAndGetDescription()
        {
            // Arrange
            const string TestValue = "TestValue235585689";

            // Act
            _TestClass.Description = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Description);
        }

        [Fact]
        public void CanSetAndGetDocs()
        {
            // Arrange
            const string TestValue = "TestValue1354380907";

            // Act
            _TestClass.Docs = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Docs);
        }

        [Fact]
        public void CanSetAndGetExplicit()
        {
            // Arrange
            const bool TestValue = true;

            // Act
            _TestClass.Explicit = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Explicit);
        }

        [Fact]
        public void CanSetAndGetImageUrl()
        {
            // Arrange
            const string TestValue = "TestValue912092970";

            // Act
            _TestClass.ImageUrl = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.ImageUrl);
        }

        [Fact]
        public void CanSetAndGetIndexer()
        {
            // Arrange
            var TestClass = new Channel() { new FeedItem(), new FeedItem() };
            var TestValue = new FeedItem();
            const int Index = 1;

            // Act
            TestClass[Index] = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass[Index]);
        }

        [Fact]
        public void CanSetAndGetLanguage()
        {
            // Arrange
            const string TestValue = "TestValue543583085";

            // Act
            _TestClass.Language = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Language);
        }

        [Fact]
        public void CanSetAndGetLink()
        {
            // Arrange
            const string TestValue = "TestValue2083093558";

            // Act
            _TestClass.Link = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Link);
        }

        [Fact]
        public void CanSetAndGetPubDate()
        {
            // Arrange
            var TestValue = DateTime.UtcNow;

            // Act
            _TestClass.PubDate = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.PubDate);
        }

        [Fact]
        public void CanSetAndGetTitle()
        {
            // Arrange
            const string TestValue = "TestValue494963059";

            // Act
            _TestClass.Title = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Title);
        }

        [Fact]
        public void CanSetAndGetTTL()
        {
            // Arrange
            const int TestValue = 225435676;

            // Act
            _TestClass.TTL = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.TTL);
        }

        [Fact]
        public void CanSetAndGetWebMaster()
        {
            // Arrange
            const string TestValue = "TestValue1122951304";

            // Act
            _TestClass.WebMaster = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.WebMaster);
        }

        [Fact]
        public void ImplementsIComparable_Channel()
        {
            // Arrange
            var FeedItems = new List<FeedItem> { new FeedItem(), new FeedItem() };
            var PubDate = DateTime.UtcNow;
            Channel BaseValue = new Channel() { PubDate = PubDate };
            BaseValue.AddRange(FeedItems);
            Channel EqualToBaseValue = new Channel() { PubDate = PubDate };
            EqualToBaseValue.AddRange(FeedItems);
            Channel GreaterThanBaseValue = new Channel { new FeedItem(), new FeedItem(), new FeedItem() };

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_Channel()
        {
            // Arrange
            var PubDate = DateTime.UtcNow;
            var FeedItems = new[] { new FeedItem(), new FeedItem() };
            var TestObject = new Channel() { PubDate = PubDate };
            TestObject.AddRange(FeedItems);
            var Same = new Channel() { PubDate = PubDate };
            Same.AddRange(FeedItems);
            var Different = new Channel() { new FeedItem(), new FeedItem(), new FeedItem() };

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