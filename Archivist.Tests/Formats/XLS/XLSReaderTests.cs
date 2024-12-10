using Archivist.Converters;
using Archivist.Formats.XLS;
using Archivist.Interfaces;
using Archivist.Options;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.XLS
{
    public class XLSReaderTests : TestBaseClass<XLSReader>
    {
        public XLSReaderTests()
        {
            _Options = new ExcelOptions();
            _Converter = new Convertinator(new List<IDataConverter>());
            _TestClass = new XLSReader(_Options, _Converter, null);
            TestObject = new XLSReader(_Options, _Converter, null);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        private readonly Convertinator _Converter;
        private readonly ExcelOptions _Options;
        private readonly XLSReader _TestClass;

        [Fact]
        public void CanCallInternalCanRead()
        {
            // Arrange
            var Stream = new FileStream(Path.Combine(AppContext.BaseDirectory, "TestData", "TestXLS.xls"), FileMode.Open, FileAccess.Read, FileShare.Read);

            // Act
            var Result = _TestClass.InternalCanRead(Stream);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public async Task CanCallReadAsync()
        {
            // Arrange
            var Stream = new FileStream(Path.Combine(AppContext.BaseDirectory, "TestData", "TestXLS.xls"), FileMode.Open, FileAccess.Read, FileShare.Read);

            // Act
            IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new XLSReader(_Options, _Converter, null);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullConverter() => _ = new XLSReader(_Options, default, null);

        [Fact]
        public void CanConstructWithNullOptions() => _ = new XLSReader(default, _Converter, null);

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            _ = Assert.IsType<byte[]>(_TestClass.HeaderInfo);

            Assert.NotNull(_TestClass.HeaderInfo);
            Assert.Equal(new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }, _TestClass.HeaderInfo);
        }
    }
}