using Archivist.DataTypes;
using Archivist.Formats.VCard;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.VCard
{
    public class VCardReaderTests : TestBaseClass<VCardReader>
    {
        public VCardReaderTests()
        {
            _TestClass = new VCardReader(null, null);
            TestObject = new VCardReader(null, null);
        }

        private readonly VCardReader _TestClass;

        [Fact]
        public void CanCallCanRead()
        {
            // Arrange
            var Stream = new MemoryStream();
            var Writer = new StreamWriter(Stream);
            Writer.WriteLine("BEGIN:VCARD");
            Writer.WriteLine("END:VCARD");
            Writer.Flush();
            Stream.Position = 0;

            // Act
            var Results = _TestClass.CanRead(Stream);

            // Assert
            Assert.True(Results);
        }

        [Fact]
        public void CanCallCanReadWithInvalidHeader()
        {
            // Arrange
            var Stream = new MemoryStream();
            var Writer = new StreamWriter(Stream);
            Writer.WriteLine("ABEGIN:VCARD");
            Writer.WriteLine("END:VCARD");
            Writer.Flush();
            Stream.Position = 0;

            // Act
            var Results = _TestClass.CanRead(Stream);

            // Assert
            Assert.False(Results);
        }

        [Fact]
        public async Task CanCallReadAsync()
        {
            // Arrange
            var Stream = new MemoryStream();

            // Act
            Interfaces.IGenericFile? Results = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Results);
            Card ResultCard = Assert.IsType<Card>(Results);
            Assert.Empty(ResultCard.Fields);
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            var Results = Assert.IsType<byte[]>(_TestClass.HeaderInfo);
            Assert.Equal(new byte[] { 0x42, 0x45, 0x47, 0x49, 0x4E, 0x3A, 0x56, 0x43, 0x41, 0x52, 0x44 }, Results);
        }

        [Fact]
        public async Task CanReadVCardFileWithMultipleFieldsAsync()
        {
            // Arrange
            var Stream = new MemoryStream();
            var Writer = new StreamWriter(Stream);
            await Writer.WriteLineAsync("BEGIN:VCARD");
            await Writer.WriteLineAsync("FN:John Doe");
            await Writer.WriteLineAsync("EMAIL:john.doe@example.com");
            await Writer.WriteLineAsync("END:VCARD");
            await Writer.FlushAsync();
            Stream.Position = 0;

            // Act
            IGenericFile? Results = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Results);
            Card ResultCard = Assert.IsType<Card>(Results);
            Assert.Equal(2, ResultCard.Fields.Count);
            KeyValueField? Field1 = ResultCard.Fields[0];
            Assert.NotNull(Field1);
            Assert.Equal("FN", Field1.Property);
            Assert.Empty(Field1.Parameters);
            Assert.Equal("John Doe", Field1.Value);
            KeyValueField? Field2 = ResultCard.Fields[1];
            Assert.NotNull(Field2);
            Assert.Equal("EMAIL", Field2.Property);
            Assert.Empty(Field2.Parameters);
            Assert.Equal("john.doe@example.com", Field2.Value);
        }

        [Fact]
        public async Task CanReadVCardFileWithSingleFieldAsync()
        {
            // Arrange
            var Stream = new MemoryStream();
            var Writer = new StreamWriter(Stream);
            await Writer.WriteLineAsync("BEGIN:VCARD");
            await Writer.WriteLineAsync("FN:John Doe");
            await Writer.WriteLineAsync("END:VCARD");
            await Writer.FlushAsync();
            Stream.Position = 0;

            // Act
            IGenericFile? Results = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Results);
            Card ResultCard = Assert.IsType<Card>(Results);
            _ = Assert.Single(ResultCard.Fields);
            KeyValueField? Field = ResultCard.Fields[0];
            Assert.NotNull(Field);
            Assert.Equal("FN", Field.Property);
            Assert.Empty(Field.Parameters);
            Assert.Equal("John Doe", Field.Value);
        }
    }
}