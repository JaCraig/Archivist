using Archivist.Formats.FixedLength;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;

namespace Archivist.BaseClasses
{
    /// <summary>
    /// Base class for fixed-length format classes.
    /// </summary>
    /// <typeparam name="TFormat">The type of the derived fixed-length format class.</typeparam>
    /// <typeparam name="TFileReader">The type of the file reader.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="FixedLengthFormatBaseClass{TFormat, TFileReader}"/> class.
    /// </remarks>
    /// <param name="reader">The file reader.</param>
    /// <param name="logger">The logger.</param>"
    public abstract class FixedLengthFormatBaseClass<TFormat, TFileReader>(TFileReader reader, ILogger? logger)
        : FormatBaseClass<TFormat, TFileReader, FixedLengthWriter>(reader, new FixedLengthWriter(logger))
            where TFormat : FixedLengthFormatBaseClass<TFormat, TFileReader>
            where TFileReader : IFormatReader
    {
    }
}