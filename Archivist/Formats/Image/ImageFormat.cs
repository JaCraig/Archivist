using Archivist.BaseClasses;
using Archivist.Converters;
using Microsoft.Extensions.Logging;

namespace Archivist.Formats.Image
{
    /// <summary>
    /// Represents the Image format.
    /// </summary>
    /// <seealso cref="FormatBaseClass{TFormat, TFileReader, TFileWriter}"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ImageFormat"/> class.
    /// </remarks>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger.</param>
    public class ImageFormat(Convertinator? converter, ILogger<ImageFormat>? logger) : FormatBaseClass<ImageFormat, ImageReader, ImageWriter>(new ImageReader(converter, logger), new ImageWriter(logger))
    {
        /// <summary>
        /// Gets the file extensions associated with the Image format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "GIF", "JPG", "JPEG", "BMP", "PNG", "WEBP", "ICO", "WBMP", "HEIF" };

        /// <summary>
        /// Gets the MIME types associated with the Image format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "IMAGE/GIF", "IMAGE/JPEG", "IMAGE/BMP", "IMAGE/PNG", "IMAGE/WEBP", "IMAGE/ICO", "IMAGE/WBMP", "IMAGE/HEIF" };
    }
}