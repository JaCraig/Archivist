using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.DataTypes.Feeds;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Archivist.Formats.RSS
{
    /// <summary>
    /// Represents a RSS reader for reading RSS files.
    /// </summary>
    public class RSSReader : ReaderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RSSReader"/> class.
        /// </summary>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public RSSReader(Convertinator? converter)
        {
            _Converter = converter;
        }

        /// <summary>
        /// Gets the header information of the RSS file.
        /// </summary>
        public override byte[] HeaderInfo { get; } = new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x20 };

        /// <summary>
        /// The converter used to convert between IGenericFile objects.
        /// </summary>
        private readonly Convertinator? _Converter;

        /// <summary>
        /// Determines if the reader can read the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns><c>true</c> if the reader can read the stream; otherwise, <c>false</c>.</returns>
        public override bool InternalCanRead(Stream? stream)
        {
            try
            {
                var Document = new XmlDocument();
                Document.LoadXml(stream.ReadAll());
                Feed TempFeed = Load(Document.CreateNavigator(), _Converter);
                if (TempFeed.Channels.Count > 0 && TempFeed.Channels[0].Count > 0)
                    return true;
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Reads the RSS file asynchronously.
        /// </summary>
        /// <param name="stream">The stream to read the RSS file from.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the generic
        /// file representation of the RSS file.
        /// </returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            if (stream is null)
                return new Feed(_Converter);
            var Data = await GetDataAsync(stream).ConfigureAwait(false);
            if (string.IsNullOrEmpty(Data))
                return new Feed(_Converter);
            var Document = new XmlDocument();
            Document.LoadXml(Data);
            return Load(Document.CreateNavigator(), _Converter);
        }

        /// <summary>
        /// Reads the RSS file asynchronously.
        /// </summary>
        /// <param name="stream">The stream to read the RSS file from.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the generic
        /// file representation of the RSS file.
        /// </returns>
        private static async Task<string> GetDataAsync(Stream? stream)
        {
            if (stream?.CanRead != true)
                return "";
            try
            {
                return await stream.ReadAllAsync().ConfigureAwait(false) ?? "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Loads the specified document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="convertinator">The convertinator.</param>
        /// <returns>The feed object.</returns>
        private static Feed Load(XPathNavigator? document, Convertinator? convertinator)
        {
            if (document is null)
                return new Feed(convertinator);
            var ReturnValue = new Feed(convertinator);
            XPathNavigator? Navigator = document.CreateNavigator();
            if (Navigator is null)
                return ReturnValue;
            var NamespaceManager = new XmlNamespaceManager(Navigator.NameTable);
            XPathNodeIterator Nodes = Navigator.Select("./channel", NamespaceManager);
            foreach (XPathNavigator Element in Nodes)
            {
                ReturnValue.Channels.Add(LoadChannel(Element));
            }
            if (ReturnValue.Channels.Count > 0)
                return ReturnValue;
            Nodes = Navigator.Select(".//channel", NamespaceManager);
            foreach (XPathNavigator Element in Nodes)
            {
                ReturnValue.Channels.Add(LoadChannel(Element));
            }
            if (ReturnValue.Channels.Count == 0 || ReturnValue.Channels.FirstOrDefault()?.Count > 0)
                return ReturnValue;
            var Items = new List<FeedItem>();
            Nodes = Navigator.Select(".//item", NamespaceManager);
            foreach (XPathNavigator Element in Nodes)
            {
                Items.Add(ReadFeedItem(Element));
            }
            ReturnValue.Channels.FirstOrDefault()?.AddRange(Items);
            return ReturnValue;
        }

        /// <summary>
        /// Loads the channel from the specified document.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns>The channel object.</returns>
        private static Channel LoadChannel(XPathNavigator? doc)
        {
            if (doc is null)
                return new Channel();
            var ReturnValue = new Channel();
            XPathNavigator? Element = doc.CreateNavigator();
            if (Element?.Name.Equals("channel", StringComparison.OrdinalIgnoreCase) != true)
                return ReturnValue;
            var NamespaceManager = new XmlNamespaceManager(Element.NameTable);
            ReturnValue.Title = Element.SelectSingleNode("./title", NamespaceManager)?.Value ?? "";
            ReturnValue.Link = Element.SelectSingleNode("./link", NamespaceManager)?.Value ?? "";
            ReturnValue.Description = Element.SelectSingleNode("./description", NamespaceManager)?.Value ?? "";
            ReturnValue.Copyright = Element.SelectSingleNode("./copyright", NamespaceManager)?.Value ?? "";
            ReturnValue.Language = Element.SelectSingleNode("./language", NamespaceManager)?.Value ?? "";
            ReturnValue.WebMaster = Element.SelectSingleNode("./webmaster", NamespaceManager)?.Value ?? "";
            ReturnValue.PubDateUtc = ReadDateTime(Element.SelectSingleNode("./pubDate", NamespaceManager));
            XPathNodeIterator Nodes = Element.Select("./category", NamespaceManager);
            foreach (XPathNavigator TempNode in Nodes)
            {
                ReturnValue.Categories.Add(TempNode.Value.StripIllegalCharacters());
            }
            ReturnValue.Docs = Element.SelectSingleNode("./docs", NamespaceManager)?.Value ?? "";
            if (int.TryParse(Element.SelectSingleNode("./ttl", NamespaceManager)?.Value, out var TempTTL))
                ReturnValue.TTL = TempTTL;
            ReturnValue.ImageUrl = Element.SelectSingleNode("./image/url", NamespaceManager)?.Value ?? "";
            Nodes = Element.Select("./item", NamespaceManager);
            foreach (XPathNavigator TempNode in Nodes)
            {
                ReturnValue.Items.Add(ReadFeedItem(TempNode));
            }
            return ReturnValue;
        }

        /// <summary>
        /// Reads the date time from the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The date time object.</returns>
        private static DateTime ReadDateTime(XPathNavigator? node)
        {
            if (node is null)
                return DateTime.UtcNow;
            return DateTime.TryParse(node.Value.Replace("PDT", "-0700"), out DateTime TempDate) ? TempDate.ToUniversalTime() : DateTime.UtcNow;
        }

        /// <summary>
        /// Reads the enclosure from the specified node.
        /// </summary>
        /// <param name="doc">The node.</param>
        /// <returns>The enclosure object.</returns>
        private static Enclosure? ReadEnclosure(XPathNavigator? doc)
        {
            if (doc is null)
                return null;
            var ReturnValue = new Enclosure();
            XPathNavigator? Element = doc.CreateNavigator();
            ReturnValue.Url = Element.GetAttribute("url", "") ?? "";
            if (int.TryParse(Element.GetAttribute("length", ""), out var TempLength))
                ReturnValue.Length = TempLength;
            ReturnValue.Type = Element.GetAttribute("type", "") ?? "";
            return ReturnValue;
        }

        /// <summary>
        /// Reads the feed GUID from the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The feed GUID object.</returns>
        private static FeedGuid? ReadFeedGuid(XPathNavigator? node)
        {
            if (node is null)
                return null;
            var ReturnValue = new FeedGuid();
            XPathNavigator? Navigator = node.CreateNavigator();
            if (!string.IsNullOrEmpty(Navigator.GetAttribute("isPermaLink", "")))
            {
                ReturnValue.IsPermaLink = bool.Parse(Navigator.GetAttribute("isPermaLink", ""));
            }
            ReturnValue.GuidText = Navigator.Value;
            return ReturnValue;
        }

        /// <summary>
        /// Reads the feed item from the specified document.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns>The feed item object.</returns>
        private static FeedItem ReadFeedItem(XPathNavigator doc)
        {
            if (doc is null)
                return new FeedItem();
            var ReturnValue = new FeedItem();
            XPathNavigator Element = doc.CreateNavigator();
            var NamespaceManager = new XmlNamespaceManager(Element.NameTable);
            NamespaceManager.AddNamespace("media", "http://search.yahoo.com/mrss/");
            ReturnValue.Title = Element.SelectSingleNode("./title", NamespaceManager)?.Value ?? "";
            ReturnValue.Link = Element.SelectSingleNode("./link", NamespaceManager)?.Value ?? "";
            ReturnValue.Description = Element.SelectSingleNode("./description", NamespaceManager)?.Value ?? "";
            ReturnValue.Author = Element.SelectSingleNode("./author", NamespaceManager)?.Value ?? "";
            foreach (XPathNavigator TempNode in Element.Select("./category", NamespaceManager))
            {
                ReturnValue.Categories.Add(TempNode.Value.StripIllegalCharacters());
            }
            ReturnValue.Enclosure = ReadEnclosure(Element.SelectSingleNode("./enclosure", NamespaceManager));
            ReturnValue.PubDateUtc = ReadDateTime(Element.SelectSingleNode("./pubDate", NamespaceManager));
            ReturnValue.Thumbnail = ReadThumbnail(Element.SelectSingleNode("./media:thumbnail", NamespaceManager));
            ReturnValue.GUID = ReadFeedGuid(Element.SelectSingleNode("./guid", NamespaceManager));
            return ReturnValue;
        }

        /// <summary>
        /// Reads the thumbnail from the specified document.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns>The thumbnail object.</returns>
        private static Thumbnail? ReadThumbnail(XPathNavigator? doc)
        {
            if (doc is null)
                return null;
            var ReturnValue = new Thumbnail();
            XPathNavigator? Element = doc.CreateNavigator();
            ReturnValue.Url = Element.GetAttribute("url", "");
            if (!string.IsNullOrEmpty(Element.GetAttribute("width", "")))
            {
                ReturnValue.Width = int.Parse(Element.GetAttribute("width", ""), CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(Element.GetAttribute("height", "")))
            {
                ReturnValue.Height = int.Parse(Element.GetAttribute("height", ""), CultureInfo.InvariantCulture);
            }
            return ReturnValue;
        }
    }
}