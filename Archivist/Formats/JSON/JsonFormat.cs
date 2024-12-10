using Archivist.BaseClasses;
using Archivist.Converters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Archivist.Formats.JSON
{
    /// <summary>
    /// Represents the JSON format.
    /// </summary>
    /// <seealso cref="FormatBaseClass{TFormat, TFileReader, TFileWriter}"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="JsonFormat"/> class.
    /// </remarks>
    /// <param name="options">The options to use when deserializing JSON.</param>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger.</param>
    public class JsonFormat(JsonSerializerSettings? options, Convertinator? converter, ILogger<JsonFormat>? logger) : FormatBaseClass<JsonFormat, JsonReader, JsonWriter>(new JsonReader(options, converter, logger), new JsonWriter(options, logger))
    {
        /// <summary>
        /// Gets the file extensions associated with the JSON format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "JSON" };

        /// <summary>
        /// Gets the MIME types associated with the JSON format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "APPLICATION/JSON" };
    }
}