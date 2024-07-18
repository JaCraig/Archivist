using Archivist.ExtensionMethods;
using Archivist.Tests.BaseClasses;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.ExtensionMethods
{
    public class InternalExtensionMethodsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; } = typeof(InternalExtensionMethods);

        [Fact]
        public void AddSpaces_Should_AddSpacesBeforeEachCapitalLetter()
        {
            // Arrange
            const string Input = "AddSpacesTestString";
            const string Expected = "Add Spaces Test String";

            // Act
            var Result = Input.AddSpaces();

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void AddSpaces_Should_ReturnEmptyString_When_InputIsEmpty()
        {
            // Arrange
            const string Input = "";
            const string Expected = "";

            // Act
            var Result = Input.AddSpaces();

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void AddSpaces_Should_ReturnEmptyString_When_InputIsNull()
        {
            // Arrange
            const string? Input = null;
            const string Expected = "";

            // Act
            var Result = Input!.AddSpaces();

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void Left_Should_ReturnEmptyString_When_InputIsNull()
        {
            // Arrange
            const string? Input = null;
            const int Length = 5;
            const string Expected = "";

            // Act
            var Result = Input.Left(Length);

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void Left_Should_ReturnEmptyString_When_LengthIsZero()
        {
            // Arrange
            const string Input = "LeftTestString";
            const int Length = 0;
            const string Expected = "";

            // Act
            var Result = Input.Left(Length);

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void Left_Should_ReturnSubstringWithGivenLength()
        {
            // Arrange
            const string Input = "LeftTestString";
            const int Length = 4;
            const string Expected = "Left";

            // Act
            var Result = Input.Left(Length);

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void ReadAll_Should_ReturnEmptyString_When_StreamIsNull()
        {
            // Arrange
            Stream? Stream = null;
            const string Expected = "";

            // Act
            var Result = Stream.ReadAll();

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void ReadAll_Should_ReturnStringWithContentOfStream()
        {
            // Arrange
            const string Expected = "Test Content";
            var ContentBytes = Encoding.UTF8.GetBytes(Expected);
            using var Stream = new MemoryStream(ContentBytes);

            // Act
            var Result = Stream.ReadAll();

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public async Task ReadAllAsync_Should_ReturnEmptyString_When_StreamIsNullAsync()
        {
            // Arrange
            Stream? Stream = null;
            const string Expected = "";

            // Act
            var Result = await Stream.ReadAllAsync();

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public async Task ReadAllAsync_Should_ReturnStringWithContentOfStreamAsync()
        {
            // Arrange
            const string Expected = "Test Content";
            var ContentBytes = Encoding.UTF8.GetBytes(Expected);
            await using var Stream = new MemoryStream(ContentBytes);

            // Act
            var Result = await Stream.ReadAllAsync();

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void ReadAllBinary_Should_ReturnByteArrayWithContentOfStream()
        {
            // Arrange
            const string Expected = "Test Content";
            var ContentBytes = Encoding.UTF8.GetBytes(Expected);
            using var Stream = new MemoryStream(ContentBytes);

            // Act
            var Result = Stream.ReadAllBinary();

            // Assert
            Assert.Equal(ContentBytes, Result);
        }

        [Fact]
        public void ReadAllBinary_Should_ReturnEmptyByteArray_When_StreamIsNull()
        {
            // Arrange
            Stream? Stream = null;
            var Expected = Array.Empty<byte>();

            // Act
            var Result = Stream.ReadAllBinary();

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public async Task ReadAllBinaryAsync_Should_ReturnByteArrayWithContentOfStreamAsync()
        {
            // Arrange
            const string Expected = "Test Content";
            var ContentBytes = Encoding.UTF8.GetBytes(Expected);
            await using var Stream = new MemoryStream(ContentBytes);

            // Act
            var Result = await Stream.ReadAllBinaryAsync();

            // Assert
            Assert.Equal(ContentBytes, Result);
        }

        [Fact]
        public async Task ReadAllBinaryAsync_Should_ReturnEmptyByteArray_When_StreamIsNullAsync()
        {
            // Arrange
            Stream? Stream = null;
            var Expected = Array.Empty<byte>();

            // Act
            var Result = await Stream!.ReadAllBinaryAsync();

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void ToString_Should_ReturnEmptyString_When_ByteArrayIsNull()
        {
            // Arrange
            byte[]? Input = null;
            const string Expected = "";

            // Act
            var Result = Input.ToString(Encoding.UTF8);

            // Assert
            Assert.Equal(Expected, Result);
        }

        [Fact]
        public void ToString_Should_ReturnStringFromByteArray()
        {
            // Arrange
            const string Expected = "Test String";
            var Input = Encoding.UTF8.GetBytes(Expected);

            // Act
            var Result = Input.ToString(Encoding.UTF8);

            // Assert
            Assert.Equal(Expected, Result);
        }
    }
}