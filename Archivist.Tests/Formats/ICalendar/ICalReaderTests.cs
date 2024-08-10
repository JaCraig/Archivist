namespace Archivist.Tests.Formats.ICalendar
{
    using Archivist.Formats.ICalendar;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Xunit;

    public class ICalReaderTests
    {
        private ICalReader _testClass;

        public ICalReaderTests()
        {
            _testClass = new ICalReader();
        }

        [Fact]
        public async Task CanCallReadAsync()
        {
            // Arrange
            var stream = new MemoryStream();

            // Act
            var result = await _testClass.ReadAsync(stream);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            Assert.IsType<byte[]>(_testClass.HeaderInfo);

            throw new NotImplementedException("Create or modify test");
        }
    }
}