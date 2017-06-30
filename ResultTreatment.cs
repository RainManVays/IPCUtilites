using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IPCUtilities
{
    internal static class ResultTreatment
    {
        private static Regex _numberOnly = new Regex(@"[^\d]");
        internal static string[] ResultToArray(string result)
        {
            if (!string.IsNullOrEmpty(result))
                return result.Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            return null;
        }
        internal static string GetRowValue(string row,string indexOf)
        {
            if (!string.IsNullOrEmpty(row))
            {
                return row.Substring(row.IndexOf(indexOf) + indexOf.Length).Trim();
            }
                
            return null;
        }
        internal static string GetRowValue(string row, string indexOf,char[] trimsChar)
        {
            if (!string.IsNullOrEmpty(row))
            {
                return row.Substring(row.IndexOf(indexOf) + indexOf.Length).Trim(trimsChar);
            }

            return null;
        }
        internal static int ResultToInt32(string result)
        {
            return Convert.ToInt32(_numberOnly.Replace(result, ""));
        }
        internal static Int64 ResultToInt64(string result)
        {
            return Convert.ToInt64(_numberOnly.Replace(result, ""));
        }

    }
}
