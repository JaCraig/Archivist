using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.DataTypes.Feeds;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class FeedTests : TestBaseClass<Feed>
    {
        public FeedTests()
        {
            _Converter = new Convertinator(new[] { Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>() });
            _TestClass = new Feed(_Converter);
            TestObject = new Feed();
        }

        private readonly Convertinator _Converter;
        private readonly Feed _TestClass;

        [Fact]
        public void CanCallAdd()
        {
            // Arrange
            var TestClass = new Feed();
            var Item = new Channel();

            // Act
            TestClass.Add(Item);

            // Assert
            Assert.Contains(Item, TestClass);
        }

        [Fact]
        public void CanCallAddWithNullItem()
        {
            // Arrange
            var TestClass = new Feed
            {
                // Act
                null!
            };

            // Assert
            Assert.Empty(TestClass);
        }

        [Fact]
        public void CanCallClear()
        {
            // Arrange
            var TestClass = new Feed
            {
                new Channel()
            };

            // Act
            TestClass.Clear();

            // Assert
            Assert.Empty(TestClass);
        }

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new Feed() { new Channel() };

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.NotEqual(0, Result);
        }

        [Fact]
        public void CanCallContains()
        {
            // Arrange
            var Item = new Channel();

            // Act
            var Result = _TestClass.Contains(Item);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallContainsWithNullItem()
        {
            // Arrange
            var TestClass = new Feed();

            // Act
            var Result = TestClass.Contains(null);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallCopyTo()
        {
            // Arrange
            var TestObject = new Feed() { new Channel(), new Channel() };
            Channel[] Array = new[] { new Channel(), new Channel(), new Channel() };
            const int ArrayIndex = 1;

            // Act
            TestObject.CopyTo(Array, ArrayIndex);

            // Assert
            Assert.Contains(TestObject[0], Array);
        }

        [Fact]
        public void CanCallCopyToWithNullArray()
        {
            // Arrange
            var TestClass = new Feed();

            // Act
            TestClass.CopyTo(null, 0);

            // Assert
            Assert.Empty(TestClass);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new Feed();
            var Right = new Feed();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft() => Assert.False(default == new Feed());

        [Fact]
        public void CanCallEqualityOperatorWithNullRight() => Assert.False(new Feed() == default);

        [Fact]
        public void CanCallEqualsWithFeed()
        {
            // Arrange
            var Other = new Feed();

            // Act
            var Result = _TestClass.Equals(Other);

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
        public void CanCallGetContent()
        {
            // Arrange
            var TestClass = new Feed(_Converter) { new Channel() };

            // Act
            var Result = TestClass.GetContent();

            // Assert
            var Content = Assert.IsType<string>(Result);
            Assert.NotEmpty(Content);
        }

        [Fact]
        public void CanCallGetEnumeratorForIEnumerableWithNoParameters()
        {
            // Act
            IEnumerator Result = ((IEnumerable)_TestClass).GetEnumerator();

            // Assert
            _ = Assert.IsAssignableFrom<IEnumerator>(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Act
            IEnumerator<Channel> Result = _TestClass.GetEnumerator();

            // Assert
            _ = Assert.IsAssignableFrom<IEnumerator<Channel>>(Result);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var Result = _TestClass.GetHashCode();

            // Assert
            _ = Assert.IsType<int>(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var Left = new Feed();
            var Right = new Feed();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft() => Assert.False(default(Feed) >= new Feed());

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight() => Assert.False(new Feed() >= default(Feed));

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new Feed();
            var Right = new Feed();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft() => Assert.False(default(Feed) > new Feed());

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight() => Assert.False(new Feed() > default(Feed));

        [Fact]
        public void CanCallIndexOf()
        {
            // Arrange
            var Item = new Channel();

            // Act
            var Result = _TestClass.IndexOf(Item);

            // Assert
            Assert.Equal(-1, Result);
        }

        [Fact]
        public void CanCallIndexOfWithNullItem()
        {
            // Arrange
            var TestClass = new Feed();

            // Act
            var Result = TestClass.IndexOf(null);

            // Assert
            Assert.Equal(-1, Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new Feed();
            var Right = new Feed();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft() => Assert.True(default != new Feed());

        [Fact]
        public void CanCallInequalityOperatorWithNullRight() => Assert.True(new Feed() != default);

        [Fact]
        public void CanCallInsert()
        {
            // Arrange
            var TestObject = new Feed();
            var Item = new Channel();

            // Act
            TestObject.Insert(0, Item);

            // Assert
            Assert.Contains(Item, TestObject);
        }

        [Fact]
        public void CanCallInsertWithNullItem()
        {
            // Arrange
            var TestClass = new Feed();

            // Act
            TestClass.Insert(0, null);

            // Assert
            Assert.Empty(TestClass);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new Feed();
            var Right = new Feed();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft() => Assert.True(default(Feed) <= new Feed());

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight() => Assert.True(new Feed() <= default(Feed));

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new Feed();
            var Right = new Feed();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft() => Assert.True(default(Feed) < new Feed());

        [Fact]
        public void CanCallLessThanOperatorWithNullRight() => Assert.True(new Feed() < default(Feed));

        [Fact]
        public void CanCallRemove()
        {
            // Arrange
            var TestClass = new Feed();
            var Item = new Channel();
            TestClass.Add(Item);

            // Act
            var Result = TestClass.Remove(Item);

            // Assert
            Assert.True(Result);
            Assert.DoesNotContain(Item, TestClass);
        }

        [Fact]
        public void CanCallRemoveAt()
        {
            // Arrange
            var TestClass = new Feed();
            var Item = new Channel();
            TestClass.Add(Item);

            // Act
            TestClass.RemoveAt(0);

            // Assert
            Assert.DoesNotContain(Item, TestClass);
        }

        [Fact]
        public void CanCallRemoveWithNullItem()
        {
            // Arrange
            var TestClass = new Feed();

            // Act
            var Result = TestClass.Remove(null);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallToFileType()
        {
            // Act
            Text? Result = _TestClass.ToFileType<Text>();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(_TestClass.GetContent(), Result.GetContent());
        }

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            _ = Assert.IsType<string>(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new Feed();

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new Feed(_Converter);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetChannels()
        {
            // Assert
            _ = Assert.IsType<List<Channel>>(_TestClass.Channels);

            Assert.Empty(_TestClass.Channels);
        }

        [Fact]
        public void CanGetCount()
        {
            // Assert
            _ = Assert.IsType<int>(_TestClass.Count);

            Assert.Empty(_TestClass);
        }

        [Fact]
        public void CanGetIsReadOnly()
        {
            // Assert
            _ = Assert.IsType<bool>(_TestClass.IsReadOnly);

            Assert.False(_TestClass.IsReadOnly);
        }

        [Fact]
        public void CanSetAndGetIndexer()
        {
            // Arrange
            var TestObject = new Feed() { new Channel(), new Channel(), new Channel() };
            var TestValue = new Channel();
            const int Index = 1;

            // Act
            TestObject[Index] = TestValue;

            // Assert
            Assert.Same(TestValue, TestObject[Index]);
        }

        [Fact]
        public void ImplementsIComparable_Feed()
        {
            // Arrange
            var BaseValue = new Feed() { new Channel() };
            var EqualToBaseValue = new Feed() { new Channel() };
            var LessThanBaseValue = new Feed() { new Channel(), new Channel() };

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(LessThanBaseValue) > 0);
            Assert.True(LessThanBaseValue.CompareTo(BaseValue) < 0);
        }

        [Fact]
        public void ImplementsIEquatable_Feed()
        {
            // Arrange
            var TestClass = new Feed() { new Channel(), new Channel() };
            var Same = new Feed() { new Channel(), new Channel() };
            var Different = new Feed() { new Channel() };

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
    }
}