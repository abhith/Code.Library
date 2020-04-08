namespace Code.Library.Helpers
{
    using System;

    /// <summary>
    /// All DateTime helper methods.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Format Dates.
        /// </summary>
        /// <param name="startDate">Start Date.</param>
        /// <param name="endDate">End Date.</param>
        /// <returns>Formatted date string.</returns>
        public static string FormatDates(DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null && endDate != null)
            {
                if (startDate == endDate)
                {
                    return Convert.ToDateTime(startDate).ToString("MMM dd , yyyy");
                }
                else
                {
                    if (Convert.ToDateTime(startDate).ToString("MM yyyy") ==
                        Convert.ToDateTime(endDate).ToString("MM yyyy"))
                    {
                        return Convert.ToDateTime(startDate).ToString("MMM dd") + " - " +
                               Convert.ToDateTime(endDate).ToString("MMM dd , yyyy");
                    }
                    else
                    {
                        return Convert.ToDateTime(startDate).ToString("MMM dd , yyyy") + " - " +
                               Convert.ToDateTime(endDate).ToString("MMM dd , yyyy");
                    }
                }
            }

            if (startDate != null)
            {
                return Convert.ToDateTime(startDate).ToString("MMM dd , yyyy");
            }

            return endDate != null ? Convert.ToDateTime(endDate).ToString("MMM dd , yyyy") : "not specified !";
        }
    }
}