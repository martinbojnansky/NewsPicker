using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Robot.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly string[] _formats =
        {
            "ddd, dd MMM yyyy HH:mm:ss ZZZ"
        };

        public static DateTime ToDateTime(this string value)
        {
            // Try default parsing
            try { return DateTime.Parse(value); } catch { }

            // Try custom format parsing
            foreach (var format in _formats)
            {
                try { return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture); } catch { }
            }

            throw new FormatException($"String '{value}' was not recognized as valid DateTime format.");
        }
    }
}
