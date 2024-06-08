using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class CardFieldTests : TestBaseClass<CardField>
    {
        public CardFieldTests()
        {
            _Property = "TestValue1888918131";
            _Parameters = new[] { new CardFieldParameter("TestValue685308228", "TestValue180072147"), new CardFieldParameter("TestValue90202416", "TestValue350054617"), new CardFieldParameter("TestValue2036030522", "TestValue437133569") };
            _Value = "TestValue1524892779";
            _TestClass = new CardField(_Property, _Parameters, _Value);
            TestObject = new CardField(_Property, _Parameters, _Value);
        }

        private readonly IEnumerable<CardFieldParameter> _Parameters;
        private readonly string _Property;
        private readonly CardField _TestClass;
        private readonly string _Value;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new CardField("TestValue1679602629", new[] { new CardFieldParameter("TestValue1218056838", "TestValue2012520448"), new CardFieldParameter("TestValue1719592574", "TestValue996400362"), new CardFieldParameter("TestValue1418274380", "TestValue1888673702") }, "TestValue353578718");

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.True(Result < 0);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new CardField("TestValue62789295", new[] { new CardFieldParameter("TestValue514868294", "TestValue243275833"), new CardFieldParameter("TestValue1297108557", "TestValue795407286"), new CardFieldParameter("TestValue721727863", "TestValue97718742") }, "TestValue1262482019");
            var Right = new CardField("TestValue2117745589", new[] { new CardFieldParameter("TestValue88030829", "TestValue5559796"), new CardFieldParameter("TestValue17645645", "TestValue2011956806"), new CardFieldParameter("TestValue1744956684", "TestValue1483426637") }, "TestValue708200301");

            // Act
            var Result = Left == Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478");

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478") == default;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithCardField()
        {
            // Arrange
            var Other = new CardField("TestValue1404791298", new[] { new CardFieldParameter("TestValue1902554862", "TestValue342956205"), new CardFieldParameter("TestValue1251609983", "TestValue198814801"), new CardFieldParameter("TestValue496712691", "TestValue191137234") }, "TestValue116127913");

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
            var Left = new CardField("TestValue276964821", new[] { new CardFieldParameter("TestValue981525434", "TestValue1708847936"), new CardFieldParameter("TestValue1898918254", "TestValue1804391860"), new CardFieldParameter("TestValue1528081636", "TestValue694951054") }, "TestValue1633475861");
            var Right = new CardField("TestValue108582824", new[] { new CardFieldParameter("TestValue1399346484", "TestValue310317565"), new CardFieldParameter("TestValue1139666932", "TestValue739843773"), new CardFieldParameter("TestValue41285847", "TestValue1939350330") }, "TestValue1887457520");

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = null >= new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478");

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478") >= null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new CardField("TestValue456281356", new[] { new CardFieldParameter("TestValue113038552", "TestValue2088048471"), new CardFieldParameter("TestValue2102828648", "TestValue13249247"), new CardFieldParameter("TestValue1017765101", "TestValue2134146511") }, "TestValue1239257069");
            var Right = new CardField("TestValue873160885", new[] { new CardFieldParameter("TestValue1606519052", "TestValue2017009461"), new CardFieldParameter("TestValue1865504150", "TestValue1447243795"), new CardFieldParameter("TestValue481725564", "TestValue13144385") }, "TestValue356077654");

            // Act
            var Result = Left > Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = null > new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478");

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478") >= null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new CardField("TestValue356595556", new[] { new CardFieldParameter("TestValue2008956717", "TestValue709232893"), new CardFieldParameter("TestValue771736398", "TestValue878586843"), new CardFieldParameter("TestValue1819518231", "TestValue1736037105") }, "TestValue1500573810");
            var Right = new CardField("TestValue1639366598", new[] { new CardFieldParameter("TestValue208694106", "TestValue1547578824"), new CardFieldParameter("TestValue628839645", "TestValue1740597998"), new CardFieldParameter("TestValue563027124", "TestValue60771295") }, "TestValue1278811141");

            // Act
            var Result = Left != Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = null != new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478");

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478") != null;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new CardField("TestValue544430423", new[] { new CardFieldParameter("TestValue721070646", "TestValue464426771"), new CardFieldParameter("TestValue38736504", "TestValue744045432"), new CardFieldParameter("TestValue579301548", "TestValue1241366769") }, "TestValue32443107");
            var Right = new CardField("TestValue353383941", new[] { new CardFieldParameter("TestValue309491324", "TestValue422322171"), new CardFieldParameter("TestValue3354119", "TestValue470213295"), new CardFieldParameter("TestValue256525080", "TestValue167390011") }, "TestValue782070150");

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = null <= new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478");

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478") <= null;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new CardField("TestValue164412874", new[] { new CardFieldParameter("TestValue1701861494", "TestValue1966465339"), new CardFieldParameter("TestValue1628953394", "TestValue37444372"), new CardFieldParameter("TestValue1098692938", "TestValue1633970769") }, "TestValue2007568045");
            var Right = new CardField("TestValue1629503380", new[] { new CardFieldParameter("TestValue511946638", "TestValue80225976"), new CardFieldParameter("TestValue787706256", "TestValue2018179203"), new CardFieldParameter("TestValue1259834057", "TestValue760464367") }, "TestValue841764174");

            // Act
            var Result = Left < Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = null < new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478");

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new CardField("TestValue503757315", new[] { new CardFieldParameter("TestValue95245688", "TestValue491119906"), new CardFieldParameter("TestValue1183478450", "TestValue598702337"), new CardFieldParameter("TestValue609369587", "TestValue1906789997") }, "TestValue1239110478") < null;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Results = _TestClass.ToString();

            // Assert
            Assert.Equal($"{_Property} ({string.Join(';', _Parameters)}): {_Value}", Results);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new CardField(_Property, _Parameters, _Value);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidProperty(string? value) => _ = new CardField(value, _Parameters, _Value);

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidValue(string value) => _ = new CardField(_Property, _Parameters, value);

        [Fact]
        public void CanSetAndGetProperty()
        {
            // Arrange
            const string TestValue = "TestValue1187036884";

            // Act
            _TestClass.Property = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Property);
        }

        [Fact]
        public void CanSetAndGetValue()
        {
            // Arrange
            const string TestValue = "TestValue1517126714";

            // Act
            _TestClass.Value = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Value);
        }

        [Fact]
        public void ImplementsIComparable_CardField()
        {
            // Arrange
            var BaseValue = new CardField("TestValue1323220869", new[] { new CardFieldParameter("TestValue355550108", "TestValue1817989407"), new CardFieldParameter("TestValue175577247", "TestValue1490426464"), new CardFieldParameter("TestValue746677265", "TestValue1870429336") }, "TestValue365073965");
            var EqualToBaseValue = new CardField("TestValue1323220869", new[] { new CardFieldParameter("TestValue355550108", "TestValue1817989407"), new CardFieldParameter("TestValue175577247", "TestValue1490426464"), new CardFieldParameter("TestValue746677265", "TestValue1870429336") }, "TestValue365073965");
            var LessThanBaseValue = new CardField("TestValue1456359947", new[] { new CardFieldParameter("TestValue1783094262", "TestValue927544370"), new CardFieldParameter("TestValue1069951452", "TestValue170888559"), new CardFieldParameter("TestValue1394372672", "TestValue1178135252") }, "TestValue2010562131");

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(LessThanBaseValue) > 0);
            Assert.True(LessThanBaseValue.CompareTo(BaseValue) < 0);
        }

        [Fact]
        public void ImplementsIEquatable_CardField()
        {
            // Arrange
            var TestClass = new CardField("TestValue1323220869", new[] { new CardFieldParameter("TestValue355550108", "TestValue1817989407"), new CardFieldParameter("TestValue175577247", "TestValue1490426464"), new CardFieldParameter("TestValue746677265", "TestValue1870429336") }, "TestValue365073965");
            var Same = new CardField("TestValue1323220869", new[] { new CardFieldParameter("TestValue355550108", "TestValue1817989407"), new CardFieldParameter("TestValue175577247", "TestValue1490426464"), new CardFieldParameter("TestValue746677265", "TestValue1870429336") }, "TestValue365073965");
            var Different = new CardField("TestValue1456359947", new[] { new CardFieldParameter("TestValue1783094262", "TestValue927544370"), new CardFieldParameter("TestValue1069951452", "TestValue170888559"), new CardFieldParameter("TestValue1394372672", "TestValue1178135252") }, "TestValue2010562131");

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

        [Fact]
        public void ParametersIsInitializedCorrectly() => Assert.Equal(_Parameters, new CardField(_Property, _Parameters, _Value).Parameters);

        [Fact]
        public void PropertyIsInitializedCorrectly() => Assert.Equal(_Property, new CardField(_Property, _Parameters, _Value).Property);

        [Fact]
        public void ValueIsInitializedCorrectly() => Assert.Equal(_Value, new CardField(_Property, _Parameters, _Value).Value);
    }
}