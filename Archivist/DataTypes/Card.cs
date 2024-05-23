using Archivist.BaseClasses;
using Archivist.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a card (vCard, etc.) file.
    /// </summary>
    /// <seealso cref="FileBaseClass{Card}"/>
    public class Card : FileBaseClass<Card>, IComparable<Card>, IEquatable<Card>, IEnumerable<CardField?>, IEnumerable
    {
        /// <summary>
        /// Gets the addresses for the card.
        /// </summary>
        public IEnumerable<CardField?> Addresses => this[CommonCardFields.Address];

        /// <summary>
        /// Gets the anniversaries for the card.
        /// </summary>
        public IEnumerable<CardField?> Anniversaries => this[CommonCardFields.Anniversary];

        /// <summary>
        /// Gets the birthdays for the card.
        /// </summary>
        public IEnumerable<CardField?> Birthdays => this[CommonCardFields.Birthday];

        /// <summary>
        /// Gets the number of fields in the card.
        /// </summary>
        public int Count => Fields.Count;

        /// <summary>
        /// Gets the emails for the card.
        /// </summary>
        public IEnumerable<CardField?> Emails => this[CommonCardFields.Email];

        /// <summary>
        /// Gets or sets the fields of the card.
        /// </summary>
        public List<CardField?> Fields { get; } = new List<CardField?>();

        /// <summary>
        /// Gets the first name from the card.
        /// </summary>
        public string FirstName
        {
            get => GetNameField(1);
            set => UpdateNameField(value, 1);
        }

        /// <summary>
        /// Gets the full name for the card.
        /// </summary>
        public CardField? FullName => this[CommonCardFields.FullName].FirstOrDefault();

        /// <summary>
        /// Gets the IM entries from the card.
        /// </summary>
        public IEnumerable<CardField?> InstantMessengers => this[CommonCardFields.IMPP];

        /// <summary>
        /// Gets the languages from the card.
        /// </summary>
        public IEnumerable<CardField?> Languages => this[CommonCardFields.Language];

        /// <summary>
        /// Gets the last name from the card.
        /// </summary>
        public string LastName
        {
            get => GetNameField(0);
            set => UpdateNameField(value, 0);
        }

        /// <summary>
        /// Gets the logos from the card.
        /// </summary>
        public IEnumerable<CardField?> Logos => this[CommonCardFields.Logo];

        /// <summary>
        /// Gets the middle name from the card.
        /// </summary>
        public string MiddleName
        {
            get => GetNameField(2);
            set => UpdateNameField(value, 2);
        }

        /// <summary>
        /// Gets the name for the card.
        /// </summary>
        public CardField? Name => this[CommonCardFields.Name].FirstOrDefault();

        /// <summary>
        /// Gets the nicknames from the card.
        /// </summary>
        public IEnumerable<CardField?> Nicknames => this[CommonCardFields.Nickname];

        /// <summary>
        /// Gets the notes from the card.
        /// </summary>
        public IEnumerable<CardField?> Notes => this[CommonCardFields.Note];

        /// <summary>
        /// Gets the organizations from the card.
        /// </summary>
        public IEnumerable<CardField?> Organizations => this[CommonCardFields.Organization];

        /// <summary>
        /// Gets the phone numbers from the card.
        /// </summary>
        public IEnumerable<CardField?> PhoneNumbers => this[CommonCardFields.Phone];

        /// <summary>
        /// Gets the photos from the card.
        /// </summary>
        public IEnumerable<CardField?> Photos => this[CommonCardFields.Photo];

        /// <summary>
        /// Gets the prefix from the card.
        /// </summary>
        public string Prefix
        {
            get => GetNameField(3);
            set => UpdateNameField(value, 3);
        }

        /// <summary>
        /// Gets the roles of the card.
        /// </summary>
        public IEnumerable<CardField?> Roles => this[CommonCardFields.Role];

        /// <summary>
        /// Gets the sounds from the card.
        /// </summary>
        public IEnumerable<CardField?> Sounds => this[CommonCardFields.Sound];

        /// <summary>
        /// Gets the suffix from the card.
        /// </summary>
        public string Suffix
        {
            get => GetNameField(4);
            set => UpdateNameField(value, 4);
        }

        /// <summary>
        /// Gets the time zones from the card.
        /// </summary>
        public IEnumerable<CardField?> TimeZones => this[CommonCardFields.TimeZone];

        /// <summary>
        /// Gets the titles from the card.
        /// </summary>
        public IEnumerable<CardField?> Titles => this[CommonCardFields.Title];

        /// <summary>
        /// Gets the URLs from the card.
        /// </summary>
        public IEnumerable<CardField?> Websites => this[CommonCardFields.URL];

        /// <summary>
        /// Gets or sets the field at the specified index.
        /// </summary>
        /// <param name="index">The index of the field.</param>
        /// <returns>The field at the specified index.</returns>
        public CardField? this[int index]
        {
            get
            {
                if (index < 0 || index >= Fields.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return Fields[index];
            }
            set
            {
                if (index < 0 || index >= Fields.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                Fields[index] = value;
            }
        }

        /// <summary>
        /// Gets the fields with the specified property name.
        /// </summary>
        /// <param name="property">The property name of the fields.</param>
        /// <returns>The fields with the specified property name.</returns>
        public IEnumerable<CardField?> this[string property] => Fields.Where(field => field?.Property == property).ToList();

        /// <summary>
        /// Gets the fields with the specified property name and parameter.
        /// </summary>
        /// <param name="property">The property name of the fields.</param>
        /// <param name="parameter">The parameter of the fields.</param>
        /// <returns>The fields with the specified property name and parameter.</returns>
        public IEnumerable<CardField?> this[string property, string? parameter] => Fields.Where(field => field?.Property == property && (field?.Parameters.Any(fieldParam => fieldParam.ToString() == parameter) ?? false)).ToList();

        /// <summary>
        /// Determines whether two card objects are not equal.
        /// </summary>
        /// <param name="left">The first card object to compare.</param>
        /// <param name="right">The second card object to compare.</param>
        /// <returns>True if the two card objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Card? left, Card? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether one card object is less than another card object.
        /// </summary>
        /// <param name="left">The first card object to compare.</param>
        /// <param name="right">The second card object to compare.</param>
        /// <returns>
        /// True if the first card object is less than the second card object; otherwise, false.
        /// </returns>
        public static bool operator <(Card? left, Card? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether one card object is less than or equal to another card object.
        /// </summary>
        /// <param name="left">The first card object to compare.</param>
        /// <param name="right">The second card object to compare.</param>
        /// <returns>
        /// True if the first card object is less than or equal to the second card object;
        /// otherwise, false.
        /// </returns>
        public static bool operator <=(Card? left, Card? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two card objects are equal.
        /// </summary>
        /// <param name="left">The first card object to compare.</param>
        /// <param name="right">The second card object to compare.</param>
        /// <returns>True if the two card objects are equal; otherwise, false.</returns>
        public static bool operator ==(Card? left, Card? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether one card object is greater than another card object.
        /// </summary>
        /// <param name="left">The first card object to compare.</param>
        /// <param name="right">The second card object to compare.</param>
        /// <returns>
        /// True if the first card object is greater than the second card object; otherwise, false.
        /// </returns>
        public static bool operator >(Card? left, Card? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether one card object is greater than or equal to another card object.
        /// </summary>
        /// <param name="left">The first card object to compare.</param>
        /// <param name="right">The second card object to compare.</param>
        /// <returns>
        /// True if the first card object is greater than or equal to the second card object;
        /// otherwise, false.
        /// </returns>
        public static bool operator >=(Card? left, Card? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the card to another card based on their content.
        /// </summary>
        /// <param name="other">The other card to compare.</param>
        /// <returns>An integer that indicates the relative order of the cards.</returns>
        public override int CompareTo(Card? other) => string.Compare(other?.GetContent(), GetContent(), StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the card is equal to another card based on their content.
        /// </summary>
        /// <param name="other">The other card to compare.</param>
        /// <returns>True if the cards are equal; otherwise, false.</returns>
        public override bool Equals(Card? other) => string.Equals(GetContent(), other?.GetContent(), StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the card is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the card is equal to the object; otherwise, false.</returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is not null && Equals(obj as Card));

        /// <summary>
        /// Gets the content of the card.
        /// </summary>
        /// <returns>The content of the card.</returns>
        public override string? GetContent() => string.Join(Environment.NewLine, Fields.Where(field => field is not null));

        /// <inheritdoc/>
        public IEnumerator<CardField?> GetEnumerator() => ((IEnumerable<CardField?>)Fields).GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Fields).GetEnumerator();

        /// <summary>
        /// Gets the hash code of the card based on its content.
        /// </summary>
        /// <returns>The hash code of the card.</returns>
        public override int GetHashCode() => GetContent()?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;

        /// <summary>
        /// Gets the portion of the name field at the specified index.
        /// </summary>
        /// <param name="index">The index of the name field.</param>
        /// <returns>The portion of the name field at the specified index.</returns>
        private string GetNameField(int index)
        {
            var NameParts = Name?.Value?.Split(';');
            return NameParts?.Length > index ? NameParts[index] : "";
        }

        /// <summary>
        /// Updates the portion of the name field at the specified index.
        /// </summary>
        /// <param name="value">Value to update it to</param>
        /// <param name="index">The index of the name field.</param>
        private void UpdateNameField(string value, int index)
        {
            CardField? Field = Name;
            if (Field is null)
            {
                Field = new CardField(CommonCardFields.Name, Array.Empty<CardFieldParameter>(), ";;;;");
                Fields.Add(Field);
            }
            var NameParts = Field.Value?.Split(';') ?? Array.Empty<string>();
            if (NameParts.Length < 5)
            {
                var Temp = NameParts;
                NameParts = new string[5];
                Temp.CopyTo(NameParts, 0);
            }
            NameParts[index] = value;
            Field.Value = string.Join(';', NameParts);
        }
    }
}