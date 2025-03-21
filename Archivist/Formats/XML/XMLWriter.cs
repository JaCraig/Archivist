﻿using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.XML
{
    /// <summary>
    /// Represents a writer for XML files.
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="XMLWriter"/> class.</remarks>
    /// <param name="logger">The logger to use for logging.</param>
    public class XMLWriter(ILogger? logger) : WriterBaseClass(logger)
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
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (file is null || !IsValidStream(stream))
                return false;
            if (file is not StructuredObject StructuredObject)
                StructuredObject = file.ToFileType<StructuredObject>()!;
            if (StructuredObject is null)
            {
                Logger?.LogDebug("XMLWriter.WriteAsync(): StructuredObject is null.");
                return false;
            }
            ExpandoObject? Content = StructuredObject.ConvertTo<ExpandoObject>();
            if (Content is null)
            {
                Logger?.LogDebug("XMLWriter.WriteAsync(): Content is null.");
                return false;
            }
            var Xml = JsonConvert.DeserializeXNode("{\"root\":" + JsonConvert.SerializeObject(Content) + "}")?.ToString();
            if (string.IsNullOrEmpty(Xml))
            {
                Logger?.LogDebug("XMLWriter.WriteAsync(): Xml is null or empty.");
                return false;
            }
            Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" + Xml;
            await stream.WriteAsync(Xml.ToByteArray()).ConfigureAwait(false);
            return true;
        }
    }
}