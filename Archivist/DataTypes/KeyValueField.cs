using Archivist.Interfaces;
using ObjectCartographer;
using System;
using System.Collections.Generic;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a field in a card.
    /// </summary>
    public class KeyValueField : IEquatable<KeyValueField>, IComparable<KeyValueField>, IObjectConvertable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueField"/> class.
        /// </summary>
        /// <param name="property">The property of the field.</param>
        /// <param name="parameters">The parameters of the field.</param>
        /// <param name="value">The value of the field.</param>
        public KeyValueField(string? property, IEnumerable<KeyValueParameter>? parameters, string? value)
        {
            Property = property ?? "";
            Parameters.AddRange(parameters ?? Array.Empty<KeyValueParameter>());
            Value = value ?? "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueField"/> class.
        /// </summary>
        /// <param name="field">The field to copy.</param>
        public KeyValueField(KeyValueField? field)
            : this(field?.Property, field?.Parameters, field?.Value)
        {
        }

        /// <summary>
        /// Gets or sets the parameter of the field (sub type).
        /// </summary>
        public List<KeyValueParameter> Parameters { get; } = new List<KeyValueParameter>();

        /// <summary>
        /// Gets or sets the property of the field (the type).
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the value of the field.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Determines whether two <see cref="KeyValueField"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="KeyValueField"/> to compare.</param>
        /// <param name="right">The second <see cref="KeyValueField"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="KeyValueField"/> objects are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(KeyValueField? left, KeyValueField? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Compares two <see cref="KeyValueField"/> objects and determines whether the first one is
        /// less than the second one.
        /// </summary>
        /// <param name="left">The first <see cref="KeyValueField"/> to compare.</param>
        /// <param name="right">The second <see cref="KeyValueField"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="KeyValueField"/> is less than the second one;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(KeyValueField? left, KeyValueField? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Compares two <see cref="KeyValueField"/> objects and determines whether the first one is
        /// less than or equal to the second one.
        /// </summary>
        /// <param name="left">The first <see cref="KeyValueField"/> to compare.</param>
        /// <param name="right">The second <see cref="KeyValueField"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="KeyValueField"/> is less than or equal to the second
        /// one; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(KeyValueField? left, KeyValueField? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two <see cref="KeyValueField"/> objects are equal.
        /// </summary>
        /// <param name="left">The first <see cref="KeyValueField"/> to compare.</param>
        /// <param name="right">The second <see cref="KeyValueField"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="KeyValueField"/> objects are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(KeyValueField? left, KeyValueField? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="KeyValueField"/> objects and determines whether the first one is
        /// greater than the second one.
        /// </summary>
        /// <param name="left">The first <see cref="KeyValueField"/> to compare.</param>
        /// <param name="right">The second <see cref="KeyValueField"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="KeyValueField"/> is greater than the second one;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(KeyValueField? left, KeyValueField? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Compares two <see cref="KeyValueField"/> objects and determines whether the first one is
        /// greater than or equal to the second one.
        /// </summary>
        /// <param name="left">The first <see cref="KeyValueField"/> to compare.</param>
        /// <param name="right">The second <see cref="KeyValueField"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="KeyValueField"/> is greater than or equal to the
        /// second one; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(KeyValueField? left, KeyValueField? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the current <see cref="KeyValueField"/> object with another <see
        /// cref="KeyValueField"/> object and returns an integer that indicates whether the current
        /// object precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">
        /// The <see cref="KeyValueField"/> object to compare with the current object.
        /// </param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return
        /// value has the following meanings: Value Meaning Less than zero This object is less than
        /// the <paramref name="other"/> parameter. Zero This object is equal to <paramref
        /// name="other"/>. Greater than zero This object is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(KeyValueField? other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (other is null)
                return 1;
            var ReturnValue = other.Property?.CompareTo(Property) ?? 1;
            if (ReturnValue != 0)
                return ReturnValue;
            ReturnValue = other.Parameters.Count.CompareTo(Parameters.Count);
            if (ReturnValue != 0)
                return ReturnValue;
            foreach (KeyValueParameter Parameter in Parameters)
            {
                ReturnValue = other.Parameters.Contains(Parameter) ? 0 : 1;
                if (ReturnValue != 0)
                    return ReturnValue;
            }
            return other.Value?.CompareTo(Value) ?? 1;
        }

        /// <summary>
        /// Converts the object to a string and sets the content of the field.
        /// </summary>
        /// <typeparam name="TObject">Object type</typeparam>
        /// <param name="obj">Object to convert</param>
        public void ConvertFrom<TObject>(TObject obj) => Value = obj?.ToString() ?? "";

        /// <summary>
        /// Converts the content of the field to the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type to convert the content to.</typeparam>
        /// <returns>The converted content of the field.</returns>
        public TObject? ConvertTo<TObject>() => Value.To<TObject>();

        /// <summary>
        /// Determines whether the specified object is equal to the current <see
        /// cref="KeyValueField"/> object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is KeyValueField CardFieldObject && Equals(CardFieldObject));

        /// <summary>
        /// Determines whether the specified <see cref="KeyValueField"/> object is equal to the
        /// current <see cref="KeyValueField"/> object.
        /// </summary>
        /// <param name="other">
        /// The <see cref="KeyValueField"/> object to compare with the current object.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="KeyValueField"/> object is equal to the current
        /// object; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(KeyValueField? other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (Property != other.Property)
                return false;
            if (Value != other.Value)
                return false;
            if (Parameters.Count != other.Parameters.Count)
                return false;
            foreach (KeyValueParameter Parameter in Parameters)
            {
                if (!other.Parameters.Contains(Parameter))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="KeyValueField"/> object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            var HashCode = new HashCode();
            HashCode.Add(Property);
            HashCode.Add(Value);
            foreach (KeyValueParameter Parameter in Parameters)
            {
                HashCode.Add(Parameter);
            }
            return HashCode.ToHashCode();
        }

        /// <summary>
        /// Returns a string representation of the card field.
        /// </summary>
        /// <returns>A string representation of the card field.</returns>
        public override string ToString() => $"{Property} ({string.Join(';', Parameters)}): {Value}";
    }
}