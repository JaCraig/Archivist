using Archivist.Enums;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Enums
{
    public class CommonCalendarFieldsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; } = typeof(CommonCalendarFields);

        [Fact]
        public static void CanGetAction() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Action);

        [Fact]
        public static void CanGetAttachment() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Attachment);

        [Fact]
        public static void CanGetAttendee() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Attendee);

        [Fact]
        public static void CanGetCategories() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Categories);

        [Fact]
        public static void CanGetClass() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Class);

        [Fact]
        public static void CanGetComment() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Comment);

        [Fact]
        public static void CanGetCompleted() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Completed);

        [Fact]
        public static void CanGetContact() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Contact);

        [Fact]
        public static void CanGetCreated() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Created);

        [Fact]
        public static void CanGetDateStamp() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.DateStamp);

        [Fact]
        public static void CanGetDescription() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Description);

        [Fact]
        public static void CanGetDue() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Due);

        [Fact]
        public static void CanGetDuration() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Duration);

        [Fact]
        public static void CanGetEndDate() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.EndDate);

        [Fact]
        public static void CanGetExcludeDates() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.ExcludeDates);

        [Fact]
        public static void CanGetExcludeRule() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.ExcludeRule);

        [Fact]
        public static void CanGetFreeBusy() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.FreeBusy);

        [Fact]
        public static void CanGetGeo() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Geo);

        [Fact]
        public static void CanGetLastModified() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.LastModified);

        [Fact]
        public static void CanGetLocation() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Location);

        [Fact]
        public static void CanGetMethod() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Method);

        [Fact]
        public static void CanGetOrganizer() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Organizer);

        [Fact]
        public static void CanGetPriority() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Priority);

        [Fact]
        public static void CanGetProductId() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.ProductId);

        [Fact]
        public static void CanGetRecurrenceId() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.RecurrenceId);

        [Fact]
        public static void CanGetRelatedTo() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.RelatedTo);

        [Fact]
        public static void CanGetReoccurDates() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.ReoccurDates);

        [Fact]
        public static void CanGetReoccurRule() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.ReoccurRule);

        [Fact]
        public static void CanGetRepeatCount() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.RepeatCount);

        [Fact]
        public static void CanGetResources() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Resources);

        [Fact]
        public static void CanGetSequence() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Sequence);

        [Fact]
        public static void CanGetStartDate() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.StartDate);

        [Fact]
        public static void CanGetStatus() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Status);

        [Fact]
        public static void CanGetSummary() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Summary);

        [Fact]
        public static void CanGetTimezone() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Timezone);

        [Fact]
        public static void CanGetTimezoneName() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.TimezoneName);

        [Fact]
        public static void CanGetTimezoneOffsetFrom() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.TimezoneOffsetFrom);

        [Fact]
        public static void CanGetTimezoneOffsetTo() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.TimezoneOffsetTo);

        [Fact]
        public static void CanGetTimezoneUrl() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.TimezoneUrl);

        [Fact]
        public static void CanGetTransp() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Transp);

        [Fact]
        public static void CanGetTrigger() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Trigger);

        [Fact]
        public static void CanGetUid() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Uid);

        [Fact]
        public static void CanGetUrl() =>
            // Assert
            Assert.IsType<string>(CommonCalendarFields.Url);
    }
}