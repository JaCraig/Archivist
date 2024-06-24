namespace Archivist.Options
{
    /// <summary>
    /// Represents the options for Excel files.
    /// </summary>
    public class ExcelOptions
    {
        /// <summary>
        /// Gets the default Excel options.
        /// </summary>
        public static ExcelOptions Default { get; } = new ExcelOptions
        {
            FirstRowIsColumnHeaders = true
        };

        /// <summary>
        /// Gets or sets a value indicating whether the first row in the Excel file is treated as
        /// column headers.
        /// </summary>
        public bool FirstRowIsColumnHeaders { get; set; } = true;
    }
}