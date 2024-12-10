using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class StructuredObjectTests : TestBaseClass<StructuredObject>
    {
        public StructuredObjectTests()
        {
            _TestClass = new StructuredObject();
            TestObject = new StructuredObject();
        }

        private readonly StructuredObject _TestClass;

        [Fact]
        public void CanCallAddWithKeyValuePairOfStringAndObject()
        {
            // Arrange
            var Item = new KeyValuePair<string, object?>("Test1234", new object());

            // Act
            _TestClass.Add(Item);

            // Assert
            Assert.True(Assert.IsType<bool>(_TestClass.Contains(Item)));
        }

        [Fact]
        public void CanCallAddWithStringAndObject()
        {
            // Arrange
            const string Key = "TestValue1986306822";
            var Value = new object();

            // Act
            _TestClass.Add(Key, Value);

            // Assert
            Assert.Same(Value, _TestClass[Key]);
        }

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
            var Other = new StructuredObject
            {
                ["TestValue2083265119"] = "TestValue2083265119"
            };
            var TestClass = new StructuredObject
            {
                ["TestValue2083265119"] = "TestValue2083265119"
            };

            // Act
            var Result = TestClass.CompareTo(Other);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallContains()
        {
            // Arrange
            var Item = new KeyValuePair<string, object?>();

            // Act
            var Result = _TestClass.Contains(Item);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallContainsKey()
        {
            // Arrange
            const string Key = "TestValue104013451";

            // Act
            var Result = _TestClass.ContainsKey(Key);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallConvertFrom()
        {
            // Arrange
            const string Obj = "{ \"TestValue2083265119\": \"TestValue2083265119\" }";

            // Act
            _TestClass.ConvertFrom(Obj);
            var Result = _TestClass.GetValue<string>("TestValue2083265119");

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("TestValue2083265119", Result);
        }

        [Fact]
        public void CanCallConvertTo()
        {
            // Arrange
            var TestClass = new StructuredObject()
            {
                ["Property1"] = "TestValue2083265119",
                ["Property2"] = "TestValue2083265118",
                ["Property3"] = "TestValue2083265117"
            };
            // Act
            TestClassType? Result = TestClass.ConvertTo<TestClassType>();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("TestValue2083265119", Result.Property1);
            Assert.Equal("TestValue2083265118", Result.Property2);
            Assert.Equal("TestValue2083265117", Result.Property3);
        }

        [Fact]
        public void CanCallCopyTo()
        {
            // Arrange
            var TestClass = new StructuredObject()
            {
                ["TestValue2083265119"] = "TestValue2083265119",
                ["TestValue2083265118"] = "TestValue2083265118"
            };
            KeyValuePair<string, object?>[] Array = new[] { new KeyValuePair<string, object?>(), new KeyValuePair<string, object?>(), new KeyValuePair<string, object?>() };
            const int ArrayIndex = 1;

            // Act
            TestClass.CopyTo(Array, ArrayIndex);

            // Assert
            Assert.Equal("TestValue2083265119", Array[ArrayIndex].Value);
            Assert.Equal("TestValue2083265118", Array[ArrayIndex + 1].Value);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new StructuredObject();
            var Right = new StructuredObject();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default! == new StructuredObject();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new StructuredObject() == default!;

            // Assert
            Assert.False(Result);
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
        public void CanCallEqualsWithStructuredObject()
        {
            // Arrange
            var Other = new StructuredObject();

            // Act
            var Result = _TestClass.Equals(Other);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGetContent()
        {
            // Arrange
            var TestClass = new StructuredObject();

            // Act
            var Result = TestClass.GetContent();

            // Assert
            Assert.Equal("{}", Result);
        }

        [Fact]
        public void CanCallGetEnumeratorForIEnumerableWithNoParameters()
        {
            // Arrange
            var TestClass = new StructuredObject();

            // Act
            IEnumerator Result = ((IEnumerable)TestClass).GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Arrange
            var TestClass = new StructuredObject();

            // Act
            IEnumerator<KeyValuePair<string, object?>> Result = TestClass.GetEnumerator();

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
        public void CanCallGetValue()
        {
            // Arrange
            const string Key = "TestValue607058881";

            // Act
            var Result = _TestClass.GetValue<string>(Key);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var Left = new StructuredObject();
            var Right = new StructuredObject();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(StructuredObject) >= new StructuredObject();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new StructuredObject() >= default(StructuredObject);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new StructuredObject();
            var Right = new StructuredObject();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(StructuredObject) > new StructuredObject();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new StructuredObject() > default(StructuredObject);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new StructuredObject();
            var Right = new StructuredObject();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default! != new StructuredObject();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new StructuredObject() != default!;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new StructuredObject();
            var Right = new StructuredObject();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(StructuredObject) <= new StructuredObject();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new StructuredObject() <= default(StructuredObject);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new StructuredObject();
            var Right = new StructuredObject();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(StructuredObject) < new StructuredObject();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new StructuredObject() < default(StructuredObject);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallRemoveWithKeyValuePairOfStringAndObject()
        {
            // Arrange
            var Item = new KeyValuePair<string, object?>("A", new object());

            // Act
            var Result = _TestClass.Remove(Item);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallRemoveWithString()
        {
            // Arrange
            const string Key = "TestValue1161925247";

            // Act
            var Result = _TestClass.Remove(Key);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallSetValue()
        {
            // Arrange
            const string Key = "TestValue1373163283";
            const string Value = "TestValue2013408480";

            // Act
            StructuredObject Result = _TestClass.SetValue(Key, Value);
            var ActualValue = _TestClass.GetValue<string>(Key);

            // Assert
            Assert.Same(_TestClass, Result);
            Assert.Equal(Value, ActualValue);
        }

        [Fact]
        public void CanCallTryGetValueWithStringAndObject()
        {
            // Arrange
            const string Key = "TestValue627025032";

            // Act
            var Result = _TestClass.TryGetValue(Key, out var Value);

            // Assert
            Assert.False(Result);
            Assert.Null(Value);
        }

        [Fact]
        public void CanCallTryGetValueWithStringAndTObject()
        {
            // Arrange
            const string Key = "TestValue1606255958";

            // Act
            var Result = _TestClass.TryGetValue<string>(Key, out var Value);

            // Assert
            Assert.False(Result);
            Assert.Null(Value);
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
        public void CanGetKeys()
        {
            // Assert
            ICollection<string> Result = Assert.IsAssignableFrom<ICollection<string>>(_TestClass.Keys);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetValues()
        {
            // Assert
            ICollection<object> Result = Assert.IsAssignableFrom<ICollection<object>>(_TestClass.Values);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanSetAndGetIndexer()
        {
            var TestValue = new object();
            Assert.Null(_TestClass["TestValue894956329"]);
            _TestClass["TestValue894956329"] = TestValue;
            Assert.Same(TestValue, _TestClass["TestValue894956329"]);
        }

        [Fact]
        public void ImplementsIComparable_StructuredObject()
        {
            // Arrange
            var BaseValue = new StructuredObject();
            BaseValue.ConvertFrom("{ \"TestValue2083265119\": \"TestValue2083265119\" }");
            var EqualToBaseValue = new StructuredObject();
            EqualToBaseValue.ConvertFrom("{ \"TestValue2083265119\": \"TestValue2083265119\" }");
            var GreaterThanBaseValue = new StructuredObject();
            GreaterThanBaseValue.ConvertFrom("{ \"TestValue2083265119\": \"TestValue2083265111\" }");

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_StructuredObject()
        {
            // Arrange
            var TestClass = new StructuredObject();
            TestClass.ConvertFrom("{ \"TestValue2083265119\": \"TestValue2083265111\" }");
            var Same = new StructuredObject();
            Same.ConvertFrom("{ \"TestValue2083265119\": \"TestValue2083265111\" }");
            var Different = new StructuredObject();
            Different.ConvertFrom("{ \"TestValue2083265119\": \"TestValue2083265119\" }");

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

    public class TestClassType
    {
        public string? Property1 { get; set; }
        public string? Property2 { get; set; }
        public string? Property3 { get; set; }
    }
}