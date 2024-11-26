using Archivist.DataTypes;
using Archivist.Formats.Txt;
using Archivist.Tests.BaseClasses;
using BigBook;
using BigBook.ExtensionMethods;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Txt
{
    public class TextFormatTests : TestBaseClass<TextFormat>
    {
        public TextFormatTests()
        {
            _TestClass = new TextFormat(null);
            TestObject = new TextFormat(null);
        }

        private readonly TextFormat _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new TextFormat(null);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Arrange
            var ExpectedExtensions = new[] { "TXT" };

            // Act
            var Extensions = _TestClass.Extensions;

            // Assert
            Assert.Equal(ExpectedExtensions, Extensions);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Arrange
            var ExpectedMimeTypes = new[] { "TEXT/PLAIN" };

            // Act
            var MimeTypes = _TestClass.MimeTypes;

            // Assert
            Assert.Equal(ExpectedMimeTypes, MimeTypes);
        }

        [Fact]
        public void CanGetOrder()
        {
            // Arrange
            const int ExpectedOrder = int.MaxValue;

            // Act
            var Order = _TestClass.Order;

            // Assert
            Assert.Equal(ExpectedOrder, Order);
        }

        [Fact]
        public async Task CanReadTextAsync()
        {
            // Arrange
            const string Text = "Hello, World!";
            var Format = new TextFormat(null);
            var Stream = new MemoryStream();
            Stream.Write(Text.ToByteArray());

            // Act
            Interfaces.IGenericFile? Result = await Format.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(Text, Result.GetContent());
            Assert.Equal(Text, Result.Title);
        }

        [Fact]
        public async Task CanWriteTextAsync()
        {
            // Arrange
            const string Text = "Hello, World!";
            const string Title = "Hello, World Title!";
            var Format = new TextFormat(null);
            var Stream = new MemoryStream();
            var FileObject = new Text(Text, Title);

            // Act
            var Result = await Format.WriteAsync(Stream, FileObject);

            // Assert
            Assert.True(Result);
            Stream.Position = 0;
            var Data = Stream.ToArray();
            var WrittenContent = Data.ToString(System.Text.Encoding.UTF8);
            Assert.Equal(Text, WrittenContent);
        }
    }
}