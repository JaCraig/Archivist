namespace Archivist.Tests.Enums
{
    using Archivist.Enums;
    using System;
    using Xunit;

    public static class ImageTypesTests
    {
        [Fact]
        public static void CanGetBmp()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Bmp);
            Assert.Equal("bmp", ImageTypes.Bmp);
        }

        [Fact]
        public static void CanGetGif()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Gif);
            Assert.Equal("gif", ImageTypes.Gif);
        }

        [Fact]
        public static void CanGetHeif()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Heif);
            Assert.Equal("heif", ImageTypes.Heif);
        }

        [Fact]
        public static void CanGetIco()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Ico);
            Assert.Equal("ico", ImageTypes.Ico);
        }

        [Fact]
        public static void CanGetJpg()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Jpg);
            Assert.Equal("jpg", ImageTypes.Jpg);
        }

        [Fact]
        public static void CanGetPng()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Png);
            Assert.Equal("png", ImageTypes.Png);
        }

        [Fact]
        public static void CanGetWbmp()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Wbmp);
            Assert.Equal("wbmp", ImageTypes.Wbmp);
        }

        [Fact]
        public static void CanGetWebp()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Webp);
            Assert.Equal("webp", ImageTypes.Webp);
        }
    }
}
