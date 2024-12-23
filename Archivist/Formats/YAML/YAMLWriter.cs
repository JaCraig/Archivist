using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Archivist.Formats.YAML
{
    /// <summary>
    /// Represents a YAML writer for serializing structured objects.
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="YAMLWriter"/> class.</remarks>
    /// <param name="logger">The logger to use for logging.</param>
    public class YAMLWriter(ILogger? logger) : WriterBaseClass(logger)
    {
        /// <summary>
        /// The YAML serializer.
        /// </summary>
        private ISerializer Serializer { get; } = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

        /// <summary>
        /// Writes the structured object to the specified stream as YAML.
        /// </summary>
        /// <param name="file">The structured object to write.</param>
        /// <param name="stream">The stream to write the YAML to.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is a boolean indicating
        /// whether the write operation was successful.
        /// </returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (file is null || !IsValidStream(stream))
            {
                Logger?.LogDebug("{writerName}.WriteAsync(): File or stream is null or invalid.", nameof(YAMLWriter));
                return false;
            }
            if (file is not StructuredObject StructuredObject)
                StructuredObject = file.ToFileType<StructuredObject>()!;
            if (StructuredObject is null)
            {
                Logger?.LogDebug("{writerName}.WriteAsync(): File is not a structured object.", nameof(YAMLWriter));
                return false;
            }
            ExpandoObject? Content = StructuredObject.ConvertTo<ExpandoObject>();
            if (Content is null)
            {
                Logger?.LogDebug("{writerName}.WriteAsync(): Content is null.", nameof(YAMLWriter));
                return false;
            }
            var StringContent = Serializer.Serialize(Content, Content.GetType());
            await stream.WriteAsync(StringContent.ToByteArray()).ConfigureAwait(false);
            return true;
        }
    }
}