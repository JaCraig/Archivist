namespace Archivist.Tests.Formats.Image
{
    using Archivist.Converters;
    using Archivist.Formats.Image;
    using Archivist.Interfaces;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class ImageFormatTests
    {
        private readonly ImageFormat _testClass;
        private Convertinator _converter;

        public ImageFormatTests()
        {
            _converter = new Convertinator(new[] { Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>(), Substitute.For<IDataConverter>() });
            _testClass = new ImageFormat(_converter);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ImageFormat(_converter);

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Act
            var result = _testClass.Extensions;

            // Assert
            Assert.NotNull(result);
            Assert.Contains("GIF", result);
            Assert.Contains("JPG", result);
            Assert.Contains("JPEG", result);
            Assert.Contains("BMP", result);
            Assert.Contains("PNG", result);
            Assert.Contains("WEBP", result);
            Assert.Contains("ICO", result);
            Assert.Contains("WBMP", result);
            Assert.Contains("HEIF", result);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Act
            var result = _testClass.MimeTypes;

            // Assert
            Assert.NotNull(result);
            Assert.Contains("IMAGE/GIF", result);
            Assert.Contains("IMAGE/JPEG", result);
            Assert.Contains("IMAGE/BMP", result);
            Assert.Contains("IMAGE/PNG", result);
            Assert.Contains("IMAGE/WEBP", result);
            Assert.Contains("IMAGE/ICO", result);
            Assert.Contains("IMAGE/WBMP", result);
            Assert.Contains("IMAGE/HEIF", result);
        }
    }
}
