using Archivist.DataTypes;
using Archivist.DataTypes.Feeds;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a <see cref="Feed"/> object to a <see cref="Card"/> object.
    /// </summary>
    public class FeedToCardConverter : IDataConverter
    {
        /// <summary>
        /// Converts a <see cref="Feed"/> object to a <see cref="Card"/> object.
        /// </summary>
        /// <param name="file">The <see cref="Feed"/> object to convert.</param>
        /// <returns>The converted <see cref="Card"/> object.</returns>
        public static Card? Convert(Feed? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Card();
            foreach (Channel? Channel in file)
            {
                if (Channel is null)
                    continue;
                foreach (FeedItem? Item in Channel)
                {
                    if (Item is null)
                        continue;
                    ReturnValue.Fields.Add(new KeyValueField("Title", Array.Empty<KeyValueParameter>(), Item.Title));
                    ReturnValue.Fields.Add(new KeyValueField("Description", Array.Empty<KeyValueParameter>(), Item.Description));
                    ReturnValue.Fields.Add(new KeyValueField("StartDateUtc", Array.Empty<KeyValueParameter>(), Item.PubDateUtc.ToUniversalTime().ToString()));
                    ReturnValue.Fields.Add(new KeyValueField("URLs", Array.Empty<KeyValueParameter>(), Item.Link));
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
        /// Determines whether this converter can convert from the specified source type to the
        /// specified destination type.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>
        /// <c>true</c> if this converter can convert from the specified source type to the
        /// specified destination type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Feed) && destination == typeof(Card);

        /// <summary>
        /// Converts the specified object to the specified destination type.
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