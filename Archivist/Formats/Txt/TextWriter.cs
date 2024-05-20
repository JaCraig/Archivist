using Archivist.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.Txt
{
    /// <summary>
    ///
    /// </summary>
    public class TextWriter : IFormatWriter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool CanWrite(IGenericFile file) => throw new System.NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Task<bool> WriteAsync(IGenericFile file, Stream stream) => throw new System.NotImplementedException();
    }
}