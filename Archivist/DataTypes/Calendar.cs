using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.Enums;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a calendar object.
    /// </summary>
    public class Calendar : FileBaseClass<Calendar>, IComparable<Calendar>, IEquatable<Calendar>, IObjectConvertable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Calendar"/> class.
        /// </summary>
        public Calendar()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Calendar"/> class with the specified converter.
        /// </summary>
        /// <param name="converter">The converter to use.</param>
        public Calendar(Convertinator? converter)
            : base(converter)
        {
        }

        /// <summary>
        /// Gets the list of alarms in the calendar.
        /// </summary>
        public List<CalendarComponent> Alarms { get; } = new List<CalendarComponent>();

        /// <summary>
        /// Gets the list of all components in the calendar.
        /// </summary>
        public IEnumerable<CalendarComponent> Components => Alarms.Concat(Events).Concat(FreeBusy).Concat(Journals).Concat(TimeZones).Concat(ToDos);

        /// <summary>
        /// Gets or sets the current time zone.
        /// </summary>
        public TimeZoneInfo CurrentTimeZone { get; set; } = TimeZoneInfo.Local;

        /// <summary>
        /// Gets the list of events in the calendar.
        /// </summary>
        public List<CalendarComponent> Events { get; } = new List<CalendarComponent>();

        /// <summary>
        /// Gets the list of free/busy time slots in the calendar.
        /// </summary>
        public List<CalendarComponent> FreeBusy { get; } = new List<CalendarComponent>();

        /// <summary>
        /// Gets a value indicating whether the calendar item is a cancellation.
        /// </summary>
        public bool IsCancelled => string.Equals(Method, "CANCEL", StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Gets the list of journals in the calendar.
        /// </summary>
        public List<CalendarComponent> Journals { get; } = new List<CalendarComponent>();

        /// <summary>
        /// Gets or sets the method of the calendar item.
        /// </summary>
        public string Method { get; set; } = CalendarMethods.Request;

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        public string ProductId { get; set; } = "-//Archivist//EN";

        /// <summary>
        /// Gets the list of time zones in the calendar.
        /// </summary>
        public List<CalendarComponent> TimeZones { get; } = new List<CalendarComponent>();

        /// <summary>
        /// Gets the list of to-do items in the calendar.
        /// </summary>
        public List<CalendarComponent> ToDos { get; } = new List<CalendarComponent>();

        /// <summary>
        /// Gets or sets the version of the calendar item.
        /// </summary>
        public string Version { get; set; } = "2.0";

        /// <summary>
        /// Converts the Calendar to a Card.
        /// </summary>
        /// <param name="file">The Calendar to convert.</param>
        /// <returns>The Card representation of the Calendar.</returns>
        public static implicit operator Card?(Calendar? file)
        {
            return CalendarToCardConverter.Convert(file);
        }

        /// <summary>
        /// Converts the Calendar to a structured object.
        /// </summary>
        /// <param name="file">The Calendar to convert.</param>
        /// <returns>The structured object representation of the Calendar.</returns>
        public static implicit operator StructuredObject?(Calendar? file)
        {
            return CalendarToStructuredObjectConverter.Convert(file);
        }

        /// <summary>
        /// Converts the Calendar to a table.
        /// </summary>
        /// <param name="file">The Calendar to convert.</param>
        /// <returns>The table representation of the Calendar.</returns>
        public static implicit operator Table?(Calendar? file)
        {
            return CalendarToTableConverter.Convert(file);
        }

        /// <summary>
        /// Converts the Calendar to a Tables file.
        /// </summary>
        /// <param name="file">The Calendar to convert.</param>
        /// <returns>The Tables representation of the Calendar.</returns>
        public static implicit operator Tables?(Calendar? file)
        {
            return CalendarToTablesConverter.Convert(file);
        }

        /// <summary>
        /// Converts the Calendar to text.
        /// </summary>
        /// <param name="file">The Calendar to convert.</param>
        /// <returns>The text representation of the Calendar.</returns>
        public static implicit operator Text?(Calendar? file)
        {
            if (file is null)
                return null;
            Text? ReturnValue = AnythingToTextConverter.Convert(file);
            if (ReturnValue is not null)
                ReturnValue.Title ??= file.Events?.FirstOrDefault()?.Descriptions.FirstOrDefault()?.Value;
            return ReturnValue;
        }

        /// <summary>
        /// Adds an alarm to the Calendar item.
        /// </summary>
        /// <param name="action">Action taken by the alarm</param>
        /// <param name="trigger">
        /// Trigger for the alarm (ex: "-P2D" = 2 days before the event, "-PT30M" = 30 minutes
        /// before the event, see https://icalendar.org/iCalendar-RFC-5545/3-6-6-alarm-component.html)
        /// </param>
        /// <param name="description">Description of the alarm</param>
        /// <returns>The alarm that was added.</returns>
        public CalendarComponent AddAlarm(string? action, string? trigger, string? description)
        {
            var NewAlarm = new CalendarComponent(this)
            {
                Action = action ?? "",
                Trigger = trigger ?? "",
                Description = description ?? ""
            };
            Alarms.Add(NewAlarm);
            return NewAlarm;
        }

        /// <summary>
        /// Adds a new event to the calendar.
        /// </summary>
        /// <param name="summary">The summary of the event.</param>
        /// <param name="description">The description of the event.</param>
        /// <param name="location">The location of the event.</param>
        /// <param name="start">The start date and time of the event.</param>
        /// <param name="end">The end date and time of the event.</param>
        /// <returns>The newly added calendar component representing the event.</returns>
        public CalendarComponent AddEvent(string? summary, string? description, string? location, DateTime start, DateTime end)
        {
            var NewEvent = new CalendarComponent(this)
            {
                Summary = summary ?? "",
                Description = description ?? "",
                Location = location ?? "",
                StartDate = start,
                EndDate = end
            };
            Events.Add(NewEvent);
            return NewEvent;
        }

        /// <summary>
        /// Adds a new free/busy time slot to the calendar.
        /// </summary>
        /// <returns>The newly added calendar component representing the free/busy time slot.</returns>
        public CalendarComponent AddFreeBusy()
        {
            var NewFreeBusy = new CalendarComponent(this);
            FreeBusy.Add(NewFreeBusy);
            return NewFreeBusy;
        }

        /// <summary>
        /// Adds a new journal to the calendar.
        /// </summary>
        /// <returns>The newly added calendar component representing the journal.</returns>
        public CalendarComponent AddJournal()
        {
            var NewJournal = new CalendarComponent(this);
            Journals.Add(NewJournal);
            return NewJournal;
        }

        /// <summary>
        /// Adds a new time zone to the calendar.
        /// </summary>
        /// <returns>The newly added calendar component representing the time zone.</returns>
        public CalendarComponent AddTimeZone()
        {
            var NewTimeZone = new CalendarComponent(this);
            TimeZones.Add(NewTimeZone);
            return NewTimeZone;
        }

        /// <summary>
        /// Adds a new to-do item to the calendar.
        /// </summary>
        /// <returns>The newly added calendar component representing the to-do item.</returns>
        public CalendarComponent AddToDo()
        {
            var NewToDo = new CalendarComponent(this);
            ToDos.Add(NewToDo);
            return NewToDo;
        }

        /// <summary>
        /// Compares the Calendar to another Calendar based on their content.
        /// </summary>
        /// <param name="other">The other Calendar to compare.</param>
        /// <returns>An integer that indicates the relative order of the Calendars.</returns>
        public override int CompareTo(Calendar? other) => string.Compare(other?.GetContent(), GetContent(), StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Converts the object to the Calendar.
        /// </summary>
        /// <typeparam name="TObject">The type of object to convert.</typeparam>
        /// <param name="obj">The object to convert.</param>
        public void ConvertFrom<TObject>(TObject obj)
        {
            if (obj is null)
                return;
            if (Events.Count == 0)
                _ = AddEvent("", "", "", DateTime.Now, DateTime.Now);
            foreach (System.Reflection.PropertyInfo Property in typeof(TObject).GetProperties())
            {
                var Found = false;
                foreach (CalendarComponent Component in Components)
                {
                    KeyValueField? Field = Component.Fields.Find(field => string.Equals(field?.Property, Property.Name, StringComparison.OrdinalIgnoreCase));
                    if (Field is not null)
                    {
                        Field.Value = Property.GetValue(obj)?.ToString() ?? "";
                        Found = true;
                    }
                }
                if (!Found)
                    Events[0].Fields.Add(new KeyValueField(Property.Name, Array.Empty<KeyValueParameter>(), Property.GetValue(obj)?.ToString() ?? ""));
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
                foreach (CalendarComponent Component in Components)
                {
                    KeyValueField? Field = Component.Fields.Find(field => string.Equals(field?.Property, Property.Name, StringComparison.OrdinalIgnoreCase));
                    if (Field is null)
                        continue;
                    Property.SetValue(Result, Field.Value);
                }
            }
            return Result;
        }

        /// <summary>
        /// Determines whether the Calendar is equal to another Calendar based on their content.
        /// </summary>
        /// <param name="other">The other Calendar to compare.</param>
        /// <returns>True if the Calendars are equal; otherwise, false.</returns>
        public override bool Equals(Calendar? other) => string.Equals(GetContent(), other?.GetContent(), StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the Calendar is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the Calendar is equal to the object; otherwise, false.</returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is Calendar CalendarObject && Equals(CalendarObject));

        /// <summary>
        /// Gets the content of the Calendar.
        /// </summary>
        /// <returns>The content of the Calendar.</returns>
        public override string? GetContent()
        {
            var StringContent = new StringBuilder();
            foreach (CalendarComponent Component in Components)
            {
                foreach (KeyValueField? Field in Component.Fields)
                {
                    if (Field is null)
                        continue;
                    _ = StringContent.Append(Field.Property);
                    _ = StringContent.Append(": ");
                    _ = StringContent.Append(Field.Value);
                    _ = StringContent.Append('\n');
                }
                _ = StringContent.Append('\n');
            }
            return StringContent.ToString();
        }

        /// <summary>
        /// Gets the hash code of the Calendar based on its content.
        /// </summary>
        /// <returns>The hash code of the Calendar.</returns>
        public override int GetHashCode() => GetContent()?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;

        /// <summary>
        /// Converts the Calendar to the specified object type.
        /// </summary>
        /// <typeparam name="TFile">The type to convert the Calendar to.</typeparam>
        /// <returns>The converted Calendar.</returns>
        public override TFile? ToFileType<TFile>()
            where TFile : default
        {
            Type FileType = typeof(TFile);
            IGenericFile? ReturnValue;
            if (FileType == typeof(Calendar))
                ReturnValue = (IGenericFile?)this;
            else if (FileType == typeof(Table))
                ReturnValue = (Table?)this;
            else if (FileType == typeof(Tables))
                ReturnValue = (Tables?)this;
            else if (FileType == typeof(Text))
                ReturnValue = (Text?)this;
            else if (FileType == typeof(StructuredObject))
                ReturnValue = (StructuredObject?)this;
            else
                ReturnValue = (IGenericFile?)Converter?.Convert(this, typeof(TFile));

            return (TFile?)ReturnValue;
        }
    }
}