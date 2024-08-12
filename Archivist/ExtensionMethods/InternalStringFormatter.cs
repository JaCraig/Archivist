using System;
using System.Text;

namespace Archivist.ExtensionMethods
{
    /// <summary>
    /// Generic string formatter
    /// </summary>
    public static class InternalStringFormatter
    {
        /// <summary>
        /// Represents alpha characters (defaults to @)
        /// </summary>
        private const char _AlphaChar = '@';

        /// <summary>
        /// Represents digits (defaults to #)
        /// </summary>
        private const char _DigitChar = '#';

        /// <summary>
        /// Represents the escape character (defaults to \)
        /// </summary>
        private const char _EscapeChar = '\\';

        /// <summary>
        /// Formats the string based on the pattern
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="formatPattern">Format pattern</param>
        /// <returns>The formatted string</returns>
        public static string Format(string? input, string? formatPattern)
        {
            if (string.IsNullOrEmpty(formatPattern) || !IsValid(formatPattern))
            {
                throw new ArgumentException("Format pattern is not valid.", nameof(formatPattern));
            }

            var ReturnValue = new StringBuilder();
            for (var X = 0; X < formatPattern.Length; ++X)
            {
                if (formatPattern[X] == _EscapeChar)
                {
                    ++X;
                    _ = ReturnValue.Append(formatPattern[X]);
                }
                else
                {
                    input = GetMatchingInput(input, formatPattern[X], out var NextValue);
                    if (NextValue != char.MinValue)
                    {
                        _ = ReturnValue.Append(NextValue);
                    }
                }
            }
            return ReturnValue.ToString();
        }

        /// <summary>
        /// Gets matching input
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="formatChar">Current format character</param>
        /// <param name="matchChar">The matching character found</param>
        /// <returns>The remainder of the input string left</returns>
        private static string? GetMatchingInput(string? input, char formatChar, out char matchChar)
        {
            var Digit = formatChar == _DigitChar;
            var Alpha = formatChar == _AlphaChar;
            if (!Digit && !Alpha)
            {
                matchChar = formatChar;
                return input;
            }
            var Index = 0;
            matchChar = char.MinValue;
            for (var X = 0; X < input?.Length; ++X)
            {
                if ((Digit && char.IsDigit(input[X])) || (Alpha && char.IsLetter(input[X])))
                {
                    matchChar = input[X];
                    Index = X + 1;
                    break;
                }
            }
            return input?[Index..];
        }

        /// <summary>
        /// Checks if the format pattern is valid
        /// </summary>
        /// <param name="formatPattern">Format pattern</param>
        /// <returns>Returns true if it's valid, otherwise false</returns>
        private static bool IsValid(string? formatPattern)
        {
            if (string.IsNullOrEmpty(formatPattern))
                return false;

            var EscapeCharFound = false;
            for (var X = 0; X < formatPattern.Length; ++X)
            {
                if (EscapeCharFound && formatPattern[X] != _DigitChar
                        && formatPattern[X] != _AlphaChar
                        && formatPattern[X] != _EscapeChar)
                {
                    return false;
                }

                if (EscapeCharFound)
                {
                    EscapeCharFound = false;
                }
                else
                {
                    EscapeCharFound |= formatPattern[X] == _EscapeChar;
                }
            }
            return !EscapeCharFound;
        }
    }
}