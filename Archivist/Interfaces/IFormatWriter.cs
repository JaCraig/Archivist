using System.IO;
using System.Threading.Tasks;

namespace Archivist.Interfaces
{
    /// <summary>
    /// Represents a format writer.
    /// </summary>
    public interface IFormatWriter
    {
        /// <summary>
        /// Determines whether the format writer can write the specified file.
        /// </summary>
        /// <param name="file">The file to write.</param>
        /// <returns><c>true</c> if the format writer can write the file; otherwise, <c>false</c>.</returns>
        bool CanWrite(IGenericFile file);

        /// <summary>
        /// Asynchronously writes the specified file to the stream.
        /// </summary>
        /// <param name="file">The file to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> WriteAsync(IGenericFile file, Stream stream);
    }
}