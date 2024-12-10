using Archivist.BaseClasses;
using Archivist.Interfaces;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using System.IO;
using System.Threading.Tasks;

namespace Archivist.Formats.Image
{
    /// <summary>
    /// Represents a writer for Image files.
    /// </summary>
    /// <seealso cref="WriterBaseClass"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ImageWriter"/> class.
    /// </remarks>
    /// <param name="logger">The logger to use for logging.</param>
    public class ImageWriter(ILogger? logger) : WriterBaseClass(logger)
    {
        /// <summary>
        /// Writes the structured object to the specified stream as Image.
        /// </summary>
        /// <param name="file">The structured object to write.</param>
        /// <param name="stream">The stream to write the Image to.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is a boolean indicating
        /// whether the write operation was successful.
        /// </returns>
        public override Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (file is null || !IsValidStream(stream))
            {
                Logger?.LogDebug("{writerName}.WriteAsync(): File or stream is null.", nameof(ImageWriter));
                return Task.FromResult(false);
            }
            if (file is not DataTypes.Image Image)
                Image = file.ToFileType<DataTypes.Image>()!;
            if (Image is null)
            {
                Logger?.LogDebug("{writerName}.WriteAsync(): File is not an Image.", nameof(ImageWriter));
                return Task.FromResult(false);
            }
            SKColorType ColorType = Image.BytesPerPixel switch
            {
                1 => SKColorType.Gray8,
                3 => SKColorType.Rgb888x,
                4 => SKColorType.Rgba8888,
                _ => SKColorType.Unknown
            };
            SKAlphaType AlphaType = Image.BytesPerPixel switch
            {
                1 => SKAlphaType.Opaque,
                3 => SKAlphaType.Opaque,
                4 => SKAlphaType.Premul,
                _ => SKAlphaType.Unknown
            };
            var ColorSpace = SKColorSpace.CreateSrgb();

            var FinalImageInfo = new SKImageInfo(Image.Width, Image.Height, ColorType, AlphaType, ColorSpace);
            var ImageType = Image.ImageType;
            SKEncodedImageFormat EncodingUsing = ImageType switch
            {
                "jpg" => SKEncodedImageFormat.Jpeg,
                "png" => SKEncodedImageFormat.Png,
                "webp" => SKEncodedImageFormat.Webp,
                "gif" => SKEncodedImageFormat.Gif,
                "bmp" => SKEncodedImageFormat.Bmp,
                "ico" => SKEncodedImageFormat.Ico,
                "wbmp" => SKEncodedImageFormat.Wbmp,
                "heif" => SKEncodedImageFormat.Heif,
                _ => SKEncodedImageFormat.Png
            };
            using var FinalImage = SKImage.Create(FinalImageInfo);
            using var FinalBitmap = SKBitmap.FromImage(FinalImage);
            for (var Y = 0; Y < Image.Height; ++Y)
            {
                var RowStart = Y * Image.Width;
                for (var X = 0; X < Image.Width; ++X)
                {
                    var PixelStart = X * Image.BytesPerPixel;
                    var Pixel = new SKColor();
                    switch (Image.BytesPerPixel)
                    {
                        case 1:
                            Pixel = new SKColor(Image.Data[PixelStart + RowStart], Image.Data[PixelStart + RowStart], Image.Data[PixelStart + RowStart]);
                            break;

                        case 3:
                            Pixel = new SKColor(Image.Data[PixelStart + RowStart + 2], Image.Data[PixelStart + RowStart + 1], Image.Data[PixelStart + RowStart]);
                            break;

                        case 4:
                            Pixel = new SKColor(Image.Data[PixelStart + RowStart + 3], Image.Data[PixelStart + RowStart + 2], Image.Data[PixelStart + RowStart + 1], Image.Data[PixelStart + RowStart]);
                            break;
                    }
                    FinalBitmap.SetPixel(X, Y, Pixel);
                }
            }
            using SKData EncodedImage = FinalImage.Encode(EncodingUsing, 80);
            EncodedImage.SaveTo(stream);
            return Task.FromResult(true);
        }
    }
}