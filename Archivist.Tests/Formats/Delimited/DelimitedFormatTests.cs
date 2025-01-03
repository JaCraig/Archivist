using Archivist.Converters;
using Archivist.Formats.Delimited;
using Archivist.Interfaces;
using Archivist.Options;
using Archivist.Tests.BaseClasses;
using BigBook;
using BigBook.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.Delimited
{
    public class DelimitedFormatTests : TestBaseClass<DelimitedFormat>
    {
        public DelimitedFormatTests()
        {
            var Services = new ServiceCollection();
            _ = Services.AddOptions();
            _ = Services.Configure<DelimitedOptions>(_ => { });
            _ = Services.AddSingleton<Convertinator>();
            _ = Services.AddAllSingleton<IDataConverter>();
            ServiceProvider = Services.BuildServiceProvider();
            _TestClass = new DelimitedFormat(ServiceProvider.GetService<IOptions<DelimitedOptions>>(), ServiceProvider.GetService<Convertinator>(), null);
            TestObject = new DelimitedFormat(ServiceProvider.GetService<IOptions<DelimitedOptions>>(), ServiceProvider.GetService<Convertinator>(), null);
        }

        private readonly DelimitedFormat _TestClass;
        private ServiceProvider ServiceProvider { get; }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new DelimitedFormat(ServiceProvider.GetService<IOptions<DelimitedOptions>>(), ServiceProvider.GetService<Convertinator>(), null);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.Extensions);

            Assert.NotNull(Result);
            Assert.NotEmpty(Result);
            Assert.Contains("CSV", Result, StringComparer.OrdinalIgnoreCase);
            Assert.Contains("TSV", Result, StringComparer.OrdinalIgnoreCase);
            Assert.Contains("TAB", Result, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.MimeTypes);

            Assert.NotNull(Result);
            Assert.NotEmpty(Result);
            Assert.Contains("TEXT/CSV", Result, StringComparer.OrdinalIgnoreCase);
            Assert.Contains("TEXT/TAB-SEPARATED-VALUES", Result, StringComparer.OrdinalIgnoreCase);
            Assert.Contains("TEXT/COMMA-SEPARATED-VALUES", Result, StringComparer.OrdinalIgnoreCase);
            Assert.Contains("APPLICATION/CSV", Result, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public void CanGetOrder()
        {
            // Arrange
            const int ExpectedOrder = 200;

            // Act
            var Order = _TestClass.Order;

            // Assert
            Assert.Equal(ExpectedOrder, Order);
        }

        [Fact]
        public async Task CanReadAsync_WithEmptyStreamAsync()
        {
            // Arrange
            var Stream = new System.IO.MemoryStream();

            // Act
            Interfaces.IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("\n", Result.GetContent() ?? "");
        }

        [Fact]
        public async Task CanReadAsync_WithNullStreamAsync()
        {
            // Act
            Interfaces.IGenericFile? Result = await _TestClass.ReadAsync(null);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("\n", Result.GetContent() ?? "");
        }

        [Fact]
        public async Task CanReadAsync_WithStreamThatHasNoDataAsync()
        {
            // Arrange
            var Stream = new System.IO.MemoryStream();
            Stream.Write("".ToByteArray());
            Stream.Position = 0;

            // Act
            Interfaces.IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("\n", Result.GetContent() ?? "");
        }

        [Fact]
        public async Task CanReadAsync_WithStreamThatHasOnlyHeaderAsync()
        {
            // Arrange
            var Stream = new System.IO.MemoryStream();
            Stream.Write($"Header 1,Header 2,Header 3,Header 4,Header 5,Header 6{Environment.NewLine}".ToByteArray());
            Stream.Position = 0;

            // Act
            var Result = (Archivist.DataTypes.Table?)await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            Assert.NotEmpty(Result.Metadata);
            Assert.Equal(",", Result.Delimiter);
            Assert.Equal(6, Result.Columns.Count);

            Assert.Contains("Header 1", Result.Columns);
            Assert.Contains("Header 2", Result.Columns);
            Assert.Contains("Header 3", Result.Columns);
            Assert.Contains("Header 4", Result.Columns);
            Assert.Contains("Header 5", Result.Columns);
            Assert.Contains("Header 6", Result.Columns);
        }

        [Fact]
        public async Task CanReadCSVAsync()
        {
            // Arrange
            var Stream = new System.IO.MemoryStream();
            Stream.Write(Utils.CSVFileData.ExampleCSV1.ToByteArray());
            Stream.Position = 0;

            // Act
            var Result = (Archivist.DataTypes.Table?)await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            Assert.NotEmpty(Result.Metadata);
            Assert.Equal(",", Result.Delimiter);
            Assert.Equal(6, Result.Columns.Count);

            Assert.Contains("Header 1", Result.Columns);
            Assert.Contains("Header 2", Result.Columns);
            Assert.Contains("Header 3", Result.Columns);
            Assert.Contains("Header 4", Result.Columns);
            Assert.Contains("Header 5", Result.Columns);
            Assert.Contains("Header 6", Result.Columns);

            Assert.Equal(3, Result.Count);
            Assert.Equal("This", Result[0]["Header 1"].Content);
            Assert.Equal("is", Result[0]["Header 2"].Content);
            Assert.Equal("a", Result[0]["Header 3"].Content);
            Assert.Equal("test", Result[0]["Header 4"].Content);
            Assert.Equal("CSV", Result[0]["Header 5"].Content);
            Assert.Equal("file", Result[0]["Header 6"].Content);

            Assert.Equal("Tons", Result[1]["Header 1"].Content);
            Assert.Equal("of", Result[1]["Header 2"].Content);
            Assert.Equal("data", Result[1]["Header 3"].Content);
            Assert.Equal("in here", Result[1]["Header 4"].Content);
            Assert.Equal("is", Result[1]["Header 5"].Content);
            Assert.Equal("super", Result[1]["Header 6"].Content);

            Assert.Equal("important", Result[2]["Header 1"].Content);
        }

        [Fact]
        public async Task CanReadPipeAsync()
        {
            // Arrange
            var Stream = new System.IO.MemoryStream();
            Stream.Write(Utils.CSVFileData.ExamplePipe1.ToByteArray());
            Stream.Position = 0;

            // Act
            var Result = (Archivist.DataTypes.Table?)await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            Assert.NotEmpty(Result.Metadata);
            Assert.Equal("|", Result.Delimiter);
            Assert.Equal(6, Result.Columns.Count);

            Assert.Contains("Header 1", Result.Columns);
            Assert.Contains("Header 2", Result.Columns);
            Assert.Contains("Header 3", Result.Columns);
            Assert.Contains("Header 4", Result.Columns);
            Assert.Contains("Header 5", Result.Columns);
            Assert.Contains("Header 6", Result.Columns);

            Assert.Equal(3, Result.Count);
            Assert.Equal("This", Result[0]["Header 1"].Content);
            Assert.Equal("is", Result[0]["Header 2"].Content);
            Assert.Equal("a", Result[0]["Header 3"].Content);
            Assert.Equal("test", Result[0]["Header 4"].Content);
            Assert.Equal("CSV", Result[0]["Header 5"].Content);
            Assert.Equal("file", Result[0]["Header 6"].Content);

            Assert.Equal("Tons", Result[1]["Header 1"].Content);
            Assert.Equal("of", Result[1]["Header 2"].Content);
            Assert.Equal("data", Result[1]["Header 3"].Content);
            Assert.Equal("in here", Result[1]["Header 4"].Content);
            Assert.Equal("is", Result[1]["Header 5"].Content);
            Assert.Equal("super", Result[1]["Header 6"].Content);

            Assert.Equal("important", Result[2]["Header 1"].Content);
        }

        [Fact]
        public async Task CanReadTSVAsync()
        {
            // Arrange
            var Stream = new System.IO.MemoryStream();
            Stream.Write(Utils.CSVFileData.ExampleTSV1.ToByteArray());
            Stream.Position = 0;

            // Act
            var Result = (Archivist.DataTypes.Table?)await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result.Title ?? "");
            Assert.NotEmpty(Result.Metadata);
            Assert.Equal("\t", Result.Delimiter);
            Assert.Equal(6, Result.Columns.Count);

            Assert.Contains("Header 1", Result.Columns);
            Assert.Contains("Header 2", Result.Columns);
            Assert.Contains("Header 3", Result.Columns);
            Assert.Contains("Header 4", Result.Columns);
            Assert.Contains("Header 5", Result.Columns);
            Assert.Contains("Header 6", Result.Columns);

            Assert.Equal(3, Result.Count);
            Assert.Equal("This", Result[0]["Header 1"].Content);
            Assert.Equal("is", Result[0]["Header 2"].Content);
            Assert.Equal("a", Result[0]["Header 3"].Content);
            Assert.Equal("test", Result[0]["Header 4"].Content);
            Assert.Equal("CSV", Result[0]["Header 5"].Content);
            Assert.Equal("file", Result[0]["Header 6"].Content);

            Assert.Equal("Tons", Result[1]["Header 1"].Content);
            Assert.Equal("of", Result[1]["Header 2"].Content);
            Assert.Equal("data", Result[1]["Header 3"].Content);
            Assert.Equal("in here", Result[1]["Header 4"].Content);
            Assert.Equal("is", Result[1]["Header 5"].Content);
            Assert.Equal("super", Result[1]["Header 6"].Content);

            Assert.Equal("important", Result[2]["Header 1"].Content);
        }

        [Fact]
        public async Task CanWrite2Async()
        {
            // Arrange
            var Table = new Archivist.DataTypes.Table();
            Table.AddRow().Add("Test");
            var Stream = new System.IO.MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(Stream, Table);
            var StreamData = Stream.ToArray().ToString(Encoding.UTF8);

            // Assert
            Assert.True(Result);
            Assert.True(StreamData.Length > 0);
            Assert.Equal($"\"Test\"{Environment.NewLine}", StreamData);
        }

        [Fact]
        public async Task CanWriteAsync()
        {
            // Arrange
            var Table = new Archivist.DataTypes.Table();
            Table.Columns.Add("Test Header");
            Table.AddRow().Add("Test Val1");
            Table.AddRow().Add("Test Val2");
            var Stream = new System.IO.MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(Stream, Table);
            var StreamData = Stream.ToArray().ToString(Encoding.UTF8);

            // Assert
            Assert.True(Result);
            Assert.True(StreamData.Length > 0);
            Assert.Equal($"\"Test Header\"{Environment.NewLine}\"Test Val1\"{Environment.NewLine}\"Test Val2\"{Environment.NewLine}", StreamData);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenFileIsNullAsync()
        {
            // Arrange
            var Stream = new System.IO.MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(Stream, null);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenStreamIsNullAsync()
        {
            // Arrange
            var Table = new Archivist.DataTypes.Table();

            // Act
            var Result = await _TestClass.WriteAsync(null, Table);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task WriteAsync_ReturnsFalse_WhenWriteFailsAsync()
        {
            // Arrange
            var Table = new Archivist.DataTypes.Table();
            Table.AddRow().Add("Test");
            var Stream = new System.IO.MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(Stream, Table);

            // Assert
            Assert.True(Result);
        }
    }
}