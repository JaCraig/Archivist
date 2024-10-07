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
            // Assert
            Assert.IsType<string[]>(_testClass.Extensions);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            Assert.IsType<string[]>(_testClass.MimeTypes);

            throw new NotImplementedException("Create or modify test");
        }
    }
}