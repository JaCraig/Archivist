using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.ExtensionMethods
{
    /// <summary>
    /// Internal extension methods.
    /// </summary>
    public static class InternalExtensionMethods
    {
        /// <summary>
        /// Adds spaces before each capital letter in the input string.
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>String with spaces before each capital letter</returns>
        public static string AddSpaces(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }

            StringBuilder NewText = new StringBuilder(input.Length * 2).Append(input[0]);
            for (int X = 1, InputLength = input.Length; X < InputLength; ++X)
            {
                var CurrentChar = input[X];
                var PreviousChar = input[X - 1];
                if (char.IsUpper(CurrentChar) && !(char.IsWhiteSpace(PreviousChar) || char.IsUpper(PreviousChar)))
                {
                    _ = NewText.Append(' ');
                }
                _ = NewText.Append(CurrentChar);
            }
            return NewText.ToString();
        }

        /// <summary>
        /// Gets the first x number of characters from the left hand side
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="length">x number of characters to return</param>
        /// <returns>The resulting string</returns>
        public static string Left(this string? input, int length)
        {
            return length <= 0
                ? ""
                : string.IsNullOrEmpty(input) ? "" : input[..(input.Length > length ? length : input.Length)];
        }

        /// <summary>
        /// Takes all of the data in the stream and returns it as a string
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encodingUsing">Encoding that the string should be in (defaults to UTF8)</param>
        /// <returns>A string containing the content of the stream</returns>
        public static async Task<string> ReadAllAsync(this Stream input, Encoding? encodingUsing = null)
        {
            if (input is null)
                return "";
            return (await input.ReadAllBinaryAsync().ConfigureAwait(false)).ToString(encodingUsing) ?? "";
        }

        /// <summary>
        /// Takes all of the data in the stream and returns it as an array of bytes
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <returns>A byte array</returns>
        public static async Task<byte[]> ReadAllBinaryAsync(this Stream input)
        {
            if (input is null)
                return Array.Empty<byte>();

            if (input is MemoryStream TempInput)
                return TempInput.ToArray();

            ArrayPool<byte> Pool = ArrayPool<byte>.Shared;
            var Buffer = Pool.Rent(4096);
            using var Temp = new MemoryStream();
            while (true)
            {
                var Count = await input.ReadAsync(Buffer.AsMemory(0, Buffer.Length)).ConfigureAwait(false);
                if (Count <= 0)
                {
                    Pool.Return(Buffer);
                    return Temp.ToArray();
                }
                Temp.Write(Buffer, 0, Count);
            }
        }

        /// <summary>
        /// Converts a byte array to a string
        /// </summary>
        /// <param name="input">Input byte array</param>
        /// <param name="encodingUsing">Encoding that the string should be in (defaults to UTF8)</param>
        /// <param name="index">Index to start at</param>
        /// <param name="count">Number of bytes to convert</param>
        /// <returns>A string containing the content of the byte array</returns>
        public static string ToString(this byte[] input, Encoding? encodingUsing, int index = 0, int count = -1)
        {
            if (input is null)
                return "";

            if (count == -1)
                count = input.Length - index;

            encodingUsing ??= Encoding.UTF8;
            return encodingUsing.GetString(input, index, count);
        }
    }
}