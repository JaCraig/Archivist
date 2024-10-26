using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;

namespace Archivist.Converters
{
    /// <summary>
    /// Converts any object to Image type.
    /// </summary>
    public class AnythingToImageConverter : IDataConverter
    {
        /// <summary>
        /// Converts the specified IGenericFile object to Image.
        /// </summary>
        /// <param name="file">The IGenericFile object to convert.</param>
        /// <returns>The converted Image object.</returns>
        public static Image? Convert(IGenericFile? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Image
            {
                Title = file.Title
            };
            if (file.Metadata is null)
                return ReturnValue;
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Determines if the converter can convert from the specified source type to the specified
        /// destination type.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns><c>true</c> if the converter can convert the types; otherwise, <c>false</c>.</returns>
        public bool CanConvert(Type? source, Type? destination) => source?.IsAssignableTo(typeof(IGenericFile)) == true && destination == typeof(Image);

        /// <summary>
        /// Converts the specified source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns>The converted object.</returns>
        public object? Convert(object? source, Type? destination)
        {
            if (source is not IGenericFile File || destination is null)
                return null;
            return Convert(File);
        }
    }
}