using Archivist.BaseClasses;

namespace Archivist.Formats.Excel
{
    /// <summary>
    /// Represents the Excel format in the Archivist library.
    /// </summary>
    public class ExcelFormat : FormatBaseClass<ExcelFormat, ExcelReader, ExcelWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelFormat"/> class.
        /// </summary>
        public ExcelFormat()
            : base(new ExcelReader(), new ExcelWriter())
        {
        }

        /// <summary>
        /// Gets the file extensions associated with the Excel format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "XLSX" };

        /// <summary>
        /// Gets the MIME types associated with the Excel format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT.SPREADSHEETML.SHEET" };
    }
}