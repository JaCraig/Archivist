using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.Interfaces;
using Archivist.Options;
using ExcelDataReader;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.Formats.XLS
{
    /// <summary>
    /// Represents a reader for XLS files.
    /// </summary>
    /// <seealso cref="ReaderBaseClass"/>
    /// <remarks>Initializes a new instance of the <see cref="XLSReader"/> class.</remarks>
    /// <param name="options">The XLS options.</param>
    /// <param name="converter">The converter.</param>
    public class XLSReader(ExcelOptions? options, Convertinator? converter)
        : ReaderBaseClass
    {
        /// <summary>
        /// Gets the header information for XLS files.
        /// </summary>
        public override byte[] HeaderInfo { get; } = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };

        /// <summary>
        /// Gets the XLS options.
        /// </summary>
        private ExcelOptions Options { get; } = options ?? ExcelOptions.Default;

        /// <summary>
        /// The converter.
        /// </summary>
        private readonly Convertinator? _Converter = converter;

        /// <summary>
        /// Determines if the reader can read the given stream as an XLS file.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>True if the reader can read the file, false otherwise.</returns>
        public override bool InternalCanRead(Stream? stream)
        {
            if (stream is null || !IsValidStream(stream))
                return false;
            try
            {
                using IExcelDataReader Reader = ExcelReaderFactory.CreateReader(stream);
                return Reader.Read();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reads the XLS file asynchronously and returns the data as a generic file.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is the generic file data.
        /// </returns>
        public override Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            var ReturnValue = new DataTypes.Tables(_Converter);
            if (stream is null || !IsValidStream(stream))
                return Task.FromResult<IGenericFile?>(ReturnValue);
            try
            {
                using IExcelDataReader Reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
                {
                    FallbackEncoding = Encoding.GetEncoding(1252)
                });
                do
                {
                    DataTypes.Table CurrentTable = ReturnValue.AddTable();
                    CurrentTable.Title = Reader.Name;
                    while (Reader.Read())
                    {
                        DataTypes.TableRow CurrentRow = CurrentTable.AddRow();
                        for (var X = 0; X < Reader.FieldCount; ++X)
                        {
                            CurrentRow.Add(new DataTypes.TableCell(Reader.GetValue(X)?.ToString() ?? ""));
                        }
                    }
                    if (Options.FirstRowIsColumnHeaders)
                    {
                        CurrentTable.Columns.AddRange(CurrentTable[0].Select(x => x.Content ?? "") ?? []);
                        CurrentTable.RemoveAt(0);
                    }
                    else
                    {
                        for (var X = 0; X < CurrentTable[0].Count; ++X)
                        {
                            CurrentTable.Columns.Add($"Column {X + 1}");
                        }
                    }
                } while (Reader.NextResult());
            }
            catch (Exception)
            {
                return Task.FromResult<IGenericFile?>(ReturnValue);
            }

            return Task.FromResult<IGenericFile?>(ReturnValue);
        }
    }
}