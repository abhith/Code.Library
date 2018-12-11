namespace Code.Library
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// All String extensions.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly char[] CleanForXssChars = "*?(){}[];:%<>/\\|&'\"".ToCharArray();

        /// <summary>
        /// Returns default value if string is null or whitespace.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Returns default value if string is null or whitespace else return the string itself.</returns>
        public static string IfNullOrWhiteSpace(this string str, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(str) ? defaultValue : str;
        }

        /// <summary>
        /// string to nullable int.
        /// </summary>
        /// <param name="s">string.</param>
        /// <returns>integer or null.</returns>
        public static int? ToNullableInt32(this string s)
        {
            int i;
            if (int.TryParse(s, out i))
            {
                return i;
            }

            return null;
        }

        /// <summary>
        /// string to nullable bool.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <returns>Null or bool.</returns>
        public static bool? ToNullableBool(this string s)
        {
            bool i;
            if (bool.TryParse(s, out i))
            {
                return i;
            }

            return null;
        }

        /// <summary>
        /// Cleans string to aid in preventing xss attacks.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <param name="ignoreFromClean">Characters to ignore during clean.</param>
        /// <returns>Cleaned string.</returns>
        public static string CleanForXss(this string input, params char[] ignoreFromClean)
        {
            // remove any html
            input = input.StripHtml();

            // strip out any potential chars involved with XSS
            return input.ExceptChars(new HashSet<char>(CleanForXssChars.Except(ignoreFromClean)));
        }

        /// <summary>
        /// returns "safe" URL, stripping anything outside normal charsets for URL
        /// </summary>
        /// <param name="url">Input URL string.</param>
        /// <returns>Safe URL.</returns>
        public static string SanitizeURL(this string url)
        {
            return Regex.Replace(url, @"[^-A-Za-z0-9+&@#/%?=~_|!:,.;\(\)]", string.Empty);
        }

        public static string EnsureEndsWith(this string input, char value)
        {
            return input.EndsWith(value.ToString(CultureInfo.InvariantCulture)) ? input : input + value;
        }

        public static string EnsureEndsWith(this string input, string toEndWith)
        {
            return input.EndsWith(toEndWith.ToString(CultureInfo.InvariantCulture)) ? input : input + toEndWith;
        }

        public static string EnsureStartsWith(this string input, char value)
        {
            return input.StartsWith(value.ToString(CultureInfo.InvariantCulture)) ? input : value + input;
        }

        public static string ExceptChars(this string str, HashSet<char> toExclude)
        {
            var sb = new StringBuilder(str.Length);
            foreach (var c in str.Where(c => toExclude.Contains(c) == false))
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        public static bool IsLowerCase(this char ch)
        {
            return ch.ToString(CultureInfo.InvariantCulture) == ch.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
        }

        public static bool IsUpperCase(this char ch)
        {
            return ch.ToString(CultureInfo.InvariantCulture) == ch.ToString(CultureInfo.InvariantCulture).ToUpperInvariant();
        }

        public static string OrIfNullOrWhiteSpace(this string input, string alternative)
        {
            return !string.IsNullOrWhiteSpace(input)
                ? input
                : alternative;
        }

        /// <summary>
        /// Strips all html from a string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>Returns the string without any html tags.</returns>
        public static string StripHtml(this string text)
        {
            const string pattern = @"<(.|\n)*?>";
            return Regex.Replace(text, pattern, String.Empty);
        }

        /// <summary>
        /// Returns a copy of the string with the first character converted to lowercase.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <returns>The converted string.</returns>
        public static string ToFirstLower(this string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? input
                : input.Substring(0, 1).ToLower() + input.Substring(1);
        }

        /// <summary>
        /// Returns a copy of the string with the first character converted to lowercase using the casing rules of the specified culture.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted string.</returns>
        public static string ToFirstLower(this string input, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(input)
                ? input
                : input.Substring(0, 1).ToLower(culture) + input.Substring(1);
        }

        /// <summary>
        /// Returns a copy of the string with the first character converted to lowercase using the casing rules of the invariant culture.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <returns>The converted string.</returns>
        public static string ToFirstLowerInvariant(this string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? input
                : input.Substring(0, 1).ToLowerInvariant() + input.Substring(1);
        }

        /// <summary>
        /// Returns a copy of the string with the first character converted to uppercase.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <returns>The converted string.</returns>
        public static string ToFirstUpper(this string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? input
                : input.Substring(0, 1).ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Returns a copy of the string with the first character converted to uppercase using the casing rules of the specified culture.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted string.</returns>
        public static string ToFirstUpper(this string input, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(input)
                ? input
                : input.Substring(0, 1).ToUpper(culture) + input.Substring(1);
        }

        /// <summary>
        /// Returns a copy of the string with the first character converted to uppercase using the casing rules of the invariant culture.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <returns>The converted string.</returns>
        public static string ToFirstUpperInvariant(this string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? input
                : input.Substring(0, 1).ToUpperInvariant() + input.Substring(1);
        }
    }
}