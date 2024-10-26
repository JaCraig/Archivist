namespace Archivist.Tests.Enums
{
    using Archivist.Enums;
    using System;
    using Xunit;

    public static class CommonImageMetadataFieldsTests
    {
        [Fact]
        public static void CanGetArtist()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Artist);
            Assert.Equal("Artist", CommonImageMetadataFields.Artist);
        }

        [Fact]
        public static void CanGetBrightness()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Brightness);
            Assert.Equal("Brightness", CommonImageMetadataFields.Brightness);
        }

        [Fact]
        public static void CanGetContrast()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Contrast);
            Assert.Equal("Contrast", CommonImageMetadataFields.Contrast);
        }

        [Fact]
        public static void CanGetConverter()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Converter);
            Assert.Equal("Converter", CommonImageMetadataFields.Converter);
        }

        [Fact]
        public static void CanGetCopyright()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Copyright);
            Assert.Equal("Copyright", CommonImageMetadataFields.Copyright);
        }

        [Fact]
        public static void CanGetDateTimeOriginal()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.DateTimeOriginal);
            Assert.Equal("DateTimeOriginal", CommonImageMetadataFields.DateTimeOriginal);
        }

        [Fact]
        public static void CanGetDescription()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Description);
            Assert.Equal("ImageDescription", CommonImageMetadataFields.Description);
        }

        [Fact]
        public static void CanGetDocumentName()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.DocumentName);
            Assert.Equal("DocumentName", CommonImageMetadataFields.DocumentName);
        }

        [Fact]
        public static void CanGetExposure()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Exposure);
            Assert.Equal("Exposure", CommonImageMetadataFields.Exposure);
        }

        [Fact]
        public static void CanGetHostComputer()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.HostComputer);
            Assert.Equal("HostComputer", CommonImageMetadataFields.HostComputer);
        }

        [Fact]
        public static void CanGetImageType()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.ImageType);
            Assert.Equal("ImageType", CommonImageMetadataFields.ImageType);
        }

        [Fact]
        public static void CanGetMake()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Make);
            Assert.Equal("Make", CommonImageMetadataFields.Make);
        }

        [Fact]
        public static void CanGetModel()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Model);
            Assert.Equal("Model", CommonImageMetadataFields.Model);
        }

        [Fact]
        public static void CanGetMoireFilter()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.MoireFilter);
            Assert.Equal("MoireFilter", CommonImageMetadataFields.MoireFilter);
        }

        [Fact]
        public static void CanGetOffsetTimeOriginal()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.OffsetTimeOriginal);
            Assert.Equal("OffsetTimeOriginal", CommonImageMetadataFields.OffsetTimeOriginal);
        }

        [Fact]
        public static void CanGetOrientation()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Orientation);
            Assert.Equal("Orientation", CommonImageMetadataFields.Orientation);
        }

        [Fact]
        public static void CanGetOwnerName()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.OwnerName);
            Assert.Equal("OwnerName", CommonImageMetadataFields.OwnerName);
        }

        [Fact]
        public static void CanGetPhotographer()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Photographer);
            Assert.Equal("Photographer", CommonImageMetadataFields.Photographer);
        }

        [Fact]
        public static void CanGetProcessingSoftware()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.ProcessingSoftware);
            Assert.Equal("ProcessingSoftware", CommonImageMetadataFields.ProcessingSoftware);
        }

        [Fact]
        public static void CanGetRawFile()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.RawFile);
            Assert.Equal("RawFile", CommonImageMetadataFields.RawFile);
        }

        [Fact]
        public static void CanGetSaturation()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Saturation);
            Assert.Equal("Saturation", CommonImageMetadataFields.Saturation);
        }

        [Fact]
        public static void CanGetSecurityClassification()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.SecurityClassification);
            Assert.Equal("SecurityClassification", CommonImageMetadataFields.SecurityClassification);
        }

        [Fact]
        public static void CanGetSerialNumber()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.SerialNumber);
            Assert.Equal("SerialNumber", CommonImageMetadataFields.SerialNumber);
        }

        [Fact]
        public static void CanGetShadows()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Shadows);
            Assert.Equal("Shadows", CommonImageMetadataFields.Shadows);
        }

        [Fact]
        public static void CanGetSharpness()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Sharpness);
            Assert.Equal("Sharpness", CommonImageMetadataFields.Sharpness);
        }

        [Fact]
        public static void CanGetSmoothness()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Smoothness);
            Assert.Equal("Smoothness", CommonImageMetadataFields.Smoothness);
        }

        [Fact]
        public static void CanGetSoftware()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.Software);
            Assert.Equal("Software", CommonImageMetadataFields.Software);
        }

        [Fact]
        public static void CanGetWhiteBalance()
        {
            // Assert
            Assert.IsType<string>(CommonImageMetadataFields.WhiteBalance);
            Assert.Equal("WhiteBalance", CommonImageMetadataFields.WhiteBalance);
        }
    }
}
