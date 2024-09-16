using System;

namespace Archivist.DataTypes.Feeds
{
    /// <summary>
    /// Feed GUID for RSS feeds.
    /// </summary>
    /// <seealso cref="IComparable{FeedGuid}"/>
    /// <seealso cref="IEquatable{FeedGuid}"/>
    public class FeedGuid : IComparable<FeedGuid>, IEquatable<FeedGuid>
    {
        /// <summary>
        /// Creates a new instance of <see cref="FeedGuid"/>.
        /// </summary>
        public FeedGuid()
        { }

        /// <summary>
        /// Creates a new instance of <see cref="FeedGuid"/>.
        /// </summary>
        /// <param name="guidText">The GUID text.</param>
        /// <param name="isPermaLink">Is this a perma link?</param>
        public FeedGuid(string? guidText, bool isPermaLink = false)
        {
            GuidText = guidText;
            IsPermaLink = isPermaLink;
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
        /// Determines whether two guids are not equal.
        /// </summary>
        /// <param name="left">The first guid to compare.</param>
        /// <param name="right">The second guid to compare.</param>
        /// <returns>True if the two guids are not equal; otherwise, false.</returns>
        public static bool operator !=(FeedGuid? left, FeedGuid? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the left FeedGuid is less than the right FeedGuid.
        /// </summary>
        /// <param name="left">The first FeedGuid to compare.</param>
        /// <param name="right">The second FeedGuid to compare.</param>
        /// <returns>True if the left FeedGuid is less than the right FeedGuid; otherwise, false.</returns>
        public static bool operator <(FeedGuid? left, FeedGuid? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the left FeedGuid is less than or equal to the right FeedGuid.
        /// </summary>
        /// <param name="left">The first FeedGuid to compare.</param>
        /// <param name="right">The second FeedGuid to compare.</param>
        /// <returns>
        /// True if the left FeedGuid is less than or equal to the right FeedGuid; otherwise, false.
        /// </returns>
        public static bool operator <=(FeedGuid? left, FeedGuid? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two guids are equal.
        /// </summary>
        /// <param name="left">The first guid to compare.</param>
        /// <param name="right">The second guid to compare.</param>
        /// <returns>True if the two guids are equal; otherwise, false.</returns>
        public static bool operator ==(FeedGuid? left, FeedGuid? right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the left FeedGuid is greater than the right FeedGuid.
        /// </summary>
        /// <param name="left">The first FeedGuid to compare.</param>
        /// <param name="right">The second FeedGuid to compare.</param>
        /// <returns>
        /// True if the left FeedGuid is greater than the right FeedGuid; otherwise, false.
        /// </returns>
        public static bool operator >(FeedGuid? left, FeedGuid? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the left FeedGuid is greater than or equal to the right FeedGuid.
        /// </summary>
        /// <param name="left">The first FeedGuid to compare.</param>
        /// <param name="right">The second FeedGuid to compare.</param>
        /// <returns>
        /// True if the left FeedGuid is greater than or equal to the right FeedGuid; otherwise, false.
        /// </returns>
        public static bool operator >=(FeedGuid? left, FeedGuid? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the current instance with another FeedGuid and returns an integer that
        /// indicates whether the current instance precedes, follows, or occurs in the same position
        /// in the sort order as the other FeedGuid.
        /// </summary>
        /// <param name="other">The FeedGuid to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(FeedGuid? other)
        {
            if (other is null)
                return 1;
            if (GuidText == other.GuidText)
                return IsPermaLink.CompareTo(other.IsPermaLink);
            return string.CompareOrdinal(GuidText, other.GuidText);
        }

        /// <summary>
        /// Determines whether the specified FeedGuid is equal to the current FeedGuid.
        /// </summary>
        /// <param name="other">The FeedGuid to compare with the current FeedGuid.</param>
        /// <returns>
        /// True if the specified FeedGuid is equal to the current FeedGuid; otherwise, false.
        /// </returns>
        public bool Equals(FeedGuid? other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other is null)
                return false;
            return GuidText == other.GuidText && IsPermaLink == other.IsPermaLink;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current FeedGuid.
        /// </summary>
        /// <param name="obj">The object to compare with the current FeedGuid.</param>
        /// <returns>
        /// True if the specified object is equal to the current FeedGuid; otherwise, false.
        /// </returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is not null && Equals(obj as FeedGuid));

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current FeedGuid.</returns>
        public override int GetHashCode() => HashCode.Combine(GuidText, IsPermaLink);

        /// <summary>
        /// Returns a string that represents the current FeedGuid.
        /// </summary>
        /// <returns>A string formatted for RSS output.</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(GuidText))
                return "";
            return $"Guid (IsPermaLink={IsPermaLink}): {GuidText}\r\n";
        }
    }
}