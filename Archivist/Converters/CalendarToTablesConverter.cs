using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Calendar object to Tables object.
    /// </summary>
    public class CalendarToTablesConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Calendar object to Tables object.
        /// </summary>
        /// <param name="file">The Calendar object to convert.</param>
        /// <returns>The converted Tables object.</returns>
        public static Tables? Convert(CalendarComponent? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Tables
            {
                file
            };
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            ReturnValue.Title = file.Title ?? file.Summary;
            return ReturnValue;
        }

        /// <summary>
        /// Determines if the conversion is possible between the specified types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(CalendarComponent) && destination == typeof(Tables);

        /// <summary>
        /// Converts the specified object to the destination type.
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