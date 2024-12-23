using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Archivist.Formats.XML
{
    /// <summary>
    /// Represents a reader for XML files.
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="JsonReader"/> class.</remarks>
    /// <param name="options">The options to use when deserializing JSON.</param>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger.</param>
    public class XMLReader(JsonSerializerSettings? options, Convertinator? converter, ILogger? logger) : ReaderBaseClass(logger)
    {
        /// <summary>
        /// Gets the header information of the XML file.
        /// </summary>
        public override byte[] HeaderInfo { get; } = "<?xml version=\"1.0\""u8.ToArray();

        /// <summary>
        /// The options to use when deserializing JSON.
        /// </summary>
        private JsonSerializerSettings Options { get; } = options ?? new JsonSerializerSettings();

        /// <summary>
        /// The converter used to convert between IGenericFile objects.
        /// </summary>
        private readonly Convertinator? _Converter = converter;

        /// <summary>
        /// Reads a JSON file asynchronously from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read the JSON file from.</param>
        /// <returns>The parsed JSON file.</returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            if (!IsValidStream(stream))
            {
                Logger?.LogDebug("{readerName}.ReadAsync(): Stream is null or invalid.", nameof(XMLReader));
                return new StructuredObject(_Converter, new ExpandoObject());
            }
            var StreamData = await stream.ReadAllAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(StreamData))
                return new StructuredObject(_Converter, new ExpandoObject());
            var Doc = new XmlDocument();
            Doc.LoadXml(StreamData);
            var JsonContent = JsonConvert.SerializeXmlNode(Doc);
            if (string.IsNullOrEmpty(JsonContent))
                return new StructuredObject(_Converter, new ExpandoObject());

            ExpandoObject? Data = JsonConvert.DeserializeObject<ExpandoObject>(JsonContent, Options);
            if (Data is null)
                return new StructuredObject(_Converter, new ExpandoObject());

            return new StructuredObject(_Converter, CleanUpReturnValue(Data));
        }

        /// <summary>
        /// Cleans up the return value by removing unnecessary properties.
        /// </summary>
        /// <param name="data">The data to clean up.</param>
        /// <returns>The cleaned up data.</returns>
        private static ExpandoObject? CleanUpReturnValue(ExpandoObject? data)
        {
            if (data is null)
                return new ExpandoObject();
            var DataDictionary = data as IDictionary<string, object>;
            _ = DataDictionary.Remove("?xml");

            if (DataDictionary.Count == 1 && DataDictionary.TryGetValue("root", out var Value) && Value is ExpandoObject Root)
                return Root;

            return data;
        }
    }
}