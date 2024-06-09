namespace Archivist.Interfaces
{
    /// <summary>
    /// Represents an object that can be converted to another type.
    /// </summary>
    public interface IObjectConvertable
    {
        /// <summary>
        /// Converts the object to the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>The resulting object.</returns>
        TObject? ConvertTo<TObject>();
    }
}