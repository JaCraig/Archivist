using Archivist.Formats.XML;
using Archivist.Tests.BaseClasses;
using System.Text.Json;
using Xunit;

namespace Archivist.Tests.Formats.XML
{
    public class XMLFormatTests : TestBaseClass<XMLFormat>
    {
        public XMLFormatTests()
        {
            _Options = new JsonSerializerOptions();
            _TestClass = new XMLFormat(_Options);
            TestObject = new XMLFormat(_Options);
        }

        private readonly JsonSerializerOptions _Options;
        private readonly XMLFormat _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new XMLFormat(_Options);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullOptions() => new XMLFormat(default);

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.Extensions);

            Assert.Equal(new[] { "XML" }, Result);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.MimeTypes);

            Assert.Equal(new[] { "TEXT/XML", "APPLICATION/XML" }, Result);
        }
    }
}