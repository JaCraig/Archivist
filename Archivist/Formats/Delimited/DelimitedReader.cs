using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Archivist.Options;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Archivist.Formats.Delimited
{
    /// <summary>
    /// Delimited file reader
    /// </summary>
    /// <seealso cref="ReaderBaseClass"/>
    public class DelimitedReader : ReaderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelimitedReader"/> class.
        /// </summary>
        public DelimitedReader(DelimitedOptions options)
        {
            string[] Delimiters = { ",", "|", "\t", "$", ";", ":" };
            foreach (var Delimiter in Delimiters)
            {
                _DelimiterSplitters.Add(Delimiter, new Regex(string.Format(CultureInfo.InvariantCulture, "(?<Value>\"(?:[^\"]|\"\")*\"|[^{0}\r\n]*?)(?<Delimiter>{0}|\r\n|\n|$)", Regex.Escape(Delimiter)), RegexOptions.Compiled));
            }
            Options = options;
        }

        /// <summary>
        /// Gets the header information.
        /// </summary>
        public override byte[] HeaderInfo { get; } = Array.Empty<byte>();

        /// <summary>
        /// Gets the delimiter splitter.
        /// </summary>
        private static Regex DelimiterSplitter { get; } = new Regex("[^\"\r\n]*(\r\n|\n|$)|(([^\"\r\n]*)(\"[^\"]*\")([^\"\r\n]*))*(\r\n|\n|$)", RegexOptions.Compiled);

        /// <summary>
        /// Gets the options.
        /// </summary>
        private DelimitedOptions Options { get; }

        /// <summary>
        /// Gets the delimiter splitters.
        /// </summary>
        private readonly Dictionary<string, Regex> _DelimiterSplitters = new();

        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The table read from the stream</returns>
        public override async Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            var ReturnValue = new Table();
            if (stream?.CanRead != true)
                return ReturnValue;
            var FileContent = await stream.ReadAllAsync().ConfigureAwait(false);
            var Delimiter = "";
            if (string.IsNullOrEmpty(FileContent))
                return ReturnValue;

            MatchCollection? Matches = DelimiterSplitter.Matches(FileContent);
            if (string.IsNullOrEmpty(Delimiter) && Matches != null)
                Delimiter = CheckDelimiters((Matches.FirstOrDefault(x => !string.IsNullOrEmpty(x.Value))?.Value) ?? ",", _DelimiterSplitters);

            if (string.IsNullOrEmpty(Delimiter))
                return ReturnValue;

            ReturnValue.Metadata["Delimiter"] = Delimiter;

            _ = _DelimiterSplitters.TryGetValue(Delimiter, out Regex? Splitter);
            if (Splitter is null)
                return ReturnValue;

            foreach (Match? TempRowData in Matches!.Where(x => !string.IsNullOrEmpty(x.Value)))
            {
                ReadRow(ReturnValue, TempRowData.Value, Splitter);
            }

            if (ReturnValue.Count > 0)
                SetupColumnHeaders(ReturnValue, Options);
            return ReturnValue;
        }

        /// <summary>
        /// Checks the delimiters.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="delimiterSplitters">The delimiter splitters.</param>
        /// <returns>The delimiter in the file</returns>
        private static string CheckDelimiters(string content, Dictionary<string, Regex> delimiterSplitters)
        {
            if (string.IsNullOrEmpty(content))
                return ",";
            var Count = ArrayPool<int>.Shared.Rent(delimiterSplitters.Count);
            var MaxIndex = 0;
            var X = 0;
            foreach (var Key in delimiterSplitters.Keys)
            {
                Regex TempSplitter = delimiterSplitters[Key];
                Count[X] = TempSplitter.Matches(content).Count;
                if (Count[MaxIndex] < Count[X])
                    MaxIndex = X;
                ++X;
            }
            var ReturnValue = Count[MaxIndex] > 1 ? delimiterSplitters.Keys.ElementAt(MaxIndex) : ",";
            ArrayPool<int>.Shared.Return(Count);
            return ReturnValue;
        }

        /// <summary>
        /// Reads the row.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="value">The value.</param>
        /// <param name="delimiterSplitter">The delimiter splitter.</param>
        /// <exception cref="ArgumentNullException">value</exception>
        private static void ReadRow(Table table, string value, Regex delimiterSplitter)
        {
            if (table is null)
                return;
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            TableRow ReturnValue = table.AddRow();

            MatchCollection Matches = delimiterSplitter.Matches(value);
            var Finished = false;
            foreach (Match TempMatch in Matches)
            {
                if (!Finished)
                {
                    ReturnValue.Add(TempMatch.Groups["Value"].Value.Replace("\"", ""));
                }
                Finished = string.IsNullOrEmpty(TempMatch.Groups["Delimiter"].Value) || TempMatch.Groups["Delimiter"].Value == "\r\n" || TempMatch.Groups["Delimiter"].Value == "\n";
            }
        }

        /// <summary>
        /// Setups the column headers.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        /// <param name="options">The options.</param>
        private static void SetupColumnHeaders(Table returnValue, DelimitedOptions options)
        {
            if (!options.FirstRowIsColumnHeaders)
                return;
            TableRow FirstRow = returnValue[0];
            _ = returnValue.Remove(FirstRow);
            foreach (TableCell Cell in FirstRow)
            {
                returnValue.Columns.Add(Cell.Content ?? "");
            }
        }
    }
}