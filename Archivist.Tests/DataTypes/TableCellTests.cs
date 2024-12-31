using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class TableCellTests : TestBaseClass<TableCell>
    {
        public TableCellTests()
        {
            _ = GetServiceProvider();
            _Content = "TestValue388175019";
            _TestClass = new TableCell(_Content);
            TestObject = new TableCell(_Content);
        }

        private readonly string _Content;
        private readonly TableCell _TestClass;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new TableCell("TestValue714641302");

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.True(Result < 0);
        }

        [Fact]
        public void CanCallConvertFrom()
        {
            // Arrange
            var TestClass = new TableCell("TestValue1831879296");

            // Act
            TestClass.ConvertFrom(1234);

            // Assert
            Assert.Equal("1234", TestClass.Content);
        }

        [Fact]
        public void CanCallConvertTo()
        {
            // Arrange
            var TestClass = new TableCell("192");
            // Act
            var Result = TestClass.ConvertTo<int>();

            // Assert
            Assert.Equal(192, Result);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new TableCell("TestValue616470187");
            var Right = new TableCell("TestValue118518131");

            // Act
            var Result = Left == Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new TableCell("TestValue1831879296");

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new TableCell("TestValue1831879296") == default;

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
        public void CanCallEqualsWithTableCell()
        {
            // Arrange
            var Other = new TableCell("TestValue404138676");

            // Act
            var Result = _TestClass.Equals(Other);

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
            var Left = new TableCell("TestValue502010422");
            var Right = new TableCell("TestValue699309394");

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = null >= new TableCell("TestValue1831879296");

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new TableCell("TestValue1831879296") >= null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new TableCell("TestValue30800470");
            var Right = new TableCell("TestValue1996095180");

            // Act
            var Result = Left > Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = null > new TableCell("TestValue1831879296");

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new TableCell("TestValue1831879296") > null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new TableCell("TestValue1624102698");
            var Right = new TableCell("TestValue1991765382");

            // Act
            var Result = Left != Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = null != new TableCell("TestValue1831879296");

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new TableCell("TestValue1831879296") != null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new TableCell("TestValue26732962");
            var Right = new TableCell("TestValue1491139055");

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = null <= new TableCell("TestValue1831879296");

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new TableCell("TestValue1831879296") <= null;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new TableCell("TestValue1059487825");
            var Right = new TableCell("TestValue503506848");

            // Act
            var Result = Left < Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = null < new TableCell("TestValue1831879296");

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new TableCell("TestValue1831879296") < null;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            Assert.Equal(_Content, Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new TableCell(_Content);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidContent(string value) => _ = new TableCell(value);

        [Fact]
        public void CanSetAndGetContent()
        {
            // Arrange
            const string TestValue = "TestValue1110463361";

            // Act
            _TestClass.Content = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Content);
        }

        [Fact]
        public void ContentIsInitializedCorrectly() => Assert.Equal(_Content, _TestClass.Content);

        [Fact]
        public void ImplementsIComparable_TableCell()
        {
            // Arrange
            var BaseValue = new TableCell("TestValue1860730737");
            var EqualToBaseValue = new TableCell("TestValue1860730737");
            var GreaterThanBaseValue = new TableCell("TestValue1860730739");

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_TableCell()
        {
            // Arrange
            var TestClass = new TableCell("TestValue1831879296");
            var Same = new TableCell("TestValue1831879296");
            var Different = new TableCell("TestValue259103760");

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