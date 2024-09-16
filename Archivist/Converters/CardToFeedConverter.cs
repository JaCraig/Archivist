using Archivist.DataTypes;
using Archivist.DataTypes.Feeds;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Card object to a Feed object.
    /// </summary>
    public class CardToFeedConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Card object to a Feed object.
        /// </summary>
        /// <param name="file">The Card object to convert.</param>
        /// <returns>The converted Feed object.</returns>
        public static Feed? Convert(Card? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Feed()
            {
                new Channel()
            };
            var PhoneNumbers = string.Join("; ", file.PhoneNumbers.Select(x => x?.Value ?? ""));
            var Emails = string.Join("; ", file.Emails.Select(x => x?.Value ?? ""));
            var Websites = string.Join("; ", file.Websites.Select(x => x?.Value ?? ""));

            ReturnValue[0].Add(new FeedItem()
            {
                Title = file.FirstName + " " + file.LastName,
                Description = "Phone Numbers: " + PhoneNumbers + "\r\nEmails: " + Emails + "\r\nWebsites: " + Websites,
                PubDateUtc = DateTime.UtcNow,
                Link = file.Websites.FirstOrDefault()?.Value ?? "",
            });
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata[Metadata.Key] = Metadata.Value;
            }
            ReturnValue.Title = file.Title ?? file.FullName?.Value;
            return ReturnValue;
        }

        /// <summary>
        /// Determines if the conversion is possible between the specified types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Card) && destination == typeof(Feed);

        /// <summary>
        /// Converts an object from the source type to the destination type.
        /// </summary>
        /// <param name="source">The object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not Card File || destination is null)
                return null;
            return Convert(File);
        }
    }
}