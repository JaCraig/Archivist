using Archivist.DataTypes;
using Archivist.Interfaces;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class CalendarTests : TestBaseClass<Calendar>
    {
        public CalendarTests()
        {
            _TestClass = new Calendar();
            TestObject = new Calendar();
        }

        private readonly Calendar _TestClass;

        [Fact]
        public void AddAlarmPerformsMapping()
        {
            // Arrange
            var TestClass = new Calendar();
            const string Action = "TestValue884643476";
            const string Trigger = "TestValue319854872";
            const string Description = "TestValue305244354";

            // Act
            CalendarComponent Result = TestClass.AddAlarm(Action, Trigger, Description);

            // Assert
            Assert.Equal(Action, Result.Action);
            Assert.Equal(Trigger, Result.Trigger);
            Assert.Equal(Description, Result.Description);
        }

        [Fact]
        public void AddEventPerformsMapping()
        {
            // Arrange
            const string Summary = "TestValue1161632130";
            const string Description = "TestValue385974400";
            const string Location = "TestValue1208207011";
            DateTime Start = DateTime.UtcNow;
            DateTime End = DateTime.UtcNow;

            // Act
            CalendarComponent Result = _TestClass.AddEvent(Summary, Description, Location, Start, End);

            // Assert
            Assert.Equal(Summary, Result.Summary);
            Assert.Equal(Description, Result.Description);
            Assert.Equal(Location, Result.Location);
            Assert.Equal(Start, Result.StartDateUtc);
            Assert.Equal(End, Result.EndDateUtc);
        }

        [Fact]
        public void CanCallAddAlarm()
        {
            // Arrange
            const string Action = "TestValue1555939749";
            const string Trigger = "TestValue1169064201";
            const string Description = "TestValue928802609";

            // Act
            CalendarComponent Result = _TestClass.AddAlarm(Action, Trigger, Description);

            // Assert
            Assert.NotNull(Result);
            Assert.Contains(Result, _TestClass.Alarms);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanCallAddAlarmWithInvalidAction(string? value) => _TestClass.AddAlarm(value, "TestValue966658955", "TestValue1161771901");

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanCallAddAlarmWithInvalidDescription(string? value) => _TestClass.AddAlarm("TestValue1950930619", "TestValue480908067", value);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanCallAddAlarmWithInvalidTrigger(string? value) => _TestClass.AddAlarm("TestValue1144983151", value, "TestValue392274026");

        [Fact]
        public void CanCallAddEvent()
        {
            // Arrange
            const string Summary = "TestValue1917208499";
            const string Description = "TestValue395846009";
            const string Location = "TestValue1376346072";
            DateTime Start = DateTime.UtcNow;
            DateTime End = DateTime.UtcNow;

            // Act
            CalendarComponent Result = _TestClass.AddEvent(Summary, Description, Location, Start, End);

            // Assert
            Assert.NotNull(Result);
            Assert.Contains(Result, _TestClass.Events);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanCallAddEventWithInvalidDescription(string? value) => _TestClass.AddEvent("TestValue38527075", value, "TestValue604663013", DateTime.UtcNow, DateTime.UtcNow);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanCallAddEventWithInvalidLocation(string? value) => _TestClass.AddEvent("TestValue2121266683", "TestValue1794214679", value, DateTime.UtcNow, DateTime.UtcNow);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanCallAddEventWithInvalidSummary(string? value) => _TestClass.AddEvent(value, "TestValue1512912249", "TestValue520575757", DateTime.UtcNow, DateTime.UtcNow);

        [Fact]
        public void CanCallAddFreeBusy()
        {
            // Act
            CalendarComponent Result = _TestClass.AddFreeBusy();

            // Assert
            Assert.NotNull(Result);
            Assert.Contains(Result, _TestClass.FreeBusy);
        }

        [Fact]
        public void CanCallAddJournal()
        {
            // Act
            CalendarComponent Result = _TestClass.AddJournal();

            // Assert
            Assert.NotNull(Result);
            Assert.Contains(Result, _TestClass.Journals);
        }

        [Fact]
        public void CanCallAddTimeZone()
        {
            // Act
            CalendarComponent Result = _TestClass.AddTimeZone();

            // Assert
            Assert.NotNull(Result);
            Assert.Contains(Result, _TestClass.TimeZones);
        }

        [Fact]
        public void CanCallAddToDo()
        {
            // Act
            CalendarComponent Result = _TestClass.AddToDo();

            // Assert
            Assert.NotNull(Result);
            Assert.Contains(Result, _TestClass.ToDos);
        }

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new Calendar();
            var TestClass = new Calendar();

            // Act
            var Result = TestClass.CompareTo(Other);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallConvertFrom()
        {
            // Arrange
            var TestClass = new Calendar();
            var Obj = new TestClass { Summary = "TestValue" };

            // Act
            TestClass.ConvertFrom(Obj);

            // Assert
            Assert.Equal(Obj.Summary, TestClass.Events[0].Summary);
        }

        [Fact]
        public void CanCallConvertTo()
        {
            // Act
            var TestClass = new Calendar();
            _ = TestClass.AddEvent("TestValue1", "TestValue2", "TestValue3", DateTime.UtcNow, DateTime.UtcNow);
            TestClass? Result = _TestClass.ConvertTo<TestClass>();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(TestClass.Events[0].Summary, Result.Summary);
        }

        [Fact]
        public void CanCallEqualsWithCalendar()
        {
            // Arrange
            var Other = new Calendar();
            var TestClass = new Calendar();

            // Act
            var Result = TestClass.Equals(Other);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualsWithObject()
        {
            // Arrange
            var Obj = new object();

            // Act
            var Result = _TestClass.Equals(Obj);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGetContent()
        {
            // Arrange
            var TestClass = new Calendar();
            _ = TestClass.AddEvent("TestValue1", "TestValue2", "TestValue3", DateTime.UtcNow, DateTime.UtcNow);

            // Act
            var Result = TestClass.GetContent();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("SUMMARY:TestValue1\r\nDESCRIPTION:TestValue2\r\nLOCATION:TestValue3\r\nSTART:2021-09-01T00:00:00Z\r\nEND:2021-09-01T00:00:00Z\r\n", Result);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var Result = _TestClass.GetHashCode();

            // Assert
            _ = Assert.IsType<int>(Result);
            Assert.NotEqual(0, Result);
        }

        [Fact]
        public void CanCallToFileType()
        {
            // Arrange
            var TestClass = new Calendar();
            _ = TestClass.AddEvent("TestValue1", "TestValue2", "TestValue3", DateTime.UtcNow, DateTime.UtcNow);

            // Act
            Card? Result = TestClass.ToFileType<Card>();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(TestClass.Events[0].Summary, Result.Title);
            Assert.Equal(TestClass.Events[0].Description, Result["DESCRIPTION"].FirstOrDefault()?.Value);
            Assert.Equal(TestClass.Events[0].Location, Result["LOCATION"].FirstOrDefault()?.Value);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new Calendar();

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new Calendar(new Archivist.Converters.Convertinator(Array.Empty<IDataConverter>()));

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetAlarms()
        {
            // Assert
            List<CalendarComponent> Result = Assert.IsType<List<CalendarComponent>>(_TestClass.Alarms);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetComponents()
        {
            // Assert
            IEnumerable<CalendarComponent> Result = Assert.IsAssignableFrom<IEnumerable<CalendarComponent>>(_TestClass.Components);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetEvents()
        {
            // Assert
            List<CalendarComponent> Result = Assert.IsType<List<CalendarComponent>>(_TestClass.Events);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetFreeBusy()
        {
            // Assert
            List<CalendarComponent> Result = Assert.IsType<List<CalendarComponent>>(_TestClass.FreeBusy);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetIsCancelled()
        {
            // Assert
            var Result = Assert.IsType<bool>(_TestClass.IsCancelled);

            Assert.False(Result);
        }

        [Fact]
        public void CanGetJournals()
        {
            // Assert
            List<CalendarComponent> Result = Assert.IsType<List<CalendarComponent>>(_TestClass.Journals);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetTimeZones()
        {
            // Assert
            List<CalendarComponent> Result = Assert.IsType<List<CalendarComponent>>(_TestClass.TimeZones);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetToDos()
        {
            // Assert
            List<CalendarComponent> Result = Assert.IsType<List<CalendarComponent>>(_TestClass.ToDos);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanSetAndGetCurrentTimeZone()
        {
            // Arrange
            TimeZoneInfo TestValue = TimeZoneInfo.Local;

            // Act
            _TestClass.CurrentTimeZone = TestValue;

            // Assert
            Assert.Same(TestValue, _TestClass.CurrentTimeZone);
        }

        [Fact]
        public void CanSetAndGetMethod()
        {
            // Arrange
            const string TestValue = "TestValue329914848";

            // Act
            _TestClass.Method = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Method);
        }

        [Fact]
        public void CanSetAndGetProductId()
        {
            // Arrange
            const string TestValue = "TestValue1167610750";

            // Act
            _TestClass.ProductId = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.ProductId);
        }

        [Fact]
        public void CanSetAndGetVersion()
        {
            // Arrange
            const string TestValue = "TestValue551862787";

            // Act
            _TestClass.Version = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Version);
        }

        [Fact]
        public void ImplementsIComparable_Calendar()
        {
            // Arrange
            var BaseValue = new Calendar();
            _ = BaseValue.AddEvent("TestValue1", "TestValue2", "TestValue3", DateTime.UtcNow, DateTime.UtcNow);
            var EqualToBaseValue = new Calendar();
            _ = EqualToBaseValue.AddEvent("TestValue1", "TestValue2", "TestValue3", DateTime.UtcNow, DateTime.UtcNow);
            var GreaterThanBaseValue = new Calendar();
            _ = GreaterThanBaseValue.AddEvent("TestValue2", "TestValue2", "TestValue3", DateTime.UtcNow, DateTime.UtcNow);

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEquatable_Calendar()
        {
            // Arrange
            var TestClass = new Calendar();
            _ = TestClass.AddEvent("TestValue1", "TestValue2", "TestValue3", DateTime.UtcNow, DateTime.UtcNow);
            var Same = new Calendar();
            _ = Same.AddEvent("TestValue1", "TestValue2", "TestValue3", DateTime.UtcNow, DateTime.UtcNow);
            var Different = new Calendar();
            _ = Different.AddEvent("TestValue4", "TestValue5", "TestValue6", DateTime.UtcNow, DateTime.UtcNow);

            // Assert
            Assert.False(TestClass.Equals(default(object)));
            Assert.False(TestClass.Equals(new object()));
            Assert.True(TestClass.Equals((object)Same));
            Assert.False(TestClass.Equals((object)Different));
            Assert.True(TestClass.Equals(Same));
            Assert.False(TestClass.Equals(Different));
            Assert.Equal(Same.GetHashCode(), TestClass.GetHashCode());
            Assert.NotEqual(Different.GetHashCode(), TestClass.GetHashCode());
            Assert.True(TestClass == Same);
            Assert.False(TestClass == Different);
            Assert.False(TestClass != Same);
            Assert.True(TestClass != Different);
        }

        private class TestClass
        {
            public string? Summary { get; set; }
        }
    }
}