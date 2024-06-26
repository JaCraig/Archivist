using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a StructuredObject to a Card.
    /// </summary>
    public class StructuredObjectToCardConverter : IDataConverter
    {
        /// <summary>
        /// Converts a StructuredObject to a Card.
        /// </summary>
        /// <param name="file">The StructuredObject to convert.</param>
        /// <returns>The converted Card.</returns>
        public static Card? Convert(StructuredObject? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Card();
            foreach (var Key in file.Keys)
            {
                ReturnValue.Fields.Add(new CardField(Key, Array.Empty<CardFieldParameter>(), file[Key]?.ToString() ?? ""));
            }
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value?.ToString() ?? "");
            }
            ReturnValue.Title = file.Title;
            return ReturnValue;
        }

        /// <summary>
        /// Determines if the conversion is possible between the source and destination types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(StructuredObject) && destination == typeof(Card);

        /// <summary>
        /// Converts an object to the specified destination type.
        /// </summary>
        /// <param name="source">The object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not StructuredObject File || destination is null)
                return null;
            return Convert(File);
        }
    }
}