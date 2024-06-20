using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;

namespace Archivist.Tests.DataTypes
{
    public class TablesTests : TestBaseClass<Tables>
    {
        public TablesTests()
        {
            _TestClass = new Tables();
            TestObject = new Tables();
        }

        private readonly Tables _TestClass;

        [Fact]
        public void CanCallAdd()
        {
            // Arrange
            var Item = new Table();
            var TestClass = new Tables
            {
                // Act
                Item
            };

            // Assert
            Assert.All(TestClass, item => Assert.Same(Item, item));
            _ = Assert.Single(TestClass);
        }

        [Fact]
        public void CanCallAddTable()
        {
            // Arrange
            var TestClass = new Tables();

            // Act
            Table Result = TestClass.AddTable();

            // Assert
            _ = Assert.IsType<Table>(Result);
            Assert.All(TestClass, item => Assert.Same(Result, item));
            _ = Assert.Single(TestClass);
        }

        [Fact]
        public void CanCallClear()
        {
            // Arrange
            var TestClass = new Tables
            {
                new Table(),
                new Table(),
                new Table()
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
            var TestClass = new Tables();
            var Other = new Tables();

            // Act
            var Result = TestClass.CompareTo(Other);

            // Assert
            _ = Assert.IsType<int>(Result);
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallContains()
        {
            // Arrange
            var Item = new Table();
            var TestClass = new Tables();

            // Act
            var Result = TestClass.Contains(Item);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallConvertFrom()
        {
            // Arrange
            var Obj = new List<TestObject?>
            {
                new() { Name = "Test1", Value = "Test2" },
                new() { Name = "Test3", Value = "Test4" }
            };
            var TestClass = new Tables();

            // Act
            TestClass.ConvertFrom(Obj);

            // Assert
            _ = Assert.Single(TestClass);
            Assert.Equal(2, TestClass[0].Count);
            Assert.Equal("Test1", TestClass[0][0][0].Content);
            Assert.Equal("Test2", TestClass[0][0][1].Content);
            Assert.Equal("Test3", TestClass[0][1][0].Content);
            Assert.Equal("Test4", TestClass[0][1][1].Content);
        }

        [Fact]
        public void CanCallConvertFromWithNullObj() => _TestClass.ConvertFrom(default(List<TestClass?>));

        [Fact]
        public void CanCallConvertTo()
        {
            // Act
            List<TestObject?> Result = _TestClass.ConvertTo<TestObject>();

            // Assert
            _ = Assert.IsType<List<TestObject?>>(Result);
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallCopyTo()
        {
            // Arrange
            Table[] Array = new[] { new Table(), new Table(), new Table() };
            const int ArrayIndex = 1;
            var TestClass = new Tables
            {
                new Table()
            };
            TestClass[0].AddRow().Add("Something");

            // Act
            TestClass.CopyTo(Array, ArrayIndex);

            // Assert
            Assert.Same(TestClass[0], Array[ArrayIndex]);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new Tables();
            var Right = new Tables();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualsWithObject()
        {
            // Arrange
            var Obj = new object();

            // Act
            var Result = _TestClass.Equals(Obj);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithTables()
        {
            // Arrange
            var Other = new Tables();

            // Act
            var Result = _TestClass.Equals(Other);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGetContent()
        {
            // Act
            var Result = _TestClass.GetContent();

            // Assert
            _ = Assert.IsType<string>(Result);
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorForIEnumerableWithNoParameters()
        {
            // Act
            IEnumerator Result = ((IEnumerable)_TestClass).GetEnumerator();

            // Assert
            _ = Assert.IsAssignableFrom<IEnumerator>(Result);
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Act
            IEnumerator<Table> Result = _TestClass.GetEnumerator();

            // Assert
            _ = Assert.IsAssignableFrom<IEnumerator<Table>>(Result);
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var Result = _TestClass.GetHashCode();

            // Assert
            _ = Assert.IsType<int>(Result);
            Assert.NotEqual(0, Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var Left = new Tables();
            var Right = new Tables();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new Tables();
            var Right = new Tables();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallIndexOf()
        {
            // Arrange
            var Item = new Table();

            // Act
            var Result = _TestClass.IndexOf(Item);

            // Assert
            Assert.Equal(-1, Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new Tables();
            var Right = new Tables();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInsert()
        {
            // Arrange
            const int Index = 0;
            var Item = new Table();

            // Act
            _TestClass.Insert(Index, Item);

            // Assert
            Assert.Same(Item, _TestClass[Index]);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new Tables();
            var Right = new Tables();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new Tables();
            var Right = new Tables();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallRemove()
        {
            // Arrange
            var Item = new Table();

            // Act
            var Result = _TestClass.Remove(Item);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallRemoveAt()
        {
            // Arrange
            var TestClass = new Tables
            {
                new Table(),
                new Table(),
                new Table()
            };
            const int Index = 1;

            // Act
            TestClass.RemoveAt(Index);

            // Assert
            Assert.Equal(2, TestClass.Count);
        }

        [Fact]
        public void CanCallToFileType()
        {
            // Arrange
            var TestClass = new Tables
            {
                new Table()
            };

            // Act
            Table? Result = TestClass.ToFileType<Table>();

            // Assert
            _ = Assert.IsType<Table>(Result);
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetCount()
        {
            // Assert
            var Result = Assert.IsType<int>(_TestClass.Count);

            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanGetIsReadOnly()
        {
            // Assert
            var Result = Assert.IsType<bool>(_TestClass.IsReadOnly);

            Assert.False(Result);
        }

        [Fact]
        public void CanSetAndGetIndexer()
        {
            // Arrange
            var TestClass = new Tables
            {
                new Table(),
                new Table(),
                new Table()
            };
            var TestValue = new Table();
            const int Index = 1;

            // Act
            TestClass[Index] = TestValue;

            // Assert
            Assert.Same(TestValue, TestClass[Index]);
        }

        [Fact]
        public void ImplementsIComparable_Tables()
        {
            // Arrange
            var BaseValue = new Tables
            {
                new Table()
            };
            var EqualToBaseValue = new Tables
            {
                new Table()
            };
            var LessThanBaseValue = new Tables();
            LessThanBaseValue.AddTable().AddRow().Add("Something");

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(LessThanBaseValue) > 0);
            Assert.True(LessThanBaseValue.CompareTo(BaseValue) < 0);
        }

        [Fact]
        public void ImplementsIEquatable_Tables()
        {
            // Arrange
            var TestClass = new Tables();
            TestClass.AddTable().AddRow().Add("Something");
            var Same = new Tables();
            Same.AddTable().AddRow().Add("Something");
            var Different = new Tables();
            Different.AddTable().AddRow().Add("Something Else");

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

    public class TestObject
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}