using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Archivist.BaseClasses
{
    /// <summary>
    /// Base class for file formats.
    /// </summary>
    /// <typeparam name="TFormat">The derived format class.</typeparam>
    /// <typeparam name="TFileReader">The file reader class.</typeparam>
    /// <typeparam name="TFileWriter">The file writer class.</typeparam>
    /// <seealso cref="IFormat"/>
    public abstract class FormatBaseClass<TFormat, TFileReader, TFileWriter> : IFormat
        where TFormat : FormatBaseClass<TFormat, TFileReader, TFileWriter>
        where TFileReader : IFormatReader
        where TFileWriter : IFormatWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormatBaseClass{TFormat, TFileReader, TFileWriter}"/> class.
        /// </summary>
        /// <param name="reader">The file reader instance.</param>
        /// <param name="writer">The file writer instance.</param>
        protected FormatBaseClass(TFileReader reader, TFileWriter writer)
        {
            Reader = reader;
            Writer = writer;
        }

        /// <summary>
        /// Gets the display name of the format.
        /// </summary>
        public virtual string DisplayName { get; } = typeof(TFormat).Name.AddSpaces().Replace("Format", "").Trim();

        /// <summary>
        /// Gets the file extensions associated with the format.
        /// </summary>
        public abstract string[] Extensions { get; }

        /// <summary>
        /// Gets the header information of the format.
        /// </summary>
        public byte[] HeaderInfo => Reader?.HeaderInfo ?? Array.Empty<byte>();

        /// <summary>
        /// Gets the content types supported by the format.
        /// </summary>
        public abstract string[] MimeTypes { get; }

        /// <summary>
        /// Gets the order that the file format should be checked. The lower the value, the higher the priority.
        /// Note that the order is only relevant when multiple file formats have the same HeaderInfo length.
        /// The system uses HeaderInfo to determine the order to check first, with longer headers checked first.
        /// </summary>
        public virtual int Order { get; }

        /// <summary>
        /// Gets the file reader instance.
        /// </summary>
        protected TFileReader Reader { get; }

        /// <summary>
        /// Gets the file writer instance.
        /// </summary>
        protected TFileWriter Writer { get; }

        /// <summary>
        /// Determines whether the format can read the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns><c>true</c> if the format can read the file; otherwise, <c>false</c>.</returns>
        public bool CanRead(string? fileName) => !string.IsNullOrEmpty(fileName) && Extensions.Any(x => fileName.EndsWith(x, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Determines whether the format can read the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns><c>true</c> if the format can read the stream; otherwise, <c>false</c>.</returns>
        public bool CanRead(Stream? stream) => Reader.CanRead(stream);

        /// <summary>
        /// Determines whether the format can write the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns><c>true</c> if the format can write the file; otherwise, <c>false</c>.</returns>
        public bool CanWrite(string? fileName) => !string.IsNullOrEmpty(fileName) && Extensions.Any(x => fileName.EndsWith(x, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Determines whether the format can write the specified file.
        /// </summary>
        /// <param name="file">The file to write.</param>
        /// <returns><c>true</c> if the format can write the file; otherwise, <c>false</c>.</returns>
        public bool CanWrite(IGenericFile? file) => Writer.CanWrite(file);

        /// <summary>
        /// Reads the file asynchronously from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>A task that represents the asynchronous read operation. The task result contains the generic file.</returns>
        public Task<IGenericFile?> ReadAsync(Stream? stream) => Reader.ReadAsync(stream);

        /// <summary>
        /// Writes the file asynchronously to the specified stream.
        /// </summary>
        /// <param name="writer">The stream to write.</param>
        /// <param name="file">The file to write.</param>
        /// <returns>A task that represents the asynchronous write operation. The task result contains a value indicating whether the write operation was successful.</returns>
        public Task<bool> WriteAsync(Stream? writer, IGenericFile? file) => Writer.WriteAsync(file, writer);
    }
}