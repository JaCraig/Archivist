using System.IO;
using System.Threading.Tasks;

namespace Archivist.Interfaces
{
    /// <summary>
    /// Represents a file format.
    /// </summary>
    public interface IFormat
    {
        /// <summary>
        /// Gets the display name of this file format.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the file extensions associated with this file format.
        /// </summary>
        string[] Extensions { get; }

        /// <summary>
        /// Gets the header information of this file format.
        /// </summary>
        byte[] HeaderInfo { get; }

        /// <summary>
        /// Gets the content types supported by this file format.
        /// </summary>
        string[] MimeTypes { get; }

        /// <summary>
        /// Gets the order that the file format should be checked. The lower the value, the higher the priority.
        /// Note that the order is only relevant when multiple file formats have the same HeaderInfo length.
        /// The system uses HeaderInfo to determine the order to check first, with longer headers checked first.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Determines whether this file format can read the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to check.</param>
        /// <returns><c>true</c> if this file format can read the specified file; otherwise, <c>false</c>.</returns>
        bool CanRead(string fileName);

        /// <summary>
        /// Determines whether this file format can read the specified stream.
        /// </summary>
        /// <param name="stream">The stream to check.</param>
        /// <returns><c>true</c> if this file format can read the specified stream; otherwise, <c>false</c>.</returns>
        bool CanRead(Stream stream);

        /// <summary>
        /// Determines whether this file format can write the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to check.</param>
        /// <returns><c>true</c> if this file format can write the specified file; otherwise, <c>false</c>.</returns>
        bool CanWrite(string fileName);

        /// <summary>
        /// Determines whether this file format can write the specified file.
        /// </summary>
        /// <param name="file">The file to check.</param>
        /// <returns><c>true</c> if this file format can write the specified file; otherwise, <c>false</c>.</returns>
        bool CanWrite(IGenericFile file);

        /// <summary>
        /// Asynchronously reads the specified stream and returns an instance of <see cref="IGenericFile"/>.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an instance of <see cref="IGenericFile"/>.</returns>
        Task<IGenericFile> ReadAsync(Stream stream);

        /// <summary>
        /// Asynchronously writes the specified <see cref="IGenericFile"/> to the specified stream.
        /// </summary>
        /// <param name="writer">The stream to write to.</param>
        /// <param name="file">The file to write.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the write operation is successful.</returns>
        Task<bool> WriteAsync(Stream writer, IGenericFile file);
    }
}