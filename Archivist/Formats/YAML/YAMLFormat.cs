using Archivist.BaseClasses;
using Archivist.Converters;
using Microsoft.Extensions.Logging;

namespace Archivist.Formats.YAML
{
    /// <summary>
    /// Represents an YAML format for storing data.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="YAMLFormat"/> class.
    /// </remarks>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger"></param>
    public class YAMLFormat(Convertinator? converter, ILogger<YAMLFormat>? logger) : FormatBaseClass<YAMLFormat, YAMLReader, YAMLWriter>(new YAMLReader(converter, logger), new YAMLWriter(logger))
    {
        /// <summary>
        /// Gets the file extensions associated with the YAML format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "YAML", "YML" };

        /// <summary>
        /// Gets the MIME types associated with the YAML format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "TEXT/YAML", "APPLICATION/YAML" };
    }
}