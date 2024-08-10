namespace Archivist.Tests.Formats.ICalendar
{
    using Archivist.Formats.ICalendar;
    using System;
    using Xunit;

    public class ICalFormatTests
    {
        private ICalFormat _testClass;

        public ICalFormatTests()
        {
            _testClass = new ICalFormat();
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ICalFormat();

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            Assert.IsType<string[]>(_testClass.Extensions);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            Assert.IsType<string[]>(_testClass.MimeTypes);

            throw new NotImplementedException("Create or modify test");
        }
    }
}