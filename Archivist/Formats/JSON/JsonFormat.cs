using Archivist.BaseClasses;
using System.Text.Json;

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
        public JsonFormat(JsonSerializerOptions? options)
            : base(new JsonReader(options), new JsonWriter(options))
        {
        }

        /// <summary>
        /// Gets the file extensions associated with the JSON format.
        /// </summary>
        public override string[] Extensions { get; } = new[] { "JSON" };

        /// <summary>
        /// Gets the MIME types associated with the Excel format.
        /// </summary>
        public override string[] MimeTypes { get; } = new[] { "APPLICATION/JSON" };
    }
}