using System;

namespace Archivist.Interfaces
{
    /// <summary>
    /// Interface for data converters.
    /// </summary>
    public interface IDataConverter
    {
        /// <summary>
        /// Determines if the converter can convert the source type to the destination type.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <param name="destination">Destination type</param>
        /// <returns>True if the converter can convert the source type to the destination type, otherwise false.</returns>
        bool CanConvert(Type? source, Type? destination);

        /// <summary>
        /// Converts the source object to the destination type.
        /// </summary>
        /// <param name="source">Source type</param>
        /// <param name="destination">Destination type</param>
        /// <returns>Converted object</returns>
        object? Convert(object? source, Type? destination);
    }
}