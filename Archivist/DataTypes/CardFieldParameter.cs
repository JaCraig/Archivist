namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a parameter for a card field.
    /// </summary>
    public class CardFieldParameter
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
        /// Returns a string representation of the card field parameter.
        /// </summary>
        /// <returns>A string representation of the card field parameter.</returns>
        public override string ToString() => $"{Name}={Value}";
    }
}