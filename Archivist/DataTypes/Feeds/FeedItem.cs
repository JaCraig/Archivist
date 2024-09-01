using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Archivist.DataTypes.Feeds
{
    /// <summary>
    /// Feed item
    /// </summary>
    /// <seealso cref="IFeedItem"/>
    public class FeedItem : IFeedItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FeedItem()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="doc">XML element containing the item content</param>
        public FeedItem(IXPathNavigable doc)
            : this()
        {
            if (doc is null)
                throw new ArgumentNullException(nameof(doc));
            var Element = doc.CreateNavigator();
            var NamespaceManager = new XmlNamespaceManager(Element.NameTable);
            NamespaceManager.AddNamespace("media", "http://search.yahoo.com/mrss/");
            Title = Element.SelectSingleNode("./title", NamespaceManager)?.Value ?? "";
            Link = Element.SelectSingleNode("./link", NamespaceManager)?.Value ?? "";
            Description = Element.SelectSingleNode("./description", NamespaceManager)?.Value ?? "";
            Author = Element.SelectSingleNode("./author", NamespaceManager)?.Value ?? "";
            foreach (XPathNavigator TempNode in Element.Select("./category", NamespaceManager))
            {
                Categories.Add(Utils.StripIllegalCharacters(TempNode.Value));
            }
            var Node = Element.SelectSingleNode("./enclosure", NamespaceManager);
            if (Node != null)
            {
                Enclosure = new Enclosure(Node);
            }
            Node = Element.SelectSingleNode("./pubDate", NamespaceManager);
            if (Node != null)
            {
                if (DateTime.TryParse(Node.Value.Replace("PDT", "-0700"), out var TempDate))
                {
                    PubDate = TempDate;
                }
                else
                {
                    PubDate = DateTime.Now;
                }
            }
            Node = Element.SelectSingleNode("./media:thumbnail", NamespaceManager);
            if (Node != null)
            {
                Thumbnail = new Thumbnail(Node);
            }
            Node = Element.SelectSingleNode("./guid", NamespaceManager);
            if (Node != null)
            {
                GUID = new FeedGuid(Node);
            }
        }

        /// <summary>
        /// Author
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Categories
        /// </summary>
        public IList<string> Categories { get; } = new List<string>();

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content => Description ?? "";

        /// <summary>
        /// Description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Enclosure
        /// </summary>
        public IEnclosure? Enclosure { get; set; }

        /// <summary>
        /// GUID for the item
        /// </summary>
        public virtual IFeedGuid? GUID { get; set; }

        /// <summary>
        /// Link
        /// </summary>
        public string? Link { get; set; }

        /// <summary>
        /// Publication date
        /// </summary>
        public DateTime PubDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Thumbnail
        /// </summary>
        public IThumbnail? Thumbnail { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Outputs a string ready for RSS
        /// </summary>
        /// <returns>A string formatted for RSS</returns>
        public override string ToString()
        {
            var ItemString = new StringBuilder();
            ItemString.Append("<item><title>").Append(Utils.StripIllegalCharacters(Title ?? "")).Append("</title>\r\n<link>")
            .Append(Link).Append("</link>\r\n<author>").Append(Utils.StripIllegalCharacters(Author ?? ""))
                .Append("</author>\r\n");
            foreach (var Category in Categories)
            {
                ItemString.Append("<category>").Append(Utils.StripIllegalCharacters(Category)).Append("</category>\r\n");
            }
            ItemString.Append("<pubDate>").Append(PubDate.ToString("r", CultureInfo.InvariantCulture)).Append("</pubDate>\r\n");
            if (Enclosure != null)
                ItemString.Append(Enclosure.ToString());
            if (Thumbnail != null)
                ItemString.Append(Thumbnail.ToString());
            ItemString.Append("<description><![CDATA[").Append(Description).Append("]]></description>\r\n");
            if (GUID != null)
                ItemString.Append(GUID.ToString());
            ItemString.Append("<itunes:subtitle>").Append(Utils.StripIllegalCharacters(Title)).Append("</itunes:subtitle>");
            ItemString.Append("<itunes:summary><![CDATA[").Append(Description).Append("]]></itunes:summary>");
            ItemString.Append("</item>\r\n");
            return ItemString.ToString();
        }
    }
}