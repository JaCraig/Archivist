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
            _TestClass = new FormatManager(_Formats);
            TestObject = new FormatManager(_Formats);
        }

        private readonly IEnumerable<IFormat> _Formats;
        private readonly FormatManager _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new FormatManager(_Formats);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullFormats() => _ = new FormatManager(default);
    }
}