using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests
{
    public class FormatManagerTests : TestBaseClass<FormatManager>
    {
        public FormatManagerTests()
        {
            _Formats = new[] { Substitute.For<IFormat>(), Substitute.For<IFormat>(), Substitute.For<IFormat>() };
            _SubProcessors = new[] { Substitute.For<ISubProcessor>(), Substitute.For<ISubProcessor>() };
            _TestClass = new FormatManager(_Formats, _SubProcessors);
            TestObject = new FormatManager(_Formats, _SubProcessors);
        }

        private readonly IEnumerable<IFormat> _Formats;
        private readonly IEnumerable<ISubProcessor> _SubProcessors;
        private readonly FormatManager _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new FormatManager(_Formats, _SubProcessors);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullFormats() => _ = new FormatManager(default, _SubProcessors);

        [Fact]
        public void CanConstructWithNullSubProcessors() => _ = new FormatManager(_Formats, default);

        [Fact]
        public void CanConstructWithNullFormatsAndSubProcessors() => _ = new FormatManager(default, default);
    }
}
