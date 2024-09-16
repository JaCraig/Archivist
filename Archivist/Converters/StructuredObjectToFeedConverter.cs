using Archivist.DataTypes;
using Archivist.DataTypes.Feeds;
using Archivist.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a StructuredObject to a Feed.
    /// </summary>
    public class StructuredObjectToFeedConverter : IDataConverter
    {
        /// <summary>
        /// Converts a StructuredObject to a Feed.
        /// </summary>
        /// <param name="file">The StructuredObject to convert.</param>
        /// <returns>The converted Feed.</returns>
        public static Feed? Convert(StructuredObject? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Feed()
            {
                new Channel()
            };
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value?.ToString() ?? "");
            }
            ReturnValue.Title = file.Title;
            if (!file.TryGetValue("Channels", out var Channels) || Channels is not IEnumerable ChannelList)
                return ReturnValue;
            foreach (var ChannelObject in ChannelList)
            {
                if (ChannelObject is not IDictionary<string, object?> Channel)
                    continue;
                var NewChannel = new Channel();
                ReturnValue.Add(NewChannel);
                if (Channel.TryGetValue("Title", out var Title))
                    NewChannel.Title = Title?.ToString() ?? "";
                if (Channel.TryGetValue("Description", out var Description))
                    NewChannel.Description = Description?.ToString() ?? "";
                if (Channel.TryGetValue("URLs", out var URLs))
                    NewChannel.Link = URLs?.ToString() ?? "";
                if (!Channel.TryGetValue("Items", out var Items) || Items is not IEnumerable ItemList)
                    continue;
                foreach (var ItemObject in ItemList)
                {
                    if (ItemObject is not IDictionary<string, object?> Item)
                        continue;
                    var NewItem = new FeedItem();
                    NewChannel.Add(NewItem);
                    if (Item.TryGetValue("Title", out var ItemTitle))
                        NewItem.Title = ItemTitle?.ToString() ?? "";
                    if (Item.TryGetValue("Description", out var ItemDescription))
                        NewItem.Description = ItemDescription?.ToString() ?? "";
                    if (Item.TryGetValue("PubDate", out var PubDate) && DateTime.TryParse(PubDate?.ToString(), out DateTime PubDateValue))
                        NewItem.PubDateUtc = PubDateValue.ToUniversalTime();
                    if (Item.TryGetValue("Link", out var Link))
                        NewItem.Link = Link?.ToString() ?? "";
                    if (Item.TryGetValue("Author", out var Author))
                        NewItem.Author = Author?.ToString() ?? "";
                    if (Item.TryGetValue("GUID", out var GUID))
                        NewItem.GUID = new FeedGuid(GUID?.ToString() ?? "");
                    if (Item.TryGetValue("Categories", out var Categories) && Categories is IEnumerable CategoryList)
                    {
                        foreach (var Category in CategoryList)
                        {
                            NewItem.Categories.Add(Category?.ToString() ?? "");
                        }
                    }
                    if (Item.TryGetValue("Enclosure", out var Enclosure))
                        NewItem.Enclosure = new Enclosure("", Enclosure?.ToString() ?? "", 0);
                    if (Item.TryGetValue("Thumbnail", out var Thumbnail))
                        NewItem.Thumbnail = new Thumbnail(Thumbnail?.ToString() ?? "");
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// Determines if the conversion is possible between the source and destination types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(StructuredObject) && destination == typeof(Feed);

        /// <summary>
        /// Converts an object to the specified destination type.
        /// </summary>
        /// <param name="source">The object to convert.</param>
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