namespace Archivist.Tests.Formats.Image
{
    using Archivist.Converters;
    using Archivist.Formats.Image;
    using Archivist.Interfaces;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Xunit;

    public class ImageReaderTests
    {
        private readonly ImageReader _testClass;
        private Convertinator _converter;

        public ImageReaderTests()
        {
            _converter = new Convertinator(new[] { Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>() });
            _testClass = new ImageReader(_converter);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ImageReader(_converter);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CanCallInternalCanRead()
        {
            // Arrange
            var stream = new MemoryStream();

            // Act
            var result = _testClass.InternalCanRead(stream);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public async Task CanCallReadAsync()
        {
            // Arrange
            var stream = new MemoryStream();

            // Act
            var result = await _testClass.ReadAsync(stream);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            Assert.IsType<byte[]>(_testClass.HeaderInfo);

            throw new NotImplementedException("Create or modify test");
        }
    }
}