using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using MetadataExtractor;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.Image
{
    /// <summary>
    /// Represents a reader for Image files.
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="ImageReader"/> class.</remarks>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger.</param>
    public class ImageReader(Convertinator? converter, ILogger? logger) : ReaderBaseClass(logger)
    {
        /// <summary>
        /// Gets the header information for the Image format.
        /// </summary>
        public override byte[] HeaderInfo => Array.Empty<byte>();

        /// <summary>
        /// The converter used to convert between IGenericFile objects.
        /// </summary>
        private readonly Convertinator? _Converter = converter;

        /// <summary>
        /// Determines if the reader can read the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns><c>true</c> if the reader can read the stream; otherwise, <c>false</c>.</returns>
        public override bool InternalCanRead(Stream? stream)
        {
            if (!IsValidStream(stream))
            {
                Logger?.LogDebug("{readerName}.CanRead(): Stream is null or invalid.", nameof(ImageReader));
                return false;
            }
            try
            {
                using var TempImage = SKBitmap.Decode(stream);
                _ = stream.Seek(0, SeekOrigin.Begin);
                return true;
            }
            catch (Exception Ex)
            {
                Logger?.LogDebug(Ex, "{readerName}.CanRead(): Exception occurred.", nameof(ImageReader));
                return false;
            }
        }

        /// <summary>
        /// Reads a Image file asynchronously from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read the Image file from.</param>
        /// <returns>The parsed Image file.</returns>
        public override Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            var ReturnValue = new DataTypes.Image(_Converter);
            if (!IsValidStream(stream))
            {
                Logger?.LogDebug("{readerName}.ReadAsync(): Stream is null or invalid.", nameof(ImageReader));
                return Task.FromResult<IGenericFile?>(ReturnValue);
            }
            try
            {
                GetImageFormat(stream, ReturnValue, Logger);
                GetImageData(stream, ReturnValue, Logger);
                GetImageMetadata(stream, ReturnValue, Logger);
            }
            catch (Exception Ex)
            {
                Logger?.LogError(Ex, "{readerName}.ReadAsync(): Exception occurred.", nameof(ImageReader));
                return Task.FromResult<IGenericFile?>(null);
            }
            return Task.FromResult<IGenericFile?>(ReturnValue);
        }

        /// <summary>
        /// Gets the image data.
        /// </summary>
        /// <param name="stream">The image stream</param>
        /// <param name="returnValue">The image object</param>
        /// <param name="logger">The logger.</param>
        private static void GetImageData(Stream? stream, DataTypes.Image returnValue, ILogger? logger)
        {
            if (!IsValidStream(stream))
            {
                logger?.LogDebug("{readerName}.GetImageData(): Stream is null or invalid.", nameof(ImageReader));
                return;
            }
            // Get the image data
            _ = stream.Seek(0, SeekOrigin.Begin);
            using var TempImage = SKBitmap.Decode(stream);
            if (TempImage is null)
            {
                logger?.LogDebug("{readerName}.GetImageData(): Unable to decode image.", nameof(ImageReader));
                return;
            }
            returnValue.Height = TempImage.Height;
            returnValue.Width = TempImage.Width;
            returnValue.Data = TempImage.Bytes;
            returnValue.BytesPerPixel = TempImage.BytesPerPixel;
        }

        /// <summary>
        /// Gets the image format.
        /// </summary>
        /// <param name="stream">The image stream</param>
        /// <param name="returnValue">The image object</param>
        /// <param name="logger">The logger.</param>
        private static void GetImageFormat(Stream? stream, DataTypes.Image returnValue, ILogger? logger)
        {
            if (!IsValidStream(stream))
            {
                logger?.LogDebug("{readerName}.GetImageFormat(): Stream is null or invalid.", nameof(ImageReader));
                return;
            }
            // Get the image type
            _ = stream.Seek(0, SeekOrigin.Begin);
            using var TempMemoryStream = new MemoryStream(stream.ReadAllBinary());
            using var TempEncoding = SKCodec.Create(TempMemoryStream);
            if (TempEncoding is null)
            {
                logger?.LogDebug("{readerName}.GetImageFormat(): Unable to create codec.", nameof(ImageReader));
                return;
            }
            returnValue.ImageType = (Enum.GetName(TempEncoding.EncodedFormat) ?? "").ToLowerInvariant();
            if (returnValue.ImageType == "jpeg")
                returnValue.ImageType = "jpg";
        }

        /// <summary>
        /// Gets the metadata for the image.
        /// </summary>
        /// <param name="stream">The image stream</param>
        /// <param name="returnValue">The image object</param>
        /// <param name="logger">The logger.</param>
        private static void GetImageMetadata(Stream? stream, DataTypes.Image returnValue, ILogger? logger)
        {
            if (!IsValidStream(stream))
            {
                logger?.LogDebug("{readerName}.GetImageMetadata(): Stream is null or invalid.", nameof(ImageReader));
                return;
            }
            _ = stream.Seek(0, SeekOrigin.Begin);
            foreach (MetadataExtractor.Directory Directory in ImageMetadataReader.ReadMetadata(stream))
            {
                foreach (Tag Tag in Directory.Tags)
                {
                    if (!Tag.HasName)
                        continue;
                    returnValue.Metadata[Tag.Name] = Tag.Description ?? "";
                }
            }
        }
    }
}