using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.RSS
{
    /// <summary>
    /// Represents a RSS reader for reading RSS files.
    /// </summary>
    public class RSSReader : ReaderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RSSReader"/> class.
        /// </summary>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public RSSReader(Convertinator? converter)
        {
            _Converter = converter;
        }

        /// <summary>
        /// Gets the header information of the RSS file.
        /// </summary>
        public override byte[] HeaderInfo { get; } = Array.Empty<byte>();

        /// <summary>
        /// The converter used to convert between IGenericFile objects.
        /// </summary>
        private readonly Convertinator? _Converter;

        /// <summary>
        /// Reads the RSS file asynchronously.
        /// </summary>
        /// <param name="stream">The stream to read the RSS file from.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the generic
        /// file representation of the RSS file.
        /// </returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            var Content = await GetDataAsync(stream).ConfigureAwait(false);
            return new RSS(_Converter, Content, Content.Left(30));
        }

        /// <summary>
        /// Reads the RSS file asynchronously.
        /// </summary>
        /// <param name="stream">The stream to read the RSS file from.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the generic
        /// file representation of the RSS file.
        /// </returns>
        private static async Task<string> GetDataAsync(Stream? stream)
        {
            if (stream?.CanRead != true)
                return "";
            try
            {
                return await stream.ReadAllAsync().ConfigureAwait(false) ?? "";
            }
            catch
            {
                return "";
            }
        }
    }
}