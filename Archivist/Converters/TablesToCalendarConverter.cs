using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts Tables to Calendar.
    /// </summary>
    public class TablesToCalendarConverter : IDataConverter
    {
        /// <summary>
        /// Converts Tables to Calendar.
        /// </summary>
        /// <param name="file">The Tables object to convert.</param>
        /// <returns>The converted Calendar object.</returns>
        public static Calendar? Convert(Tables? file)
        {
            if (file is null)
                return null;
            var ReturnValue = (Calendar?)file.FirstOrDefault();
            if (ReturnValue is null)
                return null;
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            ReturnValue.Title = file.Title;
            return ReturnValue;
        }

        /// <summary>
        /// Checks if conversion is possible from Tables to Calendar.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Tables) && destination == typeof(Calendar);

        /// <summary>
        /// Converts the source object to the destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not Tables File || destination is null)
                return null;
            return Convert(File);
        }
    }
}