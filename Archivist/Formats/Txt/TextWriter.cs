using Archivist.BaseClasses;
using Archivist.Interfaces;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.Formats.Txt
{
    /// <summary>
    /// Represents a text writer for the Txt format.
    /// </summary>
    public class TextWriter : WriterBaseClass
    {
        /// <summary>
        /// Writes the content of the specified file to the provided stream asynchronously.
        /// </summary>
        /// <param name="file">The file to be written.</param>
        /// <param name="stream">The stream to write the file content to.</param>
        /// <returns>True if the file was written successfully; otherwise, false.</returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (stream is null)
                return false;
            var TempData = Encoding.UTF8.GetBytes(file?.GetContent() ?? "");
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