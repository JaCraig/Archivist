using System;

namespace Archivist.DataTypes.Feeds
{
    /// <summary>
    /// Enclosure
    /// </summary>
    public class Enclosure : IComparable<Enclosure>, IEquatable<Enclosure>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enclosure"/> class.
        /// </summary>
        public Enclosure()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enclosure"/> class.
        /// </summary>
        /// <param name="type">The file type.</param>
        /// <param name="url">The location of the item.</param>
        /// <param name="length">The size in bytes.</param>
        public Enclosure(string? type, string? url, int length)
        {
            Type = type;
            Url = url;
            Length = length;
        }

        /// <summary>
        /// Gets or sets the size in bytes.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the file type.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the location of the item.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Determines whether two enclosures are not equal.
        /// </summary>
        /// <param name="left">The first enclosure to compare.</param>
        /// <param name="right">The second enclosure to compare.</param>
        /// <returns>True if the two enclosures are not equal; otherwise, false.</returns>
        public static bool operator !=(Enclosure? left, Enclosure? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the first enclosure is less than the second enclosure.
        /// </summary>
        /// <param name="left">The first enclosure to compare.</param>
        /// <param name="right">The second enclosure to compare.</param>
        /// <returns>
        /// True if the first enclosure is less than the second enclosure; otherwise, false.
        /// </returns>
        public static bool operator <(Enclosure? left, Enclosure? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the first enclosure is less than or equal to the second enclosure.
        /// </summary>
        /// <param name="left">The first enclosure to compare.</param>
        /// <param name="right">The second enclosure to compare.</param>
        /// <returns>
        /// True if the first enclosure is less than or equal to the second enclosure; otherwise, false.
        /// </returns>
        public static bool operator <=(Enclosure? left, Enclosure? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two enclosures are equal.
        /// </summary>
        /// <param name="left">The first enclosure to compare.</param>
        /// <param name="right">The second enclosure to compare.</param>
        /// <returns>True if the two enclosures are equal; otherwise, false.</returns>
        public static bool operator ==(Enclosure? left, Enclosure? right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the first enclosure is greater than the second enclosure.
        /// </summary>
        /// <param name="left">The first enclosure to compare.</param>
        /// <param name="right">The second enclosure to compare.</param>
        /// <returns>
        /// True if the first enclosure is greater than the second enclosure; otherwise, false.
        /// </returns>
        public static bool operator >(Enclosure? left, Enclosure? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the first enclosure is greater than or equal to the second enclosure.
        /// </summary>
        /// <param name="left">The first enclosure to compare.</param>
        /// <param name="right">The second enclosure to compare.</param>
        /// <returns>
        /// True if the first enclosure is greater than or equal to the second enclosure; otherwise, false.
        /// </returns>
        public static bool operator >=(Enclosure? left, Enclosure? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the enclosure to another enclosure based on their content.
        /// </summary>
        /// <param name="other">The other enclosure to compare.</param>
        /// <returns>An integer that indicates the relative order of the enclosures.</returns>
        public int CompareTo(Enclosure? other)
        {
            if (other is null)
                return 1;
            var ReturnValue = Length.CompareTo(other.Length);
            if (ReturnValue != 0)
                return ReturnValue;
            ReturnValue = string.CompareOrdinal(Type, other.Type);
            if (ReturnValue != 0)
                return ReturnValue;
            return string.CompareOrdinal(Url, other.Url);
        }

        /// <summary>
        /// Determines whether two enclosures are equal.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the two enclosures are equal; otherwise, false.</returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is not null && Equals(obj as Enclosure));

        /// <summary>
        /// Determines whether two enclosures are equal.
        /// </summary>
        /// <param name="other">The object to compare.</param>
        /// <returns>True if the two enclosures are equal; otherwise, false.</returns>
        public bool Equals(Enclosure? other)
        {
            if (other is null)
                return false;
            return Length == other.Length && Type == other.Type && Url == other.Url;
        }

        /// <summary>
        /// Gets the hash code for the enclosure.
        /// </summary>
        /// <returns>The hash code for the enclosure.</returns>
        public override int GetHashCode() => HashCode.Combine(Length, Type, Url);

        /// <summary>
        /// Returns a string that represents the current enclosure.
        /// </summary>
        /// <returns>A string that represents the current enclosure.</returns>
        public override string ToString() => $"Enclosure (Type={Type}; Length={Length}): {Url}\r\n";
    }
}