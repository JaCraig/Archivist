using Archivist.BaseClasses;
using Archivist.Converters;
using Microsoft.Extensions.Logging;

namespace Archivist.Formats.RSS
{
    /// <summary>
    /// Represents a RSS format for archiving.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RSSFormat"/> class.
    /// </remarks>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger"></param>
    public class RSSFormat(Convertinator? converter, ILogger<RSSFormat>? logger) : FormatBaseClass<RSSFormat, RSSReader, RSSWriter>(new RSSReader(converter, logger), new RSSWriter(logger))
    {
        /// <summary>
        /// Gets the file extensions associated with the RSS format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "RSS" };

        /// <summary>
        /// Gets the MIME types associated with the RSS format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "APPLICATION/RSS+XML" };
    }
}