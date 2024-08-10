namespace Archivist.Tests.Enums
{
    using Archivist.Enums;
    using System;
    using Xunit;

    public static class CalendarStatusesTests
    {
        [Fact]
        public static void CanGetBusy()
        {
            // Assert
            Assert.IsType<string>(CalendarStatuses.Busy);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetCancelled()
        {
            // Assert
            Assert.IsType<string>(CalendarStatuses.Cancelled);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetCompleted()
        {
            // Assert
            Assert.IsType<string>(CalendarStatuses.Completed);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetConfirmed()
        {
            // Assert
            Assert.IsType<string>(CalendarStatuses.Confirmed);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetDraft()
        {
            // Assert
            Assert.IsType<string>(CalendarStatuses.Draft);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetFinal()
        {
            // Assert
            Assert.IsType<string>(CalendarStatuses.Final);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetFree()
        {
            // Assert
            Assert.IsType<string>(CalendarStatuses.Free);

            throw new NotImplementedException("Create or modify test");
        }
    }
}