using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.JSON
{
    /// <summary>
    /// Represents a JSON writer for serializing structured objects.
    /// </summary>
    public class JsonWriter : WriterBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonWriter"/> class.
        /// </summary>
        /// <param name="options">The options to use when serializing JSON.</param>
        public JsonWriter(JsonSerializerSettings? options)
        {
            Options = options ?? new JsonSerializerSettings();
        }

        /// <summary>
        /// JsonSerializer options
        /// </summary>
        private JsonSerializerSettings Options { get; }

        /// <summary>
        /// Writes the structured object to the specified stream as JSON.
        /// </summary>
        /// <param name="file">The structured object to write.</param>
        /// <param name="stream">The stream to write the JSON to.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is a boolean indicating
        /// whether the write operation was successful.
        /// </returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (file is null || stream is null)
                return false;
            if (!stream.CanWrite)
                return false;
            if (file is not StructuredObject StructuredObject)
                StructuredObject = file.ToFileType<StructuredObject>()!;
            if (StructuredObject is null)
                return false;
            ExpandoObject? Content = StructuredObject.ConvertTo<ExpandoObject>();
            if (Content is null)
                return false;
            var StringContent = JsonConvert.SerializeObject(Content, Content.GetType(), Options);
            await stream.WriteAsync(StringContent.ToByteArray()).ConfigureAwait(false);
            return true;
        }
    }
}