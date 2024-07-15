using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts Tables to StructuredObject.
    /// </summary>
    public class TablesToStructuredObjectConverter : IDataConverter
    {
        /// <summary>
        /// Converts Tables to StructuredObject.
        /// </summary>
        /// <param name="file">The Tables object to convert.</param>
        /// <returns>The converted StructuredObject.</returns>
        public static StructuredObject? Convert(Tables? file)
        {
            if (file is null)
                return null;
            IDictionary<string, object?> ContentObject = new ExpandoObject();
            foreach (Table Table in file)
            {
                if (string.IsNullOrEmpty(Table.Title))
                    continue;
                ContentObject.Add(Table.Title, (StructuredObject?)Table);
            }
            var ReturnValue = new StructuredObject(ContentObject);
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            ReturnValue.Title = file.Title;
            return ReturnValue;
        }

        /// <summary>
        /// Determines if the conversion is possible between Tables and StructuredObject.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Tables) && destination == typeof(StructuredObject);

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