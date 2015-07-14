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
    }
}
