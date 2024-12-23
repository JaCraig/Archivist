using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Calendar object to a StructuredObject object.
    /// </summary>
    public class CalendarToStructuredObjectConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Calendar object to a StructuredObject object.
        /// </summary>
        /// <param name="file">The Calendar object to convert.</param>
        /// <returns>The converted StructuredObject object.</returns>
        [return: NotNullIfNotNull(nameof(file))]
        public static StructuredObject? Convert(Calendar? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new StructuredObject();
            foreach (CalendarComponent Component in file.Components)
            {
                foreach (KeyValueField? Field in Component.Fields)
                {
                    if (Field is null)
                        continue;
                    ReturnValue[Field.Property] = Field.Value;
                }
            }
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata[Metadata.Key] = Metadata.Value;
            }
            ReturnValue.Title = file.Title ?? file.Events?.FirstOrDefault()?.Summary;
            return ReturnValue;
        }

        /// <summary>
        /// Determines if the conversion is possible between the specified types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Calendar) && destination == typeof(StructuredObject);

        /// <summary>
        /// Converts the source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        [return: NotNullIfNotNull(nameof(source))]
        [return: NotNullIfNotNull(nameof(destination))]
        public object? Convert(object? source, Type? destination)
        {
            if (source is not Calendar File || destination is null)
                return null;
            return Convert(File);
        }
    }
}