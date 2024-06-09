using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System.Collections.Generic;
using System.Dynamic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class TableRowTests : TestBaseClass<TableRow>
    {
        public TableRowTests()
        {
            _Columns = new List<string>
            {
                "TestValue1"
            };
            _TestClass = new TableRow(_Columns);
            TestObject = new TableRow(_Columns);
        }

        private readonly List<string> _Columns;
        private readonly TableRow _TestClass;

        [Fact]
        public void CanCallAdd()
        {
            // Arrange
            var Item = new TableCell("TestValue53673051");

            // Act
            _TestClass.Add(Item);

            // Assert
            Assert.Contains(Item, _TestClass);
        }

        [Fact]
        public void CanCallAddRange()
        {
            // Arrange
            var TestClass = new TableRow(_Columns);
            var Items = new List<TableCell> { new("TestValue1") };

            // Act
            TestClass.AddRange(Items);

            // Assert
            Assert.Contains(Items[0], TestClass);
        }

        [Fact]
        public void CanCallAddWithNullItem() => _TestClass.Add(default(TableCell));

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
            var Other = new TableRow(new List<string>());

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallContains()
        {
            // Arrange
            var Item = new TableCell("TestValue1465860330");

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
            var TestClass = new TableRow(new List<string>() { "TestValue1" })
            {
                new TableCell("TestVal")
            };
            // Act
            dynamic Result = TestClass.ConvertTo<ExpandoObject>();

            // Assert
            Assert.Equal("TestVal", Result.TestValue1);
        }

        [Fact]
        public void CanCallCopyTo()
        {
            // Arrange
            var TestClass = new TableRow(new List<string> { "TestValue1" })
            {
                new TableCell("TestValue1")
            };
            TableCell[] Array = new[] { new TableCell("TestValue2138396531"), new TableCell("TestValue952676572"), new TableCell("TestValue887368306") };
            const int ArrayIndex = 1;

            // Act
            TestClass.CopyTo(Array, ArrayIndex);

            // Assert
            Assert.Equal(3, Array.Length);
            Assert.Equal("TestValue2138396531", Array[0].Content);
            Assert.Equal("TestValue1", Array[1].Content);
            Assert.Equal("TestValue887368306", Array[2].Content);
        }

        [Fact]
        public void CanCallCopyToWithNullArray() => _TestClass.CopyTo(default, 1840850477);

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new TableRow(new List<string>());
            var Right = new TableRow(new List<string>());

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new TableRow(new List<string>());

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new TableRow(new List<string>()) == default;

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
        public void CanCallEqualsWithTableRow()
        {
            // Arrange
            var Other = new TableRow(new List<string>());

            // Act
            var Result = _TestClass.Equals(Other);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorForIEnumerable_TableCell_WithNoParameters()
        {
            // Act
            IEnumerator<TableCell> Result = ((IEnumerable<TableCell>)_TestClass).GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Act
            System.Collections.IEnumerator Result = _TestClass.GetEnumerator();

            // Assert
            Assert.NotNull(Result);
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
            var Left = new TableRow(new List<string>());
            var Right = new TableRow(new List<string>());

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = null >= new TableRow(new List<string>());

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new TableRow(new List<string>()) >= null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new TableRow(new List<string>());
            var Right = new TableRow(new List<string>());

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = null > new TableRow(new List<string>());

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new TableRow(new List<string>()) > null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallIndexOf()
        {
            // Arrange
            var Item = new TableCell("TestValue1894265549");

            // Act
            var Result = _TestClass.IndexOf(Item);

            // Assert
            Assert.Equal(-1, Result);
        }

        [Fact]
        public void CanCallIndexOfWithNullItem() => _TestClass.IndexOf(default);

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new TableRow(new List<string>());
            var Right = new TableRow(new List<string>());

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new TableRow(new List<string>());

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new TableRow(new List<string>()) != default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInsert()
        {
            // Arrange
            var TestClass = new TableRow(new List<string> { "TestValue1" })
            {
                new TableCell("TestValue1")
            };
            const int Index = 0;
            var Item = new TableCell("TestValue1820079551");

            // Act
            TestClass.Insert(Index, Item);

            // Assert
            Assert.Equal(Item, TestClass[Index]);
        }

        [Fact]
        public void CanCallInsertWithNullItem() => _TestClass.Insert(0, default);

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new TableRow(new List<string>());
            var Right = new TableRow(new List<string>());

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = null <= new TableRow(new List<string>());

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new TableRow(new List<string>()) <= null;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new TableRow(new List<string>());
            var Right = new TableRow(new List<string>());

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = null < new TableRow(new List<string>());

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new TableRow(new List<string>()) < null;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallRemove()
        {
            // Arrange
            var Item = new TableCell("TestValue410492227");

            // Act
            var Result = _TestClass.Remove(Item);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallRemoveAt()
        {
            // Arrange
            var TestClass = new TableRow(new List<string> { "TestValue1" })
            {
                new TableCell("TestValue1")
            };
            const int Index = 0;

            // Act
            TestClass.RemoveAt(Index);

            // Assert
            Assert.Empty(TestClass);
        }

        [Fact]
        public void CanCallRemoveWithNullItem() => _ = _TestClass.Remove(default);

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new TableRow(_Columns);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullColumns() => _ = new TableRow(default);

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
        public void CanSetAndGetIndexerForInt()
        {
            // Arrange
            var TestClass = new TableRow(new List<string> { "TestValue1" })
            {
                new TableCell("TestValue1")
            };
            var TestValue = new TableCell("TestValue2049611901");
            _ = Assert.IsType<TableCell>(TestClass[0]);
            TestClass[0] = TestValue;
            Assert.Same(TestValue, TestClass[0]);
        }

        [Fact]
        public void CanSetAndGetIndexerForString()
        {
            var TestClass = new TableRow(new List<string> { "TestValue1" })
            {
                new TableCell("TestValue1")
            };
            var TestValue = new TableCell("TestValue2");
            _ = Assert.IsType<TableCell>(TestClass["TestValue1"]);
            TestClass["TestValue1"] = TestValue;
            Assert.Same(TestValue, TestClass["TestValue1"]);
        }

        [Fact]
        public void ImplementsIComparable_TableRow()
        {
            // Arrange
            var BaseValue = new TableRow(new List<string> { "TestValue1" })
            {
                new TableCell("Val")
            };
            var EqualToBaseValue = new TableRow(new List<string> { "TestValue1" })
            {
                new TableCell("Val")
            };
            var GreaterThanBaseValue = new TableRow(new List<string> { "TestValue" })
            {
                new TableCell("Val1")
            };

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_TableRow()
        {
            // Arrange
            var TestClass = new TableRow(new List<string> { "TestValue1" })
            {
                new TableCell("TestValue1")
            };
            var Same = new TableRow(new List<string> { "TestValue1" })
                {
                new TableCell("TestValue1")
            };
            var Different = new TableRow(new List<string> { "TestValue1" }) { new TableCell("TestValue2") };

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