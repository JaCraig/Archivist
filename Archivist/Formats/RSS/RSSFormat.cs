using Archivist.BaseClasses;
using Archivist.Converters;

namespace Archivist.Formats.RSS
{
    /// <summary>
    /// Represents a RSS format for archiving.
    /// </summary>
    public class RSSFormat : FormatBaseClass<RSSFormat, RSSReader, RSSWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RSSFormat"/> class.
        /// </summary>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public RSSFormat(Convertinator? converter)
            : base(new RSSReader(converter), new RSSWriter())
        {
        }

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