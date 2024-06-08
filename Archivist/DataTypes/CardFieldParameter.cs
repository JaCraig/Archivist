using System;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a parameter for a card field.
    /// </summary>
    public class CardFieldParameter : IEquatable<CardFieldParameter>, IComparable<CardFieldParameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardFieldParameter"/> class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        public CardFieldParameter(string? name, string? value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Determines whether two card field parameters are not equal.
        /// </summary>
        /// <param name="left">The first card field parameter to compare.</param>
        /// <param name="right">The second card field parameter to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the card field parameters are not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(CardFieldParameter? left, CardFieldParameter? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines if the card field parameter is less than another card field parameter.
        /// </summary>
        /// <param name="left">The first card field parameter to compare.</param>
        /// <param name="right">The second card field parameter to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the card field parameter is less than the other card field
        /// parameter; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(CardFieldParameter? left, CardFieldParameter? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if the card field parameter is less than or equal to another card field parameter.
        /// </summary>
        /// <param name="left">The first card field parameter to compare.</param>
        /// <param name="right">The second card field parameter to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the card field parameter is less than or equal to the other
        /// card field parameter; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(CardFieldParameter? left, CardFieldParameter? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether the card field parameter is equal to the specified card field parameter.
        /// </summary>
        /// <param name="left">The card field parameter to compare with the card field parameter.</param>
        /// <param name="right">The card field parameter to compare with the card field parameter.</param>
        /// <returns>
        /// <see langword="true"/> if the card field parameter is equal to the other card field
        /// parameter; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(CardFieldParameter? left, CardFieldParameter? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines if the card field parameter is greater than another card field parameter.
        /// </summary>
        /// <param name="left">The first card field parameter to compare.</param>
        /// <param name="right">The second card field parameter to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the card field parameter is greater than the other card field
        /// parameter; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(CardFieldParameter? left, CardFieldParameter? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if the card field parameter is greater than or equal to another card field parameter.
        /// </summary>
        /// <param name="left">The first card field parameter to compare.</param>
        /// <param name="right">The second card field parameter to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the card field parameter is greater than or equal to the other
        /// card field parameter; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(CardFieldParameter? left, CardFieldParameter? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the card field parameter to another card field parameter to determine the
        /// relative ordering of the two objects.
        /// </summary>
        /// <param name="other">The card field parameter to compare to this card field parameter.</param>
        /// <returns>A value that indicates the relative ordering of the two objects.</returns>
        public int CompareTo(CardFieldParameter? other)
        {
            if (other is null)
                return 1;
            var ReturnValue = string.CompareOrdinal(Name, other.Name);
            if (ReturnValue != 0)
                return ReturnValue;
            return string.CompareOrdinal(Value, other.Value);
        }

        /// <summary>
        /// Determines whether the card field parameter is equal to the specified object.
        /// </summary>
        /// <param name="obj">The object to compare with the card field parameter.</param>
        /// <returns>
        /// <see langword="true"/> if the card field parameter is equal to the object; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is CardFieldParameter CardFieldParameterObject && Equals(CardFieldParameterObject));

        /// <summary>
        /// Compares the card field parameter to another card field parameter to determine if they
        /// are equal.
        /// </summary>
        /// <param name="other">The card field parameter to compare to this card field parameter.</param>
        /// <returns>
        /// <see langword="true"/> if the card field parameter is equal to the other card field
        /// parameter; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(CardFieldParameter? other)
        {
            return other is not null
                && string.Equals(Name, other.Name, StringComparison.Ordinal)
                && string.Equals(Value, other.Value, StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets a hash code for the card field parameter.
        /// </summary>
        /// <returns>A hash code for the card field parameter.</returns>
        public override int GetHashCode() => HashCode.Combine(Name, Value);

        /// <summary>
        /// Returns a string representation of the card field parameter.
        /// </summary>
        /// <returns>A string representation of the card field parameter.</returns>
        public override string ToString() => $"{Name}={Value}";
    }
}