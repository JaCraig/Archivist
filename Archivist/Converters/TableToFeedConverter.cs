using Archivist.DataTypes;
using Archivist.DataTypes.Feeds;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Table object to a Feed object.
    /// </summary>
    public class TableToFeedConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Table object to a Feed object.
        /// </summary>
        /// <param name="file">The Table object to convert.</param>
        /// <returns>The converted Feed object.</returns>
        public static Feed? Convert(Table? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Feed() { Title = file.Title };
            ReturnValue.Add(new Channel());
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            if (file.Count == 0 || file.Columns.Count == 0)
                return ReturnValue;
            var TitleColumn = file.Columns.Find(x => string.Equals(x, "title", StringComparison.OrdinalIgnoreCase));
            var DescriptionColumn = file.Columns.Find(x => string.Equals(x, "description", StringComparison.OrdinalIgnoreCase));
            var LinkColumn = file.Columns.Find(x => string.Equals(x, "link", StringComparison.OrdinalIgnoreCase));
            var PubDateColumn = file.Columns.Find(x => string.Equals(x, "pubdate", StringComparison.OrdinalIgnoreCase));
            var AuthorColumn = file.Columns.Find(x => string.Equals(x, "author", StringComparison.OrdinalIgnoreCase));
            var GUIDColumn = file.Columns.Find(x => string.Equals(x, "guid", StringComparison.OrdinalIgnoreCase));
            var CategoriesColumn = file.Columns.Find(x => string.Equals(x, "categories", StringComparison.OrdinalIgnoreCase));
            var EnclosureColumn = file.Columns.Find(x => string.Equals(x, "enclosure", StringComparison.OrdinalIgnoreCase));
            var ThumbnailColumn = file.Columns.Find(x => string.Equals(x, "thumbnail", StringComparison.OrdinalIgnoreCase));

            foreach (TableRow Row in file)
            {
                var Item = new FeedItem();
                ReturnValue[0].Add(Item);
                if (TitleColumn is not null)
                    Item.Title = Row[TitleColumn].Content;
                if (DescriptionColumn is not null)
                    Item.Description = Row[DescriptionColumn].Content;
                if (LinkColumn is not null)
                    Item.Link = Row[LinkColumn].Content;
                if (PubDateColumn is not null && DateTime.TryParse(Row[PubDateColumn].Content, out DateTime PubDate))
                    Item.PubDateUtc = PubDate.ToUniversalTime();
                if (AuthorColumn is not null)
                    Item.Author = Row[AuthorColumn].Content;
                if (GUIDColumn is not null)
                    Item.GUID = new FeedGuid(Row[GUIDColumn].Content);
                if (CategoriesColumn is not null)
                    Item.Categories.AddRange(Row[CategoriesColumn].Content?.Split(',').ToList() ?? (IEnumerable<string>)Array.Empty<string>());
                if (EnclosureColumn is not null)
                    Item.Enclosure = new Enclosure("", Row[EnclosureColumn].Content, 0);
                if (ThumbnailColumn is not null)
                    Item.Thumbnail = new Thumbnail(Row[ThumbnailColumn].Content);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Checks if the conversion is possible between the specified source and destination types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Table) && destination == typeof(Feed);

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