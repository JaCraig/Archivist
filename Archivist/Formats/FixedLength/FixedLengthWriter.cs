using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.Interfaces;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.Formats.FixedLength
{
    /// <summary>
    /// Represents a writer for fixed-length files.
    /// </summary>
    public class FixedLengthWriter : WriterBaseClass
    {
        /// <summary>
        /// Determines if the writer can write the specified file.
        /// </summary>
        /// <param name="file">The file to be written.</param>
        /// <returns><c>true</c> if the writer can write the file; otherwise, <c>false</c>.</returns>
        public override bool CanWrite(IGenericFile? file) => file is FixedLengthFile;

        /// <summary>
        /// Writes the specified file to the provided stream asynchronously.
        /// </summary>
        /// <param name="file">The file to be written.</param>
        /// <param name="stream">The stream to write the file to.</param>
        /// <returns><c>true</c> if the file was written successfully; otherwise, <c>false</c>.</returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (file is not FixedLengthFile FixedLengthFile || stream is null)
                return false;
            var TempData = Encoding.UTF8.GetBytes(FixedLengthFile.GetContent() ?? "");
            try
            {
                await stream.WriteAsync(TempData).ConfigureAwait(false);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}