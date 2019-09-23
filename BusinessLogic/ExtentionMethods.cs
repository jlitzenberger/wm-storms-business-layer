using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.STORMS.BusinessLayer
{
    public static class ExtensionMethods
    {
        public static string Truncate(this string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        public static int? ToIntOrNull(this string value)
        {
            return String.IsNullOrEmpty(value) ? (int?)null : int.Parse(value);
        }
        public static string DateToStringOrEmpty(this DateTime? value)
        {
            return value.HasValue ? string.Empty : value.ToString();
        }

        public static string EntityToStringOrEmpty(this object value)
        {
            return value == null ? string.Empty : value.ToString();
        }
        
    }  
}
