using Archivist.DataTypes;
using Archivist.ExtensionMethods;
using Archivist.Interfaces;
using Archivist.OCR.SubProcessors;
using Archivist.OCR.Tests.BaseClasses;
using NSubstitute;

namespace Archivist.OCR.Tests.SubProcessors
{
    public class OcrSubProcessorTests : TestBaseClass<OcrSubProcessor>
    {
        public OcrSubProcessorTests()
        {
            _TestClass = new OcrSubProcessor();
            TestObject = new OcrSubProcessor();
        }

        private readonly OcrSubProcessor _TestClass;

        [Fact]
        public void CanCallProcess()
        {
            // Arrange
            using var FileData = new FileStream("./TestData/TestJPG.jpg", FileMode.Open, FileAccess.Read);
            IGenericFile File = new Image
            {
                Data = FileData.ReadAllBinary(),
                Description = "Test Description"
            };
            using var Stream = new MemoryStream();

            // Act
            var Results = _TestClass.Process(File, Stream) as Image;

            // Assert
            Assert.NotNull(Results);
            Assert.Same(File, Results);
            Assert.NotNull(Results.Metadata["OCRText"]);
            Assert.Equal(Results.Description, Results.Metadata["OCRText"]);
        }

        [Fact]
        public void CanCallProcessWithNullFile()
        {
            //Act
            IGenericFile? Result = _TestClass.Process(default, new MemoryStream());

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void CanCallProcessWithNullStream()
        {
            // Arrange
            IGenericFile File = Substitute.For<IGenericFile>();

            // Act
            IGenericFile? Result = _TestClass.Process(File, default);

            // Assert
            Assert.NotNull(Result);
            Assert.Same(File, Result);
        }
    }
}