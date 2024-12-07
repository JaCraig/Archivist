using Archivist.Formats.XLS;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using NSubstitute;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.XLS
{
    public class XLSWriterTests : TestBaseClass<XLSWriter>
    {
        public XLSWriterTests()
        {
            _TestClass = new XLSWriter();
            TestObject = new XLSWriter();
        }

        private readonly XLSWriter _TestClass;

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new XLSWriter();

            // Assert
            Assert.NotNull(Instance);
        }
    }
}