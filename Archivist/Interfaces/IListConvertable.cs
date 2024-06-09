using System.Collections.Generic;

namespace Archivist.Interfaces
{
    /// <summary>
    /// Represents a list convertable object.
    /// </summary>
    public interface IListConvertable
    {
        /// <summary>
        /// Converts the object to a list of the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>The resulting list.</returns>
        List<TObject?> ConvertTo<TObject>();
    }
}