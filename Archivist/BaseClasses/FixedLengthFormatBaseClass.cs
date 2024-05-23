using Archivist.Formats.FixedLength;
using Archivist.Interfaces;

namespace Archivist.BaseClasses
{
    /// <summary>
    /// Base class for fixed-length format classes.
    /// </summary>
    /// <typeparam name="TFormat">The type of the derived fixed-length format class.</typeparam>
    /// <typeparam name="TFileReader">The type of the file reader.</typeparam>
    public abstract class FixedLengthFormatBaseClass<TFormat, TFileReader> : FormatBaseClass<TFormat, TFileReader, FixedLengthWriter>
        where TFormat : FixedLengthFormatBaseClass<TFormat, TFileReader>
        where TFileReader : IFormatReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthFormatBaseClass{TFormat, TFileReader}"/> class.
        /// </summary>
        /// <param name="reader">The file reader.</param>
        protected FixedLengthFormatBaseClass(TFileReader reader)
            : base(reader, new FixedLengthWriter())
        {
        }
    }
}