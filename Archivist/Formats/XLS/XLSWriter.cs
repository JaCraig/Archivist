using Archivist.BaseClasses;
using Archivist.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.XLS
{
    /// <summary>
    /// Writes an XLS file.
    /// </summary>
    /// <seealso cref="WriterBaseClass"/>
    public class XLSWriter : WriterBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XLSWriter"/> class.
        /// </summary>
        public XLSWriter()
        {
        }

        /// <summary>
        /// Writes the file to the stream
        /// </summary>
        /// <param name="file">The file object to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <returns>True if it is written succesfully, false otherwise.</returns>
        public override Task<bool> WriteAsync(IGenericFile? file, Stream? stream) => Task.FromResult(false);
    }
}