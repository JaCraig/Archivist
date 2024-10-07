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

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetGif()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Gif);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetHeif()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Heif);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetIco()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Ico);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetJpg()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Jpg);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetPng()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Png);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetWbmp()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Wbmp);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetWebp()
        {
            // Assert
            Assert.IsType<string>(ImageTypes.Webp);

            throw new NotImplementedException("Create or modify test");
        }
    }
}