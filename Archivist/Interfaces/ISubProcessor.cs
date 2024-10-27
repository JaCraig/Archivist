using System.IO;

namespace Archivist.Interfaces
{
    /// <summary>
    /// Interface for sub-processors.
    /// </summary>
    public interface ISubProcessor
    {
        /// <summary>
        /// Processes the given file.
        /// </summary>
        /// <param name="file">The file to process.</param>
        /// <param name="stream">The stream to process.</param>
        /// <returns>The processed file object.</returns>
        IGenericFile? Process(IGenericFile? file, Stream? stream);
    }
}