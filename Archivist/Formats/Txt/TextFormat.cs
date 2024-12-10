using Archivist.BaseClasses;
using Archivist.Converters;
using Microsoft.Extensions.Logging;

namespace Archivist.Formats.Txt
{
    /// <summary>
    /// Represents a text format for archiving.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="TextFormat"/> class.
    /// </remarks>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger"></param>
    public class TextFormat(Convertinator? converter, ILogger<TextFormat>? logger) : FormatBaseClass<TextFormat, TextReader, TextWriter>(new TextReader(converter, logger), new TextWriter(logger))
    {
        /// <summary>
        /// Gets the file extensions associated with the text format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "TXT" };

        /// <summary>
        /// Gets the MIME types associated with the text format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "TEXT/PLAIN" };

        /// <summary>
        /// Text format is format of last resort.
        /// </summary>
        public override int Order => int.MaxValue;
    }
}