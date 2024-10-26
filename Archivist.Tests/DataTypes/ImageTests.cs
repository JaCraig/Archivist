using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class ImageTests : TestBaseClass<Image>
    {
        public ImageTests()
        {
            _Converter = new Convertinator(new[] { Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>() });
            _TestClass = new Image(_Converter);
            TestObject = new Image(_Converter);
        }

        private readonly Convertinator _Converter;
        private readonly Image _TestClass;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var TestClass = new Image();
            var Other = new Image();

            // Act
            var Result = TestClass.CompareTo(Other);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new Image();
            var Right = new Image();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualsWithImage()
        {
            // Arrange
            var TestClass = new Image();
            var Other = new Image();

            // Act
            var Result = TestClass.Equals(Other);

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
            // Act
            var Result = _TestClass.GetContent();

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
            var Left = new Image();
            var Right = new Image();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new Image();
            var Right = new Image();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var left = new Image();
            var right = new Image();

            // Act
            var result = left != right;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var left = new Image();
            var right = new Image();

            // Act
            var result = left <= right;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var left = new Image();
            var right = new Image();

            // Act
            var result = left < right;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanCallToFileType()
        {
            // Act
            var result = _TestClass.ToFileType<Image>();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new Image();

            // Assert
            Assert.NotNull(instance);

            // Act
            instance = new Image(_Converter);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotCallEqualityOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default == new Image(); });

        [Fact]
        public void CannotCallEqualityOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Image() == default; });

        [Fact]
        public void CannotCallGreaterThanEqualToOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default(Image) >= new Image(); });

        [Fact]
        public void CannotCallGreaterThanEqualToOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Image() >= default(Image); });

        [Fact]
        public void CannotCallGreaterThanOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default(Image) > new Image(); });

        [Fact]
        public void CannotCallGreaterThanOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Image() > default(Image); });

        [Fact]
        public void CannotCallInequalityOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default != new Image(); });

        [Fact]
        public void CannotCallInequalityOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Image() != default; });

        [Fact]
        public void CannotCallLessThanEqualToOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default(Image) <= new Image(); });

        [Fact]
        public void CannotCallLessThanEqualToOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Image() <= default(Image); });

        [Fact]
        public void CannotCallLessThanOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default(Image) < new Image(); });

        [Fact]
        public void CannotCallLessThanOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Image() < default(Image); });

        [Fact]
        public void CanSetAndGetBytesPerPixel()
        {
            // Arrange
            var testValue = 1705941639;

            // Act
            _TestClass.BytesPerPixel = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.BytesPerPixel);
        }

        [Fact]
        public void CanSetAndGetData()
        {
            // Arrange
            var testValue = new byte[] { 226, 77, 47, 157 };

            // Act
            _TestClass.Data = testValue;

            // Assert
            Assert.Same(testValue, _TestClass.Data);
        }

        [Fact]
        public void CanSetAndGetDescription()
        {
            // Arrange
            var testValue = "TestValue1140185640";

            // Act
            _TestClass.Description = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Description);
        }

        [Fact]
        public void CanSetAndGetHeight()
        {
            // Arrange
            var testValue = 1299733747;

            // Act
            _TestClass.Height = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Height);
        }

        [Fact]
        public void CanSetAndGetImageType()
        {
            // Arrange
            var testValue = "TestValue1850853053";

            // Act
            _TestClass.ImageType = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.ImageType);
        }

        [Fact]
        public void CanSetAndGetWidth()
        {
            // Arrange
            var testValue = 1265887064;

            // Act
            _TestClass.Width = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Width);
        }

        [Fact]
        public void ImplementsIComparable_Image()
        {
            // Arrange
            var baseValue = default(Image);
            var equalToBaseValue = default(Image);
            var greaterThanBaseValue = default(Image);

            // Assert
            Assert.Equal(0, baseValue.CompareTo(equalToBaseValue));
            Assert.True(baseValue.CompareTo(greaterThanBaseValue) < 0);
            Assert.True(greaterThanBaseValue.CompareTo(baseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_Image()
        {
            // Arrange
            var same = new Image();
            var different = new Image();

            // Assert
            Assert.False(_TestClass.Equals(default(object)));
            Assert.False(_TestClass.Equals(new object()));
            Assert.True(_TestClass.Equals((object)same));
            Assert.False(_TestClass.Equals((object)different));
            Assert.True(_TestClass.Equals(same));
            Assert.False(_TestClass.Equals(different));
            Assert.Equal(same.GetHashCode(), _TestClass.GetHashCode());
            Assert.NotEqual(different.GetHashCode(), _TestClass.GetHashCode());
            Assert.True(_TestClass == same);
            Assert.False(_TestClass == different);
            Assert.False(_TestClass != same);
            Assert.True(_TestClass != different);
        }
    }
}
