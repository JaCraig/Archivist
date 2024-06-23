namespace Archivist.Options
{
    /// <summary>
    /// Represents the options for handling delimited data.
    /// </summary>
    public class DelimitedOptions
    {
        /// <summary>
        /// The default delimited options if none are specified.
        /// </summary>
        public static DelimitedOptions Default { get; } = new DelimitedOptions
        {
            DefaultSeparator = ",",
            FirstRowIsColumnHeaders = true,
            Quote = "\""
        };

        /// <summary>
        /// Gets or sets the default separator used to delimit the data.
        /// </summary>
        public string? DefaultSeparator { get; set; } = ",";

        /// <summary>
        /// Gets or sets a value indicating whether the first row of the data is treated as column headers.
        /// </summary>
        public bool FirstRowIsColumnHeaders { get; set; } = true;

        /// <summary>
        /// Gets or sets the quote character to use (use an empty string is quotes are not desired).
        /// </summary>
        public string? Quote { get; set; } = "\"";
    }
}