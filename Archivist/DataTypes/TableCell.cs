using Archivist.Interfaces;
using ObjectCartographer;
using System;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a cell in a table.
    /// </summary>
    public class TableCell : IComparable<TableCell>, IEquatable<TableCell>, IObjectConvertable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableCell"/> class with the specified content.
        /// </summary>
        /// <param name="content">The content of the cell.</param>
        public TableCell(string? content)
        {
            Content = content;
        }

        /// <summary>
        /// Gets or sets the content of the cell.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Determines whether two <see cref="TableCell"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="TableCell"/> to compare.</param>
        /// <param name="right">The second <see cref="TableCell"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="TableCell"/> objects are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(TableCell? left, TableCell? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the first <see cref="TableCell"/> is less than the second <see cref="TableCell"/>.
        /// </summary>
        /// <param name="left">The first <see cref="TableCell"/> to compare.</param>
        /// <param name="right">The second <see cref="TableCell"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="TableCell"/> is less than the second <see
        /// cref="TableCell"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(TableCell? left, TableCell? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the first <see cref="TableCell"/> is less than or equal to the second
        /// <see cref="TableCell"/>.
        /// </summary>
        /// <param name="left">The first <see cref="TableCell"/> to compare.</param>
        /// <param name="right">The second <see cref="TableCell"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="TableCell"/> is less than or equal to the second
        /// <see cref="TableCell"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(TableCell? left, TableCell? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two <see cref="TableCell"/> objects are equal.
        /// </summary>
        /// <param name="left">The first <see cref="TableCell"/> to compare.</param>
        /// <param name="right">The second <see cref="TableCell"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="TableCell"/> objects are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(TableCell? left, TableCell? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the first <see cref="TableCell"/> is greater than the second <see cref="TableCell"/>.
        /// </summary>
        /// <param name="left">The first <see cref="TableCell"/> to compare.</param>
        /// <param name="right">The second <see cref="TableCell"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="TableCell"/> is greater than the second <see
        /// cref="TableCell"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(TableCell? left, TableCell? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the first <see cref="TableCell"/> is greater than or equal to the
        /// second <see cref="TableCell"/>.
        /// </summary>
        /// <param name="left">The first <see cref="TableCell"/> to compare.</param>
        /// <param name="right">The second <see cref="TableCell"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="TableCell"/> is greater than or equal to the second
        /// <see cref="TableCell"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(TableCell? left, TableCell? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the current <see cref="TableCell"/> with another <see cref="TableCell"/>.
        /// </summary>
        /// <param name="other">The <see cref="TableCell"/> to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(TableCell? other) => other is null ? 1 : string.CompareOrdinal(Content, other.Content);

        /// <summary>
        /// Converts the content of the <see cref="TableCell"/> to the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type to convert the content to.</typeparam>
        /// <returns>The converted content of the <see cref="TableCell"/>.</returns>
        public TObject? ConvertTo<TObject>() => Content.To<TObject>();

        /// <summary>
        /// Determines whether the current <see cref="TableCell"/> is equal to another <see cref="TableCell"/>.
        /// </summary>
        /// <param name="obj">The <see cref="TableCell"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the current <see cref="TableCell"/> is equal to the other <see
        /// cref="TableCell"/>; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is TableCell TableCellObject && Equals(TableCellObject));

        /// <summary>
        /// Determines whether the current <see cref="TableCell"/> is equal to another <see cref="TableCell"/>.
        /// </summary>
        /// <param name="other">The <see cref="TableCell"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the current <see cref="TableCell"/> is equal to the other <see
        /// cref="TableCell"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(TableCell? other) => other is not null && string.Equals(other.Content, Content, StringComparison.Ordinal);

        /// <summary>
        /// Returns the hash code for the current <see cref="TableCell"/>.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() => Content?.GetHashCode() ?? 0;

        /// <summary>
        /// Returns a string that represents the current <see cref="TableCell"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="TableCell"/>.</returns>
        public override string? ToString() => Content;
    }
}