using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a <see cref="CalendarComponent"/> object to a <see cref="Card"/> object.
    /// </summary>
    public class CalendarToCardConverter : IDataConverter
    {
        /// <summary>
        /// Converts a <see cref="CalendarComponent"/> object to a <see cref="Card"/> object.
        /// </summary>
        /// <param name="file">The <see cref="CalendarComponent"/> object to convert.</param>
        /// <returns>The converted <see cref="Card"/> object.</returns>
        public static Card? Convert(CalendarComponent? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Card();
            foreach (KeyValueField? Field in file.Fields)
            {
                if (Field is null)
                    continue;
                ReturnValue.Fields.Add(new KeyValueField(Field));
            }
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata[Metadata.Key] = Metadata.Value;
            }
            ReturnValue.Title = file.Title;
            return ReturnValue;
        }

        /// <summary>
        /// Determines whether this converter can convert from the specified source type to the
        /// specified destination type.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>
        /// <c>true</c> if this converter can convert from the specified source type to the
        /// specified destination type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(CalendarComponent) && destination == typeof(Card);

        /// <summary>
        /// Converts the specified object to the specified destination type.
        /// </summary>
        /// <param name="source">The object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not CalendarComponent File || destination is null)
                return null;
            return Convert(File);
        }
    }
}