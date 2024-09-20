using Archivist.BaseClasses;
using Archivist.Converters;

namespace Archivist.Formats.Image
{
    /// <summary>
    /// Represents the Image format.
    /// </summary>
    /// <seealso cref="FormatBaseClass{TFormat, TFileReader, TFileWriter}"/>
    public class ImageFormat : FormatBaseClass<ImageFormat, ImageReader, ImageWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFormat"/> class.
        /// </summary>
        /// <param name="options">The options to use when deserializing Image.</param>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public ImageFormat(ImageSerializerSettings? options, Convertinator? converter)
            : base(new ImageReader(options, converter), new ImageWriter(options))
        {
        }

        /// <summary>
        /// Gets the file extensions associated with the Image format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "Image" };

        /// <summary>
        /// Gets the MIME types associated with the Excel format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "APPLICATION/Image" };
    }
}