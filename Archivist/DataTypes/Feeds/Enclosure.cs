using System;
using System.Xml.XPath;

namespace Archivist.DataTypes.Feeds
{
    /// <summary>
    /// Enclosure
    /// </summary>
    /// <seealso cref="IEnclosure"/>
    public class Enclosure : IEnclosure
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Enclosure()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="doc">XML element holding info for the enclosure</param>
        public Enclosure(IXPathNavigable doc)
        {
            if (doc is null)
                throw new ArgumentNullException(nameof(doc));
            XPathNavigator? Element = doc.CreateNavigator();
            Url = Element.GetAttribute("url", "") ?? string.Empty;
            if (int.TryParse(Element.GetAttribute("length", ""), out var TempLength))
                Length = TempLength;
            Type = Element.GetAttribute("type", "") ?? string.Empty;
        }

        /// <summary>
        /// Size in bytes
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// File type
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Location of the item
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// to string item. Used for outputting the item to RSS.
        /// </summary>
        /// <returns>A string formatted for RSS output</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Url) || string.IsNullOrEmpty(Type))
                return string.Empty;
            return "<enclosure url=\"" + Url + "\" length=\"" + Length + "\" type=\"" + Type + "\" />\r\n"
                + "<media:content url=\"" + Url + "\" fileSize=\"" + Length + "\" type=\"" + Type + "\" />";
        }
    }
}