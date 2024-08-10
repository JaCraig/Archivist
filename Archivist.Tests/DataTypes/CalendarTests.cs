using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public void CanCallCompareTo()
        {
            // Arrange
            var Other = new Calendar();

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
            var _TestClass = new Calendar();

            // Act
            _TestClass.ConvertFrom(Obj);

            // Assert
            Assert.Equal("Example summary/subject", _TestClass.Summary);
        }

        [Fact]
        public void CanCallConvertTo()
        {
            // Arrange
            var _TestClass = new Calendar
            {
                Summary = "Example summary/subject"
            };
            // Act
            TestClass? Result = _TestClass.ConvertTo<TestClass>();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Example summary/subject", Result.Summary);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new Calendar();
            var Right = new Calendar();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualsWithCalendar()
        {
            // Arrange
            var Other = new Calendar
            {
                Class = "TestValue1706702071"
            };

            // Act
            var Result = _TestClass.Equals(Other);

            // Assert
            Assert.False(Result);
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
            var _TestClass = new Calendar
            {
                Summary = "TestValue1453032876"
            };
            // Act
            var Result = _TestClass.GetContent();

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
            var Left = new Calendar();
            var Right = new Calendar();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var left = new Calendar();
            var right = new Calendar();

            // Act
            _ = left > right;

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var left = new Calendar();
            var right = new Calendar();

            // Act
            _ = left != right;

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var left = new Calendar();
            var right = new Calendar();

            // Act
            _ = left <= right;

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var left = new Calendar();
            var right = new Calendar();

            // Act
            _ = left < right;

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanCallToFileType()
        {
            // Act
            var result = _TestClass.ToFileType<TFile>();

            // Assert
            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new Calendar(_Converter);

            // Assert
            Assert.NotNull(instance);

            // Act
            instance = new Calendar();

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CanGetActions()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Actions);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetAlarms()
        {
            // Assert
            _ = Assert.IsType<List<Alarm>>(_TestClass.Alarms);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetAttachments()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Attachments);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetAttendees()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Attendees);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetCategories()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Categories);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetClasses()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Classes);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetComments()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Comments);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetCompleteds()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Completeds);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetContacts()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Contacts);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetCount()
        {
            // Assert
            _ = Assert.IsType<int>(_TestClass.Count);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetCreateds()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Createds);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetCreatedUtc()
        {
            // Assert
            _ = Assert.IsType<DateTime>(_TestClass.CreatedUtc);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetDateStamps()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.DateStamps);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetDateStampUtc()
        {
            // Assert
            _ = Assert.IsType<DateTime>(_TestClass.DateStampUtc);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetDescriptions()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Descriptions);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetDues()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Dues);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetDurations()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Durations);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetEndDates()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.EndDates);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetEndDateUtc()
        {
            // Assert
            _ = Assert.IsType<DateTime>(_TestClass.EndDateUtc);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetExcludeDates()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.ExcludeDates);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetExcludeRules()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.ExcludeRules);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetFields()
        {
            // Assert
            _ = Assert.IsType<List<KeyValueField>>(_TestClass.Fields);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetFreeBusys()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.FreeBusys);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetGeoLocations()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.GeoLocations);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetIsCancelled()
        {
            // Assert
            _ = Assert.IsType<bool>(_TestClass.IsCancelled);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetLastModifieds()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.LastModifieds);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetLastModifiedUtc()
        {
            // Assert
            _ = Assert.IsType<DateTime>(_TestClass.LastModifiedUtc);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetLocations()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Locations);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetMethods()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Methods);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetOrganizers()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Organizers);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetPriorities()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Priorities);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetProductIds()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.ProductIds);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetRecurrenceIds()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.RecurrenceIds);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetRelatedTos()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.RelatedTos);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetReoccurDates()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.ReoccurDates);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetReoccurRules()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.ReoccurRules);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetRepeatCounts()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.RepeatCounts);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetResources()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Resources);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetSequences()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Sequences);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetStartDates()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.StartDates);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetStartDateUtc()
        {
            // Assert
            _ = Assert.IsType<DateTime>(_TestClass.StartDateUtc);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetStatuses()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Statuses);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetSummaries()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Summaries);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetTimezoneNames()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.TimezoneNames);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetTimezoneOffsetFroms()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.TimezoneOffsetFroms);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetTimezoneOffsetTos()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.TimezoneOffsetTos);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetTimeZones()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.TimeZones);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetTimezoneUrls()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.TimezoneUrls);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetTransps()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Transps);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetTriggers()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.Triggers);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetUIDs()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.UIDs);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CanGetURLs()
        {
            // Assert
            _ = Assert.IsAssignableFrom<IEnumerable<KeyValueField>>(_TestClass.URLs);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public void CannotCallEqualityOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default == new Calendar(); });

        [Fact]
        public void CannotCallEqualityOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Calendar() == default; });

        [Fact]
        public void CannotCallGreaterThanEqualToOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default(Calendar) >= new Calendar(); });

        [Fact]
        public void CannotCallGreaterThanEqualToOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Calendar() >= default(Calendar); });

        [Fact]
        public void CannotCallGreaterThanOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default(Calendar) > new Calendar(); });

        [Fact]
        public void CannotCallGreaterThanOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Calendar() > default(Calendar); });

        [Fact]
        public void CannotCallInequalityOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default != new Calendar(); });

        [Fact]
        public void CannotCallInequalityOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Calendar() != default; });

        [Fact]
        public void CannotCallLessThanEqualToOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default(Calendar) <= new Calendar(); });

        [Fact]
        public void CannotCallLessThanEqualToOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Calendar() <= default(Calendar); });

        [Fact]
        public void CannotCallLessThanOperatorWithNullLeft() => Assert.Throws<ArgumentNullException>(() => { var result = default(Calendar) < new Calendar(); });

        [Fact]
        public void CannotCallLessThanOperatorWithNullRight() => Assert.Throws<ArgumentNullException>(() => { var result = new Calendar() < default(Calendar); });

        [Fact]
        public void CanSetAndGetClass()
        {
            // Arrange
            var testValue = "TestValue1706702071";

            // Act
            _TestClass.Class = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Class);
        }

        [Fact]
        public void CanSetAndGetCreated()
        {
            // Arrange
            DateTime testValue = DateTime.UtcNow;

            // Act
            _TestClass.Created = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Created);
        }

        [Fact]
        public void CanSetAndGetCurrentTimeZone()
        {
            // Arrange
            var testValue = new TimeZoneInfo();

            // Act
            _TestClass.CurrentTimeZone = testValue;

            // Assert
            Assert.Same(testValue, _TestClass.CurrentTimeZone);
        }

        [Fact]
        public void CanSetAndGetDateStamp()
        {
            // Arrange
            DateTime testValue = DateTime.UtcNow;

            // Act
            _TestClass.DateStamp = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.DateStamp);
        }

        [Fact]
        public void CanSetAndGetDescription()
        {
            // Arrange
            var testValue = "TestValue1159988007";

            // Act
            _TestClass.Description = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Description);
        }

        [Fact]
        public void CanSetAndGetEndDate()
        {
            // Arrange
            DateTime testValue = DateTime.UtcNow;

            // Act
            _TestClass.EndDate = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.EndDate);
        }

        [Fact]
        public void CanSetAndGetIndexerForInt()
        {
            var testValue = new KeyValueField(default!);
            _ = Assert.IsType<KeyValueField>(_TestClass[1488354690]);
            _TestClass[1488354690] = testValue;
            Assert.Same(testValue, _TestClass[1488354690]);
        }

        [Fact]
        public void CanSetAndGetLastModified()
        {
            // Arrange
            DateTime testValue = DateTime.UtcNow;

            // Act
            _TestClass.LastModified = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.LastModified);
        }

        [Fact]
        public void CanSetAndGetLocation()
        {
            // Arrange
            var testValue = "TestValue2040051890";

            // Act
            _TestClass.Location = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Location);
        }

        [Fact]
        public void CanSetAndGetMethod()
        {
            // Arrange
            var testValue = "TestValue1545140120";

            // Act
            _TestClass.Method = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Method);
        }

        [Fact]
        public void CanSetAndGetPriority()
        {
            // Arrange
            var testValue = "TestValue812364208";

            // Act
            _TestClass.Priority = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Priority);
        }

        [Fact]
        public void CanSetAndGetProductId()
        {
            // Arrange
            var testValue = "TestValue661332337";

            // Act
            _TestClass.ProductId = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.ProductId);
        }

        [Fact]
        public void CanSetAndGetSequence()
        {
            // Arrange
            var testValue = "TestValue117496184";

            // Act
            _TestClass.Sequence = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Sequence);
        }

        [Fact]
        public void CanSetAndGetStartDate()
        {
            // Arrange
            DateTime testValue = DateTime.UtcNow;

            // Act
            _TestClass.StartDate = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.StartDate);
        }

        [Fact]
        public void CanSetAndGetStatus()
        {
            // Arrange
            var testValue = "TestValue1094523616";

            // Act
            _TestClass.Status = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Status);
        }

        [Fact]
        public void CanSetAndGetSummary()
        {
            // Arrange
            var testValue = "TestValue1453032876";

            // Act
            _TestClass.Summary = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Summary);
        }

        [Fact]
        public void CanSetAndGetTransp()
        {
            // Arrange
            var testValue = "TestValue1121566459";

            // Act
            _TestClass.Transp = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Transp);
        }

        [Fact]
        public void CanSetAndGetUID()
        {
            // Arrange
            var testValue = "TestValue5954655";

            // Act
            _TestClass.UID = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.UID);
        }

        [Fact]
        public void CanSetAndGetVersion()
        {
            // Arrange
            var testValue = "TestValue1260476273";

            // Act
            _TestClass.Version = testValue;

            // Assert
            Assert.Equal(testValue, _TestClass.Version);
        }

        [Fact]
        public void ImplementsIComparable_Calendar()
        {
            // Arrange
            var baseValue = default(Calendar);
            var equalToBaseValue = default(Calendar);
            var greaterThanBaseValue = default(Calendar);

            // Assert
            Assert.Equal(0, baseValue.CompareTo(equalToBaseValue));
            Assert.True(baseValue.CompareTo(greaterThanBaseValue) < 0);
            Assert.True(greaterThanBaseValue.CompareTo(baseValue) > 0);
        }

        [Fact]
        public void ImplementsIEnumerable_KeyValueField()
        {
            // Arrange
            var enumerable = default(Calendar);
            var expectedCount = -1;
            var actualCount = 0;

            // Act
            using (IEnumerator<KeyValueField?> enumerator = enumerable.GetEnumerator())
            {
                Assert.NotNull(enumerator);
                while (enumerator.MoveNext())
                {
                    actualCount++;
                    _ = Assert.IsType<KeyValueField>(enumerator.Current);
                }
            }

            // Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void ImplementsIEquatable_Calendar()
        {
            // Arrange
            var same = new Calendar();
            var different = new Calendar();

            // Assert
            Assert.False(_TestClass.Equals(default(object)));
            Assert.False(_TestClass.Equals(new object()));
            Assert.True(_TestClass.Equals((object)same));
            Assert.False(_TestClass.Equals((object)different));
            Assert.True(_TestClass.Equals(same));
            Assert.False(_TestClass.Equals(different));
            Assert.Equal(same.GetHashCode(), _TestClass.GetHashCode());
            Assert.NotEqual(different.GetHashCode(), _TestClass.GetHashCode());
            Assert.True(_TestClass == same);
            Assert.False(_TestClass == different);
            Assert.False(_TestClass != same);
            Assert.True(_TestClass != different);
        }

        public class TestClass
        {
            public string? Summary { get; set; }
        }
    }
}