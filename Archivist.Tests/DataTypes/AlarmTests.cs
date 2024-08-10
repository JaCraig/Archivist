namespace Archivist.Tests.DataTypes
{
    using Archivist.DataTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Xunit;

    public class AlarmTests
    {
        private Alarm _testClass;

        public AlarmTests()
        {
            _testClass = new Alarm();
        }

        [Fact]
        public void ImplementsIEquatable_Alarm()
        {
            // Arrange
            var same = new Alarm();
            var different = new Alarm();

            // Assert
            Assert.False(_testClass.Equals(default(object)));
            Assert.False(_testClass.Equals(new object()));
            Assert.True(_testClass.Equals((object)same));
            Assert.False(_testClass.Equals((object)different));
            Assert.True(_testClass.Equals(same));
            Assert.False(_testClass.Equals(different));
            Assert.Equal(same.GetHashCode(), _testClass.GetHashCode());
            Assert.NotEqual(different.GetHashCode(), _testClass.GetHashCode());
            Assert.True(_testClass == same);
            Assert.False(_testClass == different);
            Assert.False(_testClass != same);
            Assert.True(_testClass != different);
        }

        [Fact]
        public void ImplementsIComparable_Alarm()
        {
            // Arrange
            Alarm baseValue = default(Alarm);
            Alarm equalToBaseValue = default(Alarm);
            Alarm greaterThanBaseValue = default(Alarm);

            // Assert
            Assert.Equal(0, baseValue.CompareTo(equalToBaseValue));
            Assert.True(baseValue.CompareTo(greaterThanBaseValue) < 0);
            Assert.True(greaterThanBaseValue.CompareTo(baseValue) > 0);
        }

        [Fact]
        public void ImplementsIEnumerable_KeyValueField()
        {
            // Arrange
            Alarm enumerable = default(Alarm);
            int expectedCount = -1;
            int actualCount = 0;

            // Act
            using (var enumerator = enumerable.GetEnumerator())
            {
                Assert.NotNull(enumerator);
                while (enumerator.MoveNext())
                {
                    actualCount++;
                    Assert.IsType<KeyValueField>(enumerator.Current);
                }
            }

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var other = new Alarm();

            // Act
            var result = _testClass.CompareTo(other);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallEqualsWithAlarm()
        {
            // Arrange
            var other = new Alarm();

            // Act
            var result = _testClass.Equals(other);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallEqualsWithObj()
        {
            // Arrange
            var obj = new object();

            // Act
            var result = _testClass.Equals(obj);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Act
            var result = _testClass.GetEnumerator();

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var result = _testClass.GetHashCode();

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallGetEnumeratorForIEnumerableWithNoParameters()
        {
            // Act
            var result = ((IEnumerable)_testClass).GetEnumerator();

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var left = new Alarm();
            var right = new Alarm();

            // Act
            var result = left != right;

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CannotCallInequalityOperatorWithNullLeft()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = default(Alarm) != new Alarm(); });
        }

        [Fact]
        public void CannotCallInequalityOperatorWithNullRight()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = new Alarm() != default(Alarm); });
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var left = new Alarm();
            var right = new Alarm();

            // Act
            var result = left < right;

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CannotCallLessThanOperatorWithNullLeft()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = default(Alarm) < new Alarm(); });
        }

        [Fact]
        public void CannotCallLessThanOperatorWithNullRight()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = new Alarm() < default(Alarm); });
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var left = new Alarm();
            var right = new Alarm();

            // Act
            var result = left <= right;

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CannotCallLessThanEqualToOperatorWithNullLeft()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = default(Alarm) <= new Alarm(); });
        }

        [Fact]
        public void CannotCallLessThanEqualToOperatorWithNullRight()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = new Alarm() <= default(Alarm); });
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var left = new Alarm();
            var right = new Alarm();

            // Act
            var result = left == right;

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CannotCallEqualityOperatorWithNullLeft()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = default(Alarm) == new Alarm(); });
        }

        [Fact]
        public void CannotCallEqualityOperatorWithNullRight()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = new Alarm() == default(Alarm); });
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var left = new Alarm();
            var right = new Alarm();

            // Act
            var result = left > right;

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CannotCallGreaterThanOperatorWithNullLeft()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = default(Alarm) > new Alarm(); });
        }

        [Fact]
        public void CannotCallGreaterThanOperatorWithNullRight()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = new Alarm() > default(Alarm); });
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var left = new Alarm();
            var right = new Alarm();

            // Act
            var result = left >= right;

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CannotCallGreaterThanEqualToOperatorWithNullLeft()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = default(Alarm) >= new Alarm(); });
        }

        [Fact]
        public void CannotCallGreaterThanEqualToOperatorWithNullRight()
        {
            Assert.Throws<ArgumentNullException>(() => { var result = new Alarm() >= default(Alarm); });
        }

        [Fact]
        public void CanGetFields()
        {
            // Assert
            Assert.IsType<List<KeyValueField>>(_testClass.Fields);

            throw new NotImplementedException("Create or modify test");
        }
    }
}