using Archivist.BaseClasses;
using Archivist.Converters;
using Microsoft.Extensions.Logging;

namespace Archivist.Formats.ICalendar
{
    /// <summary>
    /// Represents the ICal format.
    /// </summary>
    /// <seealso cref="FormatBaseClass{TFormat, TFileReader, TFileWriter}"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ICalFormat"/> class.
    /// </remarks>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger.</param>
    public class ICalFormat(Convertinator? converter, ILogger<ICalFormat>? logger) : FormatBaseClass<ICalFormat, ICalReader, ICalWriter>(new ICalReader(converter, logger), new ICalWriter(logger))
    {
        /// <summary>
        /// Gets the file extensions associated with the ICal format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "VCS", "ICAL", "ICS", "IFB", "ICALENDAR" };

        /// <summary>
        /// Gets the MIME types associated with the Excel format.
        /// </summary>
        public override string[] MimeTypes { get; } = new string[] { "APPLICATION/HBS-VCS", "TEXT/X-VCALENDAR", "TEXT/CALENDAR" };
    }
}