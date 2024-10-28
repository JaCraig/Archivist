using Archivist.Interfaces;
using System.IO;
using Tesseract;

namespace Archivist.OCR.SubProcessors
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
        public IGenericFile? Process(IGenericFile? file, Stream? stream)
        {
            if (file is null)
                return file;

            if (file is not DataTypes.Image ImageFile)
                return file;

            using var Engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            using var Img = Pix.LoadFromMemory(ImageFile.Data);
            using Page Page = Engine.Process(Img);
            var Content = Page.GetText();
            file.Metadata["OCRText"] = ImageFile.Description = Content;

            return file;
        }
    }
}