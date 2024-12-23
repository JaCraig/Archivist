using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
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
    /// <remarks>
    /// Initializes a new instance of the <see cref="JsonReader"/> class.
    /// </remarks>
    /// <param name="options">The options to use when deserializing JSON.</param>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger.</param>
    public class JsonReader(JsonSerializerSettings? options, Convertinator? converter, ILogger? logger) : ReaderBaseClass(logger)
    {
        /// <summary>
        /// The converter used to convert between IGenericFile objects.
        /// </summary>
        private readonly Convertinator? _Converter = converter;

        /// <summary>
        /// Gets the header information for the JSON format.
        /// </summary>
        public override byte[] HeaderInfo => Array.Empty<byte>();

        /// <summary>
        /// The options to use when deserializing JSON.
        /// </summary>
        private JsonSerializerSettings Options { get; } = options ?? new JsonSerializerSettings();

        /// <summary>
        /// Determines if the reader can read the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns><c>true</c> if the reader can read the stream; otherwise, <c>false</c>.</returns>
        public override bool InternalCanRead(Stream? stream)
        {
            if (!IsValidStream(stream))
            {
                Logger?.LogDebug("{readerName}.InternalCanRead(): Stream is null or invalid.", nameof(JsonReader));
                return false;
            }
            try
            {
                var Value = stream.ReadAll();
                if (string.IsNullOrEmpty(Value))
                    return false;
                ExpandoObject? TestObject = JsonConvert.DeserializeObject<ExpandoObject>(Value, Options);
                _ = stream.Seek(0, SeekOrigin.Begin);
                return true;
            }
            catch (Exception Ex)
            {
                Logger?.LogDebug(Ex, "{readerName}.InternalCanRead(): An error occurred.", nameof(JsonReader));
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
            if (!IsValidStream(stream))
            {
                Logger?.LogDebug("{readerName}.ReadAsync(): Stream is null or invalid.", nameof(JsonReader));
                return new StructuredObject(_Converter, new ExpandoObject());
            }
            var StreamData = await stream.ReadAllAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(StreamData))
                return new StructuredObject(_Converter, new ExpandoObject());
            ExpandoObject? Data = JsonConvert.DeserializeObject<ExpandoObject>(StreamData, Options);
            return new StructuredObject(_Converter, Data ?? new ExpandoObject());
        }
    }
}