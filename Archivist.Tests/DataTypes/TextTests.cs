using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class TextTests : TestBaseClass<Text>
    {
        private readonly Text _TestClass = new();

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new Text();

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallEquals()
        {
            // Arrange
            var Other = new Text();

            // Act
            var Result = _TestClass.Equals(Other);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanSetAndGetContent()
        {
            // Arrange
            const string TestValue = "TestValue304010722";

            // Act
            _TestClass.Content = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Content);
        }

        [Fact]
        public void CompareTo_OtherTextHasDifferentContent_ReturnsNegativeValue()
        {
            // Arrange
            var Text = new Text { Content = "Sample text" };
            var OtherText = new Text { Content = "Different text" };

            // Act
            var Result = Text.CompareTo(OtherText);

            // Assert
            Assert.True(Result < 0);
        }

        [Fact]
        public void CompareTo_OtherTextHasSameContent_ReturnsZero()
        {
            // Arrange
            var Text = new Text { Content = "Sample text" };
            var OtherText = new Text { Content = "Sample text" };

            // Act
            var Result = Text.CompareTo(OtherText);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CompareTo_OtherTextIsNull_ReturnsZero()
        {
            // Arrange
            var Text = new Text();
            var OtherText = (Text?)null;

            // Act
            var Result = Text.CompareTo(OtherText);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void Content_Set_Get_ReturnsSameValue()
        {
            // Arrange
            var Text = new Text();
            const string ExpectedContent = "Sample text";

            // Act
            Text.Content = ExpectedContent;
            var ActualContent = Text.Content;

            // Assert
            Assert.Equal(ExpectedContent, ActualContent);
        }

        [Fact]
        public void Equals_OtherTextHasDifferentContent_ReturnsFalse()
        {
            // Arrange
            var Text = new Text { Content = "Sample text" };
            var OtherText = new Text { Content = "Different text" };

            // Act
            var Result = Text.Equals(OtherText);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Equals_OtherTextHasSameContent_ReturnsTrue()
        {
            // Arrange
            var Text = new Text { Content = "Sample text" };
            var OtherText = new Text { Content = "Sample text" };

            // Act
            var Result = Text.Equals(OtherText);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void Equals_OtherTextIsNull_ReturnsTrue()
        {
            // Arrange
            var Text = new Text();
            var OtherText = (Text?)null;

            // Act
            var Result = Text.Equals(OtherText);

            // Assert
            Assert.True(Result);
        }
    }
}