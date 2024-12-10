using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Formats.XLS;
using Archivist.Interfaces;
using Archivist.Options;
using Archivist.Tests.BaseClasses;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.XLS
{
    public class XLSFormatTests : TestBaseClass<XLSFormat>
    {
        public XLSFormatTests()
        {
            _Options = Microsoft.Extensions.Options.Options.Create(new ExcelOptions());
            _Converter = new Convertinator(new List<IDataConverter>());
            _TestClass = new XLSFormat(_Options, _Converter, null);
            TestObject = new XLSFormat(_Options, _Converter, null);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        private readonly Convertinator _Converter;
        private readonly IOptions<ExcelOptions> _Options;
        private readonly XLSFormat _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new XLSFormat(_Options, _Converter, null);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            var Results = Assert.IsType<string[]>(_TestClass.Extensions);

            _ = Assert.Single(Results);
            Assert.Equal("XLS", Results[0]);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            var Results = Assert.IsType<string[]>(_TestClass.MimeTypes);

            _ = Assert.Single(Results);
            Assert.Equal("application/vnd.ms-excel", Results[0]);
        }

        [Fact]
        public async Task CanReadAsync()
        {
            //Arrange
            FileStream Stream = new FileInfo("./TestData/TestXLS.xls").OpenRead();

            // Act
            IGenericFile? GenericTables = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(GenericTables);
            Tables Tables = Assert.IsType<Tables>(GenericTables);
            _ = Assert.Single(Tables);
            Assert.Equal(2, Tables[0].Count);
            Assert.Equal(2, Tables[0].Columns.Count);
            Assert.Equal("Test", Tables[0].Columns[0]);
            Assert.Equal("Data", Tables[0].Columns[1]);
            Assert.Equal(2, Tables[0][0].Count);
            Assert.Equal(2, Tables[0][1].Count);
            Assert.Equal("Sheet1", Tables[0].Title);
            Assert.Equal("Goes", Tables[0][0][0].Content);
            Assert.Equal("here", Tables[0][0][1].Content);
            Assert.Equal("", Tables[0][1][0].Content);
            Assert.Equal("1", Tables[0][1][1].Content);
        }
    }
}