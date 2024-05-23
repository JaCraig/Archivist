using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class FixedLengthRecordTests : TestBaseClass<FixedLengthRecord>
    {
        public FixedLengthRecordTests()
        {
            _TestClass = new FixedLengthRecord();
            TestObject = new FixedLengthRecord();
        }

        private readonly FixedLengthRecord _TestClass;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new FixedLengthRecord();

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new FixedLengthRecord();
            var Right = new FixedLengthRecord();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new FixedLengthRecord();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthRecord() == default;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithFixedLengthRecord()
        {
            // Arrange
            var Other = new FixedLengthRecord();

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
        public void CanCallGetEnumeratorForSystem_Collections_IEnumerableWithNoParameters()
        {
            // Act
            System.Collections.IEnumerator Result = ((System.Collections.IEnumerable)_TestClass).GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Act
            IEnumerator<FixedLengthField> Result = _TestClass.GetEnumerator();

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
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var Left = new FixedLengthRecord();
            var Right = new FixedLengthRecord();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(FixedLengthRecord?) >= new FixedLengthRecord();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthRecord() >= default(FixedLengthRecord?);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new FixedLengthRecord();
            var Right = new FixedLengthRecord();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(FixedLengthRecord?) > new FixedLengthRecord();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthRecord() > default(FixedLengthRecord?);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new FixedLengthRecord();
            var Right = new FixedLengthRecord();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new FixedLengthRecord();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthRecord() != default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new FixedLengthRecord();
            var Right = new FixedLengthRecord();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(FixedLengthRecord?) <= new FixedLengthRecord();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthRecord() <= default(FixedLengthRecord?);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new FixedLengthRecord();
            var Right = new FixedLengthRecord();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(FixedLengthRecord?) < new FixedLengthRecord();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new FixedLengthRecord() < default(FixedLengthRecord?);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            Assert.Equal("", Result);
        }

        [Fact]
        public void CanGetCount()
        {
            // Assert
            var Result = Assert.IsType<int>(_TestClass.Count);

            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanGetFields()
        {
            // Assert
            List<FixedLengthField> Result = Assert.IsType<List<FixedLengthField>>(_TestClass.Fields);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetLength()
        {
            // Assert
            var Result = Assert.IsType<int>(_TestClass.Length);

            Assert.Equal(0, Result);
        }

        [Fact]
        public void ImplementsIComparable_FixedLengthRecord()
        {
            // Arrange
            var BaseValue = new FixedLengthRecord();
            var EqualToBaseValue = new FixedLengthRecord();
            var GreaterThanBaseValue = new FixedLengthRecord();
            GreaterThanBaseValue.Fields.Add(new FixedLengthField("", 1));

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEnumerable_FixedLengthField()
        {
            // Arrange
            var Enumerable = new FixedLengthRecord();
            Enumerable.Fields.Add(new FixedLengthField("", 1));
            Enumerable.Fields.Add(new FixedLengthField("", 2));
            Enumerable.Fields.Add(new FixedLengthField("", 3));
            Enumerable.Fields.Add(new FixedLengthField("", 4));
            Enumerable.Fields.Add(new FixedLengthField("", 5));
            Enumerable.Fields.Add(new FixedLengthField("", 6));
            const int ExpectedCount = 6;
            var ActualCount = 0;

            // Act
            using (IEnumerator<FixedLengthField> Enumerator = Enumerable.GetEnumerator())
            {
                Assert.NotNull(Enumerator);
                while (Enumerator.MoveNext())
                {
                    ActualCount++;
                    FixedLengthField Result = Assert.IsType<FixedLengthField>(Enumerator.Current);
                }
            }

            // Assert
            Assert.Equal(ExpectedCount, ActualCount);
        }

        [Fact]
        public void ImplementsIEquatable_FixedLengthRecord()
        {
            // Arrange
            var TestClass = new FixedLengthRecord();
            var Same = new FixedLengthRecord();
            var Different = new FixedLengthRecord();
            Different.Fields.Add(new FixedLengthField("", 1));

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