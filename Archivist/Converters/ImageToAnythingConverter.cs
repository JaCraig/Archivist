using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts a Image object to a Anything object.
    /// </summary>
    public class ImageToAnythingConverter : IDataConverter
    {
        /// <summary>
        /// Converts a Image object to a Anything object.
        /// </summary>
        /// <param name="file">The Image object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted Anything object.</returns>
        public static IGenericFile? Convert(Image? file, Type? destination)
        {
            if (file is null || destination is null)
                return null;
            var ReturnValue = (IGenericFile?)Activator.CreateInstance(destination);
            if (ReturnValue is null)
                return null;
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata[Metadata.Key] = Metadata.Value;
            }
            ReturnValue.Title = file.Title ?? file.Description ?? "";
            return ReturnValue;
        }

        /// <summary>
        /// Determines if the conversion is possible between the specified types.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>True if the conversion is possible, otherwise false.</returns>
        public bool CanConvert(Type? source, Type? destination) => source == typeof(Image) && destination?.IsAssignableTo(typeof(IGenericFile)) == true;

        /// <summary>
        /// Converts an object from the source type to the destination type.
        /// </summary>
        /// <param name="source">The object to convert.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not Image File || destination is null)
                return null;
            return Convert(File, destination);
        }
    }
}