// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberHelper.cs" company="*">
//  *
// </copyright>
// <summary>
//   Defines the NumberHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Code.Library.Helpers
{
    using System;

    /// <summary>
    /// The number helper.
    /// </summary>
    public static class NumberHelper
    {
        #region Public Methods

        /// <summary>
        /// The get nth digit.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <param name="n">
        /// The n.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetNthDigit(this int number, int n)
        {
            return (int)((number / Math.Pow(10, n - 1)) % 10);
        }

        #endregion Public Methods
    }
}