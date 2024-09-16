using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Feed object to a StructuredObject object.
    /// </summary>
    public class FeedToStructuredObjectConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Feed object to a StructuredObject object.
        /// </summary>
        /// <param name="file">The Feed object to convert.</param>
        /// <returns>The converted StructuredObject object.</returns>
        public static StructuredObject? Convert(Feed? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new StructuredObject();
            var Channels = new List<ExpandoObject>();
            ReturnValue["Channels"] = Channels;
            foreach (DataTypes.Feeds.Channel? Channel in file)
            {
                if (Channel is null)
                    continue;
                IDictionary<string, object?> ChannelObject = new ExpandoObject();
                Channels.Add((ExpandoObject)ChannelObject);
                ChannelObject["Title"] = Channel.Title ?? "";
                ChannelObject["Description"] = Channel.Description ?? "";
                ChannelObject["URLs"] = Channel.Link ?? "";
                var ChannelItems = new List<ExpandoObject>();
                ChannelObject["Items"] = ChannelItems;
                foreach (DataTypes.Feeds.FeedItem? Item in Channel)
                {
                    if (Item is null)
                        continue;
                    IDictionary<string, object?> ItemObject = new ExpandoObject();
                    ChannelItems.Add((ExpandoObject)ItemObject);
                    ItemObject["Title"] = Item.Title;
                    ItemObject["Description"] = Item.Description;
                    ItemObject["PubDate"] = Item.PubDateUtc.ToUniversalTime().ToString();
                    ItemObject["Link"] = Item.Link;
                    ItemObject["Author"] = Item.Author;
                    ItemObject["GUID"] = Item.GUID?.GuidText ?? "";
                    ItemObject["Categories"] = Item.Categories;
                    ItemObject["Enclosure"] = Item.Enclosure?.Url ?? "";
                    ItemObject["Thumbnail"] = Item.Thumbnail?.Url ?? "";
                }
            }
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata[Metadata.Key] = Metadata.Value;
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
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Feed) && destination == typeof(StructuredObject);

        /// <summary>
        /// Converts the source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
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