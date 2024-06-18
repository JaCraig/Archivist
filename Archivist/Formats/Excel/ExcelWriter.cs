using Archivist.BaseClasses;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.Formats.Excel
{
    /// <summary>
    /// </summary>
    public class ExcelWriter : WriterBaseClass
    {
        /// <summary>
        /// </summary>
        /// <param name="file"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public override Task<bool> WriteAsync(IGenericFile? file, Stream? stream) => throw new NotImplementedException();
    }
}