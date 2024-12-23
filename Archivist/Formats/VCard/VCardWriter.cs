using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.Formats.VCard
{
    /// <summary>
    /// Represents a writer for VCard files.
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="VCardWriter"/> class.</remarks>
    /// <param name="logger">The logger to use for logging.</param>
    public class VCardWriter(ILogger? logger) : WriterBaseClass(logger)
    {
        /// <summary>
        /// Writes the VCard file asynchronously.
        /// </summary>
        /// <param name="file">The IGenericFile object representing the VCard file.</param>
        /// <param name="stream">The Stream object to write the VCard file to.</param>
        /// <returns>
        /// A task representing the asynchronous write operation. The task result is a boolean value
        /// indicating whether the write operation was successful.
        /// </returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (!IsValidStream(stream) || file is null)
            {
                Logger?.LogDebug("{writerName}.WriteAsync(): Stream is null or invalid.", nameof(VCardWriter));
                return false;
            }
            Card? FileCard = file.ToFileType<Card>();
            if (FileCard is null)
            {
                Logger?.LogDebug("{writerName}.WriteAsync(): File is null or not convertable to a card.", nameof(VCardWriter));
                return false;
            }
            var FileContent = new StringBuilder("BEGIN:VCARD\r\nVERSION:4.0\r\n");
            foreach (KeyValueField? Field in FileCard.Fields)
            {
                if (Field is null)
                    continue;
                _ = FileContent.Append(Field.Property);
                foreach (KeyValueParameter Parameter in Field.Parameters)
                {
                    _ = FileContent.AppendFormat($";{Parameter.Name}={Parameter.Value}");
                }
                _ = FileContent.AppendFormat($":{Field.Value}\r\n").AppendLine();
            }
            _ = FileContent.Append("END:VCARD");
            var TempData = Encoding.UTF8.GetBytes(FileContent.ToString());
            try
            {
                await stream.WriteAsync(TempData).ConfigureAwait(false);
            }
            catch (Exception Ex)
            {
                Logger?.LogError(Ex, "{writerName}.WriteAsync(): Error occurred while writing the VCard file.", nameof(VCardWriter));
                return false;
            }
            return true;
        }
    }
}