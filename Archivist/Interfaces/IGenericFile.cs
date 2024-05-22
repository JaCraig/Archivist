using System.Collections.Generic;

namespace Archivist.Interfaces
{
    /// <summary>
    /// Represents a generic file.
    /// </summary>
    public interface IGenericFile
    {
        /// <summary>
        /// Gets or sets the content of the file.
        /// </summary>
        string? Content { get; }

        /// <summary>
        /// Gets or sets the metadata associated with the file.
        /// </summary>
        Dictionary<string, string> Metadata { get; }

        /// <summary>
        /// Gets or sets the title of the file.
        /// </summary>
        string? Title { get; set; }
    }
}