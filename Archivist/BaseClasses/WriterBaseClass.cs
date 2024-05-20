using Archivist.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.BaseClasses
{
    /// <summary>
    ///
    /// </summary>
    public abstract class WriterBaseClass : IFormatWriter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool CanWrite(IGenericFile file) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Task<bool> WriteAsync(IGenericFile file, Stream stream) => throw new NotImplementedException();
    }
}