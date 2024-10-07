using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Enums;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class AnythingToImageConverterTests : TestBaseClass<AnythingToImageConverter>
    {
        public AnythingToImageConverterTests()
        {
            _TestClass = new AnythingToImageConverter();
            TestObject = new AnythingToImageConverter();
        }

        private readonly AnythingToImageConverter _TestClass;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Feed);
            Type Destination = typeof(Image);

            // Act
            var Result = _TestClass.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallConvertWithFile()
        {
            // Arrange
            IGenericFile File = new Card
            {
                Title = "Test"
            };
            File.Metadata.Add(CommonImageMetadataFields.Description, "Test description");
            File.Metadata.Add(CommonImageMetadataFields.Photographer, "Test Photographer");
            File.Metadata.Add(CommonImageMetadataFields.DateTimeOriginal, "2021-01-01");

            // Act
            Image? Result = AnythingToImageConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Image ImageResult = Assert.IsType<Image>(Result);
            Assert.Equal("Test", ImageResult.Title);
            Assert.Equal("Test description", ImageResult.Description);
            Assert.Equal("Test Photographer", ImageResult.Metadata[CommonImageMetadataFields.Photographer]);
            Assert.Equal("2021-01-01", ImageResult.Metadata[CommonImageMetadataFields.DateTimeOriginal]);
        }

        [Fact]
        public void CanCallConvertWithObjectAndType()
        {
            // Arrange
            var Source = new Card
            {
                Title = "Test"
            };
            Source.Metadata.Add(CommonImageMetadataFields.Description, "Test description");
            Source.Metadata.Add(CommonImageMetadataFields.Photographer, "Test Photographer");
            Source.Metadata.Add(CommonImageMetadataFields.DateTimeOriginal, "2021-01-01");

            Type Destination = typeof(Image);

            // Act
            var Result = _TestClass.Convert(Source, Destination);

            // Assert
            Assert.NotNull(Result);
            Image ImageResult = Assert.IsType<Image>(Result);
            Assert.Equal("Test", ImageResult.Title);
            Assert.Equal("Test description", ImageResult.Description);
            Assert.Equal("Test Photographer", ImageResult.Metadata[CommonImageMetadataFields.Photographer]);
            Assert.Equal("2021-01-01", ImageResult.Metadata[CommonImageMetadataFields.DateTimeOriginal]);
        }
    }
}