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
            _TestClass = new ICalReader(null);
            TestObject = new ICalReader(null);
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
            Calendar CalResult = Assert.IsAssignableFrom<Calendar>(Result);

            CalendarComponent ResultEvent = Assert.Single(CalResult.Events);
            Assert.NotNull(ResultEvent);
            Assert.Equal(new DateTime(1998, 3, 9, 23, 10, 0, DateTimeKind.Utc), ResultEvent.DateStampUtc);
            Assert.Equal("guid-1.example.com", ResultEvent.UID);
            Assert.Equal("mailto:mrbig@example.com", ResultEvent.Organizers.FirstOrDefault()?.Value);
            Assert.Equal("mailto:employee-A@example.com", ResultEvent.Attendees.FirstOrDefault()?.Value);
            Assert.Equal(new DateTime(1998, 3, 12, 8, 30, 0, DateTimeKind.Utc), ResultEvent.StartDateUtc);
            Assert.Equal(new DateTime(1998, 3, 12, 9, 30, 0, DateTimeKind.Utc), ResultEvent.EndDateUtc);
            Assert.Equal(new DateTime(1998, 3, 9, 13, 0, 0, DateTimeKind.Utc), ResultEvent.CreatedUtc);
            Assert.Equal("PUBLIC", ResultEvent.Class);
            Assert.Equal("MEETING", ResultEvent.Categories.FirstOrDefault()?.Value);
            Assert.Equal("XYZ Project Review", ResultEvent.Summary);
            Assert.Equal("Project XYZ Review Meeting", ResultEvent.Description);
            Assert.Equal("1CP Conference Room 4350", ResultEvent.Location);

            CalendarComponent ResultAlarm = Assert.Single(CalResult.Alarms);
            Assert.NotNull(ResultAlarm);
            Assert.Equal("-P2D", ResultAlarm.Trigger);
            Assert.Equal("DISPLAY", ResultAlarm.Action);
            Assert.Equal("Project XYZ Review Meeting", ResultAlarm.Description);
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
            Calendar CalResult = Assert.IsAssignableFrom<Calendar>(Result);
            CalendarComponent ResultEvent = Assert.Single(CalResult.Events);

            Assert.Equal("-//xyz Corp//NONSGML PDA Calendar Version 1.0//EN", CalResult.ProductId);
            Assert.Equal(new DateTime(1996, 7, 4, 12, 0, 0), ResultEvent.DateStampUtc);
            Assert.Equal("uid1@example.com", ResultEvent.UID);
            Assert.Equal("mailto:jsmith@example.com", ResultEvent.Organizers.FirstOrDefault()?.Value);
            Assert.Equal(new DateTime(1996, 9, 18, 14, 30, 0, DateTimeKind.Utc), ResultEvent.StartDateUtc);
            Assert.Equal(new DateTime(1996, 9, 20, 22, 0, 0, DateTimeKind.Utc), ResultEvent.EndDateUtc);
            Assert.Equal("CONFIRMED", ResultEvent.Status);
            Assert.Equal("CONFERENCE", ResultEvent.Categories.FirstOrDefault()?.Value);
            Assert.Equal("Networld+Interop Conference", ResultEvent.Summary);
            Assert.Equal("Networld+Interop Conference and Exhibit\nAtlanta World Congress Center\n Atlanta, Georgia", ResultEvent.Description);
        }
    }
}