using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Archivist.Formats.ICalendar
{
    /// <summary>
    /// Represents a reader for ICal files.
    /// </summary>
    public class ICalReader : ReaderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ICalReader"/> class.
        /// </summary>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public ICalReader(Convertinator? converter)
        {
            Converter = converter;
        }

        /// <summary>
        /// Gets the header information of the ICal file.
        /// </summary>
        public override byte[] HeaderInfo { get; } = new byte[] { 0x42, 0x45, 0x47, 0x49, 0x4E, 0x3A, 0x56, 0x43, 0x41, 0x4C, 0x45, 0x4E, 0x44, 0x41, 0x52 };

        /// <summary>
        /// Gets the regular expression used to split the parameters of the ICal property.
        /// </summary>
        private static Regex ParameterSplitter { get; } = new Regex("(?<Name>[^;=]+)=(?<Value>[^;]+)", RegexOptions.Compiled);

        /// <summary>
        /// Gets the regular expression used to split the properties of the ICal file.
        /// </summary>
        private static Regex PropertySplitter { get; } = new Regex("(?<Property>[^;:]+);?(?<Parameters>[^:]+)?:(?<Value>.*)", RegexOptions.Compiled);

        /// <summary>
        /// Gets the converter used to convert between IGenericFile objects.
        /// </summary>
        private Convertinator? Converter { get; }

        /// <summary>
        /// Represents the separator used to split the lines of the ICal file.
        /// </summary>
        private static readonly string[] _Separator = new string[] { "\r\n", Environment.NewLine };

        /// <summary>
        /// Reads a ICal file asynchronously from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read the ICal file from.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the parsed ICal file.
        /// </returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            if (stream?.CanRead != true)
                return new Calendar(Converter);
            var ReturnValue = new Calendar(Converter);
            var Content = await GetDataAsync(stream).ConfigureAwait(false);
            var Lines = Content.Split(_Separator, StringSplitOptions.RemoveEmptyEntries);
            if (Lines.Length is 0)
                return ReturnValue;
            var CurrentField = new KeyValueField("", Array.Empty<KeyValueParameter>(), "");
            var CurrentComponent = new CalendarComponent(ReturnValue);
            foreach (var Line in Lines)
            {
                if (Line.StartsWith(' '))
                {
                    CurrentField.Value += Line.Trim();
                    continue;
                }
                Match LineProperty = PropertySplitter.Match(Line);
                if (!LineProperty.Success)
                    continue;

                var PropertyName = LineProperty.Groups["Property"].Value.ToUpperInvariant().Trim();
                var Value = LineProperty.Groups["Value"].Value.Trim();
                switch (PropertyName)
                {
                    case "VERSION":
                        ReturnValue.Version = Value;
                        continue;
                    case "PRODID":
                        ReturnValue.ProductId = Value;
                        continue;
                    case "METHOD":
                        ReturnValue.Method = Value;
                        continue;
                    case "BEGIN" or "END" when Value is "VCALENDAR":
                        continue;
                    case "BEGIN" when Value is "VALARM":
                        CurrentComponent = new CalendarComponent(ReturnValue);
                        ReturnValue.Alarms.Add(CurrentComponent);
                        continue;
                    case "BEGIN" when Value is "VEVENT":
                        CurrentComponent = new CalendarComponent(ReturnValue);
                        ReturnValue.Events.Add(CurrentComponent);
                        continue;
                    case "BEGIN" when Value is "VTODO":
                        CurrentComponent = new CalendarComponent(ReturnValue);
                        ReturnValue.ToDos.Add(CurrentComponent);
                        continue;
                    case "BEGIN" when Value is "VJOURNAL":
                        CurrentComponent = new CalendarComponent(ReturnValue);
                        ReturnValue.Journals.Add(CurrentComponent);
                        continue;
                    case "BEGIN" when Value is "VFREEBUSY":
                        CurrentComponent = new CalendarComponent(ReturnValue);
                        ReturnValue.FreeBusy.Add(CurrentComponent);
                        continue;
                    case "BEGIN" when Value is "VTIMEZONE":
                        CurrentComponent = new CalendarComponent(ReturnValue);
                        ReturnValue.TimeZones.Add(CurrentComponent);
                        continue;
                }
                IEnumerable<KeyValueParameter>? Parameters = ParseParameters(LineProperty.Groups["Parameters"]?.Value);

                var Field = new KeyValueField(PropertyName, Parameters, Value);
                CurrentComponent.Fields.Add(Field);
                CurrentField = Field;
            }
            return ReturnValue;
        }

        /// <summary>
        /// Gets the data from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The data from the specified stream.</returns>
        private static async Task<string> GetDataAsync(Stream? stream)
        {
            try
            {
                var Content = await stream.ReadAllAsync().ConfigureAwait(false);
                return Content?.Replace("\r\n ", "").Replace(Environment.NewLine + " ", "") ?? "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Parses the parameters of a ICal property.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <returns>The parsed parameters as a collection of <see cref="KeyValueParameter"/>.</returns>
        private static IEnumerable<KeyValueParameter>? ParseParameters(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<KeyValueParameter>();
            var ReturnValue = new List<KeyValueParameter>();
            foreach (Match Parameter in ParameterSplitter.Matches(value))
            {
                var Name = Parameter.Groups["Name"].Value.Trim();
                var Value = Parameter.Groups["Value"].Value.Trim();
                ReturnValue.Add(new KeyValueParameter(Name, Value));
            }
            return ReturnValue;
        }
    }
}