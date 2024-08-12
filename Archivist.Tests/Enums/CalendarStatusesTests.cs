using Archivist.Enums;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Enums
{
    public class CalendarStatusesTests : TestBaseClass
    {
        protected override Type? ObjectType { get; } = typeof(CalendarStatuses);

        [Fact]
        public static void CanGetBusy() =>
            // Assert
            Assert.IsType<string>(CalendarStatuses.Busy);

        [Fact]
        public static void CanGetCancelled() =>
            // Assert
            Assert.IsType<string>(CalendarStatuses.Cancelled);

        [Fact]
        public static void CanGetCompleted() =>
            // Assert
            Assert.IsType<string>(CalendarStatuses.Completed);

        [Fact]
        public static void CanGetConfirmed() =>
            // Assert
            Assert.IsType<string>(CalendarStatuses.Confirmed);

        [Fact]
        public static void CanGetDraft() =>
            // Assert
            Assert.IsType<string>(CalendarStatuses.Draft);

        [Fact]
        public static void CanGetFinal() =>
            // Assert
            Assert.IsType<string>(CalendarStatuses.Final);

        [Fact]
        public static void CanGetFree() =>
            // Assert
            Assert.IsType<string>(CalendarStatuses.Free);
    }
}