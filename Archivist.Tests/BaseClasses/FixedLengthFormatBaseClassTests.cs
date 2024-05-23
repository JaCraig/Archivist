using Archivist.BaseClasses;
using Archivist.Interfaces;
using NSubstitute;
using System;
using Xunit;

namespace Archivist.Tests.BaseClasses
{
    public class FixedLengthFormatBaseClassTests : TestBaseClass<FixedLengthFormatBaseClassTests.TestFixedLengthFormatBaseClass>
    {
        public FixedLengthFormatBaseClassTests()
        {
            TestObject = new TestFixedLengthFormatBaseClass(Substitute.For<IFormatReader>());
        }

        [Fact]
        public void Constructor_InitializesProperties()
        {
            // Arrange
            IFormatReader ReaderMock = Substitute.For<IFormatReader>();

            // Act
            var Result = new TestFixedLengthFormatBaseClass(ReaderMock);

            // Assert
            Assert.NotNull(Result);
        }

        public class TestFixedLengthFormatBaseClass : FixedLengthFormatBaseClass<TestFixedLengthFormatBaseClass, IFormatReader>
        {
            public TestFixedLengthFormatBaseClass(IFormatReader reader)
                : base(reader)
            {
            }

            public override string[] Extensions { get; } = Array.Empty<string>();
            public override string[] MimeTypes { get; } = Array.Empty<string>();
        }
    }
}