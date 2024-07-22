using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a single <see cref="Table"/> object to a collection of <see cref="Tables"/> objects.
    /// </summary>
    public class TableToTablesConverter : IDataConverter
    {
        /// <summary>
        /// Converts a <see cref="Table"/> object to a <see cref="Tables"/> object.
        /// </summary>
        /// <param name="file">The <see cref="Table"/> object to convert.</param>
        /// <returns>The converted <see cref="Tables"/> object.</returns>
        public static Tables? Convert(Table? file)
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
            ReturnValue.Title = file.Title;
            return ReturnValue;
        }

        /// <summary>
        /// Determines whether the conversion from the source type to the destination type is supported.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns><c>true</c> if the conversion is supported; otherwise, <c>false</c>.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Table) && destination == typeof(Tables);

        /// <summary>
        /// Converts an object to the specified destination type.
        /// </summary>
        /// <param name="source">The object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not Table File || destination is null)
                return null;
            return Convert(File);
        }
    }
}