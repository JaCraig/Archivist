using Archivist.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace Archivist.DataTypes.Feeds
{
    /// <summary>
    /// Feed item
    /// </summary>
    public class FeedItem : IComparable<FeedItem>, IEquatable<FeedItem>
    {
        /// <summary>
        /// Author
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Categories
        /// </summary>
        public List<string> Categories { get; } = new List<string>();

        /// <summary>
        /// Description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Enclosure
        /// </summary>
        public Enclosure? Enclosure { get; set; }

        /// <summary>
        /// GUID for the item
        /// </summary>
        public FeedGuid? GUID { get; set; }

        /// <summary>
        /// Link
        /// </summary>
        public string? Link { get; set; }

        /// <summary>
        /// Gets the local publication date.
        /// </summary>
        public DateTime PubDate => PubDateUtc + TimeZoneInfo.Local.GetUtcOffset(PubDateUtc);

        /// <summary>
        /// Publication date
        /// </summary>
        public DateTime PubDateUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Thumbnail
        /// </summary>
        public Thumbnail? Thumbnail { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Determines whether two instances of <see cref="FeedItem"/> are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="FeedItem"/> to compare.</param>
        /// <param name="right">The second <see cref="FeedItem"/> to compare.</param>
        /// <returns><c>true</c> if the two instances are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(FeedItem? left, FeedItem? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="FeedItem"/> is less than another specified
        /// <see cref="FeedItem"/>.
        /// </summary>
        /// <param name="left">The first <see cref="FeedItem"/> to compare.</param>
        /// <param name="right">The second <see cref="FeedItem"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="FeedItem"/> is less than the second <see
        /// cref="FeedItem"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(FeedItem? left, FeedItem? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the specified <see cref="FeedItem"/> is less than or equal to another
        /// specified <see cref="FeedItem"/>.
        /// </summary>
        /// <param name="left">The first <see cref="FeedItem"/> to compare.</param>
        /// <param name="right">The second <see cref="FeedItem"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="FeedItem"/> is less than or equal to the second <see
        /// cref="FeedItem"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(FeedItem? left, FeedItem? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two instances of <see cref="FeedItem"/> are equal.
        /// </summary>
        /// <param name="left">The first <see cref="FeedItem"/> to compare.</param>
        /// <param name="right">The second <see cref="FeedItem"/> to compare.</param>
        /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(FeedItem? left, FeedItem? right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="FeedItem"/> is greater than another
        /// specified <see cref="FeedItem"/>.
        /// </summary>
        /// <param name="left">The first <see cref="FeedItem"/> to compare.</param>
        /// <param name="right">The second <see cref="FeedItem"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="FeedItem"/> is greater than the second <see
        /// cref="FeedItem"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(FeedItem? left, FeedItem? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the specified <see cref="FeedItem"/> is greater than or equal to
        /// another specified <see cref="FeedItem"/>.
        /// </summary>
        /// <param name="left">The first <see cref="FeedItem"/> to compare.</param>
        /// <param name="right">The second <see cref="FeedItem"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="FeedItem"/> is greater than or equal to the second
        /// <see cref="FeedItem"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(FeedItem? left, FeedItem? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the current instance with another <see cref="FeedItem"/> and returns an integer
        /// that indicates whether the current instance precedes, follows, or occurs in the same
        /// position in the sort order as the other <see cref="FeedItem"/>.
        /// </summary>
        /// <param name="other">The <see cref="FeedItem"/> to compare with the current instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return
        /// value has the following meanings: Value Meaning Less than zero This instance precedes
        /// <paramref name="other"/> in the sort order. Zero This instance occurs in the same
        /// position in the sort order as <paramref name="other"/>. Greater than zero This instance
        /// follows <paramref name="other"/> in the sort order.
        /// </returns>
        public int CompareTo(FeedItem? other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (other is null)
                return 1;
            if (PubDateUtc < other.PubDateUtc)
                return -1;
            if (PubDateUtc > other.PubDateUtc)
                return 1;
            return string.CompareOrdinal(Title, other.Title);
        }

        /// <summary>
        /// Determines whether the specified <see cref="FeedItem"/> is equal to the current <see cref="FeedItem"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current <see cref="FeedItem"/>.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current <see cref="FeedItem"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null)
                return false;

            return obj is FeedItem Other && Equals(Other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="FeedItem"/> is equal to the current <see cref="FeedItem"/>.
        /// </summary>
        /// <param name="other">The object to compare with the current <see cref="FeedItem"/>.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current <see cref="FeedItem"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(FeedItem? other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other is null)
                return false;
            if (PubDateUtc != other.PubDateUtc)
                return false;
            if (Title != other.Title)
                return false;
            if (Link != other.Link)
                return false;
            if (Author != other.Author)
                return false;
            if (Description != other.Description)
                return false;
            if (Enclosure != other.Enclosure)
                return false;
            if (Thumbnail != other.Thumbnail)
                return false;
            if (GUID != other.GUID)
                return false;
            foreach (var Category in Categories)
            {
                if (!other.Categories.Contains(Category))
                    return false;
            }
            foreach (var Category in other.Categories)
            {
                if (!Categories.Contains(Category))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Calculates the hash code for the current <see cref="FeedItem"/> instance.
        /// </summary>
        /// <returns>The calculated hash code.</returns>
        public override int GetHashCode()
        {
            var Hash1 = HashCode.Combine(Author, Description, Enclosure, GUID, Link);
            var Hash2 = HashCode.Combine(PubDateUtc, Thumbnail, Title);
            var ReturnValue = HashCode.Combine(Hash1, Hash2);
            foreach (var Category in Categories)
            {
                ReturnValue = HashCode.Combine(ReturnValue, Category);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Outputs a string ready for RSS
        /// </summary>
        /// <returns>A string formatted for RSS</returns>
        public override string ToString() => $@"FeedItem:Begin\r\nTitle: {Title.StripIllegalCharacters()}\r\nLink: {Link}\r\nAuthor: {Author.StripIllegalCharacters()}\r\nCategories: {string.Join(", ", Categories)}\r\nPubDate: {PubDateUtc:R}\r\n{Enclosure}\r\n{Thumbnail}\r\nDescription: {Description}\r\n{GUID}\r\nFeedItem:End";
    }
}