using Archivist.BaseClasses;
using Archivist.Converters;
using System;
using System.Collections.Generic;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a fixed-length file.
    /// </summary>
    public class FixedLengthFile : FileBaseClass<FixedLengthFile>, IEquatable<FixedLengthFile>, IComparable<FixedLengthFile>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthFile"/> class with the default
        /// line separator.
        /// </summary>
        public FixedLengthFile()
            : this(null, "\r\n")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthFile"/> class with the specified
        /// line separator.
        /// </summary>
        /// <param name="lineSeparator">The line separator to use.</param>
        public FixedLengthFile(string lineSeparator)
            : this(null, lineSeparator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthFile"/> class with the specified
        /// line separator.
        /// </summary>
        /// <param name="converter">The type converter.</param>
        /// <param name="lineSeparator">The line separator to use.</param>
        public FixedLengthFile(Convertinator? converter, string lineSeparator = "\r\n")
            : base(converter)
        {
            LineSeparator = lineSeparator;
        }

        /// <summary>
        /// Gets the line separator used in the fixed-length file.
        /// </summary>
        public string LineSeparator { get; }

        /// <summary>
        /// Gets the list of fixed-length records in the file.
        /// </summary>
        public List<FixedLengthRecord> Records { get; } = new List<FixedLengthRecord>();

        /// <summary>
        /// Determines whether two fixed-length files are not equal.
        /// </summary>
        /// <param name="left">The first fixed-length file to compare.</param>
        /// <param name="right">The second fixed-length file to compare.</param>
        /// <returns>True if the two fixed-length files are not equal; otherwise, false.</returns>
        public static bool operator !=(FixedLengthFile? left, FixedLengthFile? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the first fixed-length file is less than the second fixed-length file.
        /// </summary>
        /// <param name="left">The first fixed-length file to compare.</param>
        /// <param name="right">The second fixed-length file to compare.</param>
        /// <returns>
        /// True if the first fixed-length file is less than the second fixed-length file;
        /// otherwise, false.
        /// </returns>
        public static bool operator <(FixedLengthFile? left, FixedLengthFile? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the first fixed-length file is less than or equal to the second
        /// fixed-length file.
        /// </summary>
        /// <param name="left">The first fixed-length file to compare.</param>
        /// <param name="right">The second fixed-length file to compare.</param>
        /// <returns>
        /// True if the first fixed-length file is less than or equal to the second fixed-length
        /// file; otherwise, false.
        /// </returns>
        public static bool operator <=(FixedLengthFile? left, FixedLengthFile? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two fixed-length files are equal.
        /// </summary>
        /// <param name="left">The first fixed-length file to compare.</param>
        /// <param name="right">The second fixed-length file to compare.</param>
        /// <returns>True if the two fixed-length files are equal; otherwise, false.</returns>
        public static bool operator ==(FixedLengthFile? left, FixedLengthFile? right)
        {
            return (left is null) ? right is null : left.Equals(right);
        }

        /// <summary>
        /// Determines whether the first fixed-length file is greater than the second fixed-length file.
        /// </summary>
        /// <param name="left">The first fixed-length file to compare.</param>
        /// <param name="right">The second fixed-length file to compare.</param>
        /// <returns>
        /// True if the first fixed-length file is greater than the second fixed-length file;
        /// otherwise, false.
        /// </returns>
        public static bool operator >(FixedLengthFile? left, FixedLengthFile? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the first fixed-length file is greater than or equal to the second
        /// fixed-length file.
        /// </summary>
        /// <param name="left">The first fixed-length file to compare.</param>
        /// <param name="right">The second fixed-length file to compare.</param>
        /// <returns>
        /// True if the first fixed-length file is greater than or equal to the second fixed-length
        /// file; otherwise, false.
        /// </returns>
        public static bool operator >=(FixedLengthFile? left, FixedLengthFile? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the current fixed-length file with another fixed-length file.
        /// </summary>
        /// <param name="other">The other fixed-length file to compare.</param>
        /// <returns>An integer that indicates the relative order of the objects being compared.</returns>
        public override int CompareTo(FixedLengthFile? other)
        {
            if (other is null)
                return 1;
            if (ReferenceEquals(this, other))
                return 0;
            if (Records.Count != other.Records.Count)
                return Records.Count.CompareTo(other.Records.Count);
            for (var X = 0; X < Records.Count; ++X)
            {
                var Result = Records[X].CompareTo(other.Records[X]);
                if (Result != 0)
                    return Result;
            }
            return 0;
        }

        /// <summary>
        /// Determines whether the current fixed-length file is equal to another fixed-length file.
        /// </summary>
        /// <param name="other">The other fixed-length file to compare.</param>
        /// <returns>
        /// True if the current fixed-length file is equal to the other fixed-length file;
        /// otherwise, false.
        /// </returns>
        public override bool Equals(FixedLengthFile? other) => CompareTo(other) == 0;

        /// <summary>
        /// Determines whether the current fixed-length file is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with the current fixed-length file.</param>
        /// <returns>
        /// True if the current fixed-length file is equal to the other object; otherwise, false.
        /// </returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is not null && obj is FixedLengthFile TempItem && Equals(TempItem));

        /// <summary>
        /// Gets the content of the fixed-length file as a string.
        /// </summary>
        /// <returns>A string that represents the content of the fixed-length file.</returns>
        public override string? GetContent() => string.Join(LineSeparator, Records);

        /// <summary>
        /// Gets the hash code for the fixed-length file.
        /// </summary>
        /// <returns>An integer representing the hash code.</returns>
        public override int GetHashCode()
        {
            var ReturnValue = 0;
            foreach (FixedLengthRecord Record in Records)
                ReturnValue = HashCode.Combine(ReturnValue, Record.GetHashCode());
            return ReturnValue;
        }

        /// <summary>
        /// Returns a string that represents the current fixed-length file.
        /// </summary>
        /// <returns>A string that represents the current fixed-length file.</returns>
        public override string ToString() => GetContent() ?? "";
    }
}