using System.IO;
using System.Threading.Tasks;

namespace Archivist.Interfaces
{
    /// <summary>
    /// Represents a format reader.
    /// </summary>
    public interface IFormatReader
    {
        /// <summary>
        /// Gets the header information of the format.
        /// </summary>
        byte[] HeaderInfo { get; }

        /// <summary>
        /// Determines whether the format reader can read the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns><c>true</c> if the format reader can read the stream; otherwise, <c>false</c>.</returns>
        bool CanRead(Stream? stream);

        /// <summary>
        /// Asynchronously reads the specified stream and returns the generic file.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generic file read from the stream.</returns>
        Task<IGenericFile?> ReadAsync(Stream? stream);
    }
}