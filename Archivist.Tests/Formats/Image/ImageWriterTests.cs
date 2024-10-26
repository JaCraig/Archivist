using Archivist.Formats.Image;
using Archivist.Tests.BaseClasses;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Image
{
    public class ImageWriterTests : TestBaseClass<ImageWriter>
    {
        public ImageWriterTests()
        {
            _TestClass = new ImageWriter();
            TestObject = new ImageWriter();
        }

        private readonly ImageWriter _TestClass;

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            var File = new Archivist.DataTypes.Image
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
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.True(Result);
            Assert.NotEqual(0, Stream.Length);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new ImageWriter();

            // Assert
            Assert.NotNull(Instance);
        }
    }
}