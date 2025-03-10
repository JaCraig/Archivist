﻿using Archivist.BaseClasses;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.Formats.Txt
{
    /// <summary>
    /// Represents a text writer for the Txt format.
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="TextWriter"/> class.</remarks>
    /// <param name="logger">The logger to use for logging.</param>
    public class TextWriter(ILogger? logger) : WriterBaseClass(logger)
    {
        /// <summary>
        /// Writes the content of the specified file to the provided stream asynchronously.
        /// </summary>
        /// <param name="file">The file to be written.</param>
        /// <param name="stream">The stream to write the file content to.</param>
        /// <returns>True if the file was written successfully; otherwise, false.</returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (!IsValidStream(stream))
            {
                Logger?.LogError("TextWriter.WriteAsync(): Stream is null or invalid.");
                return false;
            }
            var TempData = Encoding.UTF8.GetBytes(file?.GetContent() ?? "");
            try
            {
                await stream.WriteAsync(TempData).ConfigureAwait(false);
            }
            catch (Exception E)
            {
                Logger?.LogError(E, "TextWriter.WriteAsync(): An error occurred while writing to the stream.");
                return false;
            }
            return true;
        }
    }
}