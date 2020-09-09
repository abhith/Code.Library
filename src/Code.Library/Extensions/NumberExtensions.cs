using System;

namespace Code.Library.Extensions
{
    /// <summary>
    /// The number helper.
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// Gets the first n digits.
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
        public static int GetFirstNDigits(this int number, int n)
        {
            var x = (int)Math.Pow(10, n);

            while (number >= x)
            {
                number /= 10;
            }

            return number;
        }

        /// <summary>
        /// Gets the nth digit.
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
    }
}