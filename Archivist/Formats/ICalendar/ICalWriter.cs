using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Archivist.Formats.ICalendar
{
    /// <summary>
    /// Represents a writer for ICal files.
    /// </summary>
    public class ICalWriter : WriterBaseClass
    {
        /// <summary>
        /// The strip HTML regex
        /// </summary>
        private static readonly Regex STRIP_HTML_REGEX = new("<[^>]*>", RegexOptions.Compiled);

        /// <summary>
        /// Fields to skip when writing the ICal file.
        /// </summary>
        private readonly string[] _SkippedFields = { "METHOD", "PRODID", "VERSION", "CLASS", "DTSTAMP", "CREATED", "DTSTART", "DTEND", "LOCATION", "UID", "SEQUENCE", "PRIORITY", "ATTENDEE", "DESCRIPTION", "LAST-MODIFIED", "STATUS", "TRANSP" };

        /// <summary>
        /// Writes the ICal file asynchronously.
        /// </summary>
        /// <param name="file">The IGenericFile object representing the ICal file.</param>
        /// <param name="stream">The Stream object to write the ICal file to.</param>
        /// <returns>
        /// A task representing the asynchronous write operation. The task result is a boolean value
        /// indicating whether the write operation was successful.
        /// </returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (stream?.CanWrite != true || file is null)
                return false;
            CalendarComponent? FileCal = file.ToFileType<CalendarComponent>();
            if (FileCal is null)
                return false;
            var FileContent = new StringBuilder("BEGIN:VCALENDAR\r\n");
            _ = FileContent.AppendFormat("METHOD:{0}\r\n", FileCal.Method)
                        .AppendFormat("PRODID:{0}\r\n", FileCal.ProductId)
                        .AppendFormat("VERSION:{0}\r\n", FileCal.Version)
                        .Append("BEGIN:VEVENT\r\n")
                        .AppendFormat("CLASS:{0}\r\n", FileCal.Class)
                        .AppendFormat("DTSTAMP:{0}\r\n", FileCal.DateStampUtc.ToString("yyyyMMddTHHmmssZ", System.Globalization.CultureInfo.InvariantCulture))
                        .AppendFormat("CREATED:{0}\r\n", FileCal.CreatedUtc.ToString("yyyyMMddTHHmmssZ", System.Globalization.CultureInfo.InvariantCulture))
                        .AppendFormat("DTSTART:{0}\r\n", FileCal.StartDateUtc.ToString("yyyyMMddTHHmmssZ", System.Globalization.CultureInfo.InvariantCulture))
                        .AppendFormat("DTEND:{0}\r\n", FileCal.EndDateUtc.ToString("yyyyMMddTHHmmssZ", System.Globalization.CultureInfo.InvariantCulture))
                        .AppendFormat("LOCATION:{0}\r\n", FileCal.Location)
                        .AppendFormat("UID:{0}\r\n", FileCal.UID)
                        .AppendFormat("SEQUENCE:{0}\r\n", FileCal.Sequence)
                        .AppendFormat("PRIORITY:{0}\r\n", FileCal.Priority);
            foreach (KeyValueField? Attendee in FileCal.Attendees)
            {
                if (Attendee is null)
                    continue;
                _ = FileContent.AppendFormat("ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN=\"{0}\":MAILTO:{0}\r\n", Attendee.Value);
            }
            AddDefaultAction(FileCal, FileContent);
            AddMicrosoftFields(FileCal, FileContent);
            if (ContainsHTML(FileCal.Description))
                _ = FileContent.AppendFormat("X-ALT-DESC;FMTTYPE=text/html:{0}\r\n", FileCal.Description.Replace("\n", ""));
            else if (!string.IsNullOrEmpty(FileCal.Description))
                _ = FileContent.AppendFormat("DESCRIPTION:{0}\r\n", FileCal.Description);
            _ = FileContent.AppendFormat("LAST-MODIFIED:{0}\r\n", FileCal.LastModifiedUtc.ToString("yyyyMMddTHHmmssZ", System.Globalization.CultureInfo.InvariantCulture))
                .AppendFormat("STATUS:{0}\r\n", FileCal.Status)
                .AppendFormat("TRANSP:{0}\r\n", FileCal.Transp);

            foreach (KeyValueField? Field in FileCal.Fields)
            {
                if (Field is null || _SkippedFields.Contains(Field.Property.ToUpper()))
                    continue;
                _ = FileContent.Append(Field.Property);
                foreach (KeyValueParameter Parameter in Field.Parameters)
                {
                    _ = FileContent.AppendFormat($";{Parameter.Name}={Parameter.Value}");
                }
                _ = FileContent.AppendFormat($":{Field.Value}\r\n");
            }
            _ = FileContent.Append("END:VEVENT\r\n");
            AddAlarms(FileCal, FileContent);
            _ = FileContent.Append("END:VCALENDAR\r\n");
            var TempData = Encoding.UTF8.GetBytes(FileContent.ToString());
            try
            {
                await stream.WriteAsync(TempData).ConfigureAwait(false);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds the alarms to the ICal file.
        /// </summary>
        /// <param name="fileCal">File object.</param>
        /// <param name="fileContent">File content.</param>
        private static void AddAlarms(CalendarComponent? fileCal, StringBuilder fileContent)
        {
            if (fileCal is null || fileContent is null)
                return;
            // Add a default alarm if none are present.
            if (fileCal.Alarms.Count == 0)
            {
                _ = fileContent.Append("BEGIN:VALARM\r\nTRIGGER:-PT15M\r\nACTION:DISPLAY\r\nDESCRIPTION:Reminder\r\nEND:VALARM\r\n");
                return;
            }
            // Add the alarms.
            foreach (CalendarAlarm Alarm in fileCal.Alarms)
            {
                _ = fileContent.Append("BEGIN:VALARM\r\n");
                foreach (KeyValueField? Field in Alarm.Fields)
                {
                    if (Field is null)
                        continue;
                    _ = fileContent.Append(Field.Property);
                    foreach (KeyValueParameter Parameter in Field.Parameters)
                    {
                        _ = fileContent.AppendFormat($";{Parameter.Name}={Parameter.Value}");
                    }
                    _ = fileContent.AppendFormat($":{Field.Value}\r\n").AppendLine();
                }
                _ = fileContent.Append("END:VALARM\r\n");
            }
        }

        /// <summary>
        /// Adds the default ACTION field to the ICal file if none are present.
        /// </summary>
        /// <param name="fileCal">Calendar object</param>
        /// <param name="fileContent">File content</param>
        private static void AddDefaultAction(CalendarComponent? fileCal, StringBuilder fileContent)
        {
            if (fileCal is null || fileContent is null || fileCal.Actions.Any())
                return;
            foreach (KeyValueField? Organizer in fileCal.Organizers)
            {
                if (Organizer is null)
                    continue;
                _ = fileContent.AppendFormat("ACTION;RSVP=TRUE;CN=\"{0}\":MAILTO:{0}\r\nORGANIZER;CN=\"{0}\":mailto:{0}\r\n", Organizer.Value);
            }
        }

        /// <summary>
        /// Adds the Microsoft/Outlook specific fields to the ICal file.
        /// </summary>
        /// <param name="fileCal">Calendar file object</param>
        /// <param name="fileContent">File content</param>
        private static void AddMicrosoftFields(CalendarComponent fileCal, StringBuilder fileContent)
        {
            if (fileCal is null || fileContent is null)
                return;
            // If the file already contains Microsoft/Outlook specific fields, do not add them again.
            if (fileCal.Fields.Any(field => field?.Property.StartsWith("X-MICROSOFT-CDO-") ?? false))
                return;
            var CurrentDateTime = fileCal.LastModifiedUtc.ToString("yyyyMMddTHHmmssZ", System.Globalization.CultureInfo.InvariantCulture);
            // Add the Microsoft/Outlook specific fields.
            _ = fileContent.AppendFormat("X-MICROSOFT-CDO-BUSYSTATUS:{0}", fileCal.Statuses.FirstOrDefault()?.Value ?? "BUSY")
                        .AppendLine()
                        .AppendLine("X-MICROSOFT-CDO-INSTTYPE:0")
                        .AppendLine("X-MICROSOFT-CDO-INTENDEDSTATUS:BUSY")
                        .AppendLine("X-MICROSOFT-CDO-ALLDAYEVENT:FALSE")
                        .AppendLine("X-MICROSOFT-CDO-IMPORTANCE:1")
                        .AppendLine("X-MICROSOFT-CDO-OWNERAPPTID:-1")
                        .AppendFormat("X-MICROSOFT-CDO-ATTENDEE-CRITICAL-CHANGE:{0}", CurrentDateTime)
                        .AppendLine()
                        .AppendFormat("X-MICROSOFT-CDO-OWNER-CRITICAL-CHANGE:{0}", CurrentDateTime)
                        .AppendLine();
        }

        /// <summary>
        /// Determines whether the specified input contains HTML.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input contains HTML; otherwise, <c>false</c>.</returns>
        private static bool ContainsHTML(string? input) => !string.IsNullOrEmpty(input) && STRIP_HTML_REGEX.IsMatch(input);
    }
}