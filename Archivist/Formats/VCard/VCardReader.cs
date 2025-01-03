﻿using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
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
    /// <remarks>
    /// Initializes a new instance of the <see cref="VCardReader"/> class.
    /// </remarks>
    /// <param name="converter">The converter used to convert between IGenericFile objects.</param>
    /// <param name="logger">The logger.</param>
    public class VCardReader(Convertinator? converter, ILogger? logger) : ReaderBaseClass(logger)
    {
        /// <summary>
        /// Represents the separator used to split the lines of the VCard file.
        /// </summary>
        private static readonly string[] _Separator = new string[] { "\r\n", Environment.NewLine };

        /// <summary>
        /// The converter used to convert between IGenericFile objects.
        /// </summary>
        private readonly Convertinator? _Converter = converter;

        /// <summary>
        /// Gets the header information of the VCard file.
        /// </summary>
        public override byte[] HeaderInfo { get; } = "BEGIN:VCARD"u8.ToArray();

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
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the parsed
        /// VCard file.
        /// </returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            if (!IsValidStream(stream))
                return new Card(_Converter);
            var ReturnValue = new Card(_Converter);
            var Content = await GetDataAsync(stream).ConfigureAwait(false);
            var Lines = Content.Split(_Separator, StringSplitOptions.RemoveEmptyEntries);
            if (Lines.Length is 0)
                return ReturnValue;
            var CurrentField = new KeyValueField("", Array.Empty<KeyValueParameter>(), "");
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
                IEnumerable<KeyValueParameter>? Parameters = ParseParameters(LineProperty.Groups["Parameters"]?.Value);

                var Field = new KeyValueField(PropertyName, Parameters, Value);
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
                return Content?.Replace("\r\n ", "").Replace(Environment.NewLine + " ", "") ?? "";
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
        /// <returns>The parsed parameters as a collection of <see cref="KeyValueParameter"/>.</returns>
        private static IEnumerable<KeyValueParameter>? ParseParameters(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return Array.Empty<KeyValueParameter>();
            var ReturnValue = new List<KeyValueParameter>();
            foreach (Match Parameter in ParameterSplitter.Matches(value))
            {
                var Name = Parameter.Groups["Name"].Value.Trim();
                var Value = Parameter.Groups["Value"].Value.Trim();
                ReturnValue.Add(new KeyValueParameter(Name, Value));
            }
            return ReturnValue;
        }
    }
}