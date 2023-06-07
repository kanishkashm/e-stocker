using Microsoft.Extensions.Primitives;

namespace EStk.API.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveQuotes(this string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            var s = value.Replace("\"", "");
            return value.Replace("\"", "");
        }

        public static string RemoveQuotes(this StringValues value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            var s = value.ToString().Replace("\"", "");
            return value.ToString().Replace("\"", "");
        }
    }
}
