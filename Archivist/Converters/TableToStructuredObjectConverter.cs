using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Table object to a StructuredObject object.
    /// </summary>
    public class TableToStructuredObjectConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Table object to a StructuredObject object.
        /// </summary>
        /// <param name="file">The Table object to convert.</param>
        /// <returns>The converted StructuredObject object.</returns>
        public static StructuredObject? Convert(Table? file)
        {
            if (file is null)
                return null;
            IDictionary<string, object?> ContentObject = new ExpandoObject();
            foreach (var Column in file.Columns)
            {
                var ColumnList = new List<object>();
                ContentObject.Add(Column, ColumnList);
                foreach (TableRow Row in file)
                {
                    var Content = Row[Column]?.Content;
                    if (string.IsNullOrEmpty(Content))
                        continue;
                    ColumnList.Add(Content);
                }
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
        /// Determines if the conversion is possible between the specified source and destination types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Table) && destination == typeof(StructuredObject);

        /// <summary>
        /// Converts the specified source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
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