using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Feed object to a Table object.
    /// </summary>
    public class FeedToTableConverter : IDataConverter
    {
        /// <summary>
        /// The columns in the table.
        /// </summary>
        private static readonly string[] _Columns = new string[] { "Title", "Description", "Author", "Categories", "Description", "Enclosure", "GUID", "Link", "PubDate", "Thumbnail" };

        /// <summary>
        /// Converts a Feed object to a Table object.
        /// </summary>
        /// <param name="file">The Feed object to convert.</param>
        /// <returns>The converted Table object, or null if the Feed object is null.</returns>
        public static Table? Convert(Feed? file)
        {
            if (file is null)
                return null;
            var Table = new Table
            {
                Title = file.Title ?? file.Channels.FirstOrDefault()?.Title ?? ""
            };
            Table.Columns.AddRange(_Columns);
            foreach (DataTypes.Feeds.Channel? Channel in file.Channels)
            {
                if (Channel is null)
                    continue;
                foreach (DataTypes.Feeds.FeedItem? Item in Channel.Items)
                {
                    if (Item is null)
                        continue;
                    TableRow Row = Table.AddRow();
                    Row.Add(Item.Title);
                    Row.Add(Item.Description);
                    Row.Add(Item.Author);
                    Row.Add(string.Join(", ", Item.Categories));
                    Row.Add(Item.Description);
                    Row.Add(Item.Enclosure?.Url ?? "");
                    Row.Add(Item.GUID?.GuidText ?? "");
                    Row.Add(Item.Link);
                    Row.Add(Item.PubDate.ToUniversalTime().ToString());
                    Row.Add(Item.Thumbnail?.Url ?? "");
                }
            }
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                Table.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            return Table;
        }

        /// <summary>
        /// Determines if the converter can convert from the specified source type to the specified
        /// destination type.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Feed) && destination == typeof(Table);

        /// <summary>
        /// Converts the specified source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object, or null if the conversion is not possible.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not Feed File || destination is null)
                return null;
            return Convert(File);
        }
    }
}