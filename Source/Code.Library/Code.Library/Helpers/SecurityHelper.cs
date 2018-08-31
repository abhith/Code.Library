namespace Code.Library.Helpers
{
    using System.Text.RegularExpressions;

    public static class SecurityHelper
    {
        /// <summary>
        /// returns "safe" URL, stripping anything outside normal charsets for URL
        /// </summary>
        public static string SanitizeUrl(this string url)
        {
            return Regex.Replace(url, @"[^-A-Za-z0-9+&@#/%?=~_|!:,.;\(\)]", string.Empty);
        }
    }
}