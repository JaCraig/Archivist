using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class CalendarComponentTests : TestBaseClass<CalendarComponent>
    {
        public CalendarComponentTests()
        {
            _Parent = new Calendar();
            _TestClass = new CalendarComponent(_Parent);
            TestObject = new CalendarComponent(new Calendar());
        }

        private readonly Calendar _Parent;
        private readonly CalendarComponent _TestClass;

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new CalendarComponent(_Parent);

            // Act
            var Result = _TestClass.CompareTo(Other);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallConvertFrom()
        {
            // Arrange
            var Obj = new TestClass { Summary = "Example summary/subject" };
            var TestClass = new CalendarComponent(_Parent);

            // Act
            TestClass.ConvertFrom(Obj);

            // Assert
            Assert.Equal("Example summary/subject", TestClass.Summary);
        }

        [Fact]
        public void CanCallConvertTo()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent)
            {
                Summary = "Example summary/subject"
            };
            // Act
            TestClass? Result = TestClass.ConvertTo<TestClass>();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Example summary/subject", Result.Summary);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new CalendarComponent(_Parent);
            var Right = new CalendarComponent(_Parent);

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new CalendarComponent(_Parent);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarComponent(_Parent) == default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualsWithCalendar()
        {
            // Arrange
            var Other = new CalendarComponent(_Parent)
            {
                Class = "TestValue1706702071"
            };

            // Act
            var Result = _TestClass.Equals(Other);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithCalendarComponent()
        {
            // Arrange
            var Other = new CalendarComponent(new Calendar());
            var TestClass = new CalendarComponent(new Calendar());

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
            var TestClass = new CalendarComponent(_Parent)
            {
                Summary = "TestValue1453032876"
            };
            // Act
            var Result = TestClass.GetContent();

            // Assert
            Assert.Equal("SUMMARY:TestValue1453032876", Result);
        }

        [Fact]
        public void CanCallGetEnumeratorForIEnumerableWithNoParameters()
        {
            // Act
            IEnumerator Result = ((IEnumerable)_TestClass).GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Act
            IEnumerator<KeyValueField?> Result = _TestClass.GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var Result = _TestClass.GetHashCode();

            // Assert
            _ = Assert.IsType<int>(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var Left = new CalendarComponent(_Parent);
            var Right = new CalendarComponent(_Parent);

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(CalendarComponent) >= new CalendarComponent(_Parent);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarComponent(_Parent) >= default(CalendarComponent);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new CalendarComponent(_Parent);
            var Right = new CalendarComponent(_Parent);

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(CalendarComponent) > new CalendarComponent(_Parent);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarComponent(_Parent) > default(CalendarComponent);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new CalendarComponent(_Parent);
            var Right = new CalendarComponent(_Parent);

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new CalendarComponent(_Parent);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarComponent(_Parent) != default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new CalendarComponent(_Parent);
            var Right = new CalendarComponent(_Parent);

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(CalendarComponent) <= new CalendarComponent(_Parent);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarComponent(_Parent) <= default(CalendarComponent);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new CalendarComponent(_Parent);
            var Right = new CalendarComponent(_Parent);

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(CalendarComponent) < new CalendarComponent(_Parent);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new CalendarComponent(_Parent) < default(CalendarComponent);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new CalendarComponent(_Parent);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetActions()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Actions);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetAttachments()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Attachments);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetAttendees()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Attendees);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetCategories()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Categories);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetClasses()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Classes);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetComments()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Comments);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetCompleteds()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Completeds);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetContacts()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Contacts);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetCount()
        {
            // Assert
            var Result = Assert.IsType<int>(_TestClass.Count);

            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanGetCreateds()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Createds);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetCreatedUtc()
        {
            // Assert
            DateTime Result = Assert.IsType<DateTime>(_TestClass.CreatedUtc);

            Assert.Equal(DateTime.UtcNow.Date, Result.Date);
        }

        [Fact]
        public void CanGetDateStamps()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.DateStamps);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetDateStampUtc()
        {
            // Assert
            DateTime Result = Assert.IsType<DateTime>(_TestClass.DateStampUtc);

            Assert.Equal(DateTime.UtcNow.Date, Result.Date);
        }

        [Fact]
        public void CanGetDescriptions()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Descriptions);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetDues()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Dues);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetDurations()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Durations);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetEndDates()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.EndDates);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetEndDateUtc()
        {
            // Assert
            DateTime Result = Assert.IsType<DateTime>(_TestClass.EndDateUtc);

            Assert.Equal(DateTime.UtcNow.Date, Result.Date);
        }

        [Fact]
        public void CanGetExcludeDates()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.ExcludeDates);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetExcludeRules()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.ExcludeRules);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetFields()
        {
            // Assert
            List<KeyValueField> Result = Assert.IsType<List<KeyValueField>>(_TestClass.Fields);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetFreeBusys()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.FreeBusys);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetGeoLocations()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.GeoLocations);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetLastModifieds()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.LastModifieds);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetLastModifiedUtc()
        {
            // Assert
            DateTime Result = Assert.IsType<DateTime>(_TestClass.LastModifiedUtc);

            Assert.Equal(DateTime.UtcNow.Date, Result.Date);
        }

        [Fact]
        public void CanGetLocations()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Locations);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetOrganizers()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Organizers);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetPriorities()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Priorities);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetRecurrenceIds()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.RecurrenceIds);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetRelatedTos()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.RelatedTos);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetReoccurDates()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.ReoccurDates);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetReoccurRules()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.ReoccurRules);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetRepeatCounts()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.RepeatCounts);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetResources()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Resources);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetSequences()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Sequences);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetStartDates()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.StartDates);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetStartDateUtc()
        {
            // Assert
            DateTime Result = Assert.IsType<DateTime>(_TestClass.StartDateUtc);

            Assert.Equal(DateTime.UtcNow.Date, Result.Date);
        }

        [Fact]
        public void CanGetStatuses()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Statuses);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetSummaries()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Summaries);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetTimezoneNames()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.TimezoneNames);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetTimezoneOffsetFroms()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.TimezoneOffsetFroms);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetTimezoneOffsetTos()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.TimezoneOffsetTos);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetTimeZones()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.TimeZones);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetTimezoneUrls()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.TimezoneUrls);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetTransps()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Transps);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetTriggers()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Triggers);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetUIDs()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.UIDs);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanGetURLs()
        {
            // Assert
            IEnumerable<KeyValueField> Result = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.URLs);

            Assert.NotNull(Result);
        }

        [Fact]
        public void CanSetAndGetAction()
        {
            // Arrange
            const string TestValue = "TestValue262265456";
            var TestClass = new CalendarComponent(_Parent)
            {
                // Act
                Action = TestValue
            };

            // Assert
            Assert.Equal(TestValue, TestClass.Action);
        }

        [Fact]
        public void CanSetAndGetClass()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            const string TestValue = "TestValue1706702071";

            // Act
            TestClass.Class = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.Class);
        }

        [Fact]
        public void CanSetAndGetCreated()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            DateTime TestValue = DateTime.UtcNow;

            // Act
            TestClass.Created = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.Created);
        }

        [Fact]
        public void CanSetAndGetDateStamp()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            DateTime TestValue = DateTime.UtcNow;

            // Act
            TestClass.DateStamp = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.DateStamp);
        }

        [Fact]
        public void CanSetAndGetDescription()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            const string TestValue = "TestValue1159988007";

            // Act
            TestClass.Description = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.Description);
        }

        [Fact]
        public void CanSetAndGetEndDate()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            DateTime TestValue = DateTime.UtcNow;

            // Act
            TestClass.EndDate = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.EndDate);
        }

        [Fact]
        public void CanSetAndGetIndexerForInt()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            var TestValue = new KeyValueField(default!);
            TestClass.Fields.Add(new KeyValueField("A", Array.Empty<KeyValueParameter>(), "B"));

            // Assert
            Assert.NotSame(TestValue, Assert.IsType<KeyValueField>(TestClass[0]));

            // Act
            TestClass[0] = TestValue;

            // Assert
            Assert.Same(TestValue, TestClass[0]);
        }

        [Fact]
        public void CanSetAndGetLastModified()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            DateTime TestValue = DateTime.UtcNow;

            // Act
            TestClass.LastModified = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.LastModified);
        }

        [Fact]
        public void CanSetAndGetLocation()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            const string TestValue = "TestValue2040051890";

            // Act
            TestClass.Location = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.Location);
        }

        [Fact]
        public void CanSetAndGetPriority()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            const string TestValue = "TestValue812364208";

            // Act
            TestClass.Priority = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.Priority);
        }

        [Fact]
        public void CanSetAndGetSequence()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            const string TestValue = "TestValue117496184";

            // Act
            TestClass.Sequence = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.Sequence);
        }

        [Fact]
        public void CanSetAndGetStartDate()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            DateTime TestValue = DateTime.UtcNow;

            // Act
            TestClass.StartDate = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.StartDate);
        }

        [Fact]
        public void CanSetAndGetStatus()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            const string TestValue = "TestValue1094523616";

            // Act
            TestClass.Status = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.Status);
        }

        [Fact]
        public void CanSetAndGetSummary()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            const string TestValue = "TestValue1453032876";

            // Act
            TestClass.Summary = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.Summary);
        }

        [Fact]
        public void CanSetAndGetTransp()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            const string TestValue = "TestValue1121566459";

            // Act
            TestClass.Transp = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.Transp);
        }

        [Fact]
        public void CanSetAndGetTrigger()
        {
            // Arrange
            const string TestValue = "TestValue1040660138";
            var TestClass = new CalendarComponent(_Parent)
            {
                // Act
                Trigger = TestValue
            };

            // Assert
            Assert.Equal(TestValue, TestClass.Trigger);
        }

        [Fact]
        public void CanSetAndGetUID()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent);
            const string TestValue = "TestValue5954655";

            // Act
            TestClass.UID = TestValue;

            // Assert
            Assert.Equal(TestValue, TestClass.UID);
        }

        [Fact]
        public void ImplementsIComparable_Calendar()
        {
            // Arrange
            var BaseValue = new CalendarComponent(_Parent)
            {
                Summary = "A",
                StartDate = DateTime.UtcNow.Date.AddDays(1)
            };
            var EqualToBaseValue = new CalendarComponent(_Parent)
            {
                Summary = "A",
                StartDate = DateTime.UtcNow.Date.AddDays(1)
            };
            var GreaterThanBaseValue = new CalendarComponent(_Parent)
            {
                Summary = "B",
                StartDate = DateTime.UtcNow.Date.AddDays(2)
            };

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) < 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) > 0);
        }

        [Fact]
        public void ImplementsIEnumerable_KeyValueField()
        {
            // Arrange
            var Enumerable = new CalendarComponent(_Parent);
            Enumerable.Fields.Add(new KeyValueField("A", Array.Empty<KeyValueParameter>(), "B"));
            Enumerable.Fields.Add(new KeyValueField("C", Array.Empty<KeyValueParameter>(), "D"));
            Enumerable.Fields.Add(new KeyValueField("E", Array.Empty<KeyValueParameter>(), "F"));
            const int ExpectedCount = 3;
            var ActualCount = 0;

            // Act
            using (IEnumerator<KeyValueField?> Enumerator = Enumerable.GetEnumerator())
            {
                Assert.NotNull(Enumerator);
                while (Enumerator.MoveNext())
                {
                    ActualCount++;
                    _ = Assert.IsType<KeyValueField>(Enumerator.Current);
                }
            }

            // Assert
            Assert.Equal(ExpectedCount, ActualCount);
        }

        [Fact]
        public void ImplementsIEquatable_Calendar()
        {
            // Arrange
            var TestClass = new CalendarComponent(_Parent)
            {
                Summary = "TestValue1706702071",
                StartDate = DateTime.UtcNow.Date.AddDays(1)
            };
            var Same = new CalendarComponent(_Parent)
            {
                Summary = "TestValue1706702071",
                StartDate = DateTime.UtcNow.Date.AddDays(1)
            };
            var Different = new CalendarComponent(_Parent)
            {
                Summary = "TestValue1706702071",
                StartDate = DateTime.UtcNow.Date.AddDays(2)
            };

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

        public class TestClass
        {
            public string? Summary { get; set; }
        }
    }
}