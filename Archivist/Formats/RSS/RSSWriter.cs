using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.DataTypes.Feeds;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.Formats.RSS
{
    /// <summary>
    /// Represents a RSS writer for the Txt format.
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="RSSWriter"/> class.</remarks>
    /// <param name="logger">The logger to use for logging.</param>
    public class RSSWriter(ILogger? logger) : WriterBaseClass(logger)
    {
        /// <summary>
        /// Writes the content of the specified file to the provided stream asynchronously.
        /// </summary>
        /// <param name="file">The file to be written.</param>
        /// <param name="stream">The stream to write the file content to.</param>
        /// <returns>True if the file was written successfully; otherwise, false.</returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            Feed? FeedFile = file?.ToFileType<Feed>();
            if (!IsValidStream(stream) || FeedFile is null)
            {
                Logger?.LogDebug("{writerName}.WriteAsync(): Stream is null or invalid.", nameof(RSSWriter));
                return false;
            }
            var TempData = Encoding.UTF8.GetBytes(WriteFeed(FeedFile).ToString());
            try
            {
                await stream.WriteAsync(TempData).ConfigureAwait(false);
            }
            catch (Exception Ex)
            {
                Logger?.LogError(Ex, "{writerName}.WriteAsync(): Error writing to stream.", nameof(RSSWriter));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Writes the channel to the specified string builder.
        /// </summary>
        /// <param name="channel">The channel to write.</param>
        /// <param name="builder">The string builder to write to.</param>
        /// <returns>The string builder with the channel written to it.</returns>
        private static StringBuilder WriteChannel(Channel channel, StringBuilder builder)
        {
            if (channel is null)
                return builder;
            _ = builder.Append("<channel>");
            _ = builder.Append("<title>").Append(channel.Title.StripIllegalCharacters()).Append("</title>\r\n");
            _ = builder.Append("<link>").Append(channel.Link).Append("</link>\r\n");
            _ = builder.Append("<atom:link xmlns:atom=\"http://www.w3.org/2005/Atom\" rel=\"self\" href=\"").Append(channel.Link).Append("\" type=\"application/rss+xml\" />");

            _ = builder.Append("<description><![CDATA[").Append(channel.Description ?? "").Append("]]></description>\r\n");
            _ = builder.Append("<language>").Append(channel.Language).Append("</language>\r\n");
            _ = builder.Append("<copyright>").Append(channel.Copyright.StripIllegalCharacters()).Append("</copyright>\r\n");
            _ = builder.Append("<webMaster>").Append(channel.WebMaster.StripIllegalCharacters()).Append("</webMaster>\r\n");
            _ = builder.Append("<pubDate>").Append(channel.PubDateUtc.ToString("r", CultureInfo.InvariantCulture)).Append("</pubDate>\r\n");
            _ = builder.Append("<itunes:explicit>").Append(channel.Explicit ? "true" : "false").Append("</itunes:explicit>");
            _ = builder.Append("<itunes:subtitle>").Append(channel.Title.StripIllegalCharacters()).Append("</itunes:subtitle>");
            _ = builder.Append("<itunes:summary><![CDATA[").Append(channel.Description.StripIllegalCharacters()).Append("]]></itunes:summary>");

            foreach (var Category in channel.Categories)
            {
                _ = builder.Append("<category>").Append(Category.StripIllegalCharacters()).Append("</category>\r\n");
                _ = builder.Append("<itunes:category text=\"").Append(Category).Append("\" />\r\n");
            }
            _ = builder.Append("<docs>").Append(channel.Docs).Append("</docs>\r\n");
            _ = builder.Append("<ttl>").Append(channel.TTL.ToString(CultureInfo.InvariantCulture)).Append("</ttl>\r\n");
            if (!string.IsNullOrEmpty(channel.ImageUrl))
            {
                _ = builder.Append("<image><url>").Append(channel.ImageUrl).Append("</url>\r\n<title>").Append(channel.Title.StripIllegalCharacters()).Append("</title>\r\n<link>").Append(channel.Link).Append("</link>\r\n</image>\r\n");
            }
            foreach (FeedItem CurrentItem in channel.Items)
            {
                WriteFeedItem(CurrentItem, builder);
            }
            return builder.Append("</channel>\r\n");
        }

        /// <summary>
        /// Writes the enclosure to the specified string builder.
        /// </summary>
        /// <param name="enclosure">The enclosure to write.</param>
        /// <param name="builder">The string builder to write to.</param>
        private static void WriteEnclosure(Enclosure? enclosure, StringBuilder builder)
        {
            if (enclosure is null || string.IsNullOrEmpty(enclosure.Url) || string.IsNullOrEmpty(enclosure.Type))
                return;
            _ = builder.Append("<enclosure url=\"" + enclosure.Url + "\" length=\"" + enclosure.Length + "\" type=\"" + enclosure.Type + "\" />\r\n"
                + "<media:content url=\"" + enclosure.Url + "\" fileSize=\"" + enclosure.Length + "\" type=\"" + enclosure.Type + "\" />\r\n");
        }

        /// <summary>
        /// Writes the feed to a string.
        /// </summary>
        /// <param name="feed">The feed to write.</param>
        /// <returns>The feed as a string.</returns>
        private static StringBuilder WriteFeed(Feed feed)
        {
            var ReturnValue = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<rss xmlns:itunes=\"http://www.itunes.com/dtds/podcast-1.0.dtd\" xmlns:media=\"http://search.yahoo.com/mrss/\" version=\"2.0\">\r\n");
            foreach (Channel Channel in feed.Channels)
            {
                ReturnValue = WriteChannel(Channel, ReturnValue);
                _ = ReturnValue.Append("\r\n");
            }
            return ReturnValue.Append("</rss>");
        }

        /// <summary>
        /// Writes the feed item to the specified string builder.
        /// </summary>
        /// <param name="currentItem">The feed item to write.</param>
        /// <param name="builder">The string builder to write to.</param>
        private static void WriteFeedItem(FeedItem currentItem, StringBuilder builder)
        {
            if (currentItem is null)
                return;

            _ = builder.Append("<item><title>").Append(currentItem.Title.StripIllegalCharacters()).Append("</title>\r\n<link>")
                .Append(currentItem.Link).Append("</link>\r\n<author>").Append(currentItem.Author.StripIllegalCharacters())
                .Append("</author>\r\n");

            foreach (var Category in currentItem.Categories)
            {
                _ = builder.Append("<category>").Append(Category.StripIllegalCharacters()).Append("</category>\r\n");
            }

            _ = builder.Append("<pubDate>").Append(currentItem.PubDateUtc.ToString("r", CultureInfo.InvariantCulture)).Append("</pubDate>\r\n");
            WriteEnclosure(currentItem.Enclosure, builder);
            WriteThumbnail(currentItem.Thumbnail, builder);
            _ = builder.Append("<description><![CDATA[").Append(currentItem.Description).Append("]]></description>\r\n");
            WriteGuid(currentItem.GUID, builder);
            _ = builder.Append("<itunes:subtitle>").Append(currentItem.Title.StripIllegalCharacters()).Append("</itunes:subtitle>");
            _ = builder.Append("<itunes:summary><![CDATA[").Append(currentItem.Description).Append("]]></itunes:summary>");
            _ = builder.Append("</item>\r\n");
        }

        /// <summary>
        /// Writes the GUID to the specified string builder.
        /// </summary>
        /// <param name="guid">The GUID to write.</param>
        /// <param name="builder">The string builder to write to.</param>
        private static void WriteGuid(FeedGuid? guid, StringBuilder builder)
        {
            if (guid is null || string.IsNullOrEmpty(guid.GuidText))
                return;
            _ = builder.Append("<guid isPermaLink=\"").Append(guid.IsPermaLink ? "true" : "false").Append("\">").Append(guid.GuidText).Append("</guid>\r\n");
        }

        /// <summary>
        /// Writes the thumbnail to the specified string builder.
        /// </summary>
        /// <param name="thumbnail">The thumbnail to write.</param>
        /// <param name="builder">The string builder to write to.</param>
        private static void WriteThumbnail(Thumbnail? thumbnail, StringBuilder builder)
        {
            if (thumbnail is null || string.IsNullOrEmpty(thumbnail.Url))
                return;
            _ = builder.Append($"<media:thumbnail url=\"{thumbnail.Url}\" width=\"{thumbnail.Width.ToString(CultureInfo.InvariantCulture)}\" height=\"{thumbnail.Height.ToString(CultureInfo.InvariantCulture)}\" />\r\n");
        }
    }
}