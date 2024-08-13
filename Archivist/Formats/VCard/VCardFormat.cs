using Archivist.BaseClasses;
using Archivist.Converters;

namespace Archivist.Formats.VCard
{
    /// <summary>
    /// Represents a VCard format for storing contact information.
    /// </summary>
    public class VCardFormat : FormatBaseClass<VCardFormat, VCardReader, VCardWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VCardFormat"/> class.
        /// </summary>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public VCardFormat(Convertinator? converter)
            : base(new VCardReader(converter), new VCardWriter())
        {
        }

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