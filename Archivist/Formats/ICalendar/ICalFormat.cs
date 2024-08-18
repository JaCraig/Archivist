using Archivist.BaseClasses;
using Archivist.Converters;

namespace Archivist.Formats.ICalendar
{
    /// <summary>
    /// Represents the ICal format.
    /// </summary>
    /// <seealso cref="FormatBaseClass{TFormat, TFileReader, TFileWriter}"/>
    public class ICalFormat : FormatBaseClass<ICalFormat, ICalReader, ICalWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ICalFormat"/> class.
        /// </summary>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public ICalFormat(Convertinator? converter)
            : base(new ICalReader(converter), new ICalWriter())
        {
        }

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