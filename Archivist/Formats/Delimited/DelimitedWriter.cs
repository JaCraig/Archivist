using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.Interfaces;
using Archivist.Options;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.Formats.Delimited
{
    /// <summary>
    /// Delimited file writer
    /// </summary>
    /// <seealso cref="WriterBaseClass"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DelimitedWriter"/> class.
    /// </remarks>
    /// <param name="options">The options for the delimited writer.</param>
    /// <param name="logger">The logger to use for logging.</param>
    public class DelimitedWriter(DelimitedOptions options, ILogger? logger) : WriterBaseClass(logger)
    {
        /// <summary>
        /// The options for the delimited writer.
        /// </summary>
        private DelimitedOptions Options { get; } = options ?? DelimitedOptions.Default;

        /// <summary>
        /// Writes the content of the file to the specified stream asynchronously.
        /// </summary>
        /// <param name="file">The file to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <returns>
        /// A task representing the asynchronous write operation. The task result is true if the
        /// write operation is successful; otherwise, false.
        /// </returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (stream?.CanWrite != true || file is null)
            {
                Logger?.LogDebug("DelimitedWriter.WriteAsync(): Stream is null or invalid.");
                return false;
            }
            var Builder = new StringBuilder();
            Table? FileTable = file.ToFileType<Table>();
            if (FileTable is not null)
            {
                _ = Builder.Append(CreateFromTable(FileTable, Options));
            }
            else
            {
                _ = Builder.Append(CreateFromFile(file, Options));
            }
            var ByteData = Encoding.UTF8.GetBytes(Builder.ToString());
            try
            {
                await stream.WriteAsync(ByteData).ConfigureAwait(false);
            }
            catch (Exception Ex)
            {
                Logger?.LogError(Ex, "DelimitedWriter.WriteAsync(): Error writing to stream.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Creates the file data from a file object.
        /// </summary>
        /// <param name="file">The file object.</param>
        /// <param name="options"></param>
        /// <returns>The file data as a string.</returns>
        private static string CreateFromFile(IGenericFile file, DelimitedOptions options) => options.Quote + file?.ToString()?.Replace(options.Quote ?? "", "") + options.Quote;

        /// <summary>
        /// Creates the file data from a table object.
        /// </summary>
        /// <param name="fileTable">The table object representing the file.</param>
        /// <param name="options">The options for the delimited writer.</param>
        /// <returns>The file data as a string.</returns>
        private static string CreateFromTable(Table fileTable, DelimitedOptions options)
        {
            if (!fileTable.Metadata.TryGetValue("Delimiter", out var Delimiter))
                Delimiter = options.DefaultSeparator;
            var Builder = new StringBuilder();
            var Seperator = "";
            if (fileTable.Columns.Count > 0)
            {
                foreach (var HeaderColumn in fileTable.Columns)
                {
                    var Header = HeaderColumn;
                    if (!string.IsNullOrEmpty(options.Quote) && !string.IsNullOrEmpty(Header))
                        Header = Header.Replace(options.Quote, "");
                    _ = Builder.Append(Seperator).Append(options.Quote).Append(Header ?? "").Append(options.Quote);
                    Seperator = Delimiter;
                }
                _ = Builder.AppendLine();
            }
            foreach (TableRow Row in fileTable)
            {
                Seperator = "";
                foreach (TableCell CurrentCell in Row)
                {
                    var Content = CurrentCell.Content;
                    if (!string.IsNullOrEmpty(options.Quote) && !string.IsNullOrEmpty(Content))
                        Content = Content.Replace(options.Quote, "");
                    _ = Builder.Append(Seperator).Append(options.Quote).Append(Content ?? "").Append(options.Quote);
                    Seperator = Delimiter;
                }
                _ = Builder.AppendLine();
            }
            return Builder.ToString();
        }
    }
}