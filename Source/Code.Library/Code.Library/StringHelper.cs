using System;
using System.Text.RegularExpressions;

namespace Code.Library
{
    public static class StringHelper
    {
        /// <summary>
        /// Author : Abhith
        /// Date : 14 July 2015
        /// Uppercase words. This program defines a method named UppercaseWords that is equivalent to the ucwords function in scripting languages such as PHP. The UppercaseWords method internally converts the string to a character array buffer.
        /// Source : http://www.dotnetperls.com/uppercase-first-letter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UppercaseWords(string value)
        {
            var array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (var i = 1; i < array.Length; i++)
            {
                if (array[i - 1] != ' ') continue;
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
            return new string(array);
        }

        /// <summary>
        /// Format Dates
        /// Author : Abhith
        /// Created On : 24 June 2015
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get friendly Url
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string GetFriendlyUrl(string title)
        {
            // make it all lower case
            title = title.ToLower();
            // remove entities
            title = Regex.Replace(title, @"&\w+;", "");
            // remove anything that is not letters, numbers, dash, or space
            title = Regex.Replace(title, @"[^a-z0-9\-\s]", "");
            // replace spaces
            title = title.Replace(' ', '-');
            // collapse dashes
            title = Regex.Replace(title, @"-{2,}", "-");
            // trim excessive dashes at the beginning
            title = title.TrimStart(new[] { '-' });
            // if it's too long, clip it
            if (title.Length > 80)
                title = title.Substring(0, 79);
            // remove trailing dashes
            title = title.TrimEnd(new[] { '-' });
            return title;
        }
    }
}
