using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class TableTests : TestBaseClass<Table>
    {
        public TableTests()
        {
            _TestClass = new Table();
            TestObject = new Table();
        }

        private readonly Table _TestClass;

        [Fact]
        public void CanCallAdd()
        {
            // Arrange
            var TestClass = new Table();
            var Item = new TableRow(new List<string>());

            // Act
            TestClass.Add(Item);

            // Assert
            _ = Assert.Single(TestClass);
            Assert.Same(Item, TestClass[0]);
        }

        [Fact]
        public void CanCallAddRow()
        {
            // Arrange
            var TestClass = new Table();
            // Act
            TableRow Result = TestClass.AddRow();

            // Assert
            _ = Assert.IsType<TableRow>(Result);
            _ = Assert.Single(TestClass);
            Assert.Same(Result, TestClass[0]);
        }

        [Fact]
        public void CanCallAddWithNullItem() => _TestClass.Add(default);

        [Fact]
        public void CanCallClear()
        {
            // Act
            _TestClass.Clear();

            // Assert
            Assert.Empty(_TestClass);
        }

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new Table();

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.False(Result < 0);
        }

        [Fact]
        public void CanCallContains()
        {
            // Arrange
            var Item = new TableRow(new List<string>());

            // Act
            var Result = _TestClass.Contains(Item);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallContainsWithNullItem() => _TestClass.Contains(default);

        [Fact]
        public void CanCallConvertTo()
        {
            // Arrange
            var TestClass = new Table();
            TestClass.Columns.Add("Column1");
            TestClass.AddRow().Add("1");
            TestClass.AddRow().Add("2");
            TestClass.AddRow().Add("3");

            // Act
            List<ConvertObject?> Result = TestClass.ConvertTo<ConvertObject>();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(3, Result.Count);
            Assert.NotNull(Result[0]);
            Assert.Equal(1, Result[0]?.Column1);
            Assert.NotNull(Result[1]);
            Assert.Equal(2, Result[1]?.Column1);
            Assert.NotNull(Result[2]);
            Assert.Equal(3, Result[2]?.Column1);
        }

        [Fact]
        public void CanCallCopyTo()
        {
            // Arrange
            var TestClass = new Table();
            TestClass.Columns.Add("Column1");
            TestClass.AddRow().Add("1");
            TableRow[] Array = new[] { new TableRow(new List<string>() { "A" }) { "A" }, new TableRow(new List<string>() { "A" }) { "A" }, new TableRow(new List<string>() { "A" }) { "A" } };
            const int ArrayIndex = 1;

            // Act
            TestClass.CopyTo(Array, ArrayIndex);

            // Assert
            Assert.Equal(3, Array.Length);
            Assert.Equal("A", Array[0][0].Content);
            Assert.Equal("1", Array[1][0].Content);
            Assert.Equal("A", Array[2][0].Content);
        }

        [Fact]
        public void CanCallCopyToWithNullArray() => _TestClass.CopyTo(default, 653183370);

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new Table();
            var Right = new Table();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new Table();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new Table() == default;

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
        public void CanCallEqualsWithTable()
        {
            // Arrange
            var TestClass = new Table();
            TestClass.Columns.Add("Column1");
            TestClass.AddRow().Add("1");
            var Other = new Table();

            // Act
            var Result = TestClass.Equals(Other);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGetContent()
        {
            // Arrange
            var TestClass = new Table();
            TestClass.Columns.Add("Column1");
            TestClass.Columns.Add("Column2");
            TableRow TestRow = TestClass.AddRow();
            TestRow.Add("1");
            TestRow.Add("2");

            // Act
            var Result = TestClass.GetContent();

            // Assert
            Assert.Equal("Column1 Column2\n1 2", Result);
        }

        [Fact]
        public void CanCallGetEnumeratorForIEnumerableWithNoParameters()
        {
            // Act
            IEnumerator Result = ((IEnumerable)_TestClass).GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Act
            IEnumerator<TableRow> Result = _TestClass.GetEnumerator();

            // Assert
            Assert.NotNull(Result);
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
            var Left = new Table();
            var Right = new Table();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = null >= new Table();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new Table() >= null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new Table();
            var Right = new Table();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = null > new Table();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new Table() > null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallIndexOf()
        {
            // Arrange
            var Item = new TableRow(new List<string>());

            // Act
            var Result = _TestClass.IndexOf(Item);

            // Assert
            Assert.Equal(-1, Result);
        }

        [Fact]
        public void CanCallIndexOfWithNullItem()
        {
            var Result = _TestClass.IndexOf(default);

            Assert.Equal(-1, Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new Table();
            var Right = new Table();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = null != new Table();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new Table() != null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInsert()
        {
            // Arrange
            const int Index = 0;
            var Item = new TableRow(new List<string>());

            // Act
            _TestClass.Insert(Index, Item);

            // Assert
            Assert.Same(Item, _TestClass[Index]);
        }

        [Fact]
        public void CanCallInsertWithNullItem() => _TestClass.Insert(0, default);

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new Table();
            var Right = new Table();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = null <= new Table();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new Table() <= null;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new Table();
            var Right = new Table();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = null < new Table();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new Table() < null;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallRemove()
        {
            // Arrange
            var Item = new TableRow(new List<string>());

            // Act
            var Result = _TestClass.Remove(Item);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallRemoveAt()
        {
            // Arrange
            var TestClass = new Table();
            TestClass.Columns.Add("Column1");
            TestClass.AddRow().Add("1");
            const int Index = 0;

            // Act
            TestClass.RemoveAt(Index);

            // Assert
            Assert.Empty(TestClass);
        }

        [Fact]
        public void CanCallRemoveWithNullItem() => _TestClass.Remove(default);

        [Fact]
        public void CanGetColumns() =>
            // Assert
            Assert.IsType<List<string>>(_TestClass.Columns);

        [Fact]
        public void CanGetCount() =>
            // Assert
            Assert.IsType<int>(_TestClass.Count);

        [Fact]
        public void CanGetIsReadOnly() =>
            // Assert
            Assert.IsType<bool>(_TestClass.IsReadOnly);

        [Fact]
        public void CanSetAndGetIndexer()
        {
            // Arrange
            var TestValue = new TableRow(new List<string>());
            var TestClass = new Table();
            TestClass.AddRow().Add("1");
            _ = Assert.IsType<TableRow>(TestClass[0]);
            TestClass[0] = TestValue;
            Assert.Same(TestValue, TestClass[0]);
        }

        [Fact]
        public void ImplementsIComparable_Table()
        {
            // Arrange
            var BaseValue = new Table();
            BaseValue.Columns.Add("Column1");
            BaseValue.AddRow().Add("A");

            var EqualToBaseValue = new Table();
            EqualToBaseValue.Columns.Add("Column1");
            EqualToBaseValue.AddRow().Add("A");

            var LessThanBaseValue = new Table();
            LessThanBaseValue.Columns.Add("Column1");
            LessThanBaseValue.AddRow().Add("B");

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(LessThanBaseValue) > 0);
            Assert.True(LessThanBaseValue.CompareTo(BaseValue) < 0);
        }

        [Fact]
        public void ImplementsIEquatable_Table()
        {
            // Arrange
            var TestClass = new Table();
            TestClass.Columns.Add("Column1");
            TestClass.AddRow().Add("1");
            var Same = new Table();
            Same.Columns.Add("Column1");
            Same.AddRow().Add("1");
            var Different = new Table();
            Different.Columns.Add("Column1");
            Different.AddRow().Add("2");

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

        public class ConvertObject
        {
            public int Column1 { get; set; }
        }
    }
}