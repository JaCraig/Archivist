using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Archivist.Formats.XLS
{
    /// <summary>
    /// Represents the XLS format in the Archivist library.
    /// </summary>
    /// <seealso cref="FormatBaseClass{TFormat, TFileReader, TFileWriter}"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="XLSFormat"/> class.
    /// </remarks>
    /// <param name="options">The options to use when deserializing XLS.</param>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger</param>
    public class XLSFormat(IOptions<ExcelOptions>? options, Convertinator? converter, ILogger<XLSFormat>? logger) : FormatBaseClass<XLSFormat, XLSReader, XLSWriter>(new XLSReader(options?.Value ?? ExcelOptions.Default, converter, logger), new XLSWriter(logger))
    {
        /// <summary>
        /// Gets the file extensions associated with the XLS format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "XLS" };

        /// <summary>
        /// Gets the MIME types associated with the XLS format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "application/vnd.ms-excel" };
    }
}