using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    /// <remarks>
    /// Initializes a new instance of the <see cref="ICalWriter"/> class.
    /// </remarks>
    /// <param name="logger">The logger to use for logging.</param>
    public class ICalWriter(ILogger? logger) : WriterBaseClass(logger)
    {
        /// <summary>
        /// The strip HTML regex
        /// </summary>
        private static readonly Regex _STRIP_HTML_REGEX = new("<[^>]*>", RegexOptions.Compiled);

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
            {
                Logger?.LogDebug("{writerName}.WriteAsync(): Stream is null or invalid.", nameof(ICalWriter));
                return false;
            }
            Calendar? FileCal = file.ToFileType<Calendar>();
            if (FileCal is null)
                return false;
            var FileContent = new StringBuilder("BEGIN:VCALENDAR\r\n");
            _ = FileContent.AppendFormat("METHOD:{0}\r\n", FileCal.Method)
                        .AppendFormat("PRODID:{0}\r\n", FileCal.ProductId)
                        .AppendFormat("VERSION:{0}\r\n", FileCal.Version);
            AddEvents(FileCal, FileContent);
            AddComponents(FileCal.Alarms, FileContent, "VALARM", "BEGIN:VALARM\r\nTRIGGER:-PT15M\r\nACTION:DISPLAY\r\nDESCRIPTION:Reminder\r\nEND:VALARM\r\n");
            AddComponents(FileCal.FreeBusy, FileContent, "VFREEBUSY", "");
            AddComponents(FileCal.TimeZones, FileContent, "VTIMEZONE", "");
            AddComponents(FileCal.ToDos, FileContent, "VTODO", "");
            AddComponents(FileCal.Journals, FileContent, "VJOURNAL", "");
            _ = FileContent.Append("END:VCALENDAR\r\n");
            var TempData = Encoding.UTF8.GetBytes(FileContent.ToString());
            try
            {
                await stream.WriteAsync(TempData).ConfigureAwait(false);
            }
            catch (Exception Ex)
            {
                Logger?.LogError(Ex, "{writerName}.WriteAsync(): Error occurred while writing the ICal file.", nameof(ICalWriter));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds the components to the ICal file.
        /// </summary>
        /// <param name="fileComponents">The list of file components.</param>
        /// <param name="fileContent">The file content.</param>
        /// <param name="componentType">The type of the component.</param>
        /// <param name="defaultComponent">The default component to add if none are present.</param>
        private static void AddComponents(List<CalendarComponent> fileComponents, StringBuilder fileContent,
            string componentType, string? defaultComponent)
        {
            if (fileComponents is null || fileContent is null || string.IsNullOrEmpty(componentType))
                return;
            // Add a default component if none are present.
            if (fileComponents.Count == 0 && !string.IsNullOrEmpty(defaultComponent))
            {
                _ = fileContent.Append(defaultComponent);
                return;
            }
            // Add the components.
            foreach (CalendarComponent Component in fileComponents)
            {
                _ = fileContent.AppendFormat("BEGIN:{0}\r\n", componentType);
                foreach (KeyValueField? Field in Component.Fields)
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
                _ = fileContent.AppendFormat("END:{0}\r\n", componentType);
            }
        }

        /// <summary>
        /// Adds the default ACTION field to the ICal file if none are present.
        /// </summary>
        /// <param name="fileCal">The calendar object.</param>
        /// <param name="fileContent">The file content.</param>
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
        /// <param name="fileCal">The calendar file object.</param>
        /// <param name="fileContent">The file content.</param>
        private static void AddMicrosoftFields(CalendarComponent fileCal, StringBuilder fileContent)
        {
            if (fileCal is null || fileContent is null)
                return;
            // If the file already contains Microsoft/Outlook specific fields, do not add them again.
            if (fileCal.Fields.Any(field => field?.Property.StartsWith("X-MICROSOFT-CDO-") ?? false))
                return;
            var CurrentDateTime = fileCal.LastModifiedUtc.ToString("yyyyMMddTHHmmssZ");
            // Add the Microsoft/Outlook specific fields.
            _ = fileContent.AppendFormat("X-MICROSOFT-CDO-BUSYSTATUS:{0}\r\n", fileCal.Statuses.FirstOrDefault()?.Value ?? "BUSY")
                        .Append("X-MICROSOFT-CDO-INSTTYPE:0\r\n")
                        .Append("X-MICROSOFT-CDO-INTENDEDSTATUS:BUSY\r\n")
                        .Append("X-MICROSOFT-CDO-ALLDAYEVENT:FALSE\r\n")
                        .Append("X-MICROSOFT-CDO-IMPORTANCE:1\r\n")
                        .Append("X-MICROSOFT-CDO-OWNERAPPTID:-1\r\n")
                        .AppendFormat("X-MICROSOFT-CDO-ATTENDEE-CRITICAL-CHANGE:{0}\r\n", CurrentDateTime)
                        .AppendFormat("X-MICROSOFT-CDO-OWNER-CRITICAL-CHANGE:{0}\r\n", CurrentDateTime);
        }

        /// <summary>
        /// Determines whether the specified input contains HTML.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns><c>true</c> if the specified input contains HTML; otherwise, <c>false</c>.</returns>
        private static bool ContainsHTML(string? input) => !string.IsNullOrEmpty(input) && _STRIP_HTML_REGEX.IsMatch(input);

        /// <summary>
        /// Adds the events to the ICal file.
        /// </summary>
        /// <param name="fileCal">The calendar file object.</param>
        /// <param name="fileContent">The file content.</param>
        private void AddEvents(Calendar fileCal, StringBuilder fileContent)
        {
            foreach (CalendarComponent Event in fileCal.Events)
            {
                _ = fileContent.Append("BEGIN:VEVENT\r\n")
                .AppendFormat("CLASS:{0}\r\n", Event.Class)
                .AppendFormat("DTSTAMP:{0}\r\n", Event.DateStampUtc.ToString("yyyyMMddTHHmmssZ"))
                .AppendFormat("CREATED:{0}\r\n", Event.CreatedUtc.ToString("yyyyMMddTHHmmssZ"))
                .AppendFormat("DTSTART:{0}\r\n", Event.StartDateUtc.ToString("yyyyMMddTHHmmssZ"))
                .AppendFormat("DTEND:{0}\r\n", Event.EndDateUtc.ToString("yyyyMMddTHHmmssZ"))
                .AppendFormat("LOCATION:{0}\r\n", Event.Location)
                .AppendFormat("UID:{0}\r\n", Event.UID)
                .AppendFormat("SEQUENCE:{0}\r\n", Event.Sequence)
                .AppendFormat("PRIORITY:{0}\r\n", Event.Priority);
                foreach (KeyValueField? Attendee in Event.Attendees)
                {
                    if (Attendee is null)
                        continue;
                    _ = fileContent.AppendFormat("ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN=\"{0}\":MAILTO:{0}\r\n", Attendee.Value);
                }
                AddDefaultAction(Event, fileContent);
                AddMicrosoftFields(Event, fileContent);
                if (ContainsHTML(Event.Description))
                    _ = fileContent.AppendFormat("X-ALT-DESC;FMTTYPE=text/html:{0}\r\n", Event.Description.Replace("\n", ""));
                else if (!string.IsNullOrEmpty(Event.Description))
                    _ = fileContent.AppendFormat("DESCRIPTION:{0}\r\n", Event.Description);
                _ = fileContent.AppendFormat("LAST-MODIFIED:{0}\r\n", Event.LastModifiedUtc.ToString("yyyyMMddTHHmmssZ"))
                    .AppendFormat("STATUS:{0}\r\n", Event.Status)
                    .AppendFormat("TRANSP:{0}\r\n", Event.Transp);

                foreach (KeyValueField? Field in Event.Fields)
                {
                    if (Field is null || _SkippedFields.Contains(Field.Property.ToUpper()))
                        continue;
                    _ = fileContent.Append(Field.Property);
                    foreach (KeyValueParameter Parameter in Field.Parameters)
                    {
                        _ = fileContent.AppendFormat($";{Parameter.Name}={Parameter.Value}");
                    }
                    _ = fileContent.AppendFormat($":{Field.Value}\r\n");
                }
                _ = fileContent.Append("END:VEVENT\r\n");
            }
        }
    }
}