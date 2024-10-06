using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using MetadataExtractor;
using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.Image
{
    /// <summary>
    /// Represents a reader for Image files.
    /// </summary>
    public class ImageReader : ReaderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageReader"/> class.
        /// </summary>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public ImageReader(Convertinator? converter)
        {
            _Converter = converter;
        }

        /// <summary>
        /// Gets the header information for the Image format.
        /// </summary>
        public override byte[] HeaderInfo => Array.Empty<byte>();

        /// <summary>
        /// The converter used to convert between IGenericFile objects.
        /// </summary>
        private readonly Convertinator? _Converter;

        /// <summary>
        /// Determines if the reader can read the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns><c>true</c> if the reader can read the stream; otherwise, <c>false</c>.</returns>
        public override bool InternalCanRead(Stream? stream)
        {
            if (stream?.CanRead != true)
                return false;
            try
            {
                using var TempImage = SKBitmap.Decode(stream);
                _ = stream.Seek(0, SeekOrigin.Begin);
                return true;
            }
            catch
            {
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
            if (stream?.CanRead != true)
                return Task.FromResult<IGenericFile?>(ReturnValue);
            GetImageFormat(stream, ReturnValue);
            GetImageData(stream, ReturnValue);
            GetImageMetadata(stream, ReturnValue);
            return Task.FromResult<IGenericFile?>(ReturnValue);
        }

        /// <summary>
        /// Gets the image data.
        /// </summary>
        /// <param name="stream">The image stream</param>
        /// <param name="returnValue">The image object</param>
        private static void GetImageData(Stream? stream, DataTypes.Image returnValue)
        {
            if (stream?.CanRead != true)
                return;
            // Get the image data
            _ = stream.Seek(0, SeekOrigin.Begin);
            using var TempImage = SKBitmap.Decode(stream);
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
        private static void GetImageFormat(Stream? stream, DataTypes.Image returnValue)
        {
            if (stream?.CanRead != true)
                return;
            // Get the image type
            _ = stream.Seek(0, SeekOrigin.Begin);
            using var TempMemoryStream = new MemoryStream(stream.ReadAllBinary());
            using var TempEncoding = SKCodec.Create(TempMemoryStream);
            returnValue.ImageType = (Enum.GetName(TempEncoding.EncodedFormat) ?? "").ToLowerInvariant();
        }

        /// <summary>
        /// Gets the metadata for the image.
        /// </summary>
        /// <param name="stream">The image stream</param>
        /// <param name="returnValue">The image object</param>
        private static void GetImageMetadata(Stream? stream, DataTypes.Image returnValue)
        {
            if (stream?.CanRead != true)
                return;
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