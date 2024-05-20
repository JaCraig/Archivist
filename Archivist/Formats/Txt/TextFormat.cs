using Archivist.BaseClasses;

namespace Archivist.Formats.Txt
{
    /// <summary>
    /// Represents a text format for archiving.
    /// </summary>
    public class TextFormat : FormatBaseClass<TextFormat, TextReader, TextWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextFormat"/> class.
        /// </summary>
        public TextFormat()
            : base(new TextReader(), new TextWriter())
        {
        }

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