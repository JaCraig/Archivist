using Archivist.BaseClasses;
using System;
using System.Collections.Generic;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a card (vCard, etc.) file.
    /// </summary>
    /// <seealso cref="FileBaseClass{Card}"/>
    public class Card : FileBaseClass<Card>
    {
        /// <summary>
        /// Gets or sets the content of the card.
        /// </summary>
        public override string? Content { get; set; }

        /// <summary>
        /// Gets or sets the fields of the card.
        /// </summary>
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName
        {
            get
            {
                if (!Fields.TryGetValue("N", out var NameValue))
                    return "";
                var Entries = NameValue.Trim().Split(';', StringSplitOptions.RemoveEmptyEntries);
                return Entries.Length > 1 ? Entries[1] : "";
            }
            set
            {
                if (!Fields.TryGetValue("N", out var NameValue))
                    NameValue = ";;;;";
                var Entries = NameValue.Trim().Split(';', StringSplitOptions.RemoveEmptyEntries);
                if (Entries.Length > 1)
                    Entries[1] = value;
                Fields["N"] = string.Join(';', Entries);
            }
        }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Middle name
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the person on the card.
        /// </summary>
        public string Name => $"{LastName};{FirstName};{MiddleName};{Prefix};{Suffix}";

        /// <summary>
        /// Prefix
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Suffix
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Compares the card to another card based on their content.
        /// </summary>
        /// <param name="other">The other card to compare.</param>
        /// <returns>An integer that indicates the relative order of the cards.</returns>
        public override int CompareTo(Card? other) => string.Compare(other?.Content, Content, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the card is equal to another card based on their content.
        /// </summary>
        /// <param name="other">The other card to compare.</param>
        /// <returns>True if the cards are equal; otherwise, false.</returns>
        public override bool Equals(Card? other) => string.Equals(Content, other?.Content, StringComparison.OrdinalIgnoreCase);
    }
}