using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.JSON
{
    /// <summary>
    /// Represents a reader for JSON files.
    /// </summary>
    public class JsonReader : ReaderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonReader"/> class.
        /// </summary>
        /// <param name="options">The options to use when deserializing JSON.</param>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public JsonReader(JsonSerializerSettings? options, Convertinator? converter)
        {
            Options = options ?? new JsonSerializerSettings();
            _Converter = converter;
        }

        /// <summary>
        /// Gets the header information for the JSON format.
        /// </summary>
        public override byte[] HeaderInfo => Array.Empty<byte>();

        /// <summary>
        /// The options to use when deserializing JSON.
        /// </summary>
        private JsonSerializerSettings Options { get; }

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
            if (stream is null || !IsValidStream(stream))
                return false;
            try
            {
                var Value = stream.ReadAll();
                if (string.IsNullOrEmpty(Value))
                    return false;
                ExpandoObject? TestObject = JsonConvert.DeserializeObject<ExpandoObject>(Value, Options);
                _ = stream.Seek(0, SeekOrigin.Begin);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reads a JSON file asynchronously from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read the JSON file from.</param>
        /// <returns>The parsed JSON file.</returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            if (stream is null || !IsValidStream(stream))
                return new StructuredObject(_Converter, new ExpandoObject());
            var StreamData = await stream.ReadAllAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(StreamData))
                return new StructuredObject(_Converter, new ExpandoObject());
            ExpandoObject? Data = JsonConvert.DeserializeObject<ExpandoObject>(StreamData, Options);
            return new StructuredObject(_Converter, Data ?? new ExpandoObject());
        }
    }
}