using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a fixed-length record.
    /// </summary>
    public class FixedLengthRecord : IEnumerable<FixedLengthField>, IComparable<FixedLengthRecord>, IEquatable<FixedLengthRecord>
    {
        /// <summary>
        /// Gets the number of fields in the record.
        /// </summary>
        public int Count => Fields.Count;

        /// <summary>
        /// Gets the list of fields in the record.
        /// </summary>
        public List<FixedLengthField> Fields { get; } = new List<FixedLengthField>();

        /// <summary>
        /// Gets the total length of the record.
        /// </summary>
        public int Length => Fields.Sum(x => x.MaxLength);

        /// <summary>
        /// Determines whether two fixed-length records are not equal.
        /// </summary>
        /// <param name="left">The first fixed-length record to compare.</param>
        /// <param name="right">The second fixed-length record to compare.</param>
        /// <returns>true if the two fixed-length records are not equal; otherwise, false.</returns>
        public static bool operator !=(FixedLengthRecord? left, FixedLengthRecord? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the first fixed-length record is less than the second fixed-length record.
        /// </summary>
        /// <param name="left">The first fixed-length record to compare.</param>
        /// <param name="right">The second fixed-length record to compare.</param>
        /// <returns>true if the first fixed-length record is less than the second fixed-length record; otherwise, false.</returns>
        public static bool operator <(FixedLengthRecord? left, FixedLengthRecord? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the first fixed-length record is less than or equal to the second fixed-length record.
        /// </summary>
        /// <param name="left">The first fixed-length record to compare.</param>
        /// <param name="right">The second fixed-length record to compare.</param>
        /// <returns>true if the first fixed-length record is less than or equal to the second fixed-length record; otherwise, false.</returns>
        public static bool operator <=(FixedLengthRecord? left, FixedLengthRecord? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two fixed-length records are equal.
        /// </summary>
        /// <param name="left">The first fixed-length record to compare.</param>
        /// <param name="right">The second fixed-length record to compare.</param>
        /// <returns>true if the two fixed-length records are equal; otherwise, false.</returns>
        public static bool operator ==(FixedLengthRecord? left, FixedLengthRecord? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the first fixed-length record is greater than the second fixed-length record.
        /// </summary>
        /// <param name="left">The first fixed-length record to compare.</param>
        /// <param name="right">The second fixed-length record to compare.</param>
        /// <returns>true if the first fixed-length record is greater than the second fixed-length record; otherwise, false.</returns>
        public static bool operator >(FixedLengthRecord? left, FixedLengthRecord? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the first fixed-length record is greater than or equal to the second fixed-length record.
        /// </summary>
        /// <param name="left">The first fixed-length record to compare.</param>
        /// <param name="right">The second fixed-length record to compare.</param>
        /// <returns>true if the first fixed-length record is greater than or equal to the second fixed-length record; otherwise, false.</returns>
        public static bool operator >=(FixedLengthRecord? left, FixedLengthRecord? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the current fixed-length record with another fixed-length record and returns an integer that indicates whether the current record is shorter, equal to, or longer than the other record.
        /// </summary>
        /// <param name="other">The fixed-length record to compare with the current record.</param>
        /// <returns>A positive integer if the current record is longer than the other record, zero if they are of equal length, or a negative integer if the current record is shorter than the other record.</returns>
        public int CompareTo(FixedLengthRecord? other)
        {
            if (other is null)
                return 1;
            if (ReferenceEquals(this, other))
                return 0;
            if (Length != other.Length)
                return Length.CompareTo(other.Length);
            for (var X = 0; X < Fields.Count; ++X)
            {
                var Result = Fields[X].CompareTo(other.Fields[X]);
                if (Result != 0)
                    return Result;
            }
            return 0;
        }

        /// <summary>
        /// Determines whether the current fixed-length record is equal to another fixed-length record.
        /// </summary>
        /// <param name="other">The fixed-length record to compare with the current record.</param>
        /// <returns>true if the current fixed-length record is equal to the other record; otherwise, false.</returns>
        public bool Equals(FixedLengthRecord? other) => CompareTo(other) == 0;

        /// <summary>
        /// Determines whether the current fixed-length record is equal to the specified object.
        /// </summary>
        /// <param name="obj">The object to compare with the current fixed-length record.</param>
        /// <returns>true if the current fixed-length record is equal to the specified object; otherwise, false.</returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is not null && obj is FixedLengthRecord TempItem && Equals(TempItem));

        /// <summary>
        /// Returns an enumerator that iterates through the fields in the record.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the fields in the record.</returns>
        public IEnumerator<FixedLengthField> GetEnumerator() => Fields.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the fields in the record.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the fields in the record.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns the hash code for the current fixed-length record.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            var Result = 0;
            foreach (FixedLengthField Item in Fields)
            {
                Result = HashCode.Combine(Item.GetHashCode(), Result);
            }
            return Result;
        }

        /// <summary>
        /// The string representation of the fixed-length record.
        /// </summary>
        /// <returns>The string representation of the fixed-length record.</returns>
        public override string ToString() => string.Concat(Fields);
    }
}