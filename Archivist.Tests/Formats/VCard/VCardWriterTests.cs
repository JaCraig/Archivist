using Archivist.DataTypes;
using Archivist.Formats.VCard;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.VCard
{
    public class VCardWriterTests : TestBaseClass<VCardWriter>
    {
        public VCardWriterTests()
        {
            _TestClass = new VCardWriter();
            TestObject = new VCardWriter();
        }

        private readonly VCardWriter _TestClass;

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithNullFileAsync()
        {
            // Arrange
            IGenericFile File = null!;
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithNullStreamAsync()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();
            Stream? Stream = null;

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithValuesAsync()
        {
            // Arrange
            IGenericFile File = new Card();
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.True(Result);
        }
    }
}