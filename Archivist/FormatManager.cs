using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist
{
    /// <summary>
    /// Represents a manager for handling different file formats.
    /// </summary>
    public class FormatManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormatManager"/> class.
        /// </summary>
        /// <param name="formats">The collection of formats to be managed.</param>
        public FormatManager(IEnumerable<IFormat>? formats)
        {
            Formats = formats ?? Array.Empty<IFormat>();
        }

        /// <summary>
        /// Gets the collection of formats managed by this instance.
        /// </summary>
        private IEnumerable<IFormat> Formats { get; }
    }
}