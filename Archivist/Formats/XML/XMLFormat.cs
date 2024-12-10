using Archivist.BaseClasses;
using Archivist.Converters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Archivist.Formats.XML
{
    /// <summary>
    /// Represents an XML format for storing data.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="XMLFormat"/> class.
    /// </remarks>
    /// <param name="options">The options to use when serializing and deserializing JSON.</param>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger.</param>
    public class XMLFormat(JsonSerializerSettings? options, Convertinator? converter, ILogger<XMLFormat>? logger) : FormatBaseClass<XMLFormat, XMLReader, XMLWriter>(new XMLReader(options, converter, logger), new XMLWriter(logger))
    {
        /// <summary>
        /// Gets the file extensions associated with the XML format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "XML" };

        /// <summary>
        /// Gets the MIME types associated with the XML format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "TEXT/XML", "APPLICATION/XML" };
    }
}