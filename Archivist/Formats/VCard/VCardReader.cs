using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Archivist.Formats.VCard
{
    /// <summary>
    /// Represents a reader for VCard files.
    /// </summary>
    public class VCardReader : ReaderBaseClass
    {
        /// <summary>
        /// Gets the header information of the VCard file.
        /// </summary>
        public override byte[] HeaderInfo { get; } = new byte[] { 0x42, 0x45, 0x47, 0x49, 0x4E, 0x3A, 0x56, 0x43, 0x41, 0x52, 0x44 };

        /// <summary>
        /// Gets the regular expression used to split the parameters of the VCard property.
        /// </summary>
        private static Regex ParameterSplitter { get; } = new Regex("(?<Name>[^;=]+)=(?<Value>[^;]+)", RegexOptions.Compiled);

        /// <summary>
        /// Gets the regular expression used to split the properties of the VCard file.
        /// </summary>
        private static Regex PropertySplitter { get; } = new Regex("(?<Property>[^;:]+);?(?<Parameters>[^:]+)?:(?<Value>.*)", RegexOptions.Compiled);

        /// <summary>
        /// Reads a VCard file asynchronously from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read the VCard file from.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the parsed VCard file.</returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            if (stream is null)
                return new Card();
            var ReturnValue = new Card();
            var Content = await GetDataAsync(stream).ConfigureAwait(false);
            var Lines = Content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (Lines.Length is 0)
                return ReturnValue;
            var CurrentField = new CardField("", Array.Empty<CardFieldParameter>(), "");
            foreach (var Line in Lines)
            {
                if (Line.StartsWith(' '))
                {
                    CurrentField.Value += Line.Trim();
                    continue;
                }
                Match LineProperty = PropertySplitter.Match(Line);
                if (!LineProperty.Success)
                    continue;

                var PropertyName = LineProperty.Groups["Property"].Value.ToUpperInvariant().Trim();
                if (PropertyName is "BEGIN" or "END" or "VERSION")
                    continue;
                var Value = LineProperty.Groups["Value"].Value.Trim();
                IEnumerable<CardFieldParameter>? Parameters = ParseParameters(LineProperty.Groups["Parameters"]?.Value);

                var Field = new CardField(PropertyName, Parameters, Value);
                ReturnValue.Fields.Add(Field);
                CurrentField = Field;
            }
            return ReturnValue;
        }

        /// <summary>
        /// Gets the data from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The data from the specified stream.</returns>
        private static async Task<string> GetDataAsync(Stream? stream)
        {
            try
            {
                var Content = await stream.ReadAllAsync().ConfigureAwait(false);
                return Content?.Replace("\r\n ", "") ?? "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Parses the parameters of a VCard property.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <returns>The parsed parameters as a collection of <see cref="CardFieldParameter"/>.</returns>
        private static IEnumerable<CardFieldParameter>? ParseParameters(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<CardFieldParameter>();
            var ReturnValue = new List<CardFieldParameter>();
            foreach (Match Parameter in ParameterSplitter.Matches(value))
            {
                var Name = Parameter.Groups["Name"].Value.Trim();
                var Value = Parameter.Groups["Value"].Value.Trim();
                ReturnValue.Add(new CardFieldParameter(Name, Value));
            }
            return ReturnValue;
        }
    }
}