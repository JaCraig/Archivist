using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.Interfaces;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Archivist.Formats.XML
{
    /// <summary>
    /// Represents a writer for XML files.
    /// </summary>
    public class XMLWriter : WriterBaseClass
    {
        /// <summary>
        /// Writes the XML file asynchronously.
        /// </summary>
        /// <param name="file">The IGenericFile object representing the XML file.</param>
        /// <param name="stream">The Stream object to write the XML file to.</param>
        /// <returns>
        /// A task representing the asynchronous write operation. The task result is a boolean value
        /// indicating whether the write operation was successful.
        /// </returns>
        public override Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (file is null || stream is null)
                return Task.FromResult(false);
            if (!stream.CanWrite)
                return Task.FromResult(false);
            if (file is not StructuredObject StructuredObject)
                StructuredObject = file.ToFileType<StructuredObject>()!;
            if (StructuredObject is null)
                return Task.FromResult(false);
            ExpandoObject? Content = StructuredObject.ConvertTo<ExpandoObject>();
            if (Content is null)
                return Task.FromResult(false);
            var DataContractSerializer = new DataContractSerializer(typeof(IDictionary<string, object?>));
            DataContractSerializer.WriteObject(stream, Content);
            return Task.FromResult(true);
        }
    }
}