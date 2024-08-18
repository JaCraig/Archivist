using Archivist.Enums;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Enums
{
    public class CalendarMethodsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; } = typeof(CalendarMethods);

        [Fact]
        public static void CanGetCancel() =>
            // Assert
            Assert.IsType<string>(CalendarMethods.Cancel);

        [Fact]
        public static void CanGetCompleted() =>
            // Assert
            Assert.IsType<string>(CalendarMethods.Completed);

        [Fact]
        public static void CanGetConfirmed() =>
            // Assert
            Assert.IsType<string>(CalendarMethods.Confirmed);

        [Fact]
        public static void CanGetDraft() =>
            // Assert
            Assert.IsType<string>(CalendarMethods.Draft);

        [Fact]
        public static void CanGetInProcess() =>
            // Assert
            Assert.IsType<string>(CalendarMethods.InProcess);

        [Fact]
        public static void CanGetNeedsAction() =>
            // Assert
            Assert.IsType<string>(CalendarMethods.NeedsAction);

        [Fact]
        public static void CanGetRequest() =>
            // Assert
            Assert.IsType<string>(CalendarMethods.Request);

        [Fact]
        public static void CanGetTentative() =>
            // Assert
            Assert.IsType<string>(CalendarMethods.Tentative);
    }
}