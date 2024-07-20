using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
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
    public class XMLReader : ReaderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonReader"/> class.
        /// </summary>
        /// <param name="options">The options to use when deserializing JSON.</param>
        public XMLReader(JsonSerializerSettings? options)
        {
            Options = options ?? new JsonSerializerSettings();
        }

        /// <summary>
        /// Gets the header information of the XML file.
        /// </summary>
        public override byte[] HeaderInfo { get; } = new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22 };

        /// <summary>
        /// The options to use when deserializing JSON.
        /// </summary>
        private JsonSerializerSettings Options { get; }

        /// <summary>
        /// Reads a JSON file asynchronously from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read the JSON file from.</param>
        /// <returns>The parsed JSON file.</returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            if (stream?.CanRead != true)
                return new StructuredObject();
            var StreamData = await stream.ReadAllAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(StreamData))
                return new StructuredObject();
            var Doc = new XmlDocument();
            Doc.LoadXml(StreamData);
            var JsonContent = JsonConvert.SerializeXmlNode(Doc);
            if (string.IsNullOrEmpty(JsonContent))
                return new StructuredObject();

            ExpandoObject? Data = JsonConvert.DeserializeObject<ExpandoObject>(JsonContent, Options);
            if (Data is null)
                return new StructuredObject();

            return new StructuredObject(CleanUpReturnValue(Data));
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
            if (DataDictionary.ContainsKey("?xml"))
                _ = DataDictionary.Remove("?xml");

            if (DataDictionary.Count == 1 && DataDictionary.TryGetValue("root", out var Value) && Value is ExpandoObject Root)
                return Root;

            return data;
        }
    }
}