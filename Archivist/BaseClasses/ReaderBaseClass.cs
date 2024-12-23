using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.BaseClasses
{
    /// <summary>
    /// Base class for format readers.
    /// </summary>
    /// <seealso cref="IFormatReader"/>
    /// <remarks>Initializes a new instance of the <see cref="ReaderBaseClass"/> class.</remarks>
    /// <param name="logger">The logger to use for logging.</param>
    public abstract class ReaderBaseClass(ILogger? logger) : IFormatReader
    {
        /// <summary>
        /// Gets the header information for the format.
        /// </summary>
        public abstract byte[] HeaderInfo { get; }

        /// <summary>
        /// Gets the logger to use for logging.
        /// </summary>
        protected ILogger? Logger { get; } = logger;

        /// <summary>
        /// Determines whether the reader can read the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns><c>true</c> if the reader can read the stream; otherwise, <c>false</c>.</returns>
        public bool CanRead(Stream? stream)
        {
            if (!IsValidStream(stream))
            {
                Logger?.LogDebug("{readerName}.CanRead(): Stream is null or invalid.", GetType().Name);
                return false;
            }
            if (HeaderInfo.Length == 0)
                return InternalCanRead(stream);
            try
            {
                _ = stream.Seek(0, SeekOrigin.Begin);
                var StartIndex = FindStartIndex(stream);
                _ = stream.Seek(StartIndex, SeekOrigin.Begin);
                var Buffer = ArrayPool<byte>.Shared.Rent(HeaderInfo.Length);
                _ = stream.Read(Buffer, 0, Buffer.Length);
                _ = stream.Seek(0, SeekOrigin.Begin);
                for (var X = 0; X < HeaderInfo.Length; ++X)
                {
                    if (Buffer[X] != HeaderInfo[X])
                    {
                        ArrayPool<byte>.Shared.Return(Buffer);
                        return false;
                    }
                }
                ArrayPool<byte>.Shared.Return(Buffer);
                return InternalCanRead(stream);
            }
            catch (Exception Ex)
            {
                Logger?.LogError(Ex, "{readerName}.CanRead(): Error occurred while checking if the reader can read the stream.", GetType().Name);
                return false;
            }
        }

        /// <summary>
        /// Used to determine if a reader can actually read the file.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns><c>true</c> if the reader can read the file; otherwise, <c>false</c>.</returns>
        public virtual bool InternalCanRead([NotNullWhen(true)] Stream? stream) => true;

        /// <summary>
        /// Reads the file asynchronously.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>
        /// A task representing the asynchronous operation that returns the generic file.
        /// </returns>
        public abstract Task<IGenericFile?> ReadAsync(Stream? stream);

        /// <summary>
        /// Validates if the provided stream is readable, has a non-zero length, and supports seeking.
        /// </summary>
        /// <param name="stream">The stream to validate.</param>
        /// <returns><c>true</c> if the stream is valid; otherwise, <c>false</c>.</returns>
        protected static bool IsValidStream([NotNullWhen(true)] Stream? stream)
        {
            if (stream?.CanRead != true)
                return false;
            if (stream.Length == 0)
                return false;
            if (!stream.CanSeek)
                return false;
            try
            {
                var TempBuffer = ArrayPool<byte>.Shared.Rent(1);
                _ = stream.Seek(0, SeekOrigin.Begin);
                _ = stream.Read(TempBuffer, 0, 1);
                _ = stream.Seek(0, SeekOrigin.Begin);
                ArrayPool<byte>.Shared.Return(TempBuffer);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Finds the start index of the file.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>The start index of the file.</returns>
        private static int FindStartIndex(Stream stream)
        {
            if (stream.Length < 3)
                return 0;
            var BOMBuffer = ArrayPool<byte>.Shared.Rent(3);
            _ = stream.Read(BOMBuffer, 0, BOMBuffer.Length);
            _ = stream.Seek(0, SeekOrigin.Begin);
            var ReturnValue = BOMBuffer[0] == 0xEF && BOMBuffer[1] == 0xBB && BOMBuffer[2] == 0xBF ? 3 : 0;
            ArrayPool<byte>.Shared.Return(BOMBuffer);
            return ReturnValue;
        }
    }
}