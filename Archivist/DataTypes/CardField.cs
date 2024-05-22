using System;
using System.Collections.Generic;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a field in a card.
    /// </summary>
    public class CardField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardField"/> class.
        /// </summary>
        /// <param name="property">The property of the field.</param>
        /// <param name="parameters">The parameters of the field.</param>
        /// <param name="value">The value of the field.</param>
        public CardField(string property, IEnumerable<CardFieldParameter>? parameters, string? value)
        {
            Property = property;
            Parameters.AddRange(parameters ?? Array.Empty<CardFieldParameter>());
            Value = value;
        }

        /// <summary>
        /// Gets or sets the parameter of the field (sub type).
        /// </summary>
        public List<CardFieldParameter> Parameters { get; } = new List<CardFieldParameter>();

        /// <summary>
        /// Gets or sets the property of the field (the type).
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the value of the field.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Returns a string representation of the card field.
        /// </summary>
        /// <returns>A string representation of the card field.</returns>
        public override string ToString() => $"{Property} ({string.Join(';', Parameters)}): {Value}";
    }
}