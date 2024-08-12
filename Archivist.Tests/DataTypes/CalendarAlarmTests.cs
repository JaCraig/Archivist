using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class CalendarAlarmTests : TestBaseClass<CalendarAlarm>
    {
        public CalendarAlarmTests()
        {
            _TestClass = new CalendarAlarm();
            TestObject = new CalendarAlarm();
        }

        private readonly CalendarAlarm _TestClass;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new CalendarAlarm();
            Other.Fields.Add(new KeyValueField("Test", Array.Empty<KeyValueParameter>(), "Val"));

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.Equal(-1, Result);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new CalendarAlarm();
            var Right = new CalendarAlarm();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default! == new CalendarAlarm();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarAlarm() == default!;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualsWithAlarm()
        {
            // Arrange
            var TestClass = new CalendarAlarm();
            var Other = new CalendarAlarm();
            Other.Fields.Add(new KeyValueField("Test", Array.Empty<KeyValueParameter>(), "Val"));

            // Act
            var Result = TestClass.Equals(Other);

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
            IEnumerator Result = ((IEnumerable)_TestClass).GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Act
            IEnumerator<KeyValueField?> Result = _TestClass.GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Arrange
            var TestClass = new CalendarAlarm();
            TestClass.Fields.Add(new KeyValueField("Test", Array.Empty<KeyValueParameter>(), "Val"));

            // Act
            var Result = TestClass.GetHashCode();

            // Assert
            Assert.NotEqual(0, Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var Left = new CalendarAlarm();
            var Right = new CalendarAlarm();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(CalendarAlarm)! >= new CalendarAlarm();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarAlarm() >= default(CalendarAlarm)!;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new CalendarAlarm();
            var Right = new CalendarAlarm();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(CalendarAlarm)! > new CalendarAlarm();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarAlarm() > default(CalendarAlarm)!;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new CalendarAlarm();
            var Right = new CalendarAlarm();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default! != new CalendarAlarm();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarAlarm() != default!;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new CalendarAlarm();
            var Right = new CalendarAlarm();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(CalendarAlarm)! <= new CalendarAlarm();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarAlarm() <= default(CalendarAlarm)!;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new CalendarAlarm();
            var Right = new CalendarAlarm();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(CalendarAlarm)! < new CalendarAlarm();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarAlarm() < default(CalendarAlarm)!;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanGetFields()
        {
            // Arrange
            var TestClass = new CalendarAlarm();

            // Assert
            List<KeyValueField> Result = Assert.IsType<List<KeyValueField>>(TestClass.Fields);

            Assert.Empty(Result);
        }

        [Fact]
        public void ImplementsIComparable_Alarm()
        {
            // Arrange
            var BaseValue = new CalendarAlarm();
            BaseValue.Fields.Add(new KeyValueField("Test", Array.Empty<KeyValueParameter>(), "Val"));
            BaseValue.Fields.Add(new KeyValueField("Test2", Array.Empty<KeyValueParameter>(), "Val2"));

            var EqualToBaseValue = new CalendarAlarm();
            EqualToBaseValue.Fields.Add(new KeyValueField("Test", Array.Empty<KeyValueParameter>(), "Val"));
            EqualToBaseValue.Fields.Add(new KeyValueField("Test2", Array.Empty<KeyValueParameter>(), "Val2"));

            var GreaterThanBaseValue = new CalendarAlarm();
            GreaterThanBaseValue.Fields.Add(new KeyValueField("Test", Array.Empty<KeyValueParameter>(), "Val"));
            GreaterThanBaseValue.Fields.Add(new KeyValueField("Test2", Array.Empty<KeyValueParameter>(), "Val3"));

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEnumerable_KeyValueField()
        {
            // Arrange
            var Enumerable = new CalendarAlarm();
            Enumerable.Fields.Add(new KeyValueField("Test", Array.Empty<KeyValueParameter>(), "Val"));
            Enumerable.Fields.Add(new KeyValueField("Test2", Array.Empty<KeyValueParameter>(), "Val2"));
            Enumerable.Fields.Add(new KeyValueField("Test3", Array.Empty<KeyValueParameter>(), "Val3"));
            const int ExpectedCount = 3;
            var ActualCount = 0;

            // Act
            using (IEnumerator<KeyValueField?> Enumerator = Enumerable.GetEnumerator())
            {
                Assert.NotNull(Enumerator);
                while (Enumerator.MoveNext())
                {
                    ActualCount++;
                    _ = Assert.IsType<KeyValueField>(Enumerator.Current);
                }
            }

            // Assert
            Assert.Equal(ExpectedCount, ActualCount);
        }

        [Fact]
        public void ImplementsIEquatable_Alarm()
        {
            // Arrange
            var TestClass = new CalendarAlarm();
            TestClass.Fields.Add(new KeyValueField("Test", Array.Empty<KeyValueParameter>(), "Val"));
            TestClass.Fields.Add(new KeyValueField("Test2", Array.Empty<KeyValueParameter>(), "Val2"));
            var Same = new CalendarAlarm();
            Same.Fields.Add(new KeyValueField("Test", Array.Empty<KeyValueParameter>(), "Val"));
            Same.Fields.Add(new KeyValueField("Test2", Array.Empty<KeyValueParameter>(), "Val2"));
            var Different = new CalendarAlarm();
            Different.Fields.Add(new KeyValueField("Test", Array.Empty<KeyValueParameter>(), "Val"));
            Different.Fields.Add(new KeyValueField("Test2", Array.Empty<KeyValueParameter>(), "Val3"));

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