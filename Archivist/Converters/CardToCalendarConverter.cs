using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Card object to a Calendar object.
    /// </summary>
    public class CardToCalendarConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Card object to a Calendar object.
        /// </summary>
        /// <param name="file">The Card object to convert.</param>
        /// <returns>The converted Calendar object.</returns>
        public static Calendar? Convert(Card? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Calendar();
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
            ReturnValue.Title = file.Title ?? file.FullName?.Value;
            return ReturnValue;
        }

        /// <summary>
        /// Determines if the conversion is possible between the specified types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Card) && destination == typeof(Calendar);

        /// <summary>
        /// Converts an object from the source type to the destination type.
        /// </summary>
        /// <param name="source">The object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not Card File || destination is null)
                return null;
            return Convert(File);
        }
    }
}