using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.Options;
using Microsoft.Extensions.Options;

namespace Archivist.Formats.XLS
{
    /// <summary>
    /// Represents the XLS format in the Archivist library.
    /// </summary>
    /// <seealso cref="FormatBaseClass{TFormat, TFileReader, TFileWriter}"/>
    public class XLSFormat : FormatBaseClass<XLSFormat, XLSReader, XLSWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XLSFormat"/> class.
        /// </summary>
        /// <param name="options">The options to use when deserializing XLS.</param>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public XLSFormat(IOptions<ExcelOptions>? options, Convertinator? converter)
            : base(new XLSReader(options?.Value ?? ExcelOptions.Default, converter), new XLSWriter())
        {
        }

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