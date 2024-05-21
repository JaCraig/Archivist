using Archivist.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.BaseClasses
{
    /// <summary>
    /// Base class for format writers.
    /// </summary>
    public abstract class WriterBaseClass : IFormatWriter
    {
        /// <summary>
        /// Determines if the writer can write the specified file.
        /// </summary>
        /// <param name="file">The file to be written.</param>
        /// <returns><c>true</c> if the writer can write the file; otherwise, <c>false</c>.</returns>
        public virtual bool CanWrite(IGenericFile? file) => true;

        /// <summary>
        /// Writes the specified file to the provided stream asynchronously.
        /// </summary>
        /// <param name="file">The file to be written.</param>
        /// <param name="stream">The stream to write the file to.</param>
        /// <returns>A task representing the asynchronous write operation. The task result is <c>true</c> if the write operation is successful; otherwise, <c>false</c>.</returns>
        public abstract Task<bool> WriteAsync(IGenericFile? file, Stream? stream);
    }
}