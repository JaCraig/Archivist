using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a collection of Tables to a single Table.
    /// </summary>
    public class TablesToTableConverter : IDataConverter
    {
        /// <summary>
        /// Converts a collection of Tables to a single Table.
        /// </summary>
        /// <param name="file">The collection of Tables to convert.</param>
        /// <returns>The converted Table.</returns>
        public static Table? Convert(Tables? file)
        {
            if (file is null)
                return null;
            Table? ReturnValue = file.FirstOrDefault();
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
        /// Determines if the conversion is possible between the specified types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type source, Type destination) => source == typeof(Tables) && destination == typeof(Table);

        /// <summary>
        /// Converts the source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type destination)
        {
            if (source is not Tables File || destination is null)
                return null;
            return Convert(File);
        }
    }
}