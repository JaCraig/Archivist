using Archivist.BaseClasses;
using Archivist.Converters;

namespace Archivist.Formats.YAML
{
    /// <summary>
    /// Represents an YAML format for storing data.
    /// </summary>
    public class YAMLFormat : FormatBaseClass<YAMLFormat, YAMLReader, YAMLWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YAMLFormat"/> class.
        /// </summary>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public YAMLFormat(Convertinator? converter)
            : base(new YAMLReader(converter), new YAMLWriter())
        {
        }

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