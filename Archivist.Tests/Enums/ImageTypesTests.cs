using Archivist.Enums;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Enums
{
    public class ImageTypesTests : TestBaseClass
    {
        protected override Type? ObjectType => typeof(ImageTypes);

        [Fact]
        public void CanGetBmp()
        {
            // Assert
            _ = Assert.IsType<string>(ImageTypes.Bmp);
            Assert.Equal("bmp", ImageTypes.Bmp);
        }

        [Fact]
        public void CanGetGif()
        {
            // Assert
            _ = Assert.IsType<string>(ImageTypes.Gif);
            Assert.Equal("gif", ImageTypes.Gif);
        }

        [Fact]
        public void CanGetHeif()
        {
            // Assert
            _ = Assert.IsType<string>(ImageTypes.Heif);
            Assert.Equal("heif", ImageTypes.Heif);
        }

        [Fact]
        public void CanGetIco()
        {
            // Assert
            _ = Assert.IsType<string>(ImageTypes.Ico);
            Assert.Equal("ico", ImageTypes.Ico);
        }

        [Fact]
        public void CanGetJpg()
        {
            // Assert
            _ = Assert.IsType<string>(ImageTypes.Jpg);
            Assert.Equal("jpg", ImageTypes.Jpg);
        }

        [Fact]
        public void CanGetPng()
        {
            // Assert
            _ = Assert.IsType<string>(ImageTypes.Png);
            Assert.Equal("png", ImageTypes.Png);
        }

        [Fact]
        public void CanGetWbmp()
        {
            // Assert
            _ = Assert.IsType<string>(ImageTypes.Wbmp);
            Assert.Equal("wbmp", ImageTypes.Wbmp);
        }

        [Fact]
        public void CanGetWebp()
        {
            // Assert
            _ = Assert.IsType<string>(ImageTypes.Webp);
            Assert.Equal("webp", ImageTypes.Webp);
        }
    }
}