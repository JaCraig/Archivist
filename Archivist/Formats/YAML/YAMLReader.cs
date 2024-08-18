using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
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
    public class YAMLReader : ReaderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YAMLReader"/> class.
        /// </summary>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public YAMLReader(Convertinator? converter)
        {
            _Converter = converter;
            Deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
        }

        /// <summary>
        /// Gets the header information for the YAML format.
        /// </summary>
        public override byte[] HeaderInfo => Array.Empty<byte>();

        /// <summary>
        /// The deserializer to use when deserializing YAML.
        /// </summary>
        private IDeserializer Deserializer { get; }

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
                ExpandoObject? TestObject = Deserializer.Deserialize<ExpandoObject>(Value);
                _ = stream.Seek(0, SeekOrigin.Begin);
                return true;
            }
            catch
            {
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
            if (stream?.CanRead != true)
                return new StructuredObject();
            var StreamData = await stream.ReadAllAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(StreamData))
                return new StructuredObject();
            ExpandoObject? Data = Deserializer.Deserialize<ExpandoObject>(StreamData);
            return new StructuredObject(_Converter, Data ?? new ExpandoObject());
        }
    }
}