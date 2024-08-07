﻿using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a StructuredObject to Tables.
    /// </summary>
    public class StructuredObjectToTablesConverter : IDataConverter
    {
        /// <summary>
        /// Converts a StructuredObject to Tables.
        /// </summary>
        /// <param name="file">The StructuredObject to convert.</param>
        /// <returns>The converted Tables object.</returns>
        public static Tables? Convert(StructuredObject? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Tables();
            Table Table = ReturnValue.AddTable();
            Table.Columns.AddRange(file.Keys);
            TableRow Row = Table.AddRow();
            foreach (var Key in file.Keys)
            {
                var Value = file[Key]?.ToString();
                Row.Add(Value);
            }
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value?.ToString() ?? "");
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
        public bool CanConvert(Type? source, Type? destination) => source == typeof(StructuredObject) && destination == typeof(Tables);

        /// <summary>
        /// Converts the source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
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