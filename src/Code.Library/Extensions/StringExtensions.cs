using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Code.Library.Extensions
{
    public static class StringExtensions
    {
        //public static string IfNullOrWhiteSpace(this string str, string defaultValue)
        //{
        //    return str.IsNullOrWhiteSpace() ? defaultValue : str;
        //}

        private static readonly char[] CleanForXssChars = "*?(){}[];:%<>/\\|&'\"".ToCharArray();

        /// <summary>
        /// Cleans string to aid in preventing xss attacks.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="ignoreFromClean"></param>
        /// <returns></returns>
        public static string CleanForXss(this string input, params char[] ignoreFromClean)
        {
            //remove any html
            input = input.StripHtml();
            //strip out any potential chars involved with XSS
            return input.ExceptChars(new HashSet<char>(CleanForXssChars.Except(ignoreFromClean)));
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

        //public static string EnsureStartsWith(this string input, string toStartWith)
        //{
        //    if (input.StartsWith(toStartWith)) return input;
        //    return toStartWith + input.TrimStart(toStartWith);
        //}
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

        ///// <summary>
        ///// Splits a Pascal cased string into a phrase separated by spaces.
        ///// </summary>
        ///// <param name="phrase">The text to split.</param>
        ///// <returns>The splitted text.</returns>
        //public static string SplitPascalCasing(this string phrase)
        //{
        //    return ShortStringHelper.SplitPascalCasing(phrase, ' ');
        //}

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

        ///// <summary>
        ///// Truncates the specified text string.
        ///// </summary>
        ///// <param name="text">The text.</param>
        ///// <param name="maxLength">Length of the max.</param>
        ///// <param name="suffix">The suffix.</param>
        ///// <returns></returns>
        //public static string Truncate(this string text, int maxLength, string suffix = "...")
        //{
        //    // replaces the truncated string to a ...
        //    var truncatedString = text;

        //    if (maxLength <= 0) return truncatedString;
        //    var strLength = maxLength - suffix.Length;

        //    if (strLength <= 0) return truncatedString;

        //    if (text == null || text.Length <= maxLength) return truncatedString;

        //    truncatedString = text.Substring(0, strLength);
        //    truncatedString = truncatedString.TrimEnd();
        //    truncatedString += suffix;

        //    return truncatedString;
        //}
        ///// <summary>
        ///// Cleans a string.
        ///// </summary>
        ///// <param name="text">The text to clean.</param>
        ///// <param name="stringType">A flag indicating the target casing and encoding of the string. By default,
        ///// strings are cleaned up to camelCase and Ascii.</param>
        ///// <returns>The clean string.</returns>
        ///// <remarks>The string is cleaned in the context of the IShortStringHelper default culture.</remarks>
        //public static string ToCleanString(this string text, CleanStringType stringType)
        //{
        //    return ShortStringHelper.CleanString(text, stringType);
        //}

        ///// <summary>
        ///// Cleans a string, using a specified separator.
        ///// </summary>
        ///// <param name="text">The text to clean.</param>
        ///// <param name="stringType">A flag indicating the target casing and encoding of the string. By default,
        ///// strings are cleaned up to camelCase and Ascii.</param>
        ///// <param name="separator">The separator.</param>
        ///// <returns>The clean string.</returns>
        ///// <remarks>The string is cleaned in the context of the IShortStringHelper default culture.</remarks>
        //public static string ToCleanString(this string text, CleanStringType stringType, char separator)
        //{
        //    return ShortStringHelper.CleanString(text, stringType, separator);
        //}

        ///// <summary>
        ///// Cleans a string in the context of a specified culture.
        ///// </summary>
        ///// <param name="text">The text to clean.</param>
        ///// <param name="stringType">A flag indicating the target casing and encoding of the string. By default,
        ///// strings are cleaned up to camelCase and Ascii.</param>
        ///// <param name="culture">The culture.</param>
        ///// <returns>The clean string.</returns>
        //public static string ToCleanString(this string text, CleanStringType stringType, CultureInfo culture)
        //{
        //    return ShortStringHelper.CleanString(text, stringType, culture);
        //}

        ///// <summary>
        ///// Cleans a string in the context of a specified culture, using a specified separator.
        ///// </summary>
        ///// <param name="text">The text to clean.</param>
        ///// <param name="stringType">A flag indicating the target casing and encoding of the string. By default,
        ///// strings are cleaned up to camelCase and Ascii.</param>
        ///// <param name="separator">The separator.</param>
        ///// <param name="culture">The culture.</param>
        ///// <returns>The clean string.</returns>
        //public static string ToCleanString(this string text, CleanStringType stringType, char separator, CultureInfo culture)
        //{
        //    return ShortStringHelper.CleanString(text, stringType, separator, culture);
        //}

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

        ///// <summary>
        ///// Cleans a string, in the context of the invariant culture, to produce a string that can safely be used as a filename,
        ///// both internally (on disk) and externally (as a url).
        ///// </summary>
        ///// <param name="text">The text to filter.</param>
        ///// <returns>The safe filename.</returns>
        //public static string ToSafeFileName(this string text)
        //{
        //    return ShortStringHelper.CleanStringForSafeFileName(text);
        //}

        ///// <summary>
        ///// Cleans a string, in the context of the invariant culture, to produce a string that can safely be used as a filename,
        ///// both internally (on disk) and externally (as a url).
        ///// </summary>
        ///// <param name="text">The text to filter.</param>
        ///// <param name="culture">The culture.</param>
        ///// <returns>The safe filename.</returns>
        //public static string ToSafeFileName(this string text, CultureInfo culture)
        //{
        //    return ShortStringHelper.CleanStringForSafeFileName(text, culture);
        //}

        ///// <summary>
        ///// Cleans a string to produce a string that can safely be used in an url segment.
        ///// </summary>
        ///// <param name="text">The text to filter.</param>
        ///// <returns>The safe url segment.</returns>
        //public static string ToUrlSegment(this string text)
        //{
        //    return ShortStringHelper.CleanStringForUrlSegment(text);
        //}

        ///// <summary>
        ///// Cleans a string, in the context of a specified culture, to produce a string that can safely be used in an url segment.
        ///// </summary>
        ///// <param name="text">The text to filter.</param>
        ///// <param name="culture">The culture.</param>
        ///// <returns>The safe url segment.</returns>
        //public static string ToUrlSegment(this string text, CultureInfo culture)
        //{
        //    return ShortStringHelper.CleanStringForUrlSegment(text, culture);
        //}
    }
}