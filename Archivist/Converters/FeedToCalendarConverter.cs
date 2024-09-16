using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Feed object to a Calendar object.
    /// </summary>
    public class FeedToCalendarConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Feed object to a Calendar object.
        /// </summary>
        /// <param name="file">The Feed object to convert.</param>
        /// <returns>The converted Calendar object.</returns>
        public static Calendar? Convert(Feed? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Calendar();
            foreach (DataTypes.Feeds.Channel? Channel in file.Channels)
            {
                if (Channel is null)
                    continue;
                foreach (DataTypes.Feeds.FeedItem? Item in Channel.Items)
                {
                    if (Item is null)
                        continue;
                    var Component = new CalendarComponent(ReturnValue);
                    ReturnValue.Events.Add(Component);
                    Component.Fields.Add(new KeyValueField("Title", Array.Empty<KeyValueParameter>(), Item.Title));
                    Component.Fields.Add(new KeyValueField("Description", Array.Empty<KeyValueParameter>(), Item.Description));
                    Component.Fields.Add(new KeyValueField("StartDateUtc", Array.Empty<KeyValueParameter>(), Item.PubDateUtc.ToUniversalTime().ToString()));
                    Component.Fields.Add(new KeyValueField("URLs", Array.Empty<KeyValueParameter>(), Item.Link));
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
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Feed) && destination == typeof(Calendar);

        /// <summary>
        /// Converts an object from the source type to the destination type.
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