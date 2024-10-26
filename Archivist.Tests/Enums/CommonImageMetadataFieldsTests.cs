using Archivist.Enums;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Enums
{
    public class CommonImageMetadataFieldsTests : TestBaseClass
    {
        protected override Type? ObjectType => typeof(CommonImageMetadataFields);

        [Fact]
        public void CanGetArtist()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Artist);
            Assert.Equal("Artist", CommonImageMetadataFields.Artist);
        }

        [Fact]
        public void CanGetBrightness()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Brightness);
            Assert.Equal("Brightness", CommonImageMetadataFields.Brightness);
        }

        [Fact]
        public void CanGetContrast()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Contrast);
            Assert.Equal("Contrast", CommonImageMetadataFields.Contrast);
        }

        [Fact]
        public void CanGetConverter()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Converter);
            Assert.Equal("Converter", CommonImageMetadataFields.Converter);
        }

        [Fact]
        public void CanGetCopyright()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Copyright);
            Assert.Equal("Copyright", CommonImageMetadataFields.Copyright);
        }

        [Fact]
        public void CanGetDateTimeOriginal()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.DateTimeOriginal);
            Assert.Equal("DateTimeOriginal", CommonImageMetadataFields.DateTimeOriginal);
        }

        [Fact]
        public void CanGetDescription()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Description);
            Assert.Equal("ImageDescription", CommonImageMetadataFields.Description);
        }

        [Fact]
        public void CanGetDocumentName()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.DocumentName);
            Assert.Equal("DocumentName", CommonImageMetadataFields.DocumentName);
        }

        [Fact]
        public void CanGetExposure()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Exposure);
            Assert.Equal("Exposure", CommonImageMetadataFields.Exposure);
        }

        [Fact]
        public void CanGetHostComputer()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.HostComputer);
            Assert.Equal("HostComputer", CommonImageMetadataFields.HostComputer);
        }

        [Fact]
        public void CanGetImageType()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.ImageType);
            Assert.Equal("ImageType", CommonImageMetadataFields.ImageType);
        }

        [Fact]
        public void CanGetMake()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Make);
            Assert.Equal("Make", CommonImageMetadataFields.Make);
        }

        [Fact]
        public void CanGetModel()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Model);
            Assert.Equal("Model", CommonImageMetadataFields.Model);
        }

        [Fact]
        public void CanGetMoireFilter()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.MoireFilter);
            Assert.Equal("MoireFilter", CommonImageMetadataFields.MoireFilter);
        }

        [Fact]
        public void CanGetOffsetTimeOriginal()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.OffsetTimeOriginal);
            Assert.Equal("OffsetTimeOriginal", CommonImageMetadataFields.OffsetTimeOriginal);
        }

        [Fact]
        public void CanGetOrientation()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Orientation);
            Assert.Equal("Orientation", CommonImageMetadataFields.Orientation);
        }

        [Fact]
        public void CanGetOwnerName()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.OwnerName);
            Assert.Equal("OwnerName", CommonImageMetadataFields.OwnerName);
        }

        [Fact]
        public void CanGetPhotographer()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Photographer);
            Assert.Equal("Photographer", CommonImageMetadataFields.Photographer);
        }

        [Fact]
        public void CanGetProcessingSoftware()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.ProcessingSoftware);
            Assert.Equal("ProcessingSoftware", CommonImageMetadataFields.ProcessingSoftware);
        }

        [Fact]
        public void CanGetRawFile()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.RawFile);
            Assert.Equal("RawFile", CommonImageMetadataFields.RawFile);
        }

        [Fact]
        public void CanGetSaturation()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Saturation);
            Assert.Equal("Saturation", CommonImageMetadataFields.Saturation);
        }

        [Fact]
        public void CanGetSecurityClassification()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.SecurityClassification);
            Assert.Equal("SecurityClassification", CommonImageMetadataFields.SecurityClassification);
        }

        [Fact]
        public void CanGetSerialNumber()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.SerialNumber);
            Assert.Equal("SerialNumber", CommonImageMetadataFields.SerialNumber);
        }

        [Fact]
        public void CanGetShadows()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Shadows);
            Assert.Equal("Shadows", CommonImageMetadataFields.Shadows);
        }

        [Fact]
        public void CanGetSharpness()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Sharpness);
            Assert.Equal("Sharpness", CommonImageMetadataFields.Sharpness);
        }

        [Fact]
        public void CanGetSmoothness()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Smoothness);
            Assert.Equal("Smoothness", CommonImageMetadataFields.Smoothness);
        }

        [Fact]
        public void CanGetSoftware()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.Software);
            Assert.Equal("Software", CommonImageMetadataFields.Software);
        }

        [Fact]
        public void CanGetWhiteBalance()
        {
            // Assert
            _ = Assert.IsType<string>(CommonImageMetadataFields.WhiteBalance);
            Assert.Equal("WhiteBalance", CommonImageMetadataFields.WhiteBalance);
        }
    }
}