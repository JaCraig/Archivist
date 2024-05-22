using Archivist.BaseClasses;
using System;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a text file.
    /// </summary>
    /// <seealso cref="FileBaseClass{Text}"/>
    public class Text : FileBaseClass<Text>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        /// <param name="content">The content of the text file.</param>
        /// <param name="title">The title of the text file.</param>
        public Text(string? content, string? title)
        {
            Content = content;
            Title = title;
        }

        /// <summary>
        /// Gets or sets the content of the text file.
        /// </summary>
        public override string? Content { get; protected set; }

        /// <summary>
        /// Compares the current text file with another text file.
        /// </summary>
        /// <param name="other">The text file to compare with.</param>
        /// <returns>An integer that indicates the relative order of the text files.</returns>
        public override int CompareTo(Text? other) => string.Compare(other?.Content, Content, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the current text file is equal to another text file.
        /// </summary>
        /// <param name="other">The text file to compare with.</param>
        /// <returns>True if the text files are equal; otherwise, false.</returns>
        public override bool Equals(Text? other) => string.Equals(Content, other?.Content, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Gets or sets the title of the text file.
        /// </summary>
        /// <param name="content">The content of the file.</param>
        public void SetContent(string content) => Content = content;
    }
}