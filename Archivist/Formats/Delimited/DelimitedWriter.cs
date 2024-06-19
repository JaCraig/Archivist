using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.Interfaces;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.Formats.Delimited
{
    /// <summary>
    /// Delimited file writer
    /// </summary>
    /// <seealso cref="WriterBaseClass"/>
    public class DelimitedWriter : WriterBaseClass
    {
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
                return false;
            var Builder = new StringBuilder();
            Table? FileTable = file.ToFileType<Table>();
            if (FileTable is not null)
            {
                _ = Builder.Append(CreateFromTable(FileTable));
            }
            else
            {
                _ = Builder.Append(CreateFromFile(file));
            }
            var ByteData = Encoding.UTF8.GetBytes(Builder.ToString());
            try
            {
                await stream.WriteAsync(ByteData).ConfigureAwait(false);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Creates the file data from a file object.
        /// </summary>
        /// <param name="file">The file object.</param>
        /// <returns>The file data as a string.</returns>
        private static string CreateFromFile(IGenericFile file) => "\"" + file?.ToString()?.Replace("\"", "") + "\"";

        /// <summary>
        /// Creates the file data from a table object.
        /// </summary>
        /// <param name="fileTable">The table object representing the file.</param>
        /// <returns>The file data as a string.</returns>
        private static string CreateFromTable(Table fileTable)
        {
            if (!fileTable.Metadata.TryGetValue("Delimiter", out var Delimiter))
                Delimiter = ",";
            var Builder = new StringBuilder();
            var Seperator = "";
            if (fileTable.Columns.Count > 0)
            {
                foreach (var HeaderColumn in fileTable.Columns)
                {
                    _ = Builder.Append(Seperator).Append('"').Append(HeaderColumn?.Replace("\"", "") ?? "").Append('"');
                    Seperator = Delimiter;
                }
                _ = Builder.AppendLine();
            }
            foreach (TableRow Row in fileTable)
            {
                Seperator = "";
                foreach (TableCell CurrentCell in Row)
                {
                    _ = Builder.Append(Seperator).Append('"').Append(CurrentCell.Content?.Replace("\"", "") ?? "").Append('"');
                    Seperator = Delimiter;
                }
                _ = Builder.AppendLine();
            }
            return Builder.ToString();
        }
    }
}