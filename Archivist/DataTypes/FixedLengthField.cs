using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using ObjectCartographer;
using System;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a fixed-length field.
    /// </summary>
    public class FixedLengthField : IComparable<FixedLengthField>, IEquatable<FixedLengthField>, IObjectConvertable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthField"/> class.
        /// </summary>
        /// <param name="value">The value of the field.</param>
        /// <param name="maxLength">The maximum length of the field.</param>
        /// <param name="fillerCharacter">The filler character used to pad the field.</param>
        /// <param name="leftAligned">Specifies whether the field is left-aligned or not.</param>
        public FixedLengthField(string? value, int maxLength, char fillerCharacter = ' ', bool leftAligned = true)
        {
            MaxLength = maxLength >= 0 ? maxLength : Value.Length;
            FillerCharacter = fillerCharacter;
            LeftAligned = leftAligned;
            Value = value ?? "";
        }

        /// <summary>
        /// Gets the filler character used to pad the field.
        /// </summary>
        public char FillerCharacter { get; }

        /// <summary>
        /// Gets a value indicating whether the field is left-aligned or not.
        /// </summary>
        public bool LeftAligned { get; }

        /// <summary>
        /// Gets or sets the maximum length of the field.
        /// </summary>
        public int MaxLength { get; }

        /// <summary>
        /// Gets or sets the value of the field.
        /// </summary>
        public string Value
        {
            get => _Value ?? "";
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Value = "";
                    return;
                }
                if (value.Length > MaxLength)
                {
                    _Value = value.Left(MaxLength);
                    return;
                }
                if (value.Length == MaxLength)
                {
                    _Value = value;
                    return;
                }
                _Value = LeftAligned
                    ? value.PadRight(MaxLength, FillerCharacter)
                    : value.PadLeft(MaxLength, FillerCharacter);
            }
        }

        /// <summary>
        /// The internal value of the field.
        /// </summary>
        private string? _Value;

        /// <summary>
        /// Determines whether two <see cref="FixedLengthField"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="FixedLengthField"/> to compare.</param>
        /// <param name="right">The second <see cref="FixedLengthField"/> to compare.</param>
        /// <returns>
        /// true if the two <see cref="FixedLengthField"/> objects are not equal; otherwise, false.
        /// </returns>
        public static bool operator !=(FixedLengthField? left, FixedLengthField? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the first <see cref="FixedLengthField"/> object is less than the
        /// second <see cref="FixedLengthField"/> object.
        /// </summary>
        /// <param name="left">The first <see cref="FixedLengthField"/> to compare.</param>
        /// <param name="right">The second <see cref="FixedLengthField"/> to compare.</param>
        /// <returns>
        /// true if the first <see cref="FixedLengthField"/> object is less than the second <see
        /// cref="FixedLengthField"/> object; otherwise, false.
        /// </returns>
        public static bool operator <(FixedLengthField? left, FixedLengthField? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the first <see cref="FixedLengthField"/> object is less than or equal
        /// to the second <see cref="FixedLengthField"/> object.
        /// </summary>
        /// <param name="left">The first <see cref="FixedLengthField"/> to compare.</param>
        /// <param name="right">The second <see cref="FixedLengthField"/> to compare.</param>
        /// <returns>
        /// true if the first <see cref="FixedLengthField"/> object is less than or equal to the
        /// second <see cref="FixedLengthField"/> object; otherwise, false.
        /// </returns>
        public static bool operator <=(FixedLengthField? left, FixedLengthField? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two <see cref="FixedLengthField"/> objects are equal.
        /// </summary>
        /// <param name="left">The first <see cref="FixedLengthField"/> to compare.</param>
        /// <param name="right">The second <see cref="FixedLengthField"/> to compare.</param>
        /// <returns>
        /// true if the two <see cref="FixedLengthField"/> objects are equal; otherwise, false.
        /// </returns>
        public static bool operator ==(FixedLengthField? left, FixedLengthField? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the first <see cref="FixedLengthField"/> object is greater than the
        /// second <see cref="FixedLengthField"/> object.
        /// </summary>
        /// <param name="left">The first <see cref="FixedLengthField"/> to compare.</param>
        /// <param name="right">The second <see cref="FixedLengthField"/> to compare.</param>
        /// <returns>
        /// true if the first <see cref="FixedLengthField"/> object is greater than the second <see
        /// cref="FixedLengthField"/> object; otherwise, false.
        /// </returns>
        public static bool operator >(FixedLengthField? left, FixedLengthField? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the first <see cref="FixedLengthField"/> object is greater than or
        /// equal to the second <see cref="FixedLengthField"/> object.
        /// </summary>
        /// <param name="left">The first <see cref="FixedLengthField"/> to compare.</param>
        /// <param name="right">The second <see cref="FixedLengthField"/> to compare.</param>
        /// <returns>
        /// true if the first <see cref="FixedLengthField"/> object is greater than or equal to the
        /// second <see cref="FixedLengthField"/> object; otherwise, false.
        /// </returns>
        public static bool operator >=(FixedLengthField? left, FixedLengthField? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an
        /// integer that indicates whether the current instance precedes, follows, or occurs in the
        /// same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">The other field.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(FixedLengthField? other) => string.CompareOrdinal(Value, other?.Value);

        /// <summary>
        /// Converts the content of the field to the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type to convert the content to.</typeparam>
        /// <returns>The converted content of the field.</returns>
        public TObject? ConvertTo<TObject>() => Value.To<TObject>();

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>True if they are the same, otherwise false.</returns>
        public bool Equals(FixedLengthField? other) => other is not null && Value == other.Value;

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if they are the same, otherwise false.</returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is not null && obj is FixedLengthField Other && Equals(Other));

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode() => Value?.GetHashCode() ?? 0;

        /// <summary>
        /// Returns the string representation of the field.
        /// </summary>
        /// <returns>The string representation of the field.</returns>
        public override string ToString() => Value;
    }
}