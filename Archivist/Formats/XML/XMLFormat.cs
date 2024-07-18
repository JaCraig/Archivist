using Archivist.BaseClasses;
using System.Text.Json;

namespace Archivist.Formats.XML
{
    /// <summary>
    /// Represents an XML format for storing data.
    /// </summary>
    public class XMLFormat : FormatBaseClass<XMLFormat, XMLReader, XMLWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFormat"/> class.
        /// </summary>
        /// <param name="options">The options to use when serializing and deserializing JSON.</param>
        public XMLFormat(JsonSerializerOptions? options)
            : base(new XMLReader(options), new XMLWriter())
        {
        }

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