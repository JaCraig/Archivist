namespace Archivist.Tests.Formats.ICalendar
{
    using Archivist.Formats.ICalendar;
    using Archivist.Interfaces;
    using NSubstitute;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Xunit;

    public class ICalWriterTests
    {
        private ICalWriter _testClass;

        public ICalWriterTests()
        {
            _testClass = new ICalWriter();
        }

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            var @file = Substitute.For<IGenericFile>();
            var stream = new MemoryStream();

            // Act
            var result = await _testClass.WriteAsync(file, stream);

            // Assert
            throw new NotImplementedException("Create or modify test");
        }
    }
}