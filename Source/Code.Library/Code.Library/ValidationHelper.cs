using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Code.Library
{
    public static class ValidationHelper
    {
        #region Extensions
        /// <summary>
        /// To check whether the string is valid email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(this string email)
        {
           return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        ///// <summary>
        ///// true, if is valid email address
        ///// from http://www.codeproject.com/Articles/31050/String-Extension-Collection-for-C
        ///// </summary>
        ///// <param name="s">email address to test</param>
        ///// <returns>true, if is valid email address</returns>

        //public static bool IsValidEmailAddressV2(this string s)
        //{
        //    return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$").IsMatch(s);
        //}
        #endregion


    }
}
