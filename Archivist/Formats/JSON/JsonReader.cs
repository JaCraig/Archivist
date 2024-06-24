using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.Interfaces;
using System;
using System.Dynamic;
using System.IO;
using System.Text.Json;
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
        public JsonReader(JsonSerializerOptions? options)
        {
            Options = options ?? new JsonSerializerOptions(JsonSerializerDefaults.Web);
        }

        /// <summary>
        /// Gets the header information for the JSON format.
        /// </summary>
        public override byte[] HeaderInfo => Array.Empty<byte>();

        /// <summary>
        /// The options to use when deserializing JSON.
        /// </summary>
        private JsonSerializerOptions Options { get; }

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
                ExpandoObject? TestObject = JsonSerializer.Deserialize<ExpandoObject>(stream!, Options);
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
            if (stream is null)
                return new StructuredObject();
            ExpandoObject? Data = await JsonSerializer.DeserializeAsync<ExpandoObject>(stream, Options).ConfigureAwait(false);
            if (Data is null)
                return new StructuredObject();

            return new StructuredObject(Data);
        }
    }
}