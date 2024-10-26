using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        /// <param name="subProcessors">The collection of sub-processors to be managed.</param>
        public FormatManager(IEnumerable<IFormat>? formats, IEnumerable<ISubProcessor>? subProcessors)
        {
            Formats = formats ?? Array.Empty<IFormat>();
            SubProcessors = subProcessors ?? Array.Empty<ISubProcessor>();
        }

        /// <summary>
        /// Gets the collection of formats managed by this instance.
        /// </summary>
        private IEnumerable<IFormat> Formats { get; }

        /// <summary>
        /// Gets the collection of sub-processors managed by this instance.
        /// </summary>
        private IEnumerable<ISubProcessor> SubProcessors { get; set; }
    }
}