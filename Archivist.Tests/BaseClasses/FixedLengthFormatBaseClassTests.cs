using Archivist.BaseClasses;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using Xunit;

namespace Archivist.Tests.BaseClasses
{
    public class FixedLengthFormatBaseClassTests : TestBaseClass<FixedLengthFormatBaseClassTests.TestFixedLengthFormatBaseClass>
    {
        public FixedLengthFormatBaseClassTests()
        {
            TestObject = new TestFixedLengthFormatBaseClass(Substitute.For<IFormatReader>(), Substitute.For<ILogger>());
        }

        [Fact]
        public void Constructor_InitializesProperties()
        {
            // Arrange
            IFormatReader ReaderMock = Substitute.For<IFormatReader>();
            ILogger LoggerMock = Substitute.For<ILogger>();

            // Act
            var Result = new TestFixedLengthFormatBaseClass(ReaderMock, LoggerMock);

            // Assert
            Assert.NotNull(Result);
        }

        public class TestFixedLengthFormatBaseClass : FixedLengthFormatBaseClass<TestFixedLengthFormatBaseClass, IFormatReader>
        {
            public TestFixedLengthFormatBaseClass(IFormatReader reader, ILogger? logger)
                : base(reader, logger)
            {
            }

            public override string[] Extensions { get; } = Array.Empty<string>();
            public override string[] MimeTypes { get; } = Array.Empty<string>();
        }
    }
}