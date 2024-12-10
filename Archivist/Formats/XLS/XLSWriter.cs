using Archivist.BaseClasses;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.XLS
{
    /// <summary>
    /// Writes an XLS file.
    /// </summary>
    /// <seealso cref="WriterBaseClass"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="XLSWriter"/> class.
    /// </remarks>
    /// <param name="logger">The logger to use for logging.</param>
    public class XLSWriter(ILogger? logger) : WriterBaseClass(logger)
    {
        /// <summary>
        /// Writes the file to the stream
        /// </summary>
        /// <param name="file">The file object to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <returns>True if it is written succesfully, false otherwise.</returns>
        public override Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            Logger?.LogDebug("{writerName}.WriteAsync(): XLS writing not supported", nameof(XLSWriter));
            return Task.FromResult(false);
        }
    }
}