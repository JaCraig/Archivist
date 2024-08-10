namespace Archivist.Tests.Enums
{
    using Archivist.Enums;
    using System;
    using Xunit;

    public static class CalendarMethodsTests
    {
        [Fact]
        public static void CanGetCancel()
        {
            // Assert
            Assert.IsType<string>(CalendarMethods.Cancel);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetCompleted()
        {
            // Assert
            Assert.IsType<string>(CalendarMethods.Completed);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetConfirmed()
        {
            // Assert
            Assert.IsType<string>(CalendarMethods.Confirmed);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetDraft()
        {
            // Assert
            Assert.IsType<string>(CalendarMethods.Draft);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetInProcess()
        {
            // Assert
            Assert.IsType<string>(CalendarMethods.InProcess);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetNeedsAction()
        {
            // Assert
            Assert.IsType<string>(CalendarMethods.NeedsAction);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetRequest()
        {
            // Assert
            Assert.IsType<string>(CalendarMethods.Request);

            throw new NotImplementedException("Create or modify test");
        }

        [Fact]
        public static void CanGetTentative()
        {
            // Assert
            Assert.IsType<string>(CalendarMethods.Tentative);

            throw new NotImplementedException("Create or modify test");
        }
    }
}