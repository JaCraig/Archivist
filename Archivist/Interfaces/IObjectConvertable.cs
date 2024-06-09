namespace Archivist.Interfaces
{
    /// <summary>
    /// Represents an object that can be converted to another type.
    /// </summary>
    public interface IObjectConvertable
    {
        /// <summary>
        /// Imports the object into the current object.
        /// </summary>
        /// <typeparam name="TObject">Type of the object importing.</typeparam>
        /// <param name="obj">The object to import.</param>
        void ConvertFrom<TObject>(TObject obj);

        /// <summary>
        /// Converts the object to the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>The resulting object.</returns>
        TObject? ConvertTo<TObject>();
    }
}