using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Newtonsoft.Image;
using System;
using System.Dynamic;
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
        /// <param name="options">The options to use when deserializing Image.</param>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public ImageReader(ImageSerializerSettings? options, Convertinator? converter)
        {
            Options = options ?? new ImageSerializerSettings();
            _Converter = converter;
        }

        /// <summary>
        /// Gets the header information for the Image format.
        /// </summary>
        public override byte[] HeaderInfo => Array.Empty<byte>();

        /// <summary>
        /// The options to use when deserializing Image.
        /// </summary>
        private ImageSerializerSettings Options { get; }

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
            if (stream is null)
                return false;
            try
            {
                var Value = stream.ReadAll();
                if (string.IsNullOrEmpty(Value))
                    return false;
                ExpandoObject? TestObject = ImageConvert.DeserializeObject<ExpandoObject>(Value, Options);
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
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            if (stream?.CanRead != true)
                return new StructuredObject(_Converter, new ExpandoObject());
            var StreamData = await stream.ReadAllAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(StreamData))
                return new StructuredObject(_Converter, new ExpandoObject());
            ExpandoObject? Data = ImageConvert.DeserializeObject<ExpandoObject>(StreamData, Options);
            return new StructuredObject(_Converter, Data ?? new ExpandoObject());
        }
    }
}