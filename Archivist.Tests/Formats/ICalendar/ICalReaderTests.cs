using Archivist.DataTypes;
using Archivist.Formats.ICalendar;
using Archivist.Tests.BaseClasses;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Archivist.Tests.Formats.ICalendar
{
    public class ICalReaderTests : TestBaseClass<ICalReader>
    {
        public ICalReaderTests()
        {
            _TestClass = new ICalReader();
            TestObject = new ICalReader();
        }

        private readonly ICalReader _TestClass;

        [Fact]
        public async Task CanCallReadAsync()
        {
            // Arrange
            var Stream = new MemoryStream();

            // Act
            Interfaces.IGenericFile? Result = await _TestClass.ReadAsync(Stream);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetHeaderInfo()
        {
            // Assert
            var Result = Assert.IsType<byte[]>(_TestClass.HeaderInfo);

            Assert.Equal(new byte[] { 0x42, 0x45, 0x47, 0x49, 0x4E, 0x3A, 0x56, 0x43, 0x41, 0x4C, 0x45, 0x4E, 0x44, 0x41, 0x52 }, Result);
        }

        [Fact]
        public async Task ReadFromICalFileAsync()
        {
            // Arrange
            FileStream TestFile = File.OpenRead("./TestData/TestICal.ics");

            // Act
            Interfaces.IGenericFile? Result = await _TestClass.ReadAsync(TestFile);

            // Assert
            Assert.NotNull(Result);

            /*BEGIN:VCALENDAR
PRODID:-//RDU Software//NONSGML HandCal//EN
VERSION:2.0
BEGIN:VTIMEZONE
TZID:America/New_York
BEGIN:STANDARD
DTSTART:19981025T020000
TZOFFSETFROM:-0400
TZOFFSETTO:-0500
TZNAME:EST
END:STANDARD
BEGIN:DAYLIGHT
DTSTART:19990404T020000
TZOFFSETFROM:-0500
TZOFFSETTO:-0400
TZNAME:EDT
END:DAYLIGHT
END:VTIMEZONE
BEGIN:VEVENT
DTSTAMP:19980309T231000Z
UID:guid-1.example.com
ORGANIZER:mailto:mrbig@example.com
ATTENDEE;RSVP=TRUE;ROLE=REQ-PARTICIPANT;CUTYPE=GROUP:mailto:employee-A@example.com
DESCRIPTION:Project XYZ Review Meeting
CATEGORIES:MEETING
CLASS:PUBLIC
CREATED:19980309T130000Z
SUMMARY:XYZ Project Review
DTSTART;TZID=America/New_York:19980312T083000
DTEND;TZID=America/New_York:19980312T093000
LOCATION:1CP Conference Room 4350
END:VEVENT
BEGIN: VALARM
TRIGGER:-P2D
ACTION: DISPLAY
DESCRIPTION:Project XYZ Review Meeting
END:VALARM
END:VCALENDAR*/
        }

        [Fact]
        public async Task ReadFromVCalFileAsync()
        {
            // Arrange
            FileStream TestFile = File.OpenRead("./TestData/TestVCal.vcs");

            // Act
            Interfaces.IGenericFile? Result = await _TestClass.ReadAsync(TestFile);

            // Assert
            Assert.NotNull(Result);
            CalendarComponent CalResult = Assert.IsAssignableFrom<CalendarComponent>(Result);

            Assert.Equal("-//xyz Corp//NONSGML PDA Calendar Version 1.0//EN", CalResult.ProductId);
            Assert.Equal(new DateTime(1996, 7, 4, 12, 0, 0), CalResult.DateStampUtc);
            Assert.Equal("uid1@example.com", CalResult.UID);
            Assert.Equal("mailto:jsmith@example.com", CalResult.Organizers.FirstOrDefault()?.Value);
            Assert.Equal(new DateTime(1996, 9, 18, 14, 30, 0, DateTimeKind.Utc), CalResult.StartDateUtc);
            Assert.Equal(new DateTime(1996, 9, 20, 22, 0, 0, DateTimeKind.Utc), CalResult.EndDateUtc);
            Assert.Equal("CONFIRMED", CalResult.Status);
            Assert.Equal("CONFERENCE", CalResult.Categories.FirstOrDefault()?.Value);
            Assert.Equal("Networld+Interop Conference", CalResult.Summary);
            Assert.Equal("Networld+Interop Conference and Exhibit\nAtlanta World Congress Center\n Atlanta, Georgia", CalResult.Description);
        }
    }
}