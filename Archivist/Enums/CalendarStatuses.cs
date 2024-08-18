namespace Archivist.Enums
{
    /// <summary>
    /// Represents the possible statuses of a calendar.
    /// </summary>
    public static class CalendarStatuses
    {
        /// <summary>
        /// Represents a busy status.
        /// </summary>
        public static string Busy => "BUSY";

        /// <summary>
        /// Represents a cancelled status.
        /// </summary>
        public static string Cancelled => "CANCELLED";

        /// <summary>
        /// Represents a completed status.
        /// </summary>
        public static string Completed => "COMPLETED";

        /// <summary>
        /// Represents a confirmed status.
        /// </summary>
        public static string Confirmed => "CONFIRMED";

        /// <summary>
        /// Represents a draft status.
        /// </summary>
        public static string Draft => "DRAFT";

        /// <summary>
        /// Represents a final status.
        /// </summary>
        public static string Final => "FINAL";

        /// <summary>
        /// Represents a free status.
        /// </summary>
        public static string Free => "FREE";
    }
}