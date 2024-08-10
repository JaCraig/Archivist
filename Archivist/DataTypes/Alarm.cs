using System;
using System.Collections;
using System.Collections.Generic;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents an alarm.
    /// </summary>
    public class Alarm : IComparable<Alarm>, IEquatable<Alarm>, IEnumerable<KeyValueField?>, IEnumerable
    {
        /// <summary>
        /// Gets the list of fields associated with the alarm.
        /// </summary>
        public List<KeyValueField?> Fields { get; } = new List<KeyValueField?>();

        /// <summary>
        /// Determines whether two alarm objects are not equal.
        /// </summary>
        /// <param name="left">The first alarm object to compare.</param>
        /// <param name="right">The second alarm object to compare.</param>
        /// <returns><c>true</c> if the alarm objects are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Alarm left, Alarm right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the first alarm object is less than the second alarm object.
        /// </summary>
        /// <param name="left">The first alarm object to compare.</param>
        /// <param name="right">The second alarm object to compare.</param>
        /// <returns>
        /// <c>true</c> if the first alarm object is less than the second alarm object; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(Alarm left, Alarm right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the first alarm object is less than or equal to the second alarm object.
        /// </summary>
        /// <param name="left">The first alarm object to compare.</param>
        /// <param name="right">The second alarm object to compare.</param>
        /// <returns>
        /// <c>true</c> if the first alarm object is less than or equal to the second alarm object;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(Alarm left, Alarm right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two alarm objects are equal.
        /// </summary>
        /// <param name="left">The first alarm object to compare.</param>
        /// <param name="right">The second alarm object to compare.</param>
        /// <returns><c>true</c> if the alarm objects are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Alarm left, Alarm right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the first alarm object is greater than the second alarm object.
        /// </summary>
        /// <param name="left">The first alarm object to compare.</param>
        /// <param name="right">The second alarm object to compare.</param>
        /// <returns>
        /// <c>true</c> if the first alarm object is greater than the second alarm object;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(Alarm left, Alarm right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the first alarm object is greater than or equal to the second alarm object.
        /// </summary>
        /// <param name="left">The first alarm object to compare.</param>
        /// <param name="right">The second alarm object to compare.</param>
        /// <returns>
        /// <c>true</c> if the first alarm object is greater than or equal to the second alarm
        /// object; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(Alarm left, Alarm right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the current alarm object with another alarm object.
        /// </summary>
        /// <param name="other">The alarm object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the alarm objects being compared.</returns>
        public int CompareTo(Alarm? other)
        {
            if (other is null)
                return 1;
            if (ReferenceEquals(this, other))
                return 0;
            foreach (KeyValueField? Field in Fields)
            {
                if (!other.Fields.Contains(Field))
                    return 1;
            }
            foreach (KeyValueField? Field in other.Fields)
            {
                if (!Fields.Contains(Field))
                    return -1;
            }
            return 0;
        }

        /// <summary>
        /// Determines whether the current alarm object is equal to another alarm object.
        /// </summary>
        /// <param name="other">The alarm object to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the current alarm object is equal to the other alarm object; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Alarm? other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            foreach (KeyValueField? Field in Fields)
            {
                if (!other.Fields.Contains(Field))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the current alarm object is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the current alarm object is equal to the other object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is Alarm CalendarObject && Equals(CalendarObject));

        /// <inheritdoc/>
        public IEnumerator<KeyValueField?> GetEnumerator() => ((IEnumerable<KeyValueField?>)Fields).GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Fields).GetEnumerator();

        /// <summary>
        /// Returns the hash code for the current alarm object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            var HashCode = new HashCode();
            foreach (KeyValueField? Field in Fields)
            {
                HashCode.Add(Field);
            }
            return HashCode.ToHashCode();
        }
    }
}