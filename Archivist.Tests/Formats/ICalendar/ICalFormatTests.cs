using Archivist.Formats.ICalendar;
using Archivist.Tests.BaseClasses;
using Xunit;

namespace Archivist.Tests.Formats.ICalendar
{
    public class ICalFormatTests : TestBaseClass<ICalFormat>
    {
        public ICalFormatTests()
        {
            _TestClass = new ICalFormat();
            TestObject = new ICalFormat();
        }

        private readonly ICalFormat _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new ICalFormat();

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetExtensions()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.Extensions);

            Assert.NotNull(Result);
            Assert.Equal(new[] { "VCS", "ICAL", "ICS", "IFB", "ICALENDAR" }, Result);
        }

        [Fact]
        public void CanGetMimeTypes()
        {
            // Assert
            var Result = Assert.IsType<string[]>(_TestClass.MimeTypes);

            Assert.NotNull(Result);
            Assert.Equal(new[] { "APPLICATION/HBS-VCS", "TEXT/X-VCALENDAR", "TEXT/CALENDAR" }, Result);
        }
    }
}