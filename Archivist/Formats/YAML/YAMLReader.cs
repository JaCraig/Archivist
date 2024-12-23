using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Archivist.Formats.YAML
{
    /// <summary>
    /// Represents a reader for YAML files.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="YAMLReader"/> class.
    /// </remarks>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger.</param>
    public class YAMLReader(Convertinator? converter, ILogger? logger) : ReaderBaseClass(logger)
    {
        /// <summary>
        /// The converter used to convert between IGenericFile objects.
        /// </summary>
        private readonly Convertinator? _Converter = converter;

        /// <summary>
        /// Gets the header information for the YAML format.
        /// </summary>
        public override byte[] HeaderInfo => Array.Empty<byte>();

        /// <summary>
        /// The deserializer to use when deserializing YAML.
        /// </summary>
        private IDeserializer Deserializer { get; } = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

        /// <summary>
        /// Determines if the reader can read the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns><c>true</c> if the reader can read the stream; otherwise, <c>false</c>.</returns>
        public override bool InternalCanRead(Stream? stream)
        {
            if (!IsValidStream(stream))
            {
                Logger?.LogDebug("{readerName}.InternalCanRead(): Stream is null or invalid.", nameof(YAMLReader));
                return false;
            }
            try
            {
                var Value = stream.ReadAll();
                if (string.IsNullOrEmpty(Value))
                    return false;
                ExpandoObject? TestObject = Deserializer.Deserialize<ExpandoObject>(Value);
                _ = stream.Seek(0, SeekOrigin.Begin);
                return true;
            }
            catch (Exception Ex)
            {
                Logger?.LogDebug(Ex, "{readerName}.InternalCanRead(): Exception occurred.", nameof(YAMLReader));
                return false;
            }
        }

        /// <summary>
        /// Reads a YAML file asynchronously from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read the YAML file from.</param>
        /// <returns>The parsed YAML file.</returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            if (!IsValidStream(stream))
            {
                Logger?.LogDebug("{readerName}.ReadAsync(): Stream is null or invalid.", nameof(YAMLReader));
                return new StructuredObject();
            }
            var StreamData = await stream.ReadAllAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(StreamData))
            {
                Logger?.LogDebug("{readerName}.ReadAsync(): Stream data is null or empty.", nameof(YAMLReader));
                return new StructuredObject();
            }
            ExpandoObject? Data = Deserializer.Deserialize<ExpandoObject>(StreamData);
            return new StructuredObject(_Converter, Data ?? new ExpandoObject());
        }
    }
}