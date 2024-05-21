using Archivist.BaseClasses;
using Archivist.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.VCard
{
    /// <summary>
    /// Represents a reader for VCard files.
    /// </summary>
    public class VCardReader : ReaderBaseClass
    {
        /// <summary>
        /// Gets the header information of the VCard file.
        /// </summary>
        public override byte[]? HeaderInfo { get; }

        /// <summary>
        /// Reads a VCard file asynchronously from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read the VCard file from.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the parsed VCard file.</returns>
        public override Task<IGenericFile?> ReadAsync(Stream? stream) => throw new NotImplementedException();
    }
}