using Archivist.Options;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.Options
{
    public class ExcelOptionsTests : TestBaseClass<ExcelOptions>
    {
        public ExcelOptionsTests()
        {
            _TestClass = new ExcelOptions();
            TestObject = new ExcelOptions();
        }

        private readonly ExcelOptions _TestClass;

        [Fact]
        public void CanGetDefault()
        {
            // Assert
            ExcelOptions Result = Assert.IsType<ExcelOptions>(ExcelOptions.Default);

            Assert.NotNull(Result);
            Assert.True(Result.FirstRowIsColumnHeaders);
        }

        [Fact]
        public void CanSetAndGetFirstRowIsColumnHeaders()
        {
            // Arrange
            const bool TestValue = false;

            // Act
            _TestClass.FirstRowIsColumnHeaders = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.FirstRowIsColumnHeaders);
        }
    }
}