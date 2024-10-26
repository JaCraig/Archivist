using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
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
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new Image();
            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new Image() == default;
            // Assert
            Assert.False(Result);
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
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(Image) >= new Image();
            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new Image() >= default(Image);
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
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(Image) > new Image();
            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new Image() > default(Image);
            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new Image();
            var Right = new Image();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new Image();
            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new Image() != default;
            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new Image();
            var Right = new Image();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(Image) <= new Image();
            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new Image() <= default(Image);
            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new Image();
            var Right = new Image();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(Image) < new Image();
            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new Image() < default(Image);
            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallToFileType()
        {
            // Act
            Image? Result = _TestClass.ToFileType<Image>();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new Image();

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new Image(_Converter);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanSetAndGetBytesPerPixel()
        {
            // Arrange
            const int TestValue = 1705941639;

            // Act
            _TestClass.BytesPerPixel = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.BytesPerPixel);
        }

        [Fact]
        public void CanSetAndGetData()
        {
            // Arrange
            var TestValue = new byte[] { 226, 77, 47, 157 };

            // Act
            _TestClass.Data = TestValue;

            // Assert
            Assert.Same(TestValue, _TestClass.Data);
        }

        [Fact]
        public void CanSetAndGetDescription()
        {
            // Arrange
            const string TestValue = "TestValue1140185640";

            // Act
            _TestClass.Description = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Description);
        }

        [Fact]
        public void CanSetAndGetHeight()
        {
            // Arrange
            const int TestValue = 1299733747;

            // Act
            _TestClass.Height = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Height);
        }

        [Fact]
        public void CanSetAndGetImageType()
        {
            // Arrange
            const string TestValue = "TestValue1850853053";

            // Act
            _TestClass.ImageType = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.ImageType);
        }

        [Fact]
        public void CanSetAndGetWidth()
        {
            // Arrange
            const int TestValue = 1265887064;

            // Act
            _TestClass.Width = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Width);
        }

        [Fact]
        public void ImplementsIComparable_Image()
        {
            // Arrange
            var BaseValue = new Image
            {
                BytesPerPixel = 1,
                Width = 1,
                Data = new byte[] { 1, 2, 3, 4 },
                Description = "Description"
            };

            var EqualToBaseValue = new Image
            {
                BytesPerPixel = 1,
                Width = 1,
                Data = new byte[] { 1, 2, 3, 4 },
                Description = "Description"
            };

            var GreaterThanBaseValue = new Image
            {
                BytesPerPixel = BaseValue.BytesPerPixel + 1,
                Width = BaseValue.Width + 1,
                Data = new byte[] { 1, 2, 3, 4 },
                Description = "Description"
            };

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_Image()
        {
            // Arrange
            var TestClass = new Image
            {
                BytesPerPixel = 1,
                Width = 1,
                Data = new byte[] { 1, 2, 3, 4 },
                Description = "Description"
            };
            var Same = new Image
            {
                BytesPerPixel = 1,
                Width = 1,
                Data = new byte[] { 1, 2, 3, 4 },
                Description = "Description"
            };

            var Different = new Image
            {
                BytesPerPixel = 2,
                Width = 2,
                Data = new byte[] { 5, 6, 7, 8 },
                Description = "Different"
            };

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