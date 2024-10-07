namespace Archivist.Tests.Formats.Image
{
    using Archivist.Formats.Image;
    using Archivist.Interfaces;
    using NSubstitute;
    using System;
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
            var @file = Substitute.For<IGenericFile>();
            var stream = new MemoryStream();

            // Act
            var result = await _testClass.WriteAsync(file, stream);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }
    }
}