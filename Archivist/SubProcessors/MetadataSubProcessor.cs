using Archivist.Interfaces;
using System;
using System.IO;

namespace Archivist.SubProcessors
{
    /// <summary>
    /// Metadata sub-processor implementation.
    /// </summary>
    public class MetadataSubProcessor : ISubProcessor
    {
        /// <summary>
        /// Processes the given file to extract metadata.
        /// </summary>
        /// <param name="file">The file to process.</param>
        /// <param name="stream">The stream to process.</param>
        /// <returns>The processed file object.</returns>
        public IGenericFile Process(IGenericFile file, Stream stream)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            // Add metadata extraction logic here
            // For example, extracting metadata from an image file
            if (file is DataTypes.Image imageFile)
            {
                // Extract metadata from the image file and add it to the file's metadata
                file.Metadata["Width"] = imageFile.Width.ToString();
                file.Metadata["Height"] = imageFile.Height.ToString();
                file.Metadata["Format"] = imageFile.ImageType.ToString();
            }

            return file;
        }
    }
}
