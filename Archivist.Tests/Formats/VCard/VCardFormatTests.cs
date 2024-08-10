using Archivist.DataTypes;
using Archivist.Formats.VCard;
using Archivist.Tests.BaseClasses;
using Archivist.Tests.Utils;
using BigBook;
using Mecha.xUnit;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.VCard
{
    public class VCardFormatTests : TestBaseClass<VCardFormat>
    {
        public VCardFormatTests()
        {
            _TestClass = new VCardFormat();
            TestObject = new VCardFormat();
        }

        private readonly VCardFormat _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new VCardFormat();

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            _ = Assert.IsType<string[]>(_TestClass.Extensions);

            Assert.NotNull(_TestClass.Extensions);
            Assert.NotEmpty(_TestClass.Extensions);
            Assert.Equal(2, _TestClass.Extensions.Length);
            Assert.Contains("VCF", _TestClass.Extensions);
            Assert.Contains("VCARD", _TestClass.Extensions);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            _ = Assert.IsType<string[]>(_TestClass.MimeTypes);

            Assert.NotNull(_TestClass.MimeTypes);
            Assert.NotEmpty(_TestClass.MimeTypes);
            Assert.Equal(2, _TestClass.MimeTypes.Length);
            Assert.Contains("TEXT/V-CARD", _TestClass.MimeTypes);
            Assert.Contains("TEXT/VCARD", _TestClass.MimeTypes);
        }

        [Fact]
        public void CanGetOrder()
        {
            // Assert
            _ = Assert.IsType<int>(_TestClass.Order);

            Assert.Equal(200, _TestClass.Order);
        }

        [Property]
        public void CanRead([Required] string fileName, [Required] string extension)
        {
            var Result = _TestClass.CanRead(fileName + ".vcf");
            Assert.True(Result);

            Result = _TestClass.CanRead(fileName + ".vcard");
            Assert.True(Result);

            Result = _TestClass.CanRead(fileName + ".VCF");
            Assert.True(Result);

            Result = _TestClass.CanRead(fileName + ".VCARD");
            Assert.True(Result);

            Result = _TestClass.CanRead(fileName + "." + extension);
            var ExpectedResult = string.Equals(extension, "VCF", System.StringComparison.OrdinalIgnoreCase) || string.Equals(extension, "VCARD", System.StringComparison.OrdinalIgnoreCase);
            Assert.Equal(ExpectedResult, Result);
        }

        [Fact]
        public async Task CanReadAsync()
        {
            // Arrange
            var TestData = VCardFileData.ExampleVCard1;
            var Stream = new MemoryStream(TestData.ToByteArray());
            var TestObject = new VCardFormat();

            // Act
            Card Result = Assert.IsType<Card>(await TestObject.ReadAsync(Stream));
            KeyValueField? FullNameProperty = Result["FN"].FirstOrDefault();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(67, Result.Count);
            Assert.NotNull(FullNameProperty);
            Assert.Equal("FN", FullNameProperty.Property);
            Assert.Equal("Prefix FirstName MiddleName LastName Suffix", FullNameProperty.Value);
            Assert.Empty(FullNameProperty.Parameters);
        }

        [Property]
        public void CanWrite([Required] string fileName, [Required] string extension)
        {
            var Result = _TestClass.CanWrite(fileName + ".vcf");
            Assert.True(Result);

            Result = _TestClass.CanWrite(fileName + ".vcard");
            Assert.True(Result);

            Result = _TestClass.CanWrite(fileName + ".VCF");
            Assert.True(Result);

            Result = _TestClass.CanWrite(fileName + ".VCARD");
            Assert.True(Result);

            Result = _TestClass.CanWrite(fileName + "." + extension);
            var ExpectedResult = string.Equals(extension, "VCF", System.StringComparison.OrdinalIgnoreCase) || string.Equals(extension, "VCARD", System.StringComparison.OrdinalIgnoreCase);
            Assert.Equal(ExpectedResult, Result);
        }

        [Fact]
        public async Task CanWriteAsync()
        {
            // Arrange
            var TestData = VCardFileData.ExampleVCard1;
            var Stream = new MemoryStream(TestData.ToByteArray());
            Card Card = Assert.IsType<Card>(await _TestClass.ReadAsync(Stream));
            var NewStream = new MemoryStream();

            // Act
            _ = await _TestClass.WriteAsync(NewStream, Card);
            _ = NewStream.Seek(0, SeekOrigin.Begin);
            var StreamContent = NewStream.ReadAll();
            Assert.NotNull(StreamContent);
            _ = NewStream.Seek(0, SeekOrigin.Begin);
            Card NewCard = Assert.IsType<Card>(await _TestClass.ReadAsync(NewStream));

            // Assert
            Assert.NotNull(NewCard);
            Assert.Equal(Card, NewCard);
        }
    }
}