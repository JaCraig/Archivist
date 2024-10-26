using Archivist.BaseClasses;
using Archivist.Enums;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a Calendar (vCalendar, etc.) file.
    /// </summary>
    /// <seealso cref="FileBaseClass{Calendar}"/>
    public class CalendarComponent : IComparable<CalendarComponent>, IEquatable<CalendarComponent>, IEnumerable<KeyValueField?>, IObjectConvertable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarComponent"/> class.
        /// </summary>
        /// <param name="parent">The parent Calendar.</param>
        public CalendarComponent(Calendar? parent)
        {
            _Parent = parent;
        }

        /// <summary>
        /// Gets or sets the action of the Calendar item.
        /// </summary>
        public string Action
        {
            get => Actions.FirstOrDefault()?.Value ?? "";
            set
            {
                foreach (KeyValueField? Item in Actions)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Action, Array.Empty<KeyValueParameter>(), value));
            }
        }

        /// <summary>
        /// Gets the action fields of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Actions => this[CommonCalendarFields.Action];

        /// <summary>
        /// Gets the attachments of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Attachments => this[CommonCalendarFields.Attachment];

        /// <summary>
        /// Gets the attendees of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Attendees => this[CommonCalendarFields.Attendee];

        /// <summary>
        /// Gets the categories of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Categories => this[CommonCalendarFields.Categories];

        /// <summary>
        /// Gets the class of the Calendar item.
        /// </summary>
        public string Class
        {
            get => Classes.FirstOrDefault()?.Value ?? "PUBLIC";
            set
            {
                foreach (KeyValueField? Item in Classes)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Class, Array.Empty<KeyValueParameter>(), value));
            }
        }

        /// <summary>
        /// Gets the classes of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Classes => this[CommonCalendarFields.Class];

        /// <summary>
        /// Gets the comments of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Comments => this[CommonCalendarFields.Comment];

        /// <summary>
        /// Gets the completed fields of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Completeds => this[CommonCalendarFields.Completed];

        /// <summary>
        /// Gets the contacts of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Contacts => this[CommonCalendarFields.Contact];

        /// <summary>
        /// Gets the number of fields in the Calendar.
        /// </summary>
        public int Count => Fields.Count;

        /// <summary>
        /// Gets or sets the creation date of the Calendar item.
        /// </summary>
        public DateTime Created
        {
            get
            {
                if (DateTime.TryParse(this[CommonCalendarFields.Created].FirstOrDefault()?.Value.FormatString("####/##/## ##:##"), CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime Result))
                    return Result + CurrentTimeZone.GetUtcOffset(Result);
                return DateTime.UtcNow + CurrentTimeZone.GetUtcOffset(DateTime.Now);
            }
            set
            {
                KeyValueField? Value = this[CommonCalendarFields.Created].FirstOrDefault();
                if (Value is not null)
                {
                    Value.Value = value.ToUniversalTime().ToString("yyyyMMddTHHmmss");
                    return;
                }
                Fields.Add(new KeyValueField(CommonCalendarFields.Created, Array.Empty<KeyValueParameter>(), value.ToUniversalTime().ToString("yyyyMMddTHHmmss")));
            }
        }

        /// <summary>
        /// Gets the created fields of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Createds => this[CommonCalendarFields.Created];

        /// <summary>
        /// Gets or sets the creation date of the Calendar item in Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime CreatedUtc
        {
            get
            {
                if (DateTime.TryParse(this[CommonCalendarFields.Created].FirstOrDefault()?.Value.FormatString("####/##/## ##:##"), CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out DateTime Result))
                    return Result.ToUniversalTime();
                return DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Gets or sets the date stamp of the Calendar item.
        /// </summary>
        public DateTime DateStamp
        {
            get
            {
                if (DateTime.TryParse(this[CommonCalendarFields.DateStamp].FirstOrDefault()?.Value.FormatString("####/##/## ##:##"), CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime Result))
                    return Result + CurrentTimeZone.GetUtcOffset(Result);
                return DateTime.UtcNow + CurrentTimeZone.GetUtcOffset(DateTime.Now);
            }
            set
            {
                KeyValueField? Value = this[CommonCalendarFields.DateStamp].FirstOrDefault();
                if (Value is not null)
                {
                    Value.Value = value.ToUniversalTime().ToString("yyyyMMddTHHmmss");
                    return;
                }
                Fields.Add(new KeyValueField(CommonCalendarFields.DateStamp, Array.Empty<KeyValueParameter>(), value.ToUniversalTime().ToString("yyyyMMddTHHmmss")));
            }
        }

        /// <summary>
        /// Gets the date stamps of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> DateStamps => this[CommonCalendarFields.DateStamp];

        /// <summary>
        /// Gets the date stamp of the Calendar item in Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime DateStampUtc
        {
            get
            {
                if (DateTime.TryParse(this[CommonCalendarFields.DateStamp].FirstOrDefault()?.Value.FormatString("####/##/## ##:##"), CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out DateTime Result))
                    return Result.ToUniversalTime();
                return DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Gets or sets the description of the Calendar item.
        /// </summary>
        public string Description
        {
            get => string.Join(' ', Descriptions.Select(x => x?.Value ?? ""))?.Replace("\\n", "\n").Replace("\\,", ",").Replace("\\r", "").Replace("\\t", "\t") ?? "";
            set
            {
                var FinalValue = value?.Replace("\n", "\\n").Replace(",", "\\,").Replace("\t", "\\t") ?? "";
                foreach (KeyValueField? Item in Descriptions)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Description, Array.Empty<KeyValueParameter>(), FinalValue));
            }
        }

        /// <summary>
        /// Gets the descriptions of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Descriptions => this[CommonCalendarFields.Description];

        /// <summary>
        /// Gets the due dates of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Dues => this[CommonCalendarFields.Due];

        /// <summary>
        /// Gets the durations of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Durations => this[CommonCalendarFields.Duration];

        /// <summary>
        /// Gets or sets the end date of the Calendar item.
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (DateTime.TryParse(this[CommonCalendarFields.EndDate].FirstOrDefault()?.Value.FormatString("####/##/## ##:##"), CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime Result))
                    return Result + CurrentTimeZone.GetUtcOffset(Result);
                return StartDate;
            }
            set
            {
                KeyValueField? Value = this[CommonCalendarFields.EndDate].FirstOrDefault();
                if (Value is not null)
                {
                    Value.Value = value.ToUniversalTime().ToString("yyyyMMddTHHmmss");
                    return;
                }
                Fields.Add(new KeyValueField(CommonCalendarFields.EndDate, Array.Empty<KeyValueParameter>(), value.ToUniversalTime().ToString("yyyyMMddTHHmmss")));
            }
        }

        /// <summary>
        /// Gets the end dates of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> EndDates => this[CommonCalendarFields.EndDate];

        /// <summary>
        /// Gets the end date of the Calendar item in Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime EndDateUtc
        {
            get
            {
                if (DateTime.TryParse(this[CommonCalendarFields.EndDate].FirstOrDefault()?.Value.FormatString("####/##/## ##:##"), CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out DateTime Result))
                    return Result.ToUniversalTime();
                return StartDateUtc;
            }
        }

        /// <summary>
        /// Gets the exceptions of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> ExcludeDates => this[CommonCalendarFields.ExcludeDates];

        /// <summary>
        /// Gets the exclude rules of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> ExcludeRules => this[CommonCalendarFields.ExcludeRule];

        /// <summary>
        /// Gets or sets the fields of the Calendar.
        /// </summary>
        public List<KeyValueField?> Fields { get; } = new List<KeyValueField?>();

        /// <summary>
        /// Gets the free/busy fields of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> FreeBusys => this[CommonCalendarFields.FreeBusy];

        /// <summary>
        /// Gets the geo locations of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> GeoLocations => this[CommonCalendarFields.Geo];

        /// <summary>
        /// Gets or sets the last modified date of the Calendar item.
        /// </summary>
        public DateTime LastModified
        {
            get
            {
                if (DateTime.TryParse(this[CommonCalendarFields.LastModified].FirstOrDefault()?.Value.FormatString("####/##/## ##:##"), CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime Result))
                    return Result + CurrentTimeZone.GetUtcOffset(Result);
                return DateTime.Now;
            }
            set
            {
                KeyValueField? Value = this[CommonCalendarFields.LastModified].FirstOrDefault();
                if (Value is not null)
                {
                    Value.Value = value.ToUniversalTime().ToString("yyyyMMddTHHmmss");
                    return;
                }
                Fields.Add(new KeyValueField(CommonCalendarFields.LastModified, Array.Empty<KeyValueParameter>(), value.ToUniversalTime().ToString("yyyyMMddTHHmmss")));
            }
        }

        /// <summary>
        /// Gets the last modified fields of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> LastModifieds => this[CommonCalendarFields.LastModified];

        /// <summary>
        /// Gets the last modified field of the Calendar item in Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime LastModifiedUtc
        {
            get
            {
                if (DateTime.TryParse(this[CommonCalendarFields.LastModified].FirstOrDefault()?.Value.FormatString("####/##/## ##:##"), CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out DateTime Result))
                    return Result.ToUniversalTime();
                return DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Gets the location of the Calendar item.
        /// </summary>
        public string Location
        {
            get => string.Join(';', Locations.Select(x => x?.Value ?? ""));
            set
            {
                foreach (KeyValueField? Item in Locations)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Location, Array.Empty<KeyValueParameter>(), value));
            }
        }

        /// <summary>
        /// Gets the locations of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Locations => this[CommonCalendarFields.Location];

        /// <summary>
        /// Gets the organizers of the Calendar.
        /// </summary>
        public IEnumerable<KeyValueField?> Organizers => this[CommonCalendarFields.Organizer];

        /// <summary>
        /// Gets the priorities of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Priorities => this[CommonCalendarFields.Priority];

        /// <summary>
        /// Gets or sets the priority of the Calendar item.
        /// </summary>
        public string Priority
        {
            get => Priorities.FirstOrDefault()?.Value ?? "0";
            set
            {
                foreach (KeyValueField? Item in Priorities)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Priority, Array.Empty<KeyValueParameter>(), value));
            }
        }

        /// <summary>
        /// Gets the recurrence IDs of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> RecurrenceIds => this[CommonCalendarFields.RecurrenceId];

        /// <summary>
        /// Gets the related to fields of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> RelatedTos => this[CommonCalendarFields.RelatedTo];

        /// <summary>
        /// Gets the reoccur dates of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> ReoccurDates => this[CommonCalendarFields.ReoccurDates];

        /// <summary>
        /// Gets the reoccur rules of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> ReoccurRules => this[CommonCalendarFields.ReoccurRule];

        /// <summary>
        /// Gets the repeat counts of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> RepeatCounts => this[CommonCalendarFields.RepeatCount];

        /// <summary>
        /// Gets the resources of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Resources => this[CommonCalendarFields.Resources];

        /// <summary>
        /// Gets or sets the sequence of the Calendar item.
        /// </summary>
        public string Sequence
        {
            get => Sequences.FirstOrDefault()?.Value ?? "1";
            set
            {
                foreach (KeyValueField? Item in Sequences)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Sequence, Array.Empty<KeyValueParameter>(), value));
            }
        }

        /// <summary>
        /// Gets the sequence fields of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Sequences => this[CommonCalendarFields.Sequence];

        /// <summary>
        /// Gets or sets the start date of the Calendar item.
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                if (DateTime.TryParse(this[CommonCalendarFields.StartDate].FirstOrDefault()?.Value.FormatString("####/##/## ##:##"), CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime Result))
                    return Result + CurrentTimeZone.GetUtcOffset(Result);
                return DateTime.Now;
            }
            set
            {
                KeyValueField? Value = this[CommonCalendarFields.StartDate].FirstOrDefault();
                if (Value is not null)
                {
                    Value.Value = value.ToUniversalTime().ToString("yyyyMMddTHHmmss");
                    return;
                }
                Fields.Add(new KeyValueField(CommonCalendarFields.StartDate, Array.Empty<KeyValueParameter>(), value.ToUniversalTime().ToString("yyyyMMddTHHmmss")));
            }
        }

        /// <summary>
        /// Gets the start dates of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> StartDates => this[CommonCalendarFields.StartDate];

        /// <summary>
        /// Gets the start date of the Calendar item in Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime StartDateUtc
        {
            get
            {
                if (DateTime.TryParse(this[CommonCalendarFields.StartDate].FirstOrDefault()?.Value.FormatString("####/##/## ##:##"), CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out DateTime Result))
                    return Result.ToUniversalTime();
                return DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Gets or sets the status of the Calendar item.
        /// </summary>
        public string? Status
        {
            get => Statuses.FirstOrDefault()?.Value ?? CalendarStatuses.Confirmed;
            set
            {
                foreach (KeyValueField? Item in Statuses)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Status, Array.Empty<KeyValueParameter>(), value));
            }
        }

        /// <summary>
        /// Gets the statuses of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Statuses => this[CommonCalendarFields.Status];

        /// <summary>
        /// Gets the summaries of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Summaries => this[CommonCalendarFields.Summary];

        /// <summary>
        /// Gets or sets the summary of the Calendar item.
        /// </summary>
        public string Summary
        {
            get => string.Join(' ', Summaries.Select(x => x?.Value ?? "")) ?? "";
            set
            {
                foreach (KeyValueField? Item in Summaries)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Summary, new[] { new KeyValueParameter("LANGUAGE", "en-us") }, value));
            }
        }

        /// <summary>
        /// Gets the time zone names of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> TimezoneNames => this[CommonCalendarFields.TimezoneName];

        /// <summary>
        /// Gets the time zone offsets from of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> TimezoneOffsetFroms => this[CommonCalendarFields.TimezoneOffsetFrom];

        /// <summary>
        /// Gets the time zone offsets to of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> TimezoneOffsetTos => this[CommonCalendarFields.TimezoneOffsetTo];

        /// <summary>
        /// Gets the time zones of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> TimeZones => this[CommonCalendarFields.Timezone];

        /// <summary>
        /// Gets the time zone URLs of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> TimezoneUrls => this[CommonCalendarFields.TimezoneUrl];

        /// <summary>
        /// Gets or sets the transparency of the Calendar item.
        /// </summary>
        public string Transp
        {
            get => Transps.FirstOrDefault()?.Value ?? "OPAQUE";
            set
            {
                foreach (KeyValueField? Item in Transps)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Transp, Array.Empty<KeyValueParameter>(), value));
            }
        }

        /// <summary>
        /// Gets the TRANSP fields of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Transps => this[CommonCalendarFields.Transp];

        /// <summary>
        /// Gets the triggers of the Calendar item.
        /// </summary>
        public string Trigger
        {
            get => Triggers.FirstOrDefault()?.Value ?? "";
            set
            {
                foreach (KeyValueField? Item in Triggers)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Trigger, Array.Empty<KeyValueParameter>(), value));
            }
        }

        /// <summary>
        /// Gets the triggers of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> Triggers => this[CommonCalendarFields.Trigger];

        /// <summary>
        /// Gets or sets the unique identifier of the Calendar item.
        /// </summary>
        public string UID
        {
            get => UIDs.FirstOrDefault()?.Value ?? string.Format("{0}{1}{2}", StartDateUtc.ToString("yyyyMMddTHHmmssZ", CultureInfo.InvariantCulture), EndDateUtc.ToString("yyyyMMddTHHmmssZ", CultureInfo.InvariantCulture), Summary);
            set
            {
                foreach (KeyValueField? Item in UIDs)
                    _ = Fields.Remove(Item);
                Fields.Add(new KeyValueField(CommonCalendarFields.Uid, Array.Empty<KeyValueParameter>(), value));
            }
        }

        /// <summary>
        /// Gets the unique identifiers of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> UIDs => this[CommonCalendarFields.Uid];

        /// <summary>
        /// Gets the URLs of the Calendar item.
        /// </summary>
        public IEnumerable<KeyValueField?> URLs => this[CommonCalendarFields.Url];

        /// <summary>
        /// Gets or sets the current time zone.
        /// </summary>
        private TimeZoneInfo CurrentTimeZone => _Parent?.CurrentTimeZone ?? TimeZoneInfo.Local;

        /// <summary>
        /// Gets the parent calendar item.
        /// </summary>
        private readonly Calendar? _Parent;

        /// <summary>
        /// Gets or sets the field at the specified index.
        /// </summary>
        /// <param name="index">The index of the field.</param>
        /// <returns>The field at the specified index.</returns>
        public KeyValueField? this[int index]
        {
            get
            {
                if (index < 0 || index >= Fields.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return Fields[index];
            }
            set
            {
                if (index < 0 || index >= Fields.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                Fields[index] = value;
            }
        }

        /// <summary>
        /// Gets the fields with the specified property name.
        /// </summary>
        /// <param name="property">The property name of the fields.</param>
        /// <returns>The fields with the specified property name.</returns>
        public IEnumerable<KeyValueField?> this[string property] => Fields.Where(field => string.Equals(field?.Property, property, StringComparison.InvariantCultureIgnoreCase)).ToList();

        /// <summary>
        /// Gets the fields with the specified property name and parameter.
        /// </summary>
        /// <param name="property">The property name of the fields.</param>
        /// <param name="parameter">The parameter of the fields.</param>
        /// <returns>The fields with the specified property name and parameter.</returns>
        public IEnumerable<KeyValueField?> this[string property, string? parameter] => Fields.Where(field => string.Equals(field?.Property, property, StringComparison.InvariantCultureIgnoreCase) && (field?.Parameters.Any(fieldParam => string.Equals(fieldParam.ToString(), parameter, StringComparison.InvariantCultureIgnoreCase)) ?? false)).ToList();

        /// <summary>
        /// Determines whether two Calendar objects are not equal.
        /// </summary>
        /// <param name="left">The first Calendar object to compare.</param>
        /// <param name="right">The second Calendar object to compare.</param>
        /// <returns>True if the two Calendar objects are not equal; otherwise, false.</returns>
        public static bool operator !=(CalendarComponent? left, CalendarComponent? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether one Calendar object is less than another Calendar object.
        /// </summary>
        /// <param name="left">The first Calendar object to compare.</param>
        /// <param name="right">The second Calendar object to compare.</param>
        /// <returns>
        /// True if the first Calendar object is less than the second Calendar object; otherwise, false.
        /// </returns>
        public static bool operator <(CalendarComponent? left, CalendarComponent? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether one Calendar object is less than or equal to another Calendar object.
        /// </summary>
        /// <param name="left">The first Calendar object to compare.</param>
        /// <param name="right">The second Calendar object to compare.</param>
        /// <returns>
        /// True if the first Calendar object is less than or equal to the second Calendar object;
        /// otherwise, false.
        /// </returns>
        public static bool operator <=(CalendarComponent? left, CalendarComponent? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two Calendar objects are equal.
        /// </summary>
        /// <param name="left">The first Calendar object to compare.</param>
        /// <param name="right">The second Calendar object to compare.</param>
        /// <returns>True if the two Calendar objects are equal; otherwise, false.</returns>
        public static bool operator ==(CalendarComponent? left, CalendarComponent? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether one Calendar object is greater than another Calendar object.
        /// </summary>
        /// <param name="left">The first Calendar object to compare.</param>
        /// <param name="right">The second Calendar object to compare.</param>
        /// <returns>
        /// True if the first Calendar object is greater than the second Calendar object; otherwise, false.
        /// </returns>
        public static bool operator >(CalendarComponent? left, CalendarComponent? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether one Calendar object is greater than or equal to another Calendar object.
        /// </summary>
        /// <param name="left">The first Calendar object to compare.</param>
        /// <param name="right">The second Calendar object to compare.</param>
        /// <returns>
        /// True if the first Calendar object is greater than or equal to the second Calendar
        /// object; otherwise, false.
        /// </returns>
        public static bool operator >=(CalendarComponent? left, CalendarComponent? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the Calendar to another Calendar based on their content.
        /// </summary>
        /// <param name="other">The other Calendar to compare.</param>
        /// <returns>An integer that indicates the relative order of the Calendars.</returns>
        public int CompareTo(CalendarComponent? other) => string.Compare(other?.GetContent(), GetContent(), StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Converts the object to the Calendar.
        /// </summary>
        /// <typeparam name="TObject">The type of object to convert.</typeparam>
        /// <param name="obj">The object to convert.</param>
        public void ConvertFrom<TObject>(TObject obj)
        {
            if (obj is null)
                return;
            foreach (System.Reflection.PropertyInfo Property in typeof(TObject).GetProperties())
            {
                KeyValueField? Field = Fields.Find(field => string.Equals(field?.Property, Property.Name, StringComparison.OrdinalIgnoreCase));
                var Value = Property.GetValue(obj)?.ToString() ?? "";
                if (Field is null)
                {
                    Field = new KeyValueField(Property.Name, Array.Empty<KeyValueParameter>(), Value);
                    Fields.Add(Field);
                }
                Field.Value = Value;
            }
        }

        /// <summary>
        /// Converts the Calendar to the specified object type.
        /// </summary>
        /// <typeparam name="TObject">The type to convert the Calendar to.</typeparam>
        /// <returns>The converted Calendar.</returns>
        public TObject? ConvertTo<TObject>()
        {
            System.Reflection.PropertyInfo[] Properties = typeof(TObject).GetProperties();
            TObject? Result = Activator.CreateInstance<TObject>();
            foreach (System.Reflection.PropertyInfo Property in Properties)
            {
                KeyValueField? Field = Fields.Find(field => string.Equals(field?.Property, Property.Name, StringComparison.OrdinalIgnoreCase));
                if (Field is null)
                    continue;
                Property.SetValue(Result, Field.Value);
            }
            return Result;
        }

        /// <summary>
        /// Determines whether the Calendar is equal to another Calendar based on their content.
        /// </summary>
        /// <param name="other">The other Calendar to compare.</param>
        /// <returns>True if the Calendars are equal; otherwise, false.</returns>
        public bool Equals(CalendarComponent? other) => string.Equals(GetContent(), other?.GetContent(), StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the Calendar is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the Calendar is equal to the object; otherwise, false.</returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is CalendarComponent CalendarObject && Equals(CalendarObject));

        /// <summary>
        /// Gets the content of the Calendar.
        /// </summary>
        /// <returns>The content of the Calendar.</returns>
        public string? GetContent() => string.Join(Environment.NewLine, Fields.Where(field => field is not null));

        /// <inheritdoc/>
        public IEnumerator<KeyValueField?> GetEnumerator() => ((IEnumerable<KeyValueField?>)Fields).GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Fields).GetEnumerator();

        /// <summary>
        /// Gets the hash code of the Calendar based on its content.
        /// </summary>
        /// <returns>The hash code of the Calendar.</returns>
        public override int GetHashCode() => GetContent()?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;
    }
}