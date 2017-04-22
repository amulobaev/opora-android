using System.Globalization;

namespace Opora
{
    public static class Helpers
    {
        public static bool TryParse(string s, out double result)
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            s = s.Replace(".", separator).Replace(",", separator);
            return double.TryParse(s, out result);
        }
    }
}