using Archivist.BaseClasses;
using Archivist.Options;
using Microsoft.Extensions.Options;

namespace Archivist.Formats.Delimited
{
    /// <summary>
    /// Delimited file format
    /// </summary>
    /// <seealso cref="FormatBaseClass{DelimitedFormat, DelimitedReader, DelimitedWriter}"/>
    public class DelimitedFormat : FormatBaseClass<DelimitedFormat, DelimitedReader, DelimitedWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelimitedFormat"/> class.
        /// </summary>
        public DelimitedFormat(IOptions<DelimitedOptions>? options)
            : base(new DelimitedReader(options?.Value ?? DelimitedOptions.Default), new DelimitedWriter(options?.Value ?? DelimitedOptions.Default))
        {
        }

        /// <summary>
        /// Gets the extensions associated with the format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "CSV", "TSV", "TAB" };

        /// <summary>
        /// Gets the content types.
        /// </summary>
        /// <value>The content types.</value>
        public override string[] MimeTypes { get; } = new[] { "TEXT/CSV", "TEXT/TAB-SEPARATED-VALUES", "TEXT/COMMA-SEPARATED-VALUES", "APPLICATION/CSV" };
    }
}