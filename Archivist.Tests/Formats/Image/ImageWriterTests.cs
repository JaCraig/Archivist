namespace Archivist.Tests.Formats.Image
{
    using Archivist.DataTypes;
    using Archivist.Formats.Image;
    using Archivist.Interfaces;
    using NSubstitute;
    using System.IO;
    using System.Threading.Tasks;
    using Xunit;

    public class ImageWriterTests
    {
        private readonly ImageWriter _testClass;

        public ImageWriterTests()
        {
            _testClass = new ImageWriter();
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ImageWriter();

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            var file = new Image
            {
                Width = 2,
                Height = 2,
                BytesPerPixel = 4,
                Data = new byte[]
                {
                    255, 0, 0, 255, // Red
                    0, 255, 0, 255, // Green
                    0, 0, 255, 255, // Blue
                    255, 255, 0, 255 // Yellow
                },
                ImageType = "png"
            };
            var stream = new MemoryStream();

            // Act
            var result = await _testClass.WriteAsync(file, stream);

            // Assert
            Assert.True(result);
            Assert.NotEqual(0, stream.Length);
        }
    }
}
