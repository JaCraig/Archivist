using Archivist.Converters;
using Archivist.Enums;
using Archivist.Formats.Image;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Image
{
    public class ImageFormatTests : TestBaseClass<ImageFormat>
    {
        public ImageFormatTests()
        {
            _Converter = new Convertinator(new[] { Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>() });
            _TestClass = new ImageFormat(_Converter);
            TestObject = new ImageFormat(_Converter);
        }

        private readonly Convertinator _Converter;
        private readonly ImageFormat _TestClass;

        [Fact]
        public async Task CanCallReadAsyncFromFileAsync()
        {
            // Arrange
            var TestData = new FileStream("./TestData/TestJPG.jpg", FileMode.Open);

            // Act
            IGenericFile? Result = await _TestClass.ReadAsync(TestData);

            // Assert
            Assert.NotNull(Result);
            Archivist.DataTypes.Image ResultImage = Assert.IsType<Archivist.DataTypes.Image>(Result);
            Assert.Equal(1024, ResultImage.Width);
            Assert.Equal(1024, ResultImage.Height);
            Assert.Equal(4, ResultImage.BytesPerPixel);
            Assert.Equal(ImageTypes.Jpg, ResultImage.ImageType);
            Assert.NotEmpty(ResultImage.Data);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new ImageFormat(_Converter);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Act
            var Result = _TestClass.Extensions;

            // Assert
            Assert.NotNull(Result);
            Assert.Contains("GIF", Result);
            Assert.Contains("JPG", Result);
            Assert.Contains("JPEG", Result);
            Assert.Contains("BMP", Result);
            Assert.Contains("PNG", Result);
            Assert.Contains("WEBP", Result);
            Assert.Contains("ICO", Result);
            Assert.Contains("WBMP", Result);
            Assert.Contains("HEIF", Result);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Act
            var Result = _TestClass.MimeTypes;

            // Assert
            Assert.NotNull(Result);
            Assert.Contains("IMAGE/GIF", Result);
            Assert.Contains("IMAGE/JPEG", Result);
            Assert.Contains("IMAGE/BMP", Result);
            Assert.Contains("IMAGE/PNG", Result);
            Assert.Contains("IMAGE/WEBP", Result);
            Assert.Contains("IMAGE/ICO", Result);
            Assert.Contains("IMAGE/WBMP", Result);
            Assert.Contains("IMAGE/HEIF", Result);
        }
    }
}