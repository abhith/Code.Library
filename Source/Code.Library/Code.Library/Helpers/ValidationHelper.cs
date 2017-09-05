using System.Text.RegularExpressions;

namespace Code.Library
{
    public static class ValidationHelper
    {
        #region Extensions

        /// <summary>
        /// To check whether the given string is Arabic.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns True if Arabic</returns>
        public static bool IsRtl(this string input)
        {
            return Regex.IsMatch(input, @"\p{IsArabic}");
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

        #endregion Extensions
    }
}