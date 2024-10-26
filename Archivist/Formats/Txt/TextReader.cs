using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.Txt
{
    /// <summary>
    /// Represents a text reader for reading text files.
    /// </summary>
    public class TextReader : ReaderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextReader"/> class.
        /// </summary>
        /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
        public TextReader(Convertinator? converter)
        {
            _Converter = converter;
        }

        /// <summary>
        /// Gets the header information of the text file.
        /// </summary>
        public override byte[] HeaderInfo { get; } = Array.Empty<byte>();

        /// <summary>
        /// The converter used to convert between IGenericFile objects.
        /// </summary>
        private readonly Convertinator? _Converter;

        /// <summary>
        /// Reads the text file asynchronously.
        /// </summary>
        /// <param name="stream">The stream to read the text file from.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the generic
        /// file representation of the text file.
        /// </returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            if (stream is null || !IsValidStream(stream))
                return new Text(_Converter, "", "");
            var Content = await GetDataAsync(stream).ConfigureAwait(false);
            return new Text(_Converter, Content, Content.Left(30));
        }

        /// <summary>
        /// Reads the text file asynchronously.
        /// </summary>
        /// <param name="stream">The stream to read the text file from.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the generic
        /// file representation of the text file.
        /// </returns>
        private static async Task<string> GetDataAsync(Stream? stream)
        {
            if (stream is null || !IsValidStream(stream))
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