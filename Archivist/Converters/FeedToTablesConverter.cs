using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Feed object to Tables object.
    /// </summary>
    public class FeedToTablesConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Feed object to Tables object.
        /// </summary>
        /// <param name="file">The Feed object to convert.</param>
        /// <returns>The converted Tables object.</returns>
        public static Tables? Convert(Feed? file)
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
            ReturnValue.Title = file.Title ?? file.Channels.FirstOrDefault()?.Title ?? "";
            return ReturnValue;
        }

        /// <summary>
        /// Determines if the conversion is possible between the specified types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Feed) && destination == typeof(Tables);

        /// <summary>
        /// Converts the specified object to the destination type.
        /// </summary>
        /// <param name="source">The object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not Feed File || destination is null)
                return null;
            return Convert(File);
        }
    }
}