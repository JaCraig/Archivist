using Archivist.BaseClasses;
using Archivist.Converters;
using Microsoft.Extensions.Logging;

namespace Archivist.Formats.VCard
{
    /// <summary>
    /// Represents a VCard format for storing contact information.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="VCardFormat"/> class.
    /// </remarks>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger.</param>
    public class VCardFormat(Convertinator? converter, ILogger<VCardFormat>? logger) : FormatBaseClass<VCardFormat, VCardReader, VCardWriter>(new VCardReader(converter, logger), new VCardWriter(logger))
    {
        /// <summary>
        /// Gets the file extensions associated with the VCard format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "VCF", "VCARD" };

        /// <summary>
        /// Gets the MIME types associated with the VCard format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "TEXT/V-CARD", "TEXT/VCARD" };
    }
}