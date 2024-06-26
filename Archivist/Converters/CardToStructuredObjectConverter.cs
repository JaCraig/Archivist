using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Card object to a StructuredObject object.
    /// </summary>
    public class CardToStructuredObjectConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Card object to a StructuredObject object.
        /// </summary>
        /// <param name="file">The Card object to convert.</param>
        /// <returns>The converted StructuredObject object.</returns>
        public static StructuredObject? Convert(Card? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new StructuredObject();
            foreach (CardField? Field in file.Fields)
            {
                if (Field is null)
                    continue;
                ReturnValue[Field.Property] = Field.Value;
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
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Card) && destination == typeof(StructuredObject);

        /// <summary>
        /// Converts the source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
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