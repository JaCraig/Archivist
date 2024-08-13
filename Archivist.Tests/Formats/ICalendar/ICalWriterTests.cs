using Archivist.DataTypes;
using Archivist.Formats.ICalendar;
using Archivist.Tests.BaseClasses;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.ICalendar
{
    public class ICalWriterTests : TestBaseClass<ICalWriter>
    {
        public ICalWriterTests()
        {
            _TestClass = new ICalWriter();
            TestObject = new ICalWriter();
        }

        private readonly ICalWriter _TestClass;

        [Fact]
        public async Task CanCallWriteAsync()
        {
            // Arrange
            var File = new Calendar();
            File.Events.Add(new CalendarComponent(File)
            {
                StartDate = new System.DateTime(2024, 8, 12, 0, 11, 23),
                EndDate = new System.DateTime(2024, 8, 12, 2, 11, 23),
                DateStamp = new System.DateTime(2024, 8, 12, 0, 11, 23),
                Created = new System.DateTime(2024, 8, 12, 0, 11, 23),
                LastModified = new System.DateTime(2024, 8, 12, 0, 14, 15)
            });
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);
            Stream.Position = 0;
            var Reader = new StreamReader(Stream);
            var Text = Reader.ReadToEnd();

            // Assert
            Assert.True(Result);
            Assert.NotEmpty(Text);
            Assert.Equal("BEGIN:VCALENDAR\r\nMETHOD:REQUEST\r\nPRODID:-//Archivist//EN\r\nVERSION:2.0\r\nBEGIN:VEVENT\r\nCLASS:PUBLIC\r\nDTSTAMP:20240812T041100Z\r\nCREATED:20240812T041100Z\r\nDTSTART:20240812T001100Z\r\nDTEND:20240812T061100Z\r\nLOCATION:\r\nUID:20240812T001100Z20240812T061100Z\r\nSEQUENCE:1\r\nPRIORITY:0\r\nX-MICROSOFT-CDO-BUSYSTATUS:BUSY\r\nX-MICROSOFT-CDO-INSTTYPE:0\r\nX-MICROSOFT-CDO-INTENDEDSTATUS:BUSY\r\nX-MICROSOFT-CDO-ALLDAYEVENT:FALSE\r\nX-MICROSOFT-CDO-IMPORTANCE:1\r\nX-MICROSOFT-CDO-OWNERAPPTID:-1\r\nX-MICROSOFT-CDO-ATTENDEE-CRITICAL-CHANGE:20240812T041400Z\r\nX-MICROSOFT-CDO-OWNER-CRITICAL-CHANGE:20240812T041400Z\r\nLAST-MODIFIED:20240812T041400Z\r\nSTATUS:CONFIRMED\r\nTRANSP:OPAQUE\r\nEND:VEVENT\r\nBEGIN:VALARM\r\nTRIGGER:-PT15M\r\nACTION:DISPLAY\r\nDESCRIPTION:Reminder\r\nEND:VALARM\r\nEND:VCALENDAR\r\n", Text);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithCustomFieldsAsync()
        {
            // Arrange
            var File = new Calendar();
            File.Events.Add(new CalendarComponent(File)
            {
                StartDate = new System.DateTime(2024, 8, 12, 0, 11, 23),
                EndDate = new System.DateTime(2024, 8, 12, 2, 11, 23),
                DateStamp = new System.DateTime(2024, 8, 12, 0, 11, 23),
                Created = new System.DateTime(2024, 8, 12, 0, 11, 23),
                LastModified = new System.DateTime(2024, 8, 12, 0, 14, 15)
            });
            File.Events[0].Fields.Add(new KeyValueField("X-TEST", Array.Empty<KeyValueParameter>(), "Test"));

            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(File, Stream);
            Stream.Position = 0;
            var Reader = new StreamReader(Stream);
            var Text = Reader.ReadToEnd();

            // Assert
            Assert.True(Result);
            Assert.NotEmpty(Text);
            Assert.Equal("BEGIN:VCALENDAR\r\nMETHOD:REQUEST\r\nPRODID:-//Archivist//EN\r\nVERSION:2.0\r\nBEGIN:VEVENT\r\nCLASS:PUBLIC\r\nDTSTAMP:20240812T041100Z\r\nCREATED:20240812T041100Z\r\nDTSTART:20240812T001100Z\r\nDTEND:20240812T061100Z\r\nLOCATION:\r\nUID:20240812T001100Z20240812T061100Z\r\nSEQUENCE:1\r\nPRIORITY:0\r\nX-MICROSOFT-CDO-BUSYSTATUS:BUSY\r\nX-MICROSOFT-CDO-INSTTYPE:0\r\nX-MICROSOFT-CDO-INTENDEDSTATUS:BUSY\r\nX-MICROSOFT-CDO-ALLDAYEVENT:FALSE\r\nX-MICROSOFT-CDO-IMPORTANCE:1\r\nX-MICROSOFT-CDO-OWNERAPPTID:-1\r\nX-MICROSOFT-CDO-ATTENDEE-CRITICAL-CHANGE:20240812T041400Z\r\nX-MICROSOFT-CDO-OWNER-CRITICAL-CHANGE:20240812T041400Z\r\nLAST-MODIFIED:20240812T041400Z\r\nSTATUS:CONFIRMED\r\nTRANSP:OPAQUE\r\nX-TEST:Test\r\nEND:VEVENT\r\nBEGIN:VALARM\r\nTRIGGER:-PT15M\r\nACTION:DISPLAY\r\nDESCRIPTION:Reminder\r\nEND:VALARM\r\nEND:VCALENDAR\r\n", Text);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithNullFileAsync()
        {
            // Arrange
            var Stream = new MemoryStream();

            // Act
            var Result = await _TestClass.WriteAsync(default, Stream);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithNullStreamAsync()
        {
            // Arrange
            var File = new Calendar();

            // Act
            var Result = await _TestClass.WriteAsync(File, default);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public async Task CanCallWriteAsyncWithNullValuesAsync()
        {
            // Act
            var Result = await _TestClass.WriteAsync(default, default);

            // Assert
            Assert.False(Result);
        }
    }
}