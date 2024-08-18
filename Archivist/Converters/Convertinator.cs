using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts data from one type to another using a collection of IDataConverter implementations.
    /// </summary>
    public class Convertinator
    {
        /// <summary>
        /// Initializes a new instance of the Convertinator class.
        /// </summary>
        /// <param name="converters">The collection of IDataConverter implementations.</param>
        public Convertinator(IEnumerable<IDataConverter>? converters)
        {
            Converters = (IEnumerable<IDataConverter>?)(converters?.OrderByDescending(x => x?.GetType()?.Assembly == typeof(Convertinator).Assembly)) ?? Array.Empty<IDataConverter>();
            Instance = this;
        }

        /// <summary>
        /// The singleton instance of the Convertinator class.
        /// </summary>
        internal static Convertinator? Instance { get; private set; }

        /// <summary>
        /// The collection of IDataConverter implementations.
        /// </summary>
        private IEnumerable<IDataConverter> Converters { get; }

        /// <summary>
        /// Converts the given source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
        /// <param name="destination">The destination type to convert to.</param>
        /// <returns>The converted object of the destination type.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is null || destination is null)
                return null;
            IDataConverter? Converter = FindConverter(source.GetType(), destination);
            return Converter?.Convert(source, destination);
        }

        /// <summary>
        /// Finds the appropriate IDataConverter implementation for the given source and destination types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>
        /// The IDataConverter implementation that can convert the source type to the destination type.
        /// </returns>
        private IDataConverter? FindConverter(Type? source, Type? destination) => Converters.FirstOrDefault(c => c.CanConvert(source, destination));
    }
}