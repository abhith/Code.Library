using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Code.Library.Extensions
{
    /// <summary>
    /// All String extensions.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly char[] CleanForXssChars = "*?(){}[];:%<>/\\|&'\"".ToCharArray();
        private static MD5CryptoServiceProvider md5CryptoServiceProvider = null;

        /// <summary>
        /// Clean the content by replacing special characters/HTML tags.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="removeHtml">
        /// The remove html.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Clean(this string content, bool removeHtml = true)
        {
            if (removeHtml)
            {
                content = content.StripHtml();
            }

            content = content.Replace("\\", string.Empty).Replace("|", string.Empty).Replace("(", string.Empty)
                .Replace(")", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty)
                .Replace("*", string.Empty).Replace("?", string.Empty).Replace("}", string.Empty)
                .Replace("{", string.Empty).Replace("^", string.Empty).Replace("+", string.Empty);

            var words = content.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var word in
                words.Select(t => t.Trim()).Where(word => word.Length >= 1))
            {
                sb.AppendFormat("{0} ", word);
            }

            return sb.ToString().Trim();
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

        /// <summary>
        /// Get last N characters from a string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="numberOfChars"></param>
        /// <returns></returns>
        public static string GetLast(this string source, int numberOfChars)
        {
            if (numberOfChars >= source.Length)
                return source;
            return source.Substring(source.Length - numberOfChars);
        }

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

        public static bool IsLowerCase(this char ch)
        {
            return ch.ToString(CultureInfo.InvariantCulture) == ch.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
        }

        /// <summary>
        /// true, if the string can be parsed as Double respective Int32
        /// Spaces are not considred.
        /// </summary>
        /// <param name="s">input string</param>
        /// <param name="floatpoint">true, if Double is considered,
        /// otherwhise Int32 is considered.</param>
        /// <returns>true, if the string contains only digits or float-point</returns>
        public static bool IsNumber(this string s, bool floatpoint)
        {
            int i;
            double d;
            string withoutWhiteSpace = s.RemoveSpaces();
            if (floatpoint)
            {
                return double.TryParse(withoutWhiteSpace, NumberStyles.Any, Thread.CurrentThread.CurrentUICulture, out d);
            }
            else
            {
                return int.TryParse(withoutWhiteSpace, out i);
            }
        }

        /// <summary>
        /// true, if float-point is considered
        /// </summary>
        /// <param name="floatpoint"></param>
        /// <returns>true, if the string contains only digits or float-point</returns>
        public static bool IsNumberOnly(this string s, bool floatpoint)
        {
            s = s.Trim();
            if (s.Length == 0)
            {
                return false;
            }

            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    if (floatpoint && (c == '.' || c == ','))
                    {
                        continue;
                    }

                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// To check whether the given string is Arabic.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns True if Arabic</returns>
        public static bool IsRtl(this string input)
        {
            return Regex.IsMatch(input, @"\p{IsArabic}");
        }

        public static bool IsUpperCase(this char ch)
        {
            return ch.ToString(CultureInfo.InvariantCulture) == ch.ToString(CultureInfo.InvariantCulture).ToUpperInvariant();
        }

        /// <summary>
        /// To check whether the string is a valid email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(this string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Validate if the given string is URL or not.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns>True or False.</returns>
        public static bool IsValidUrl(this string url)
        {
            string strRegex = "^(https?://)"
                              + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@
                              + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184
                              + "|" // allows either IP or domain
                              + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www.
                              + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]" // second level domain
                              + @"(\.[a-z]{2,6})?)" // first level domain- .com or .museum is optional
                              + "(:[0-9]{1,5})?" // port number- :80
                              + "((/?)|" // a slash isn't required if there is no file name
                              + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            return new Regex(strRegex).IsMatch(url);
        }

        /// <summary>
        /// generates MD5.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MD5(this string s)
        {
            if (md5CryptoServiceProvider == null) // creating only when needed
            {
                md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            }

            byte[] newdata = Encoding.Default.GetBytes(s);
            byte[] encrypted = md5CryptoServiceProvider.ComputeHash(newdata);
            return BitConverter.ToString(encrypted).Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        /// Replace \r\n or \n by <br />
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Nl2Br(this string s)
        {
            return s.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }

        public static string OrIfNullOrWhiteSpace(this string input, string alternative)
        {
            return !string.IsNullOrWhiteSpace(input)
                ? input
                : alternative;
        }

        public static string RemoveDiacritics(this string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        /// <summary>
        /// remove white space, not line end.
        /// Useful when parsing user input such phone,
        /// price int.Parse("1 000 000".RemoveSpaces(),.....
        /// </summary>
        /// <param name="s"></param>
        public static string RemoveSpaces(this string s)
        {
            return s.Replace(" ", "");
        }

        /// <summary>
        /// Replace last occurance in a string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="find"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceLastOccurrence(this string source, string find, string replace)
        {
            var place = source.LastIndexOf(find, StringComparison.Ordinal);

            if (place == -1)
                return string.Empty;

            var result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }

        /// <summary>
        /// Reverse the string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
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

        /// <summary>
        /// Strips all html from a string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>Returns the string without any html tags.</returns>
        public static string StripHtml(this string text)
        {
            const string pattern = @"<(.|\n)*?>";
            return Regex.Replace(text, pattern, string.Empty);
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

        /// <summary>
        /// The get friendly url.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToFriendlyUrl(this string title)
        {
            // make it all lower case
            title = title.ToLower();

            // remove entities
            title = Regex.Replace(title, @"&\w+;", string.Empty);

            // remove anything that is not letters, numbers, dash, or space
            title = Regex.Replace(title, @"[^a-z0-9\-\s]", string.Empty);

            // replace spaces
            title = title.Replace(' ', '-');

            // collapse dashes
            title = Regex.Replace(title, @"-{2,}", "-");

            // trim excessive dashes at the beginning
            title = title.TrimStart(new[] { '-' });

            // if it's too long, clip it
            if (title.Length > 80)
            {
                title = title.Substring(0, 79);
            }

            // remove trailing dashes
            title = title.TrimEnd(new[] { '-' });
            return title;
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
        /// Truncate string at a word near to the limit specified. Avoid word split.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <param name="appendDots"></param>
        /// <returns></returns>
        public static string TruncateAtWord(string input, int length, bool appendDots)
        {
            if (input == null || input.Length < length)
                return input;
            var iNextSpace = input.LastIndexOf(" ", length, StringComparison.Ordinal);
            var trimmedInput = string.Format("{0}", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());

            if (appendDots)
            {
                return trimmedInput + "...";
            }
            return trimmedInput;
        }

        /// <summary>
        /// Uppercase words. This program defines a method named UppercaseWords that is equivalent to the ucwords function in scripting languages such as PHP. The UppercaseWords method internally converts the string to a character array buffer.
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
        /// Checks if URL is valid.
        /// </summary>
        /// <param name="text"></param>
        /// <summary>
        /// Check if url (http) is available.
        /// </summary>
        /// <param name="httpUri">url to check</param>
        /// <param name="httpUrl"></param>
        /// <example>
        /// string url = "www.codeproject.com;
        /// if( !url.UrlAvailable())
        ///     ...codeproject is not available.
        /// </example>
        /// <returns>true if available.</returns>
        public static bool UrlAvailable(this string httpUrl)
        {
            if (!httpUrl.StartsWith("http://") || !httpUrl.StartsWith("https://"))
                httpUrl = "http://" + httpUrl;
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse myHttpWebResponse =
                   (HttpWebResponse)myRequest.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}