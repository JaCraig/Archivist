using Archivist.Interfaces;
using System;
using System.IO;
using Tesseract;

namespace Archivist.OCR.SubProcessors
{
    /// <summary>
    /// OCR sub-processor implementation.
    /// </summary>
    /// <remarks>
    /// This class uses the Tesseract OCR engine to process image files and extract text from them.
    /// It implements the <see cref="ISubProcessor"/> interface and the <see cref="IDisposable"/> interface.
    /// </remarks>
    /// <seealso cref="ISubProcessor"/>
    /// <seealso cref="IDisposable"/>
    public class OcrSubProcessor : ISubProcessor, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OcrSubProcessor"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the Tesseract OCR engine with the specified data path and language.
        /// </remarks>
        public OcrSubProcessor()
        {
            Engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
        }

        /// <summary>
        /// The Tesseract OCR engine.
        /// </summary>
        private TesseractEngine? Engine { get; set; }

        /// <summary>
        /// Disposes of the Tesseract OCR engine.
        /// </summary>
        public void Dispose()
        {
            Engine?.Dispose();
            Engine = null;
        }

        /// <summary>
        /// Processes the given file to perform OCR.
        /// </summary>
        /// <param name="file">The file to process.</param>
        /// <param name="stream">The stream to process.</param>
        /// <returns>The processed file object.</returns>
        public IGenericFile? Process(IGenericFile? file, Stream? stream)
        {
            if (file is null || Engine is null)
                return file;

            if (file is not DataTypes.Image ImageFile)
                return file;

            using var Img = Pix.LoadFromMemory(ImageFile.Data);
            using Page Page = Engine.Process(Img);
            var Content = Page.GetText();
            file.Metadata["OCRText"] = ImageFile.Description = Content;

            return file;
        }
    }
}