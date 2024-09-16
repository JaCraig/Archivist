using System;

namespace Archivist.DataTypes.Feeds
{
    /// <summary>
    /// Thumbnail
    /// </summary>
    public class Thumbnail : IComparable<Thumbnail>, IEquatable<Thumbnail>
    {
        /// <summary>
        /// Initializes a new instance of the Thumbnail class.
        /// </summary>
        public Thumbnail()
        { }

        /// <summary>
        /// Initializes a new instance of the Thumbnail class.
        /// </summary>
        /// <param name="url">The url</param>
        /// <param name="height">The height</param>
        /// <param name="width">The width</param>
        public Thumbnail(string? url, int height = 0, int width = 0)
        {
            Url = url;
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Image height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Location of the item
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Image width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Determines whether two Thumbnail objects are not equal.
        /// </summary>
        /// <param name="left">The first Thumbnail object to compare.</param>
        /// <param name="right">The second Thumbnail object to compare.</param>
        /// <returns>true if the two Thumbnail objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Thumbnail? left, Thumbnail? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the first Thumbnail object is less than the second Thumbnail object.
        /// </summary>
        /// <param name="left">The first Thumbnail object to compare.</param>
        /// <param name="right">The second Thumbnail object to compare.</param>
        /// <returns>
        /// true if the first Thumbnail object is less than the second Thumbnail object; otherwise, false.
        /// </returns>
        public static bool operator <(Thumbnail? left, Thumbnail? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the first Thumbnail object is less than or equal to the second
        /// Thumbnail object.
        /// </summary>
        /// <param name="left">The first Thumbnail object to compare.</param>
        /// <param name="right">The second Thumbnail object to compare.</param>
        /// <returns>
        /// true if the first Thumbnail object is less than or equal to the second Thumbnail object;
        /// otherwise, false.
        /// </returns>
        public static bool operator <=(Thumbnail? left, Thumbnail? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two Thumbnail objects are equal.
        /// </summary>
        /// <param name="left">The first Thumbnail object to compare.</param>
        /// <param name="right">The second Thumbnail object to compare.</param>
        /// <returns>true if the two Thumbnail objects are equal; otherwise, false.</returns>
        public static bool operator ==(Thumbnail? left, Thumbnail? right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the first Thumbnail object is greater than the second Thumbnail object.
        /// </summary>
        /// <param name="left">The first Thumbnail object to compare.</param>
        /// <param name="right">The second Thumbnail object to compare.</param>
        /// <returns>
        /// true if the first Thumbnail object is greater than the second Thumbnail object;
        /// otherwise, false.
        /// </returns>
        public static bool operator >(Thumbnail? left, Thumbnail? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the first Thumbnail object is greater than or equal to the second
        /// Thumbnail object.
        /// </summary>
        /// <param name="left">The first Thumbnail object to compare.</param>
        /// <param name="right">The second Thumbnail object to compare.</param>
        /// <returns>
        /// true if the first Thumbnail object is greater than or equal to the second Thumbnail
        /// object; otherwise, false.
        /// </returns>
        public static bool operator >=(Thumbnail? left, Thumbnail? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the current Thumbnail object with another Thumbnail object.
        /// </summary>
        /// <param name="other">The Thumbnail object to compare with the current object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(Thumbnail? other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (other is null)
                return 1;
            if (Height < other.Height)
                return -1;
            if (Height > other.Height)
                return 1;
            if (Width < other.Width)
                return -1;
            if (Width > other.Width)
                return 1;
            return string.CompareOrdinal(Url, other.Url);
        }

        /// <summary>
        /// Determines whether the current Thumbnail object is equal to another Thumbnail object.
        /// </summary>
        /// <param name="other">The Thumbnail object to compare with the current object.</param>
        /// <returns>
        /// true if the current Thumbnail object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Thumbnail? other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other is null)
                return false;
            return Height == other.Height && Width == other.Width && Url == other.Url;
        }

        /// <summary>
        /// Determines whether the current Thumbnail object is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the current Thumbnail object is equal to the other parameter; otherwise, false.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null)
                return false;

            return obj is Thumbnail Other && Equals(Other);
        }

        /// <summary>
        /// Returns the hash code for the current Thumbnail object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() => HashCode.Combine(Height, Width, Url);

        /// <summary>
        /// Returns a string that represents the current Thumbnail object.
        /// </summary>
        /// <returns>A string that represents the current Thumbnail object.</returns>
        public override string ToString() => $"Image (Height={Height}; Width={Width}): {Url}\r\n";
    }
}