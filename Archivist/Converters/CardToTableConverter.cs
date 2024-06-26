using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Card object to a Table object.
    /// </summary>
    public class CardToTableConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Card object to a Table object.
        /// </summary>
        /// <param name="file">The Card object to convert.</param>
        /// <returns>The converted Table object, or null if the Card object is null.</returns>
        public static Table? Convert(Card? file)
        {
            if (file is null)
                return null;
            var Table = new Table
            {
                Title = file.Title ?? file.FullName?.Value
            };
            TableRow Row = Table.AddRow();
            foreach (CardField? Field in file.Fields)
            {
                if (Field is null)
                    continue;
                Row.Add(Field.Value);
                Table.Columns.Add(Field.Property);
            }
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                Table.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            return Table;
        }

        /// <summary>
        /// Determines if the converter can convert from the specified source type to the specified destination type.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Card) && destination == typeof(Table);

        /// <summary>
        /// Converts the specified source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object, or null if the conversion is not possible.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not Card File || destination is null)
                return null;
            return Convert(File);
        }
    }
}