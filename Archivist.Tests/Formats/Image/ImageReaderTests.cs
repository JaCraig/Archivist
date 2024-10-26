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
    public class ImageReaderTests : TestBaseClass<ImageReader>
    {
        public ImageReaderTests()
        {
            _Converter = new Convertinator(new[] { Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>() });
            _TestClass = new ImageReader(_Converter);
            TestObject = new ImageReader(_Converter);
        }

        private readonly Convertinator _Converter;
        private readonly ImageReader _TestClass;

        [Fact]
        public void CanCallInternalCanRead()
        {
            // Arrange
            var Stream = new MemoryStream();

            // Act
            var Result = _TestClass.InternalCanRead(Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task CanCallReadAsync()
        {
            // Arrange
            var Stream = new MemoryStream();

            // Act
            IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
        }

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
            var Instance = new ImageReader(_Converter);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            _ = Assert.IsType<byte[]>(_TestClass.HeaderInfo);
            Assert.Empty(_TestClass.HeaderInfo);
        }
    }
}