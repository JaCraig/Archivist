namespace Archivist.Enums
{
    /// <summary>
    /// Represents the methods of a calendar item.
    /// </summary>
    public static class CalendarMethods
    {
        /// <summary>
        /// The calendar item is canceled.
        /// </summary>
        public static string Cancel => "CANCEL";

        /// <summary>
        /// The calendar item is completed.
        /// </summary>
        public static string Completed => "COMPLETED";

        /// <summary>
        /// The calendar item is confirmed.
        /// </summary>
        public static string Confirmed => "CONFIRMED";

        /// <summary>
        /// The calendar item is in draft.
        /// </summary>
        public static string Draft => "DRAFT";

        /// <summary>
        /// The calendar item is in process.
        /// </summary>
        public static string InProcess => "IN-PROCESS";

        /// <summary>
        /// The calendar item needs action.
        /// </summary>
        public static string NeedsAction => "NEEDS-ACTION";

        /// <summary>
        /// The calendar item is requested.
        /// </summary>
        public static string Request => "REQUEST";

        /// <summary>
        /// The calendar item is tentative.
        /// </summary>
        public static string Tentative => "TENTATIVE";
    }
}