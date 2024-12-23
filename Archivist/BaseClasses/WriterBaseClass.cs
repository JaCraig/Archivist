using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.BaseClasses
{
    /// <summary>
    /// Base class for format writers.
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="WriterBaseClass"/> class.</remarks>
    /// <param name="logger">The logger to use for logging.</param>
    public abstract class WriterBaseClass(ILogger? logger) : IFormatWriter
    {
        /// <summary>
        /// Gets the logger to use for logging.
        /// </summary>
        protected ILogger? Logger { get; } = logger;

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
        /// <returns>
        /// A task representing the asynchronous write operation. The task result is <c>true</c> if
        /// the write operation is successful; otherwise, <c>false</c>.
        /// </returns>
        public abstract Task<bool> WriteAsync(IGenericFile? file, Stream? stream);

        /// <summary>
        /// Determines if the stream is valid for writing.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <returns><c>true</c> if the stream is valid for writing; otherwise, <c>false</c>.</returns>
        protected static bool IsValidStream([NotNullWhen(true)] Stream? stream) => stream?.CanWrite ?? false;
    }
}