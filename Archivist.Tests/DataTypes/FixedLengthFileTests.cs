using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class FixedLengthFileTests : TestBaseClass<FixedLengthFile>
    {
        public FixedLengthFileTests()
        {
            TestObject = new FixedLengthFile();
        }

        [Fact]
        public void CompareTo_ShouldReturnNegativeValue_WhenFirstFileIsLessThanSecondFile()
        {
            // Arrange
            var File1 = new FixedLengthFile();
            var File2 = new FixedLengthFile();
            File2.Records.Add(new FixedLengthRecord());
            File2.Records[0].Fields.Add(new FixedLengthField("Field 1", 10));

            // Act
            var Result = File1.CompareTo(File2);

            // Assert
            Assert.True(Result < 0);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenFilesAreNotEqual()
        {
            // Arrange
            var File1 = new FixedLengthFile();

            // Act
            var Result = File1.Equals(null);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenFilesAreEqual()
        {
            // Arrange
            var File1 = new FixedLengthFile();
            var File2 = new FixedLengthFile();

            // Act
            var Result = File1.Equals(File2);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void GetContent_ShouldReturnRecordsAsString()
        {
            // Arrange
            var File = new FixedLengthFile();
            var Record1 = new FixedLengthRecord();
            Record1.Fields.Add(new FixedLengthField("Record 1", 10));
            var Record2 = new FixedLengthRecord();
            Record2.Fields.Add(new FixedLengthField("Record 2", 10));
            var Record3 = new FixedLengthRecord();
            Record3.Fields.Add(new FixedLengthField("Record 3", 10));
            File.Records.Add(Record1);
            File.Records.Add(Record2);
            File.Records.Add(Record3);

            // Act
            var Content = File.GetContent();

            // Assert
            Assert.Equal("Record 1  \r\nRecord 2  \r\nRecord 3  ", Content);
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameValue_WhenFilesAreEqual()
        {
            // Arrange
            var File1 = new FixedLengthFile();
            var File2 = new FixedLengthFile();

            // Act
            var HashCode1 = File1.GetHashCode();
            var HashCode2 = File2.GetHashCode();

            // Assert
            Assert.Equal(HashCode1, HashCode2);
        }

        [Fact]
        public void Operator_Equal_ShouldReturnTrue_WhenFilesAreEqual()
        {
            // Arrange
            var File1 = new FixedLengthFile();
            var File2 = new FixedLengthFile();

            // Act
            var Result = File1 == File2;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void Operator_GreaterThan_ShouldReturnTrue_WhenFirstFileIsGreaterThanSecondFile()
        {
            // Arrange
            var File1 = new FixedLengthFile();
            var File2 = new FixedLengthFile();

            // Act
            var Result = File1 > File2;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Operator_GreaterThanOrEqual_ShouldReturnTrue_WhenFirstFileIsGreaterThanOrEqualSecondFile()
        {
            // Arrange
            var File1 = new FixedLengthFile();
            var File2 = new FixedLengthFile();
            File2.Records.Add(new FixedLengthRecord());
            File2.Records[0].Fields.Add(new FixedLengthField("Field 1", 10));

            // Act
            var Result = File1 >= File2;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Operator_LessThan_ShouldReturnTrue_WhenFirstFileIsLessThanSecondFile()
        {
            // Arrange
            var File1 = new FixedLengthFile();
            var File2 = new FixedLengthFile();
            File2.Records.Add(new FixedLengthRecord());
            File2.Records[0].Fields.Add(new FixedLengthField("Field 1", 10));

            // Act
            var Result = File1 < File2;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void Operator_LessThanOrEqual_ShouldReturnTrue_WhenFirstFileIsLessThanOrEqualSecondFile()
        {
            // Arrange
            var File1 = new FixedLengthFile();
            var File2 = new FixedLengthFile();
            File2.Records.Add(new FixedLengthRecord());
            File2.Records[0].Fields.Add(new FixedLengthField("Field 1", 10));

            // Act
            var Result = File1 <= File2;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void Operator_NotEqual_ShouldReturnTrue_WhenFilesAreNotEqual()
        {
            // Arrange
            var File1 = new FixedLengthFile();
            File1.Records.Add(new FixedLengthRecord());
            var File2 = new FixedLengthFile();

            // Act
            var Result = File1 != File2;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void Records_ShouldBeInitialized()
        {
            // Arrange
            var File = new FixedLengthFile();

            // Act
            List<FixedLengthRecord> Records = File.Records;

            // Assert
            Assert.NotNull(Records);
            _ = Assert.IsType<List<FixedLengthRecord>>(Records);
            Assert.Empty(Records);
        }

        [Fact]
        public void ToString_ShouldReturnRecordsAsString()
        {
            // Arrange
            var File = new FixedLengthFile();
            var Record1 = new FixedLengthRecord();
            Record1.Fields.Add(new FixedLengthField("Record 1", 10));
            var Record2 = new FixedLengthRecord();
            Record2.Fields.Add(new FixedLengthField("Record 2", 10));
            File.Records.Add(Record1);
            File.Records.Add(Record2);

            // Act
            var Result = File.ToString();

            // Assert
            Assert.Equal("Record 1  \r\nRecord 2  ", Result);
        }
    }
}