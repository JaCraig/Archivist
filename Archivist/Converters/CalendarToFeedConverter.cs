using Archivist.DataTypes;
using Archivist.DataTypes.Feeds;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a <see cref="CalendarComponent"/> object to a <see cref="Feed"/> object.
    /// </summary>
    public class CalendarToFeedConverter : IDataConverter
    {
        /// <summary>
        /// Converts a <see cref="CalendarComponent"/> object to a <see cref="Feed"/> object.
        /// </summary>
        /// <param name="file">The <see cref="CalendarComponent"/> object to convert.</param>
        /// <returns>The converted <see cref="Feed"/> object.</returns>
        public static Feed? Convert(Calendar? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Feed
            {
                new Channel()
            };
            foreach (CalendarComponent Event in file.Events)
            {
                var Item = new FeedItem
                {
                    Title = Event.Summary,
                    Description = Event.Description,
                    PubDate = Event.StartDateUtc,
                    Link = Event.URLs.FirstOrDefault()?.Value ?? "",
                };
                ReturnValue[0].Add(Item);
            }
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata[Metadata.Key] = Metadata.Value;
            }
            ReturnValue.Title = file.Title ?? file.Events.FirstOrDefault()?.Summary;
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
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Calendar) && destination == typeof(Feed);

        /// <summary>
        /// Converts the specified object to the specified destination type.
        /// </summary>
        /// <param name="source">The object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not Calendar File || destination is null)
                return null;
            return Convert(File);
        }
    }
}