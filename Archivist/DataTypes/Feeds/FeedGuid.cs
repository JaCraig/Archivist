using System;
using System.Xml.XPath;

namespace Archivist.DataTypes.Feeds
{
    /// <summary>
    /// Feed GUID
    /// </summary>
    /// <seealso cref="IFeedGuid"/>
    public class FeedGuid : IFeedGuid
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FeedGuid()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">XML element holding info for the enclosure</param>
        public FeedGuid(IXPathNavigable element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));
            XPathNavigator? Navigator = element.CreateNavigator();
            if (!string.IsNullOrEmpty(Navigator.GetAttribute("isPermaLink", "")))
            {
                IsPermaLink = bool.Parse(Navigator.GetAttribute("isPermaLink", ""));
            }
            GuidText = Navigator.Value;
        }

        /// <summary>
        /// GUID Text
        /// </summary>
        public string? GuidText { get; set; }

        /// <summary>
        /// Is this a perma link?
        /// </summary>
        public bool IsPermaLink { get; set; }

        /// <summary>
        /// to string item. Used for outputting the item to RSS.
        /// </summary>
        /// <returns>A string formatted for RSS output</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(GuidText))
                return string.Empty;
            return "<guid" + (IsPermaLink ? " IsPermaLink='True'" : " IsPermaLink='False'") + ">" + GuidText + "</guid>\r\n";
        }
    }
}