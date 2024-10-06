using Archivist.BaseClasses;
using Archivist.Interfaces;
using Newtonsoft.Image;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivist.Formats.Image
{
    /// <summary>
    /// </summary>
    public class ImageWriter : WriterBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageWriter"/> class.
        /// </summary>
        public ImageWriter()
        {
        }

        /// <summary>
        /// Writes the structured object to the specified stream as Image.
        /// </summary>
        /// <param name="file">The structured object to write.</param>
        /// <param name="stream">The stream to write the Image to.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is a boolean indicating
        /// whether the write operation was successful.
        /// </returns>
        public override async Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (file is null || stream is null)
                return false;
            if (!stream.CanWrite)
                return false;
            if (file is not StructuredObject StructuredObject)
                StructuredObject = file.ToFileType<StructuredObject>()!;
            if (StructuredObject is null)
                return false;
            ExpandoObject? Content = StructuredObject.ConvertTo<ExpandoObject>();
            if (Content is null)
                return false;
            var StringContent = ImageConvert.SerializeObject(Content, Content.GetType(), Options);
            await stream.WriteAsync(StringContent.ToByteArray()).ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// </summary>
        /// <param name="file"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public override Task<bool> WriteAsync(IGenericFile? file, Stream? stream) => throw new NotImplementedException();
    }
}