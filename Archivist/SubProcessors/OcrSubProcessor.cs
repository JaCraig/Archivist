using Archivist.Interfaces;
using System.IO;
using Tesseract;

namespace Archivist.SubProcessors
{
    /// <summary>
    /// OCR sub-processor implementation.
    /// </summary>
    public class OcrSubProcessor : ISubProcessor
    {
        /// <summary>
        /// Processes the given file to perform OCR.
        /// </summary>
        /// <param name="file">The file to process.</param>
        /// <param name="stream">The stream to process.</param>
        /// <returns>The processed file object.</returns>
        public IGenericFile Process(IGenericFile file, Stream stream)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            if (file is not DataTypes.Image imageFile)
                return file;

            using var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            using var img = Pix.LoadFromMemory(imageFile.Data);
            using var page = engine.Process(img);
            var text = page.GetText();

            file.Metadata["OCRText"] = text;

            return file;
        }
    }
}
