using Archivist.BaseClasses;
using Archivist.Converters;
using Newtonsoft.Json;

namespace Archivist.Formats.JSON
{
    /// <summary>
    /// Represents the JSON format.
    /// </summary>
    /// <seealso cref="FormatBaseClass{TFormat, TFileReader, TFileWriter}"/>
    public class JsonFormat : FormatBaseClass<JsonFormat, JsonReader, JsonWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFormat"/> class.
        /// </summary>
        /// <param name="options">The options to use when deserializing JSON.</param>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public JsonFormat(JsonSerializerSettings? options, Convertinator? converter)
            : base(new JsonReader(options, converter), new JsonWriter(options))
        {
        }

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