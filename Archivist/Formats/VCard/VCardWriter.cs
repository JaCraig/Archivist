using Archivist.BaseClasses;
using Archivist.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.VCard
{
    /// <summary>
    /// Represents a writer for VCard files.
    /// </summary>
    public class VCardWriter : WriterBaseClass
    {
        /// <summary>
        /// Writes the VCard file asynchronously.
        /// </summary>
        /// <param name="file">The IGenericFile object representing the VCard file.</param>
        /// <param name="stream">The Stream object to write the VCard file to.</param>
        /// <returns>A task representing the asynchronous write operation. The task result is a boolean value indicating whether the write operation was successful.</returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
        }
    }
}