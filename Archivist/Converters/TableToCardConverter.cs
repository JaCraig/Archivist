using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Table object to a Card object.
    /// </summary>
    public class TableToCardConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Table object to a Card object.
        /// </summary>
        /// <param name="file">The Table object to convert.</param>
        /// <returns>The converted Card object.</returns>
        public static Card? Convert(Table? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Card() { Title = file.Title };
            if (file.Count == 0 || file.Columns.Count == 0)
                return ReturnValue;
            foreach (var Column in file.Columns)
            {
                ReturnValue.Fields.Add(new CardField(Column, Array.Empty<CardFieldParameter>(), file[0][Column].Content));
            }
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Checks if the conversion is possible between the specified source and destination types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type source, Type destination) => source == typeof(Table) && destination == typeof(Card);

        /// <summary>
        /// Converts the specified source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type destination)
        {
            if (source is not Table File || destination is null)
                return null;
            return Convert(File);
        }
    }
}