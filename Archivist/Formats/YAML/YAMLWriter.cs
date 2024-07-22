using Archivist.BaseClasses;
using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Archivist.Formats.YAML
{
    /// <summary>
    /// Represents a YAML writer for serializing structured objects.
    /// </summary>
    public class YAMLWriter : WriterBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YAMLWriter"/> class.
        /// </summary>
        public YAMLWriter()
        {
            Serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
        }

        private ISerializer Serializer { get; }

        /// <summary>
        /// Writes the structured object to the specified stream as YAML.
        /// </summary>
        /// <param name="file">The structured object to write.</param>
        /// <param name="stream">The stream to write the YAML to.</param>
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
            var StringContent = Serializer.Serialize(Content, Content.GetType());
            await stream.WriteAsync(StringContent.ToByteArray()).ConfigureAwait(false);
            return true;
        }
    }
}