using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace Code.Library
{
    public static class ConvertHelper
    {
        /// <summary>
        /// IEnumerable to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> data)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        #region Extension Methods

        /// <summary>
        /// Convert timespan to 12H format string
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static string ConvertTo12H(this TimeSpan timeSpan)
        {
            var dateTime = DateTime.MinValue.Add(timeSpan);
            var cultureInfo = CultureInfo.InvariantCulture;

            // optional
            //CultureInfo cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.Name);
            //cultureInfo.DateTimeFormat.PMDesignator = "PM";

            return dateTime.ToString("hh:mm tt", cultureInfo);
        }

        /// <summary>
        /// string to nullable int
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static int? ToNullableInt32(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        /// <summary>
        /// string to nullable bool
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool? ToNullableBool(this string s)
        {
            bool i;
            if (bool.TryParse(s, out i)) return i;
            return null;
        }

        #endregion Extension Methods
    }
}